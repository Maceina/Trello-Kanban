using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Exception class for laboratory work.
/// </summary>
public class LabException : Exception
{
    //private string message;                 // Message to show.
    //private Exception exception;            // Inner exception.
    
    /// <summary>
    /// Initializes a new instance of the LabException class.
    /// </summary>
    public LabException() : base()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the LabException class with message to show.
    /// </summary>
    /// <param name="message">Exception message to show.</param>
    public LabException(string message) : base(message)
    {

    }

    /// <summary>
    /// Initializes a new instance of the LabException class with message and inner Exception class object.
    /// </summary>
    /// <param name="message">Exception message to show</param>
    /// <param name="exception">Inner exception pass on.</param>
    public LabException(string message, Exception exception) : base(message, exception)
    {
        
    }
}