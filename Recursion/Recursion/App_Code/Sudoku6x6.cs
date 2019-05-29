using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Class for storing values of sudoku table.
/// </summary>
public class Sudoku6x6
{
    private int[,] sudokuTable;

    /// <summary>
    /// Constructor, creates a new sudoku matrix.
    /// </summary>
    public Sudoku6x6()
    {
        sudokuTable = new int[6, 6];
    }

    /// <summary>
    /// Gets value from sudoku table.
    /// </summary>
    /// <param name="n">Index of row</param>
    /// <param name="m">Index of column</param>
    /// <returns>Value of n' row & m' column from sudoku table</returns>
    public int GetValueInTable(int n, int m)
    {
        return sudokuTable[n, m];
    }

    /// <summary>
    /// Sets value in sudoku table.
    /// </summary>
    /// <param name="n">Index of row</param>
    /// <param name="m">Index of column</param>
    /// <param name="value">New value to place in to the sudoku table</param>
    public void SetValueInTable(int n, int m, int value)
    {
        sudokuTable[n, m] = value;
    }

    /// <summary>
    /// Checks if row contains a specific value (number)
    /// </summary>
    /// <param name="row">The row to check</param>
    /// <param name="number">The value that is being checked</param>
    /// <returns>True, if already contains specific value, and false otherwise</returns>
    public bool RowContains(int row, int number)
    {
        for (int i = 0; i < 6; i++)
        {
            if (sudokuTable[row, i] == number)
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Checks if column contains a specific value (number)
    /// </summary>
    /// <param name="col">The column to check</param>
    /// <param name="number">The value that is being checked</param>
    /// <returns>True, if already contains specific value, and false otherwise</returns>
    public bool ColumnContains(int col, int number)
    {
        for (int i = 0; i < 6; i++)
        {
            if (sudokuTable[i, col] == number)
            {
                return true;
            }
        }
        return false;

    }

    /// <summary>
    /// Checks if block of 3x2 square contains a specific value (number)
    /// </summary>
    /// <param name="row">The row, in which value is being checked</param>
    /// <param name="col">The column, in which value is being checked</param>
    /// <param name="number">The value that is being checked</param>
    /// <returns>True, if already contains specific values, and false otherwise</returns>
    public bool BlockContains(int row, int col, int number)
    {
        row -= row % 2;
        col -= col % 3;

        for (int i = row; i < row + 2; i++)
        {
            for (int j = col; j < col + 3; j++)
            {
                if (sudokuTable[i, j] == number)

                    return true;
            }
        }
        return false;

    }

    /// <summary>
    /// Checks if specific value (number) is not present in current row, column and block. 
    /// If one of the conditions returns 'true' (contains), a value is not allowed to be placed.
    /// </summary>
    /// <param name="row">The row to check</param>
    /// <param name="col">The column to check</param>
    /// <param name="number">The value that is being checked</param>
    /// <returns>True, if value is allowed to be placed, and false otherwise</returns>
    public bool IsAllowed(int row, int col, int number)
    {
        if (RowContains(row, number) || ColumnContains(col, number) || BlockContains(row, col, number))
        {
            return false;
        }

        return true;
    }

}