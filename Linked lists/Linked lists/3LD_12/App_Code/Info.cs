using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Class for storing information of module/student.
/// </summary>
public class Info : ILabInterface<Info>
{
    public string ModName { get; set; }                     // Module's name
    public string Surname { get; set; }                     // Person's surname
    public string Name { get; set; }                        // Person's name
    public string OtherInfo { get; set; }                   // Other information
    
    /// <summary>
    /// Creates object of Info class.
    /// </summary>
    public Info()
    {
    }

    /// <summary>
    /// Creates object of Info class.
    /// </summary>
    /// <param name="mName">Module's name</param>
    /// <param name="surname">Surname of student or lecturer</param>
    /// <param name="name">Name of student or lecturer</param>
    /// <param name="otherInfo">Other information</param>
    public Info(string mName, string surname, string name, string otherInfo)
    {
        ModName = mName;
        Surname = surname;
        Name = name;
        OtherInfo = otherInfo;
    }

    /// <summary>
    /// Formats string type line for printing results.
    /// </summary>
    /// <returns>Formatted line with information of class Info object</returns>
    public override String ToString()
    {
        return String.Format(" {0, -70} | {1, -10} {2, -15} | {3, 4} ", ModName, Name, Surname, OtherInfo);
    }

    /// <summary>
    /// Compares two objects of Info class
    /// </summary>
    /// <param name="lhs">Left object</param>
    /// <param name="rhs">Right object</param>
    /// <returns>Returns true, if left object is greater than right object, otherwise returns false</returns>
    static public bool operator >(Info lhs, Info rhs)
    {
        return lhs.CompareTo(rhs) == 1;
    }

    /// <summary>
    /// Compares two objects of Info class
    /// </summary>
    /// <param name="lhs">Left object</param>
    /// <param name="rhs">Right object</param>
    /// <returns>Returns true, if left object is less than right object, otherwise returns false</returns>
    static public bool operator <(Info lhs, Info rhs)
    {
        return lhs.CompareTo(rhs) == -1;
    }

    /// <summary>
    /// Compares two objects of Info class
    /// </summary>
    /// <param name="lhs">Left object</param>
    /// <param name="rhs">Right object</param>
    /// <returns>Returns true, if left object is not less than right object, otherwise returns false</returns>
    static public bool operator >=(Info lhs, Info rhs)
    {
        return !(lhs < rhs);
    }

    /// <summary>
    /// Compares two objects of Info class
    /// </summary>
    /// <param name="lhs">Left object</param>
    /// <param name="rhs">Right object</param>
    /// <returns>Returns true, if left object is not greater than right object, otherwise returns false</returns>
    static public bool operator <=(Info lhs, Info rhs)
    {
        return !(lhs > rhs);
    }

    /// <summary>
    /// Checks if two objects of Info class are equal by 'ModName' property.
    /// </summary>
    /// <param name="other">Other object of Info class to check with current object</param>
    /// <returns>Returns false, if <paramref name="other"/> is null or not equal, otherwise returns true></is></returns>
    public bool Equals(Info other)
    {
        if (other == null)
        {
            return false;
        }

        if (ModName == other.ModName)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if object of Info class is equal to <paramref name="obj"/> by 'ModName' property.
    /// </summary>
    /// <param name="obj">Other object of Info class to check with current object</param>
    /// <returns>Returns false, if <paramref name="obj"/> is null or not equal, otherwise returns Equals
    /// method for checking equality of two Info class' objects ></is></returns>
    public override bool Equals(Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        Info infoObj = obj as Info;

        if (infoObj == null)
        {
            return false;
        }

        else
        {
            return Equals(infoObj);
        }
    }

    /// <summary>
    /// Overrides hash code.
    /// </summary>
    /// <returns>Base hash code</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    /// <summary>
    /// Checks if two objects of Info class are equal.
    /// </summary>
    /// <param name="lhs">Left object</param>
    /// <param name="rhs">Right object</param>
    /// <returns>Equals method for objects, if one of objects is null, otherwise returns Equals
    /// method for Info class objects</returns>
    public static bool operator ==(Info lhs, Info rhs)
    {
        if (((object)lhs) == null || ((object)rhs) == null)
        {
            return Object.Equals(lhs, rhs);
        }

        return lhs.Equals(rhs);
    }

    /// <summary>
    /// Checks if two objects of Info class are not equal.
    /// </summary>
    /// <param name="lhs">Left object</param>
    /// <param name="rhs">Right object</param>
    /// <returns>Negative Equals method value for objects if one of objects is null, otherwise returns 
    /// negative Equals method value for Info class objects</returns>
    public static bool operator !=(Info lhs, Info rhs)
    {
        if (((object)lhs) == null || ((object)rhs) == null)
        {
            return !Object.Equals(lhs, rhs);
        }

        return !(lhs.Equals(rhs));
    }

    /// <summary>
    /// Checks if information given is the same as Info class object's information.
    /// </summary>
    /// <param name="info"></param>
    /// <returns>True, if information of personal info (name, surname) is the same</returns>
    public bool IsTheSame(Info info)
    {
        return (Name.CompareTo(info.Name) == 0 && Surname.CompareTo(info.Surname) == 0);
    }

    /// <summary>
    /// Compares current Info class object with other object of Info class.
    /// </summary>
    /// <param name="other">Other object of Info class</param>
    /// <returns>1, if <paramref name="other"/> is null, otherwise returns CompareTo method value
    /// for specific properties.</returns>
    public int CompareTo(Info other)
    {
        if (other == null) return 1;

        return ModName.CompareTo(other.ModName);        
    }
}