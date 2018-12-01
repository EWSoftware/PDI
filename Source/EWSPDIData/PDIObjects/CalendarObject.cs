//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : CalendarObject.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the base class for all calendar related Personal Data Interchange (PDI) classes and the
// interface that it implements.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/29/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.IO;
using System.Text;

using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// This is the common abstract base class for all calendar related PDI objects
    /// </summary>
    public abstract class CalendarObject : PDIObject
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is overridden to propagate the version to all properties in the object that need it when the
        /// version is set on the item.
        /// </summary>
        public override SpecificationVersions Version
        {
            get => base.Version;
            set
            {
                base.Version = value;
                PropagateVersion();
            }
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// The method can be called to clear all current property values in the object.  The version is left
        /// unchanged.
        /// </summary>
        public abstract void ClearProperties();

        /// <summary>
        /// This is used to propagate the version to all properties in the object that need it
        /// </summary>
        public abstract void PropagateVersion();

        /// <summary>
        /// This is used to get a list of time zones used by all owned objects
        /// </summary>
        /// <param name="timeZoneIds">A <see cref="StringCollection"/> that will be used to store the list of
        /// unique time zone IDs used by the calendar objects.</param>
        public abstract void TimeZonesUsed(StringCollection timeZoneIds);

        /// <summary>
        /// This is used to replace an old time zone ID with a new time zone ID in all properties of a calendar
        /// object.
        /// </summary>
        /// <param name="oldId">The old ID being replaced</param>
        /// <param name="newId">The new ID to use</param>
        public abstract void UpdateTimeZoneId(string oldId, string newId);

        /// <summary>
        /// This is used to apply the selected time zone to all date/time objects in the component and convert
        /// them to the new time zone.
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        /// <remarks>When applied, all date/time values in the object will be converted to the new time zone</remarks>
        public abstract void ApplyTimeZone(VTimeZone vTimeZone);

        /// <summary>
        /// This is used to set the selected time zone in all date/time objects in the component without
        /// modifying the date/time values.
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        /// <remarks>This method does not affect the date/time values</remarks>
        public abstract void SetTimeZone(VTimeZone vTimeZone);

        /// <summary>
        /// This helper method can be used to add a time zone ID to the string collection when one is used on the
        /// passed property.
        /// </summary>
        /// <param name="dateProp">The date/time property to check.</param>
        /// <param name="timeZoneIds">The string collection to which the time zone ID is added if there is one on
        /// the date/time property and it is not already in the collection.</param>
        /// <remarks>If the date/time property has no time zone ID or if it is set to <see cref="DateTime.MinValue"/>,
        /// the collection is not modified.</remarks>
        protected static void AddTimeZoneIfUsed(BaseDateTimeProperty dateProp, StringCollection timeZoneIds)
        {
            if(dateProp != null && dateProp.TimeZoneDateTime != DateTime.MinValue)
            {
                string tzId = dateProp.TimeZoneId;

                if(tzId != null && !timeZoneIds.Contains(tzId))
                    timeZoneIds.Add(tzId);
            }
        }

        /// <summary>
        /// This helper method can be used to update a time zone ID in the passed date/time property
        /// </summary>
        /// <param name="dateProp">The date/time property to check</param>
        /// <param name="oldId">The old ID being replaced</param>
        /// <param name="newId">The new ID to use</param>
        /// <remarks>If the property's current time zone ID matches the old ID, it is replaced with the new ID</remarks>
        protected static void UpdatePropertyTimeZoneId(BaseDateTimeProperty dateProp, string oldId, string newId)
        {
            if(dateProp != null && dateProp.TimeZoneId == oldId)
                dateProp.TimeZoneId = newId;
        }

        /// <summary>
        /// This helper method can be used to apply a new time zone to the passed date/time property
        /// </summary>
        /// <param name="dateProp">The date/time property to check</param>
        /// <param name="vTimeZone">The new time zone component to use</param>
        /// <remarks>If the property's current time zone ID does not match the one in the new time zone, it is
        /// updated with the new ID and the date/time is adjusted to the new time zone.</remarks>
        protected static void ApplyPropertyTimeZone(BaseDateTimeProperty dateProp, VTimeZone vTimeZone)
        {
            if(dateProp != null && (vTimeZone == null || dateProp.TimeZoneId != vTimeZone.TimeZoneId.Value))
            {
                // If the time zone is null, just clear the time zone ID
                if(vTimeZone == null)
                {
                    dateProp.TimeZoneId = null;
                    return;
                }

                DateTimeInstance dti = VCalendar.TimeZoneToTimeZone(dateProp.TimeZoneDateTime, dateProp.TimeZoneId,
                    vTimeZone.TimeZoneId.Value);

                dateProp.TimeZoneDateTime = dti.StartDateTime;
                dateProp.TimeZoneId = vTimeZone.TimeZoneId.Value;
            }
        }

        /// <summary>
        /// This helper method can be used to set a new time zone in the passed date/time property without
        /// modifying the date/time value.
        /// </summary>
        /// <param name="dateProp">The date/time property to check</param>
        /// <param name="vTimeZone">The new time zone component to use</param>
        /// <remarks>If the property's current time zone ID does not match the one in the new time zone, it is
        /// updated with the new ID.  The date/time value is not changed.</remarks>
        protected static void SetPropertyTimeZone(BaseDateTimeProperty dateProp, VTimeZone vTimeZone)
        {
            if(dateProp != null && (vTimeZone == null || dateProp.TimeZoneId != vTimeZone.TimeZoneId.Value))
            {
                // If the time zone is null, just clear the time zone ID
                if(vTimeZone == null)
                    dateProp.TimeZoneId = null;
                else
                    dateProp.TimeZoneId = vTimeZone.TimeZoneId.Value;
            }
        }

        /// <summary>
        /// Convert the object instance to its string form
        /// </summary>
        /// <returns>Returns a text description of the object suitable for saving to a PDI data stream</returns>
        public override string ToString()
        {
            using(var sw = new StringWriter(new StringBuilder(1024), CultureInfo.InvariantCulture))
            {
                this.WriteToStream(sw, null);
                return sw.ToString();
            }
        }

        /// <summary>
        /// This can be used to write a calendar object to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the calendar object is
        /// written.</param>
        /// <remarks>This is called by <see cref="ToString"/> as well as owning objects when they convert
        /// themselves to a string or write themselves to a PDI data stream.</remarks>
        /// <overloads>There are two overloads for this method</overloads>
        public void WriteToStream(TextWriter tw)
        {
            if(tw is StringWriter)
                WriteToStream(tw, null);
            else
                WriteToStream(tw, new StringBuilder(256));
        }

        /// <summary>
        /// This can be used to write a calendar object to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the calendar object is
        /// written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="ToString"/> as well as owning objects when they convert
        /// themselves to a string or write themselves to a PDI data stream.</remarks>
        public abstract void WriteToStream(TextWriter tw, StringBuilder sb);

        #endregion
    }
}
