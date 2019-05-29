using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Class of list's element (node class)
/// </summary>
public sealed class Node <type> where type : ILabInterface<type>
{
    public Node <type> Left { get; set; }                      // Left element of node
    public Node<type> Right { get; set; }                      // Right element of node
    public type Info { get; set; }                             // Info part

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
    /// <param name="left">Arrow to the left object of linked list</param>
    /// <param name="right">Arrow to the right object of linked list</param>
    public Node(type info, Node <type> left, Node <type> right)
    {
        Info = info;
        Left = left;
        Right = right;
    }
}