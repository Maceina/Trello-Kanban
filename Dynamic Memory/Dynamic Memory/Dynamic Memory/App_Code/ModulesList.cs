using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// List class
/// </summary>
public sealed class ModulesList
{
    private Node start;                               // Start of list
    private Node end;                                 // End of list
    private Node pre;                                 // For forming new queue
    private Node link;                                // Link of list

    /// <summary>
    /// Creates object of ModulesList class.
    /// </summary>
    public ModulesList()
    {
        start = new Node(null, end);
        end = new Node(null, null);
        link = null;
        pre = start;
    }

    /// <summary>
    /// Adds data to list the direct way
    /// </summary>
    /// <param name="info"></param>
    public void AddData(Info info)
    {
        pre.Next = new Node(info, end);
        pre = pre.Next;
    }

    /// <summary>
    /// Start of the list is assigned to the interface link.
    /// </summary>
    public void Beginning()
    {
        link = start.Next;
    }

    /// <summary>
    /// Next element is assigned to interface link.
    /// </summary>
    public void Next()
    {
        link = link.Next;
    }

    /// <summary>
    /// Returns true, if next element of interface link isn't empty.
    /// </summary>
    /// <returns>True, if next node of list exists, otherwise returns false</returns>
    public bool Exists()
    {
        return link.Next != null;
    }

    /// <summary>
    /// Returns value from list's interface link.
    /// </summary>
    /// <returns>Stored information from node</returns>
    public Info GetData()
    {
        return link.Info;
    }

    /// <summary>
    /// Sets new data to specific field of information
    /// </summary>
    /// <param name="data">New data to set</param>
    /// <param name="fieldToSet">Information field to overwrite</param>
    public void SetData(string data, string fieldToSet)
    {
        switch(fieldToSet)
        {
            case "modName":
                link.Info.ModName = data;
                break;

            case "name":
                link.Info.Name = data;
                break;

            case "surname":
                link.Info.Surname = data;
                break;

            default:
                link.Info.OtherInfo = data;
                break;
        }
    }

    /// <summary>
    /// Gets specific information from list.
    /// </summary>
    /// <param name="data">Information to find</param>
    /// <returns>More detailed information</returns>
    public Info GetDetailedData(Info data)
    {
        if (start.Next != null)
        {
            for (Node l = start.Next; l.Next != null; l = l.Next)
            {
                if (l.Info == data)
                {
                    return l.Info;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Checks if list is empty.
    /// </summary>
    /// <returns>Returns true if list is empty, otherwise returns false</returns>
    public bool IsEmpty()
    {
        return start.Next == null;
    }

    /// <summary>
    /// Removes specific given element from list.
    /// </summary>
    /// <param name="element">Element to remove from the list</param>
    public void RemoveElement(Info element)
    {
        Node temp = start;

        for (Node d = start.Next; d.Next != null; d = d.Next)
        {
            if (d.Info == element)
            {
                temp.Next = d.Next;
                break;
            }

            temp = d;
        }
    }

    /// <summary>
    /// Return true if list contains given information.
    /// </summary>
    /// <param name="info">Info to check in the list</param>
    /// <returns>True, if list contains given information, otherwise returns true</returns>
    public bool Contains(Info info)
    {
        if (start.Next != null)
        {
            for (Node l = start.Next; l.Next != null; l = l.Next)
            {
                if (l.Info == info)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Sorts list by swapping info.
    /// </summary>
    public void SortList()
    {
        if (start.Next == null)
        {
            return;
        }

        for (Node l1 = start.Next; l1.Next != null; l1 = l1.Next)
        {
            Node min = l1;

            for (Node l2 = l1.Next; l2.Next != null; l2 = l2.Next)
            {
                if (l2.Info < min.Info)
                {
                    min = l2;
                }
            }

            // Exchange of information parts
            Info mod = l1.Info;
            l1.Info = min.Info;
            min.Info = mod;
        }
    }

    /// <summary>
    /// Counts how many elements in the list contains same feature of information given.
    /// </summary>
    /// <param name="info">Information given</param>
    /// <returns>Count of same feature found</returns>
    public int SameCount(Info info)
    {
        int count = 0;

        for (Node mod = start.Next; mod.Next != null; mod = mod.Next)
        {
            if (mod.Info.IsTheSame(info))
            {
                count++;
            }
        }

        return count;
    }
}