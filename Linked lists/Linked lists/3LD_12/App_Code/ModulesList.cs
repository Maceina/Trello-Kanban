using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// List class
/// </summary>
public sealed class ModulesList<type> : IEnumerable<type> where type : ILabInterface<type>
{
    private Node<type> start;                               // Start of list
    private Node<type> end;                                 // End of list
    private Node<type> pre;                                 // For forming new queue
    private Node<type> link;                                // Link of list

    /// <summary>
    /// Creates object of ModulesList class.
    /// </summary>
    public ModulesList()
    {
        start = new Node<type>(default(type), null, null);
        end = new Node<type>(default(type), start, null);
        pre = start;
        start.Right = end;
        link = null;
    }

    /// <summary>
    /// Adds data to list the direct way
    /// </summary>
    /// <param name="info"></param>
    public void AddData(type info)
    {
        pre.Right = new Node<type>(info, pre, end);
        pre = pre.Right;
        end.Left = pre;
    }

    /// <summary>
    /// Start of the list is assigned to the interface link by going right way.
    /// </summary>
    public void BeginningR()
    {
        link = start.Right;
    }

    /// <summary>
    /// Right element is assigned to interface link.
    /// </summary>
    public void NextR()
    {
        link = link.Right;
    }

    /// <summary>
    /// Checks if right element is not null.
    /// </summary>
    /// <returns>True, if right element of list exists, otherwise returns false</returns>
    public bool ExistsR()
    {
        return link.Right != null;
    }

    /// <summary>
    /// Start of the list is assigned to the interface link by going left way.
    /// </summary>
    public void BeginningL()
    {
        link = end.Left;
    }

    /// <summary>
    /// Left element is assigned to interface link.
    /// </summary>
    public void NextL()
    {
        link = link.Left;
    }

    /// <summary>
    /// Checks if left element is not null.
    /// </summary>
    /// <returns>True, if left element of list exists, otherwise returns false</returns>
    public bool ExistsL()
    {
        return link.Left != null;
    }

    /// <summary>
    /// Gets value from list's interface link.
    /// </summary>
    /// <returns>Stored information from node</returns>
    public type GetData()
    {
        return link.Info;
    }

    /// <summary>
    /// Gets specific information from list.
    /// </summary>
    /// <param name="data">Information to find</param>
    /// <returns>More detailed information</returns>
    public type GetDetailedData(type data)
    {
        if (start.Right != null)
        {
            for (Node<type> l = start.Right; l.Right != null; l = l.Right)
            {
                if (l.Info.CompareTo(data) == 0)
                {
                    return l.Info;
                }
            }
        }

        return default(type);
    }

    /// <summary>
    /// Checks if list is empty from right way.
    /// </summary>
    /// <returns>Returns true if list is empty, otherwise returns false</returns>
    public bool IsEmptyR()
    {
        return start.Right == null;
    }

    /// <summary>
    /// Checks if list is empty from right way.
    /// </summary>
    /// <returns>Returns true if list is empty, otherwise returns false</returns>
    public bool IsEmptyL()
    {
        return end.Left == null;
    }

    /// <summary>
    /// Removes specific given element from list.
    /// </summary>
    /// <param name="element">Element to remove from the list</param>
    public void RemoveElement(type element)
    {
        for (Node<type> d = start.Right; d.Right != null; d = d.Right)
        {
            if (d.Info.CompareTo(element) == 0)
            {
                d.Right.Left = d.Left;
                d.Left.Right = d.Right;
                break;
            }
        }
    }

    /// <summary>
    /// Return true if list contains given information.
    /// </summary>
    /// <param name="info">Info to check in the list</param>
    /// <returns>True, if list contains given information, otherwise returns true</returns>
    public bool Contains(type info)
    {
        if (start.Right != null)
        {
            for (Node<type> l = start.Right; l.Right != null; l = l.Right)
            {
                if (l.Info.CompareTo(info) == 0)
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
        if (start.Right == null || end.Left == null)
        {
            return;
        }

        for (Node<type> l1 = start.Right; l1.Right != null; l1 = l1.Right)
        {
            Node<type> min = l1;

            for (Node<type> l2 = l1.Right; l2.Right != null; l2 = l2.Right)
            {
                if (l2.Info.CompareTo(min.Info) < 0)
                {
                    min = l2;
                }
            }

            // Exchange of information parts
            type mod = l1.Info;
            l1.Info = min.Info;
            min.Info = mod;
        }
    }

    /// <summary>
    /// Clears list.
    /// </summary>
    public void EmptyList()
    {
        start.Right = end;
        end.Left = start;
        pre = start;
    }

    /// <summary>
    /// Gets next value from list.
    /// </summary>
    /// <returns>Returns next value from list.</returns>
    public IEnumerator<type> GetEnumerator()
    {
        for(Node <type> n = start.Right; n.Right != null; n = n.Right)
        {
            yield return n.Info;
        }
    }

    /// <summary>
    /// Gets next value from list.
    /// </summary>
    /// <returns>Returns GetEnumerator method value.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}