using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class LabForm : System.Web.UI.Page
{
    private ModulesList<Info> studentsModules;                           // List of students & their chosen modules
    private ModulesList<Info> chosenModules;                             // List of chosen modules 
    private ModulesList<Info> unchosenModules;                           // List of unchosen modules
    
    private const string dataC = @"App_Data\U12Results.txt";             // Results path adress

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            studentsModules = new ModulesList<Info>();
            chosenModules = new ModulesList<Info>();
            unchosenModules = new ModulesList<Info>();

            Session["students"] = studentsModules;
            Session["chosen"] = chosenModules;
            Session["unchosen"] = unchosenModules;
        }
        else
        {
            studentsModules = (ModulesList<Info>)Session["students"];
            chosenModules = (ModulesList<Info>)Session["chosen"];
            unchosenModules = (ModulesList<Info>)Session["unchosen"];
        }
    }

    /// <summary>
    /// Method for reading primary information.
    /// </summary>
    /// <param name="data">Data path address</param>
    /// <returns>List with primary data</returns>
    private ModulesList<Info> ReadData(Stream data)
    {
        ModulesList<Info> infoList = new ModulesList<Info>();

        using (StreamReader reader = new StreamReader(data))
        {
            string line = null;
            while(null != (line = reader.ReadLine()))
            {
                string[] parts = line.Split(';');

                Info info = new Info(parts[0], parts[1], parts[2], parts[3]);
                infoList.AddData(info);
            }
        }

        return infoList;
    }

    /// <summary>
    /// Proceeds loaded data on client click.
    /// </summary>
    protected void ProceedButton_Click(object sender, EventArgs e)
    {
        if (DataUpload.HasFiles && DataUpload.PostedFiles.Count == 2 && DataUpload.FileName.EndsWith(".txt"))
        {
            studentsModules.EmptyList();            
            unchosenModules.EmptyList();

            studentsModules = ReadData(DataUpload.PostedFiles[0].InputStream);
            unchosenModules = ReadData(DataUpload.PostedFiles[1].InputStream);

            Session["students"] = studentsModules;
            Session["unchosen"] = unchosenModules;

            SuccessLabel.Text = "Data loaded successfully!";
            SuccessLabel.Visible = true;

            /** Exports primary data to results file */
            ExportPrimaryData(dataC, studentsModules, unchosenModules);
        }

        else
        {
            DataValidator.ErrorMessage = "Please select 2 of your data files, that end with .txt!";
            DataValidator.IsValid = false;
            return;
        }

        if (studentsModules.IsEmptyR() || unchosenModules.IsEmptyR())
        {
            DataValidator.IsValid = false;
            return;
        }

        chosenModules.EmptyList();

        FormLists(studentsModules, chosenModules, unchosenModules);

        Session["chosen"] = chosenModules;
        Session["unchosen"] = unchosenModules;

        SuccessLabel.Text = "Data proccessed successfully!";
        SuccessLabel.Visible = true;
    }

    /// <summary>
    /// Forms lists of chosen & unchosen modules
    /// </summary>
    /// <param name="studentsModules">List of students & their chosen modules</param>
    /// <param name="chosenModules">List of chosen modules</param>
    /// <param name="unchosenModules">List of unchosen modules</param>
    private void FormLists(ModulesList<Info> studentsModules, ModulesList<Info> chosenModules,
        ModulesList<Info> unchosenModules)
    {
        foreach (Info data in unchosenModules)
        {
            // Adds module to chosens' list, if students' list contains it and chosens' list doesn't.
            if (studentsModules.Contains(data) && (!chosenModules.Contains(data) || chosenModules.IsEmptyR()))
            {
                chosenModules.AddData(data);

                // Removes module from unchosen modules' list.
                unchosenModules.RemoveElement(data);
            }
        }

        chosenModules.SortList();
        unchosenModules.SortList();
    }

    /// <summary>
    /// Shows formed list on client click
    /// </summary>
    protected void ShowListsButton_Click(object sender, EventArgs e)
    {
        if (chosenModules.IsEmptyR() && unchosenModules.IsEmptyR())
        {
            ProceedValidator.IsValid = false;
            return;
        }

        SuccessLabel.Text = null;

        if(ChosenTable != null || UnchosenTable != null)
        {
            ChosenTable.Dispose();
            UnchosenTable.Dispose();

            CreateTable(ChosenTable, chosenModules);
            CreateTable(UnchosenTable, unchosenModules);
        }

        ChosenTable.Visible = true;
        UnchosenTable.Visible = true;
        
    }

    /// <summary>
    /// Creates table for a list to show.
    /// </summary>
    /// <param name="table">Table to show data in</param>
    /// <param name="modList">List to display in table</param>
    private void CreateTable(Table table, ModulesList<Info> modList)
    {
        TableRow row = new TableRow();
        TableCell cNumber = new TableCell();
        cNumber.Text = " # ";
        row.Cells.Add(cNumber);
        TableCell cName = new TableCell();
        cName.Text = " Module Name ";
        row.Cells.Add(cName);
        TableCell cLecturer = new TableCell();
        cLecturer.Text = " Responsible lecturer ";
        row.Cells.Add(cLecturer);
        TableCell cCredits = new TableCell();
        cCredits.Text = " Credits ";
        row.Cells.Add(cCredits);
        row.HorizontalAlign = HorizontalAlign.Left;
        row.VerticalAlign = VerticalAlign.Middle;
        row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
        row.BackColor = System.Drawing.Color.DarkBlue;
        row.ForeColor = System.Drawing.Color.White;
        table.Rows.Add(row);

        int index = 1;

        foreach(Info module in modList)
        {            
            TableRow dataRow = new TableRow();
            TableCell cIndex = new TableCell();
            cIndex.Text = Convert.ToString(index);
            index++;
            dataRow.Cells.Add(cIndex);
            TableCell cModName = new TableCell();
            cModName.Text = module.ModName;
            dataRow.Cells.Add(cModName);
            TableCell cLecturerInfo = new TableCell();
            cLecturerInfo.Text = module.Name + " " + module.Surname;
            dataRow.Cells.Add(cLecturerInfo);
            TableCell cModCredits = new TableCell();
            cModCredits.Text = module.OtherInfo;
            dataRow.Cells.Add(cModCredits);
            dataRow.HorizontalAlign = HorizontalAlign.Left;
            dataRow.VerticalAlign = VerticalAlign.Middle;
            dataRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            table.Rows.Add(dataRow);
        }
    }

    /// <summary>
    /// Shows student, who chose most modules, on client click.
    /// </summary>
    protected void ViewStudentButton_Click(object sender, EventArgs e)
    {
        if(studentsModules.IsEmptyR())
        {
            DataValidator.IsValid = false;
            return;
        }

        Tuple<Info, int> pickyStudent = PickyStudent(studentsModules);
        
        StudentInfoLabel.Text = pickyStudent.Item1.Name + " " + pickyStudent.Item1.Surname + ", total number of chosen modules: " +
            pickyStudent.Item2 + ".";

        StudentInfoLabel.Visible = true;

        // Exports found student to results file.
        ExportPickyStudent(dataC, pickyStudent);
    }

    /// <summary>
    /// Finds student, who chose most modules.
    /// </summary>
    /// <param name="studentsList">Student's & their chosen modules list</param>
    /// <returns>Personal information of found student and it's chosen modules count</returns>
    private Tuple<Info, int> PickyStudent(ModulesList<Info> studentsList)
    {
        Info pickyStudent = new Info();
        int modulesCount = 0;

        for (studentsList.BeginningR(); studentsList.ExistsR(); studentsList.NextR())
        {
            Info student = studentsList.GetData();
            int tempModulesCount = SameCount(student, studentsList);

            if (modulesCount <= tempModulesCount)
            {
                modulesCount = tempModulesCount;
                pickyStudent = student;
            }
        }

        return new Tuple<Info, int>(pickyStudent, modulesCount);
    }

    /// <summary>
    /// Counts chosen modules of same student.
    /// </summary>
    /// <param name="student">Student, whose modules are counted.</param>
    /// <param name="studentsList">List of students & their chosen modules.</param>
    /// <returns>Number of chosen modules by the same student.</returns>
    private int SameCount (Info student, ModulesList<Info> studentsList)
    {
        int counter = 1;

        for(studentsList.BeginningL(); studentsList.GetData() != student; studentsList.NextL())
        {
            if(studentsList.GetData().IsTheSame(student))
            {
                counter++;
            }
        }

        return counter;
    }

    /// <summary>
    /// Creates list of specified student's chosen modules.
    /// </summary>
    protected void CreateButton_Click(object sender, EventArgs e)
    {
        if (studentsModules.IsEmptyR() || unchosenModules.IsEmptyR())
        {
            DataValidator.IsValid = false;
            return;
        }

        string[] nameSurname = InsertInfoBox.Text.Split();
        InsertInfoBox.Text = null;

        if(nameSurname.Length == 1)
        {
            InputValidator.IsValid = false;
            return;
        }

        Info studentToFind = new Info(null, nameSurname[1], nameSurname[0], null);

        if (!IsInputCorrect(studentToFind, studentsModules))
        {
            InputValidator.IsValid = false;
            return;
        }

        ModulesList<Info> foundModules = ChosenStudentModules(studentToFind, studentsModules, chosenModules);
        
        CreateTable(StudentTable, foundModules);
        StudentTable.Caption = " Chosen modules of " + studentToFind.Name + " " + studentToFind.Surname;
        StudentTable.Visible = true;

        string info = " List of " + studentToFind.Name + " " + studentToFind.Surname + " chosen modules ";
        string header = "  # | Module name                                                             |" +
                        " Responsible lecturer       | Credits ";

        DataToFile(dataC, foundModules, info, header);
    }

    /// <summary>
    /// Checks if input is correct.
    /// </summary>
    /// <param name="studentToFind">Personal information of student to find</param>
    /// <param name="studentsModules">List of students & their chosen modules</param>
    /// <returns>True, if students' list contain such student, otherwise returns false</returns>
    private bool IsInputCorrect(Info studentToFind, ModulesList<Info> studentsModules)
    {
        foreach(Info student in studentsModules)
        {            
            if(studentToFind.IsTheSame(student))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Creates chosen modules' list of chosen student.
    /// </summary>
    /// <param name="studentToFind">Personal information of student to find</param>
    /// <param name="studentsModules">List of students & their chosen modules</param>
    /// <param name="chosenModules">List of chosen modules</param>
    /// <returns>List of chosen modules by specific student</returns>
    private ModulesList<Info> ChosenStudentModules(Info studentToFind, ModulesList<Info> studentsModules, 
        ModulesList<Info> chosenModules)
    {
        ModulesList<Info> foundModules = new ModulesList<Info>();
                                  
        foreach(Info student in studentsModules)
        {
            if(studentToFind.IsTheSame(student))
            {
                Info module = chosenModules.GetDetailedData(student);
                foundModules.AddData(module);
            }
        }

        foundModules.SortList();

        return foundModules;
    }
    
    /// <summary>
    /// Exports the rest of the results to file on client click.
    /// </summary>
    protected void ExportButton_Click(object sender, EventArgs e)
    {
        if (studentsModules.IsEmptyR() == true || unchosenModules.IsEmptyR() == true)
        {
            DataValidator.IsValid = false;
            return;
        }
        
        string header = "  # | Module name                                                             |" +
                        " Responsible lecturer       | Credits ";

        DataToFile(dataC, chosenModules, " List of chosen modules ", header);
        DataToFile(dataC, unchosenModules, " List of unchosen modules ", header);

        ExportLabel.Visible = true;
    }

    /// <summary>
    /// Exports primary data to results file.
    /// </summary>
    /// <param name="data1">List of primary data from first source</param>
    /// <param name="data2">List of primary data from secondary source</param>
    /// <param name="outputData">Results path address</param>
    private void ExportPrimaryData(string outputData, ModulesList<Info> data1, ModulesList<Info> data2)
    {
        if (File.Exists(Server.MapPath(outputData)))
        {
            File.Delete(Server.MapPath(outputData));
        }

        string header1 = "  # | Chosen module name                                                      |" +
                              " Student                    | Group ";
        string header2 = "  # | Module name                                                             |" +
                              " Responsible lecturer       | Credits ";

        string info1 = " Primary loaded data of students & their selected modules ";
        string info2 = " Primary loaded data of modules & their info ";

        DataToFile(outputData, data1, info1, header1);
        DataToFile(outputData, data2, info2, header2);
    }

    /// <summary>
    /// Exports info of student, who chose most modules, to the results file.
    /// </summary>
    /// <param name="pickyStudent">Student info</param>
    /// <param name="numberOfModules">Number of modules chosen</param>
    /// <param name="outputData">Results path adress</param>
    private void ExportPickyStudent(string outputData, Tuple<Info, int> pickyStudent)
    {
        using (var writer = File.AppendText(Server.MapPath(outputData)))
        {
            writer.WriteLine();
            writer.WriteLine(" Student, who chose most modules: " + pickyStudent.Item1.Name + " " + pickyStudent.Item1.Surname + 
                             ", total number of chosen modules: " + pickyStudent.Item2 + ".");
            writer.WriteLine();
        }
    }

    /// <summary>
    /// Prints primary data to file.
    /// </summary>
    /// <typeparam name="type">Type, which method accepts.</typeparam>
    /// <param name="outputData">Results path adress</param>
    /// <param name="dataList">List of information</param>
    /// <param name="info">Description of info being printed</param>
    /// <param name="header">Header for the table</param>
    private void DataToFile<type>(string outputData, IEnumerable<type> dataList, string info, string header)
        where type : ILabInterface<type>
    {
        string line = new string('-', 125);

        using (var writer = File.AppendText(Server.MapPath(outputData)))
        {
            writer.WriteLine(line);
            writer.WriteLine(info);
            writer.WriteLine(line);

            writer.WriteLine();

            writer.WriteLine(line);
            writer.WriteLine(header);
            writer.WriteLine(line);

            int index = 1;

            foreach (type data in dataList)
            {
                writer.WriteLine(" {0, 2} | {1} ", index++, data.ToString());
                writer.WriteLine(line);
            }

            writer.WriteLine();
        }
    }    
}