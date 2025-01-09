//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TimeZoneNamePropertyCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains a collection class for TimeZoneNameProperty objects.  It is used by the Personal Data
// Interchange (PDI) iCalendar class.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//   Date      Who  Comments
// ==============================================================================================================
// 09/20/2004  EFW  Created the code
// 03/30/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

using EWSoftware.PDI.Binding;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// A type-safe collection of <see cref="TimeZoneNameProperty"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator</remarks>
    public class TimeZoneNamePropertyCollection : ExtendedBindingList<TimeZoneNameProperty>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public TimeZoneNamePropertyCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="TimeZoneNameProperty"/> objects
        /// </summary>
        /// <param name="timeZoneNames">The <see cref="IList{T}"/> of time zone names to add</param>
        public TimeZoneNamePropertyCollection(IList<TimeZoneNameProperty> timeZoneNames) : base(timeZoneNames)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add a <see cref="TimeZoneNameProperty"/> to the collection and assign it the specified value
        /// </summary>
        /// <param name="tzId">The time zone name value to assign to the new property</param>
        /// <returns>Returns the new property that was created and added to the collection</returns>
        public TimeZoneNameProperty Add(string tzId)
        {
            TimeZoneNameProperty tzn = new() { Value = tzId };

            this.Add(tzn);

            return tzn;
        }

        /// <summary>
        /// Collection indexer by language ID
        /// </summary>
        /// <param name="languageId">The language ID by which to search</param>
        /// <returns>When retrieving an item, the entry whose <c>Language</c> property matches the specified
        /// language ID is returned.  If an exact match is not found, an attempt is made to locate a match using
        /// only the two letter ISO language name.  If that fails, an attempt is made to find an entry using the
        /// current culture.  If that fails, it returns the first entry in the list or null if there are none.</returns>
        public TimeZoneNameProperty? this[string? languageId]
        {
            get
            {
                if(languageId == null || languageId.Length == 0)
                    languageId = "??";

                string isoName = languageId.Split('-')[0], currentName = CultureInfo.CurrentCulture.Name,
                    currentISOName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

                int idx, cultureIdx = -1;
                TimeZoneNameProperty? tzn = null;

                string propLang;

                // Search for everything in one pass
                for(idx = 0; idx < base.Count; idx++)
                {
                    tzn = base[idx];
                    propLang = tzn.Language;

                    // Found an exact match by ID?
                    if(String.Compare(propLang, languageId, StringComparison.InvariantCultureIgnoreCase) == 0)
                        break;

                    // How about a partial match by ISO Name Found an exact match by ID?
                    if(String.Compare(propLang, isoName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        break;

                    // Note the location of anything matching the current culture.  We'll use the first one found
                    // if all else fails.
                    if(String.Compare(propLang, currentName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        cultureIdx = idx;
                    else
                    {
                        // Only use ISO match if there's no full match
                        if(cultureIdx == -1 && String.Compare(propLang, currentISOName, StringComparison.InvariantCultureIgnoreCase) == 0)
                            cultureIdx = idx;
                    }
                }

                // Found a match by the passed ID?
                if(idx < base.Count)
                    return tzn;

                // How about one by the current culture?
                if(cultureIdx != -1)
                    return base[cultureIdx];

                // Otherwise, give up and return the first entry in the list or null if there are no entries
                if(this.Count != 0)
                    return base[0];

                return null;
            }
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
