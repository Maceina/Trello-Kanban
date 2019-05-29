using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Class of list's element (node class)
/// </summary>
public sealed class Node
{
    public Node Next { get; set; }                      // Next element (node)
    public Info Info { get; set; }                      // Info part

    /// <summary>
    /// Creates object of Node class.
    /// </summary>
    public Node()
    {
    }

    /// <summary>
    /// Creates object of Node class.
    /// </summary>
    /// <param name="info">Information stored in Node class' object</param>
    /// <param name="next">Arrow to the next linked object of Node class</param>
    public Node(Info info, Node next)
    {
        Info = info;
        Next = next;
    }
}