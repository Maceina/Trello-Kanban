using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Form_1 : System.Web.UI.Page
{    
    private Sudoku6x6 sudoku;                                                      // A private class for saving sudoku table.
    private const string ID = "U3-Data.txt";                                       // Input data file
    private const string OD = "U3-Results.txt";                                    // Output data file

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            sudoku = (Sudoku6x6)Session["data"];
        }
        
        CreateDataTableToShow(TableData);                                           // Creates a table for sudoku to show on screen.
    }

    /// <summary>
    /// A click of LoadButton loads data to program (to Sudoku6x6 class, that saves data) & fill data file.
    /// </summary>
    protected void LoadButton_Click(object sender, EventArgs e)
    {
        GetLoadedData(out sudoku);

        Session["data"] = sudoku;

        DataLoadedLabel.Visible = true;

        FillFile(ID, sudoku, "  Unsolved Sudoku 6x6  ");

        
    }

    /// <summary>
    /// A click of ShowDataButton shows loaded data on screen, if data's not loaded - DataChecker validator 
    /// warns the user about it.
    /// </summary>
    protected void ShowDataButton_Click(object sender, EventArgs e)
    {
        sudoku = (Sudoku6x6)Session["data"];

        if(sudoku == null)
        {
            DataChecker.IsValid = false;
            return;
        }

        ShowData(sudoku, TableData);
    }

    /// <summary>
    /// A click of ProceedButton calls a recursive method for solving sudoku, after method 
    /// returns 'true' value (which means, that sudoku was solved), SuccessLabel informs user about success.
    /// </summary>
    protected void ProceedButton_Click(object sender, EventArgs e)
    {
        bool solved = Solve(sudoku);

        if(solved)
        {
            SuccessLabel.Visible = true;
        }
    }

    /// <summary>
    /// A click of ResultsButton shows results of solved sudoku on screen and fills results file.
    /// </summary>
    protected void ResultsButton_Click(object sender, EventArgs e)
    {
        ShowData(sudoku, TableData);

        FillFile(OD, sudoku, "  Solved Sudoku 6x6  ");
    }

    /// <summary>
    /// Creates a new table to show data on screen.
    /// </summary>
    private void CreateDataTableToShow(Table tableData)
    {
        for (int i = 0; i < 6; i++)
        {
            TableRow row = new TableRow();
            for (int j = 0; j < 6; j++)
            {
                TableCell cell = new TableCell();
                row.Cells.Add(cell);
            }
            tableData.Rows.Add(row);
        }
    }

    /// <summary>
    /// Gets loaded data straight to sudoku class' object.
    /// </summary>
    /// <param name="sudoku"></param>
    private void GetLoadedData(out Sudoku6x6 sudoku)
    {
        sudoku = new Sudoku6x6();

        string[] dataLines = File.ReadAllLines(Server.MapPath("App_Data/U3.txt"));

        for (int i = 0; i < 6; i++)
        {
            string[] numbers = dataLines[i].Split(' ');
            for (int j = 0; j < 6; j++)
            {
                sudoku.SetValueInTable(i, j, Convert.ToInt32(numbers[j]));
            }

        }
    }

    /// <summary>
    /// Fills a new file with loaded data or results data.
    /// </summary>
    /// <param name="file">Input/Output file's adress</param>
    /// <param name="textToShow">Text to show on table</param>
    private void FillFile(string file, Sudoku6x6 sudoku, string textToShow)
    {        
        using (StreamWriter writer = new StreamWriter(Server.MapPath(file)))
        {
            string line = new string('-', 23);
            string values;
            writer.WriteLine(line);
            writer.WriteLine(textToShow);
            writer.WriteLine(line);
            for (int i = 0; i < 6; i++)
            {                                   
                values = "  ";                  

                // Checks if a block of sudoku table is made of rows already (one block = 2 row cells x 3 column cells).
                // If 'true', adds a line after a block.

                if ((i % 2 == 0) && (i != 0))
                {
                    writer.WriteLine(line);
                }

                for (int j = 0; j < 6; j++)
                {
                    // Checks if a block of sudoku table is made of columns already (one block = 2 row cells x 3 column cells).
                    // If 'true', adds a separator after a block.

                    if ((j % 3 == 0) && (j != 0))
                    {
                        values += ":  ";
                    }

                    values += sudoku.GetValueInTable(i, j) + "  ";
                }

                writer.WriteLine(values);                                
            }

            writer.WriteLine(line);
        }
    }

    /// <summary>
    /// Imports values of sudoku to table on screen, highlights the cell with zero value.
    /// </summary>
    /// <param name="data">Sudoku table</param>
    /// <param name="dataTable">Table for sudoku</param>
    private void ShowData(Sudoku6x6 data, Table dataTable)
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                dataTable.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Center;
                dataTable.Rows[i].Cells[j].Text = Convert.ToString(data.GetValueInTable(i, j));   
                
                if(data.GetValueInTable(i, j) == 0)
                {
                    // Highlights the cells with no values.
                    dataTable.Rows[i].Cells[j].BackColor = System.Drawing.Color.LightSteelBlue;
                    dataTable.Rows[i].Cells[j].Text = null;
                }
            }
        }
    }

    /// <summary>
    /// Recursive method, which calls itself again if previous returned value was 'false' (not
    /// solved), meaning there are stil left cells with '0' value.
    /// </summary>
    /// <param name="sudoku"></param>
    /// <returns>Value, which indicates whether sudoku was solved or not.</returns>
    private bool Solve(Sudoku6x6 sudoku)
    {
        for(int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 6; col++)
            {
                if (sudoku.GetValueInTable(row, col) == 0)
                {
                    for (int value = 1; value <= 6; value++)
                    {
                        // Checks whether the value from 1 to 6 is not present in current row, column, or block.
                        if (sudoku.IsAllowed(row, col, value))
                        {   
                            sudoku.SetValueInTable(row, col, value);

                            if (Solve(sudoku))
                            {
                                // The end condition for recursive method to quit/end itself.
                                return true;  
                            }                            
                        }
                    }

                    // The recursive method goes back to previous calls of itself (stack principle) & sets other values,
                    // since none of the 1-6 values were allowed to place in the cell with '0'.
                    return false;
                }
            }
        }

        // No '0' values left, sudoku is solved, method quits (the end condition for recursive method to quit/end itself.
        return true; 
    }
    
}