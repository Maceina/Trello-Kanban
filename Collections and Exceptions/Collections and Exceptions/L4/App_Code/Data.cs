using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Data class for laboratory work.
/// </summary>
public class Data : ILabInterface<Data>
{
    public string Server { get; set; }              // Server's name
    public string Sender { get; set; }              // Sender of email
    public string Receiver { get; set; }            // Receiver of email
    public DateTime Date { get; set; }              // Date & time when sent
    public List<int> Hours { get; private set; }    // Hours of 0 bytes transfers
    public long Other { get; set; }                 // Size of email or server's speed of interface to the
                                                    // internet in bytes

    /// <summary>
    /// Initializes a new instance of the Data class.
    /// </summary>
    public Data()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the Data class with specific information.
    /// </summary>
    /// <param name="server">Server's name.</param>
    /// <param name="date">Date of transfer.</param>
    /// <param name="other">Other information stored, such as size or speed.</param>
    public Data(string server, DateTime date, long other)
    {
        Server = server;
        Date = date;
        Other = other;
        Hours = new List<int>();
        Sender = null;
        Receiver = null;
    }

    /// <summary>
    /// Formats all properties of Data class object.
    /// </summary>
    /// <returns>All properties of Data class object.</returns>
    public String PrimaryDataToString()
    {
        return String.Format(" {0, -30} | {1:yy-MM-dd HH:mm:ss} | {2, -30} | {3, -30} | {4, -15} bytes  ",
                                Server, Date, Sender, Receiver, Other);
    }

    /// <summary>
    /// Formats main properties of Data class object.
    /// </summary>
    /// <returns>Main properties of Data class object.</returns>
    public String ServerDataToString()
    {
        return String.Format(" {0, -30} | {1, -15} bytes  ", Server, Other);
    }

    /// <summary>
    /// Formats transfer date of Data class object.
    /// </summary>
    /// <returns>Transfer date of Data class object.</returns>
    public String DateToString()
    {
        return String.Format(" {0:yy-MM-dd} ", Date);
    }

    /// <summary>
    /// Formats Data class object's properties with filled List of results.
    /// </summary>
    /// <returns>Data class object's properties with filled List of results.</returns>
    public String ResultsToString()
    {
        string hours = null;

        foreach(int hour in Hours)
        {
            hours += hour + "; ";
        }

        return String.Format(" {0, -30} | {1:yy-MM-dd} | {2} ", Server, Date, hours);
    }

    /// <summary>
    /// Compares this instance with other Data class object.
    /// </summary>
    /// <param name="other">Other object of Data class.</param>
    /// <returns>An integer that indicates their relative position in the sort order.</returns>
    public int CompareTo(Data other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Server.CompareTo(other.Server) != 0)
        {
            return Server.CompareTo(other.Server);
        }

        else
        {
            return Date.CompareTo(other.Date);
        }        
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">Object to check.</param>
    /// <returns>False, if <paramref name="obj"/> is null, otherwise returns Boolean
    /// value of other Equals method.</returns>
    public override bool Equals(Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        Data dataObj = obj as Data;

        if (dataObj == null)
        {
            return false;
        }

        else
        {
            return Equals(dataObj);
        }
    }

    /// <summary>
    /// Determines whether the specified Data class object is equal to the current Data class object.
    /// </summary>
    /// <param name="other">Other Data class object to check.</param>
    /// <returns>False, if <paramref name="other"/> is null or not equal, otherwise true.</returns>
    public bool Equals(Data other)
    {
        if (other == null)
        {
            return false;
        }

        if (Server == other.Server)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    /// <summary>
    /// Gets hash code of current Data class object.
    /// </summary>
    /// <returns>The hash code for this Data class object.</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    /// Compares two objects of Data class.
    /// </summary>
    /// <param name="lhs">Left object.</param>
    /// <param name="rhs">Right object.</param>
    /// <returns>True, if <paramref name="lhs"/> is greater than <paramref name="rhs"/>.</returns>
    static public bool operator >(Data lhs, Data rhs)
    {
        return lhs.CompareTo(rhs) == 1;
    }

    /// <summary>
    /// Compares two objects of Data class.
    /// </summary>
    /// <param name="lhs">Left object.</param>
    /// <param name="rhs">Right object.</param>
    /// <returns>True, if <paramref name="lhs"/> is less than <paramref name="rhs"/>.</returns>
    static public bool operator <(Data lhs, Data rhs)
    {
        return lhs.CompareTo(rhs) == -1;
    }

    /// <summary>
    /// Compares two objects of Data class.
    /// </summary>
    /// <param name="lhs">Left object.</param>
    /// <param name="rhs">Right object.</param>
    /// <returns>True, if <paramref name="lhs"/> is not less than <paramref name="rhs"/>.</returns>
    static public bool operator >=(Data lhs, Data rhs)
    {
        return !(lhs < rhs);
    }

    /// <summary>
    /// Compares two objects of Data class.
    /// </summary>
    /// <param name="lhs">Left object.</param>
    /// <param name="rhs">Right object.</param>
    /// <returns>True, if <paramref name="lhs"/> is not greater than <paramref name="rhs"/>.</returns>
    static public bool operator <=(Data lhs, Data rhs)
    {
        return !(lhs > rhs);
    }

    /// <summary>
    /// Compares two objects of Data class.
    /// </summary>
    /// <param name="lhs">Left object.</param>
    /// <param name="rhs">Right object.</param>
    /// <returns>Boolean value of Equals method.</returns>
    public static bool operator ==(Data lhs, Data rhs)
    {
        if (((object)lhs) == null || ((object)rhs) == null)
        {
            return Object.Equals(lhs, rhs);
        }

        return lhs.Equals(rhs);
    }

    /// <summary>
    /// Compares two objects of Data class.
    /// </summary>
    /// <param name="lhs">Left object.</param>
    /// <param name="rhs">Right object.</param>
    /// <returns>Inverted Boolean value of Equals method.</returns>
    public static bool operator !=(Data lhs, Data rhs)
    {
        if (((object)lhs) == null || ((object)rhs) == null)
        {
            return !Object.Equals(lhs, rhs);
        }

        return !(lhs.Equals(rhs));
    }
}