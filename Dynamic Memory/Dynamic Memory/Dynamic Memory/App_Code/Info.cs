using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Class for storing information of module/student.
/// </summary>
public class Info
{
    public string ModName { get; set; }                     // Module's name
    public string Surname { get; set; }                     // Surname of responsible Teacher
    public string Name { get; set; }                        // Name of responsible Teacher
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
    /// <param name="otherInfo">Other information, such as group name or credits' count</param>
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
    /// Compares two objects of Info class by module's name, and personal information of person.
    /// </summary>
    /// <param name="first">First object</param>
    /// <param name="second">Second object</param>
    /// <returns>Comparison results</returns>
    public static bool operator >(Info first, Info second)
    {
        int mN = String.Compare(first.ModName, second.ModName, StringComparison.CurrentCulture);
        int n = String.Compare(first.Name, second.Name, StringComparison.CurrentCulture);
        int s = String.Compare(first.Surname, second.Surname, StringComparison.CurrentCulture);
        return (mN > 0) || (mN == 0 && n > 0) || (mN == 0 && n == 0 && s == 0);
    }

    /// <summary>
    /// Compares two objects of Info class by module's name, and personal information of person.
    /// </summary>
    /// <param name="first">First object</param>
    /// <param name="second">Second object</param>
    /// <returns>Comparison results</returns>
    public static bool operator <(Info first, Info second)
    {
        return !(first > second);
    }

    /// <summary>
    /// Checks if the object is the same.
    /// </summary>
    /// <param name="obj">Object to check</param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return this.Equals(obj as Info);
    }

    /// <summary>
    /// Checks if the object is the same.
    /// </summary>
    /// <param name="info">Object to check</param>
    /// <returns>Returns false if not or if it's null, otherwise returs true</returns>
    public bool Equals(Info info)
    {
        if (Object.ReferenceEquals(info, null))
        {
            return false;
        }

        if (this.GetType() != info.GetType())
        {
            return false;
        }

        return (ModName == info.ModName);
    }

    /// <summary>
    /// Gets exclusive hash code of specific property of object.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return ModName.GetHashCode();
    }

    /// <summary>
    /// Checks if two objects of Info class are the same object.
    /// </summary>
    /// <param name="lhs">First object</param>
    /// <param name="rhs">Second object</param>
    /// <returns>True, if they're both null, false if one of them is null, otherwise 
    /// returns Equals method's output</returns>
    public static bool operator ==(Info lhs, Info rhs)
    {
        if (Object.ReferenceEquals(lhs, null))
        {
            if (Object.ReferenceEquals(rhs, null))
            {
                return true;
            }

            return false;
        }

        return lhs.Equals(rhs);
    }

    /// <summary>
    /// Checks if two objects of Info class are not the same object.
    /// </summary>
    /// <param name="lhs">First object</param>
    /// <param name="rhs">Second object</param>
    /// <returns>False, if they're both null, true if one of them is null, otherwise 
    /// returns false Equals method's output</returns>
    public static bool operator !=(Info lhs, Info rhs)
    {
        return !(lhs == rhs);
    }

    /// <summary>
    /// Checks if information given is the same as Info class object's information
    /// </summary>
    /// <param name="info"></param>
    /// <returns>True, if information of personal info (name, surname) is the same</returns>
    public bool IsTheSame(Info info)
    {
        int n = String.Compare(Name, info.Name, StringComparison.CurrentCulture);
        int s = String.Compare(Surname, info.Surname, StringComparison.CurrentCulture);

        return (n == 0 && s == 0);
    }
}