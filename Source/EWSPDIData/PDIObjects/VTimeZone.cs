//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : VTimeZone.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the definition for the VTimeZone object used by vCalendar and iCalendar objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//   Date      Who  Comments
// ==============================================================================================================
// 03/28/2004  EFW  Created the code
// 10/08/2005  EFW  Added MergingEnabled property
// 03/05/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;
using System.ComponentModel;
using System.IO;
using System.Text;

using EWSoftware.PDI.Binding;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// This class represents a VTIMEZONE component that can appear in a calendar
    /// </summary>
    [TypeDescriptionProvider(typeof(VTimeZoneTypeDescriptionProvider))]
    public class VTimeZone : CalendarObject
    {
        #region Private data members
        //=====================================================================

        // Single properties
        private TimeZoneIdProperty   timeZoneId;
        private TimeZoneUrlProperty  timeZoneUrl;
        private LastModifiedProperty lastMod;

        // Time zone property collections.  There can be one or more of each of these properties so they are
        // stored in a collection.
        private ObservanceRuleCollection rules;

        // This is a catch-all that holds all unknown or extension properties
        private CustomPropertyCollection customProps;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.iCalendar20;

        /// <summary>
        /// This is used to get the Time Zone ID (TZID) property
        /// </summary>
        public TimeZoneIdProperty TimeZoneId
        {
            get
            {
                if(timeZoneId == null)
                {
                    timeZoneId = new TimeZoneIdProperty();
                    timeZoneId.TimeZoneIdChanged += tzid_TimeZoneIdChanged;
                }

                return timeZoneId;
            }
        }

        /// <summary>
        /// This is used to get the Time Zone Uniform Resource Locator (TZURL) property
        /// </summary>
        public TimeZoneUrlProperty TimeZoneUrl
        {
            get
            {
                if(timeZoneUrl == null)
                    timeZoneUrl = new TimeZoneUrlProperty();

                return timeZoneUrl;
            }
        }

        /// <summary>
        /// This is used to get the Last Modified (LAST-MODIFIED) property
        /// </summary>
        public LastModifiedProperty LastModified
        {
            get
            {
                if(lastMod == null)
                    lastMod = new LastModifiedProperty();

                return lastMod;
            }
        }

        /// <summary>
        /// This is used to get the observance rules (STANDARD and DAYLIGHT) used by the time zone component.
        /// There may be more than one of each.
        /// </summary>
        public ObservanceRuleCollection ObservanceRules
        {
            get
            {
                if(rules == null)
                    rules = new ObservanceRuleCollection();

                return rules;
            }
        }

        /// <summary>
        /// This is a catch-all that holds all unknown or extension properties
        /// </summary>
        /// <value>If the returned collection is empty, there are no custom properties for the calendar</value>
        public CustomPropertyCollection CustomProperties
        {
            get
            {
                if(customProps == null)
                    customProps = new CustomPropertyCollection();

                return customProps;
            }
        }
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when the <see cref="TimeZoneId"/> property is changed
        /// </summary>
        public event EventHandler<TimeZoneIdChangedEventArgs> TimeZoneIdChanged;

        /// <summary>
        /// This raises the <see cref="TimeZoneIdChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnTimeZoneIdChanged(TimeZoneIdChangedEventArgs e)
        {
            TimeZoneIdChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This is called when the <c>TimeZoneId</c> property's name changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tzid_TimeZoneIdChanged(object sender, TimeZoneIdChangedEventArgs e)
        {
            OnTimeZoneIdChanged(e);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public VTimeZone()
        {
            this.Version = SpecificationVersions.iCalendar20;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to allow cloning of a PDI object
        /// </summary>
        /// <returns>A clone of the object</returns>
        public override object Clone()
        {
            VTimeZone o = new VTimeZone();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            VTimeZone o = (VTimeZone)p;

            this.ClearProperties();

            timeZoneId = (TimeZoneIdProperty)o.TimeZoneId.Clone();
            timeZoneId.TimeZoneIdChanged += tzid_TimeZoneIdChanged;

            timeZoneUrl = (TimeZoneUrlProperty)o.TimeZoneUrl.Clone();
            lastMod = (LastModifiedProperty)o.LastModified.Clone();

            this.ObservanceRules.CloneRange(o.ObservanceRules);
            this.CustomProperties.CloneRange(o.CustomProperties);
        }

        /// <summary>
        /// The method can be called to clear all current property values from the time zone.  The version is
        /// left unchanged.
        /// </summary>
        public override void ClearProperties()
        {
            if(timeZoneId != null)
            {
                timeZoneId.TimeZoneIdChanged -= tzid_TimeZoneIdChanged;
                timeZoneId = null;
            }

            timeZoneUrl = null;
            lastMod = null;

            rules = null;
            customProps = null;
        }

        /// <summary>
        /// This is used to propagate the version to all properties in the object that need it
        /// </summary>
        public override void PropagateVersion()
        {
            if(timeZoneId != null)
                timeZoneId.Version = this.Version;

            if(timeZoneUrl != null)
                timeZoneUrl.Version = this.Version;

            if(lastMod != null)
                lastMod.Version = this.Version;

            if(rules != null)
                rules.PropagateVersion(this.Version);

            if(customProps != null)
                customProps.PropagateVersion(this.Version);
        }

        /// <summary>
        /// The time zone object does not have any properties that use time zone IDs
        /// </summary>
        /// <param name="timeZoneIds">A <see cref="StringCollection"/> that will be used to store the list of
        /// unique time zone IDs used by the calendar objects.</param>
        public override void TimeZonesUsed(StringCollection timeZoneIds)
        {
        }

        /// <summary>
        /// The time zone object does not have any properties that use time zone IDs
        /// </summary>
        /// <param name="oldId">The old ID being replaced</param>
        /// <param name="newId">The new ID to use</param>
        public override void UpdateTimeZoneId(string oldId, string newId)
        {
        }

        /// <summary>
        /// The time zone object does not have any properties that use time zone IDs
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        public override void ApplyTimeZone(VTimeZone vTimeZone)
        {
        }

        /// <summary>
        /// The time zone object does not have any properties that use time zone IDs
        /// </summary>
        /// <param name="vTimeZone">A <see cref="VTimeZone"/> object that will be used for all date/time objects
        /// in the component.</param>
        public override void SetTimeZone(VTimeZone vTimeZone)
        {
        }

        /// <summary>
        /// This can be used to write a time zone to a PDI data stream
        /// </summary>
        /// <param name="tw">A <see cref="System.IO.TextWriter"/> derived class to which the time zone is
        /// written.</param>
        /// <param name="sb">A <see cref="System.Text.StringBuilder"/> used by the properties as a temporary
        /// buffer.  This can be null if the TextWriter is a <see cref="System.IO.StringWriter"/>.</param>
        /// <remarks>This is called by <see cref="CalendarObject.ToString"/> as well as owning objects when they
        /// convert themselves to a string or write themselves to a PDI data stream.</remarks>
        /// <exception cref="ArgumentException">This is thrown if the TimeZoneId's Value property is null</exception>
        public override void WriteToStream(TextWriter tw, StringBuilder sb)
        {
            PropagateVersion();

            tw.Write("BEGIN:VTIMEZONE\r\n");

            // The TZID property is required.
            if(this.TimeZoneId.Value == null)
                throw new ArgumentException(LR.GetString("ExTZIDCannotBeNull"));

            BaseProperty.WriteToStream(timeZoneId, sb, tw);
            BaseProperty.WriteToStream(timeZoneUrl, sb, tw);
            BaseProperty.WriteToStream(lastMod, sb, tw);

            if(rules != null && rules.Count != 0)
                foreach(ObservanceRule r in rules)
                    r.WriteToStream(tw, sb);

            if(customProps != null && customProps.Count != 0)
                foreach(CustomProperty c in customProps)
                    BaseProperty.WriteToStream(c, sb, tw);

            tw.Write("END:VTIMEZONE\r\n");
        }

        /// <summary>
        /// This is overridden to allow proper comparison of time zone objects
        /// </summary>
        /// <param name="obj">The object to which this instance is compared</param>
        /// <returns>Returns true if the object equals this instance, false if it does not</returns>
        public override bool Equals(object obj)
        {
            if(!(obj is VTimeZone tz))
                return false;

            // The ToString() method returns a text representation of the time zone based on all of its settings
            // so it's a reliable way to tell if two instances are the same.
            return (this == tz || this.ToString() == tz.ToString());
        }

        /// <summary>
        /// Get a hash code for the calendar object
        /// </summary>
        /// <returns>Returns the hash code for the calendar object</returns>
        /// <remarks>Since the ToString() method returns a text representation based on all of the settings, this
        /// returns the hash code for the string returned by it.</remarks>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        #endregion
    }
}
