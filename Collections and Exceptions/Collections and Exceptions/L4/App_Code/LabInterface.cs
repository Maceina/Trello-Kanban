using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Interfaces to inherit for Data class in laboratory work.
/// </summary>
public interface ILabInterface<type> : IComparable<type>, IEquatable<type>
{
}