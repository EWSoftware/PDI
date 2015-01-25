//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : NameToValue.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/13/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class used to map a property or parameter name string to a an enumerated value
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/21/2004  EFW  Created the code
// 03/17/2007  EFW  Converted the class to be a generic class
//===============================================================================================================

using System;
using System.Globalization;

namespace EWSoftware.PDI.Parser
{
    /// <summary>
    /// This is used to map a property or parameter name string to an enumerated value or integer value
    /// </summary>
    /// <typeparam name="T">The enumerated type used for the mapping</typeparam>
    /// <remarks>The parsers can create an array of these to convert the strings to a property type or parameter
    /// type and then create the property object and assign it to the PDI object or set the parameter value based
    /// on the item found.</remarks>
    public class NameToValue<T> where T : struct
    {
        #region Private data members
        //=====================================================================

        private string name;
        private T enumValue;
        private bool isParamValue;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get the property or parameter name string
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// This is used to get the property or parameter type value
        /// </summary>
        /// <value>The value should be a member of an enumerated type</value>
        public T EnumValue
        {
            get { return enumValue; }
        }

        /// <summary>
        /// This is used when mapping parameter names and values.  It can be used to specify that this item is a
        /// parameter value rather than a parameter name.
        /// </summary>
        /// <value>True if the item is a parameter value or false if it is a parameter name</value>
        public bool IsParameterValue
        {
            get { return isParamValue; }
        }
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Use this constructor to create an instance that maps a name to a property name rather than a value
        /// </summary>
        /// <param name="itemName">The property or parameter name</param>
        /// <param name="enumVal">The value that represents the property or parameter name</param>
        /// <overloads>There are two overloads for the constructor</overloads>
        public NameToValue(string itemName, T enumVal)
        {
            name = itemName;
            enumValue = enumVal;
        }

        /// <summary>
        /// Use this constructor to create an instance that maps a name to a value that is a parameter value
        /// </summary>
        /// <param name="itemName">The property or parameter name</param>
        /// <param name="enumVal">The property or parameter value</param>
        /// <param name="itemIsValue">True if the item is a value rather than a property name</param>
        public NameToValue(string itemName, T enumVal, bool itemIsValue)
        {
            name = itemName;
            enumValue = enumVal;
            isParamValue = itemIsValue;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This can be called to see if a string matches the instance's name
        /// </summary>
        /// <param name="itemName">The name of the item.  The value is not case-sensitive.  If it ends with an
        /// equals sign (=), the equals sign is ignored.</param>
        /// <returns>True if there is a match, false if not</returns>
        public bool IsMatch(string itemName)
        {
            if(itemName == null)
                return false;

            if(!itemName.EndsWith("=", StringComparison.Ordinal))
                return (String.Compare(itemName, name, StringComparison.OrdinalIgnoreCase) == 0);

            if(itemName.Length - 1 != name.Length)
                return false;

            return (String.Compare(itemName, 0, name, 0, name.Length, StringComparison.OrdinalIgnoreCase) == 0);
        }
        #endregion
    }
}
