//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : HidePropertyAttribute.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/06/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a simple attribute class used to indicate which properties should be hidden but allows
// their children to be included for binding.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/07/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;

namespace EWSoftware.PDI.Binding
{
    /// <summary>
    /// This is a simple attribute class used to indicate which properties should be hidden but allows their
    /// children to be included for binding.
    /// </summary>
    /// <remarks>This attribute is only valid when applied to a property.  Unlike <see cref="BrowsableAttribute"/>,
    /// child properties of a property marked with this attribute will be included.</remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class HidePropertyAttribute : Attribute
    {
    }
}
