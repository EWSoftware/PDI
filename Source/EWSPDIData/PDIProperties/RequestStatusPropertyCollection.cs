//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RequestStatusPropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a collection class for RequestStatusProperty objects.  It is used by the Personal Data
// Interchange (PDI) iCalendar class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/05/2004  EFW  Created the code
// 03/30/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="RequestStatusProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class RequestStatusPropertyCollection : ExtendedBindingList<RequestStatusProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public RequestStatusPropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="RequestStatusProperty"/> objects
        /// </summary>
        /// <param name="stats">The <see cref="IList{T}"/> of request statuses to add</param>
        public RequestStatusPropertyCollection(IList<RequestStatusProperty> stats) : base(stats)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="RequestStatusProperty"/> to the collection and assign it the specified status code,
        /// message, and extended data.
        /// </summary>
        /// <param name="code">The status code to assign to the new property</param>
        /// <param name="message">The message value to assign to the new property</param>
        /// <param name="data">The extended data, if any, to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public RequestStatusProperty Add(string code, string message, string data)
        {
            RequestStatusProperty rs = new RequestStatusProperty();
            rs.StatusCode = code;
            rs.StatusMessage = message;
            rs.ExtendedData = data;
            base.Add(rs);

            return rs;
        }

        /// <summary>
        /// This is used to propagate a common version to all objects in the collection
        /// </summary>
        /// <param name="version">The version to use</param>
        public void PropagateVersion(SpecificationVersions version)
        {
            foreach(PDIObject o in this)
                o.Version = version;

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        #endregion
    }
}
