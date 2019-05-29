using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Interface which inherits required interfaces for classes 
/// </summary>
public interface ILabInterface<type> : IComparable<type>, IEquatable<type>
{
}