using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class LabForm : System.Web.UI.Page
{
    private List<Data> emailsInfo;                            // Information of e-mails transferred between servers.
    private List<Data> serversInfo;                           // Main information of servers.
    private List<Data> bytelessServersList;                   // Formed list of servers with 0 bytes transfers at any hour.
    private const string tempFile = @"C:\Users\Eglė\Desktop\5 LD\L5\L4\tempResults.txt"; // Temporary file to save results 

    /// <summary>
    /// Loads page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.IsPostBack)
        {
            emailsInfo = (List<Data>)Session["emails"];
            serversInfo = (List<Data>)Session["servers"];
            bytelessServersList = (List<Data>)Session["byteless"];
        } 
    }

    /// <summary>
    /// Actions to take after click of Proceed button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonProceed_Click(object sender, EventArgs e)
    {
        emailsInfo = new List<Data>();
        serversInfo = new List<Data>();
        bytelessServersList = new List<Data>();
        
        List<HttpPostedFile> dataFiles;
        HttpPostedFile serversFile;

        string header1 = " Server                         | Server's speed of interface to the internet ";
        string header2 = " Server                         | Date              |" +
                        " Sender                         | Receiver                       |" +
                        " E-mail size ";

        if (File.Exists(tempFile))
        {
            File.Delete(tempFile);
        }

        try
        {
            dataFiles = GetDataFiles(UploadData);
            serversFile = GetServersFile(UploadData);
            emailsInfo = GetCatalog(dataFiles);
            serversInfo = GetSData(serversFile);
            bytelessServersList = GetALLByteless(emailsInfo, serversInfo);
            bytelessServersList.OrderBy(element => element.Server).ThenBy(element => element.Date);
            ExportData(serversInfo, " Primary data of servers information", header1);
            ExportData(emailsInfo, " Primary data of sent emails between servers ", header2);            
        }   
        catch(LabException ex)
        {
            ValidatorProceed.ErrorMessage = ex.Message;
            ValidatorProceed.IsValid = false;
            ValidationInfo.Visible = true;
            return;
        }
        catch (SystemException ex)
        {
            ValidatorProceed.ErrorMessage = ex.Message;
            ValidatorProceed.IsValid = false;
            ValidationInfo.Visible = true;
            return;
        }

        LabelSuccess.Text = " Data has been proccessed successfully! ";
        LabelSuccess.Visible = true;

        Session["emails"] = emailsInfo;
        Session["servers"] = serversInfo;
        Session["byteless"] = bytelessServersList;        
    }

    /// <summary>
    /// Actions to take after click of DataButton.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonData_Click(object sender, EventArgs e)
    {
        try
        {
            CreateTable(TableResults, emailsInfo);
        }
        catch (LabException ex)
        {
            ValidatorData.ErrorMessage = ex.Message;
            ValidatorData.IsValid = false;
            ValidationInfo.Visible = true;
            return;
        }

        TableResults.Visible = true;
    }

    /// <summary>
    /// Actions to take after click of View button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonView_Click(object sender, EventArgs e)
    {
        try
        {           
            CreateTable(TableResults, bytelessServersList);
        }
        catch(LabException ex)
        {
            ValidatorView.ErrorMessage = ex.Message;
            ValidatorView.IsValid = false;
            ValidationInfo.Visible = true;
            return;
        }

        TableResults.Visible = true;
    }

    /// <summary>
    /// Actions to take after click of Export button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonExport_Click(object sender, EventArgs e)
    {
        string header = " Server                         | Date     | Hours of 0 bytes transfers ";
        
        try
        {
            ExportData(bytelessServersList, " Results ", header);            
        }
        catch(LabException ex)
        {
            ValidatorExport.ErrorMessage = ex.Message;
            ValidatorExport.IsValid = false;
            ValidationInfo.Visible = true;
            return;
        }
        
        DownloadData();
    }

    /// <summary>
    /// Gets full paths of data files.
    /// </summary>
    /// <returns>String array of full data paths.</returns>
    private static List<HttpPostedFile> GetDataFiles(FileUpload uploadedData)
    {         
        if(uploadedData.HasFiles)
        {
            List<HttpPostedFile> data = uploadedData.PostedFiles
                                        .Where(file => file.FileName.Contains("Data_"))
                                        .ToList<HttpPostedFile>();          

            return data;            
        }

        else
        {
            LabException ex = new LabException(" No data files selected! ");
            throw ex;
        }
    }

    /// <summary>
    /// Gets full path of servers' data file.
    /// </summary>
    /// <returns></returns>
    private HttpPostedFile GetServersFile(FileUpload data)
    {
        HttpPostedFile serversFile = data.PostedFiles.Where(file => file.FileName.Contains("Servers.txt")).ToList().First();
        
        if(serversFile != null)
        {
            return serversFile;
        }

        LabException ex = new LabException("Servers' data file not found!");
        throw ex;

    }

    /// <summary>
    /// Gets linked list of Data class objects filled with main information of servers.
    /// </summary>
    /// <param name="path">Full path of servers' data file.</param>
    /// <returns>Linked list of Data class objects filled with main information of servers.</returns>
    private static List<Data> GetSData(HttpPostedFile path)
    {
        List<Data> serverData = new List<Data>();

        using (var reader = new StreamReader(path.InputStream))
        {
            string line;

            if (reader.ReadLine() == null)
            {
                LabException ex = new LabException("Servers' data file is empty!");
                throw ex;
            }

            while (null != (line = reader.ReadLine()))
            {
                string[] dataParts = line.Split(';');

                if (dataParts.Length != 2)
                {
                    LabException ex = new LabException("Servers' data file does not match data format!");
                    throw ex;
                }

                else
                {
                    Data data = new Data(dataParts[0], default(DateTime), Convert.ToInt64(dataParts[1]));
                    serverData.Add(data);
                }
            }
        }

        return serverData;
    }

    /// <summary>
    /// Gets linked list of Data class objects filled with information of transfers
    /// of e-mails between servers.
    /// </summary>
    /// <param name="files">String array of full paths of data files.</param>
    /// <returns>Linked list of Data class objects filled with information of transfers of e-mails.</returns>
    private static List<Data> GetCatalog(List<HttpPostedFile> files)
    {
        List<Data> catalog = new List<Data>();
        
        foreach (HttpPostedFile file in files)
        {
            using (var reader = new StreamReader(file.InputStream))
            {
                string line = reader.ReadLine();

                if (line == null)
                {
                    LabException ex = new LabException(String.Format("Data file '{0}' is empty!", 
                                                        file.FileName));
                    throw ex;
                }

                string[] summary = line.Split(';');

                if(summary.Length != 2)
                {
                    LabException ex = new LabException(String.Format("Data file '{0}' does not match data format!",
                                                        file.FileName));
                    throw ex;
                }

                string date = summary[0];
                string server = summary[1];

                while (null != (line = reader.ReadLine()))
                {
                    string[] dataParts = line.Split(';');

                    if(dataParts.Length != 4)
                    {
                        LabException ex = new LabException(String.Format("Data file '{0}' does not match data format!",
                                                        file.FileName));
                        throw ex;
                    }

                    string dateTime = date + " " + dataParts[0];
                    Data data = new Data(server, Convert.ToDateTime(dateTime), Convert.ToInt64(dataParts[3]));
                    data.Sender = dataParts[1];
                    data.Receiver = dataParts[2];
                    catalog.Add(data);
                }
            }            
        }

        return catalog;
    }

    /// <summary>
    /// Gets linked list of Data class objects filled with information of servers
    /// with 0 bytes transfers at any hour.
    /// </summary>
    /// <param name="serversCatalog">Information of of transfers of e-mails between servers. </param>
    /// <param name="serversInfo">Main information of servers.</param>
    /// <returns>Linked list of Data class objects filled with information of servers
    /// with 0 bytes transfers at any hour.</returns>
    private static List<Data> GetALLByteless(List<Data> serversCatalog, List<Data> serversInfo)
    {
        List<Data> allByteless = new List<Data>();

        foreach (Data server in serversInfo)
        {
            IEnumerable<Data> sameServer = serversCatalog.Where(data => data.Equals(server));

            if(sameServer.Count() != 0)
            {
                Data bytelessServer = GetByteless(sameServer, server);

                if (bytelessServer.Hours.Count != 0)
                {
                    allByteless.Add(bytelessServer);
                }
            }
        }

        if(allByteless.Count != 0)
        {
            return allByteless;
        }
        
        else
        {
            LabException ex = new LabException("No servers with 0 bytes transfers found!");
            throw ex;
        }
    }

    /// <summary>
    /// Gets information of server with filled List hours of 0 bytes trasfers.
    /// </summary>
    /// <param name="info">Information of made transfers of server.</param>
    /// <param name="serverInfo">Main information of server.</param>
    /// <returns>Information of server with filled List of hours of 0 bytes transfers.</returns>
    private static Data GetByteless(IEnumerable<Data> info, Data serverInfo)
    {
        DateTime dataDate = new DateTime(info.First().Date.Year, info.First().Date.Month,
                                            info.First().Date.Day, 0, 0, 0, DateTimeKind.Local); 
        DateTime timeStart = new DateTime(info.First().Date.Year, info.First().Date.Month,
                                            info.First().Date.Day, 0, 0, 0, DateTimeKind.Local);
        DateTime timeEnd = new DateTime(info.First().Date.Year, info.First().Date.Month,
                                            info.First().Date.Day, 23, 0, 0, DateTimeKind.Local);
        Data byteless = new Data(serverInfo.Server, dataDate, 0);
        
        for (DateTime date = timeStart; date.CompareTo(timeEnd) <= 0; date = date.AddHours(1))
        {
            int counter = 0;

            counter = info.Where(x => x.Date.Hour.CompareTo(date.Hour) == 0 ||
                                      x.Date.Subtract(date) == new TimeSpan(1, 0, 0) &&
                                      x.Date.AddMinutes(x.Other / serverInfo.Other / 60).Hour
                                      .CompareTo(date.Hour) == 0).Count();            

            if (counter == 0)
            {
                byteless.Hours.Add(date.Hour);
            }
        }

        return byteless;
    }

    /// <summary>
    /// Creates table filled with information stored in <paramref name="list"/>.
    /// </summary>
    /// <param name="table">Table to create and fill with information.</param>
    /// <param name="list">Information to fill-in into the table.</param>
    private static void CreateTable(Table table, List<Data> list)
    {
        if(list == null || list.Count == 0)
        {
            LabException ex = new LabException(" No data loaded! ");
            throw ex;
        }

        table.Dispose();

        if (list.First().Sender == null)
        {
            table.GridLines = GridLines.Both;
            TableRow header = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text = " # ";
            TableCell cell2 = new TableCell();
            cell2.Text = " Server ";
            TableCell cell3 = new TableCell();
            cell3.Text = " Date ";
            TableCell cell4 = new TableCell();
            cell4.Text = " Hours of 0 bytes transfers ";
            header.Cells.AddRange(new TableCell[] { cell1, cell2, cell3, cell4 });
            table.Rows.Add(header);

            int i = 1;

            foreach (Data element in list)
            {
                TableRow row = new TableRow();
                TableCell index = new TableCell();
                index.Text = Convert.ToString(i++);
                TableCell server = new TableCell();
                server.Text = element.Server;
                TableCell date = new TableCell();
                date.Text = element.DateToString();
                TableCell hours = new TableCell();

                foreach (int hour in element.Hours)
                {
                    hours.Text += hour + "; ";
                }

                row.Cells.AddRange(new TableCell[] { index, server, date, hours });
                row.BackColor = System.Drawing.Color.White;
                row.ForeColor = System.Drawing.Color.DarkCyan;
                table.Rows.Add(row);
            }
        }

        else
        {
            table.GridLines = GridLines.Both;
            TableRow header = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text = " # ";
            TableCell cell2 = new TableCell();
            cell2.Text = " Server ";
            TableCell cell3 = new TableCell();
            cell3.Text = " Date ";
            TableCell cell4 = new TableCell();
            cell4.Text = " Sender ";
            TableCell cell5 = new TableCell();
            cell5.Text = " Receiver ";
            TableCell cell6 = new TableCell();
            cell6.Text = " E-mail size ";
            header.Cells.AddRange(new TableCell[] { cell1, cell2, cell3, cell4, cell5, cell6 });
            table.Rows.Add(header);

            int i = 1;

            foreach (Data element in list)
            {
                TableRow row = new TableRow();
                TableCell index = new TableCell();
                index.Text = Convert.ToString(i++);
                TableCell server = new TableCell();
                server.Text = element.Server;
                TableCell date = new TableCell();
                date.Text = Convert.ToString(element.Date);
                TableCell sender = new TableCell();
                sender.Text = element.Sender;
                TableCell receiver = new TableCell();
                receiver.Text = element.Receiver;
                TableCell size = new TableCell();
                size.Text = Convert.ToString(element.Other);


                row.Cells.AddRange(new TableCell[] { index, server, date, sender, receiver, size });
                row.BackColor = System.Drawing.Color.White;
                row.ForeColor = System.Drawing.Color.DarkCyan;
                table.Rows.Add(row);
            }
        }
    }

    /// <summary>
    /// Exports information stored in <paramref name="list"/> to results file.
    /// </summary>
    /// <param name="list">Information to export.</param>
    /// <param name="description">Description about information exported.</param>
    /// <param name="header">Header for the elements of information in <paramref name="list"/>.</param>
    private static void ExportData(List<Data> list, string description, string header)
    {
        if(list == null || list.Count == 0)
        {
            LabException ex = new LabException(" No data found to export!");
            throw ex;
        }

        string line = new string('-', 150);        

        using (var writer = File.AppendText(tempFile))
        {
            writer.WriteLine(line);
            writer.WriteLine(description);
            writer.WriteLine(line);

            writer.WriteLine(header);
            writer.WriteLine(line);
            
            foreach(Data element in list)
            {
                if (element.Sender == null && element.Hours.Count == 0)
                {
                    writer.WriteLine(element.ServerDataToString());
                    writer.WriteLine(line);
                }

                else if(element.Hours.Count == 0)
                {
                    writer.WriteLine(element.PrimaryDataToString());
                    writer.WriteLine(line);
                }

                else
                {
                    writer.WriteLine(element.ResultsToString());
                    writer.WriteLine(line);
                }
            }

            writer.WriteLine();
        }
    }    

    /// <summary>
    /// Downloads file to the user.
    /// </summary>
    private void DownloadData()
    {
        Response.ContentType = "text/plain";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Results.txt");

        // Write the file to the Response
        const int bufferLength = 10000;
        byte[] buffer = new Byte[bufferLength];
        int length = 0;
        Stream download = null;
        try
        {
            download = new FileStream(tempFile, FileMode.Open, FileAccess.Read);

            do
            {
                if (Response.IsClientConnected)
                {
                    length = download.Read(buffer, 0, bufferLength);
                    Response.OutputStream.Write(buffer, 0, length);
                    buffer = new Byte[bufferLength];
                }
                else
                {
                    length = -1;
                }
            }

            while (length > 0);
            Response.Flush();
            Response.End();
        }

        finally
        {
            if (download != null)
                download.Close();
        }        
    }
}