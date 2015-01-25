//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : TimeZoneIdProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/19/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Time Zone ID property classes used by the Personal Data Interchange (PDI) iCalendar
// classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//   Date      Who  Comments
// ==============================================================================================================
// 09/20/2004  EFW  Created the code
// 03/30/2007  EFW  Updated for use with .NET 2.0
//===============================================================================================================

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Time Zone ID (TZID) property of a VTIMEZONE component.  This
    /// specifies the text value that uniquely identifies the VTIMEZONE calendar component.
    /// </summary>
    /// <remarks><para>This property has no special requirements or handling.  The <see cref="Value"/> property
    /// contains the ID.  This property is only valid for use with the iCalendar 2.0 specification.</para>
    /// 
    /// <para>When the value changes, the <see cref="TimeZoneIdChanged"/> event is raised so that objects
    /// dependent upon the ID can update themselves with the new value.</para></remarks>
    public class TimeZoneIdProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports iCalendar 2.0 only</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.iCalendar20; }
        }

        /// <summary>
        /// This read-only property defines the tag (TZID)
        /// </summary>
        public override string Tag
        {
            get { return "TZID"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This is overridden to raise the <see cref="TimeZoneIdChanged"/> event when the ID changes
        /// </summary>
        /// <exception cref="ArgumentException">This is thrown if the value is set to null or an empty string</exception>
        public override string Value
        {
            get { return base.Value; }
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(LR.GetString("ExTZIDCannotBeNull"));

                string oldId = base.Value;

                // If changed, set it and raise the TimeZoneIdChanged event except on first use (old ID = null)
                if(oldId != value)
                {
                    base.Value = value;

                    if(oldId != null)
                        OnTimeZoneIdChanged(new TimeZoneIdChangedEventArgs(oldId, value));
                }
            }
        }

        /// <summary>
        /// This is overridden to raise the <see cref="TimeZoneIdChanged"/> event when the ID changes
        /// </summary>
        /// <exception cref="ArgumentException">This is thrown if the value is set to null</exception>
        public override string EncodedValue
        {
            get { return base.EncodedValue; }
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(LR.GetString("ExTZIDCannotBeNull"));

                // If changed, set it and raise the TimeZoneIdChanged event except on first use (old ID = null).
                // The decoded values are sent as the event arguments, not the encoded values.
                if(base.EncodedValue != value)
                {
                    string oldId = base.Value;

                    base.EncodedValue = value;

                    if(oldId != null)
                        OnTimeZoneIdChanged(new TimeZoneIdChangedEventArgs(oldId, base.Value));
                }
            }
        }
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when the <see cref="Value"/> property is changed
        /// </summary>
        public event EventHandler<TimeZoneIdChangedEventArgs> TimeZoneIdChanged;

        /// <summary>
        /// This raises the <see cref="TimeZoneIdChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnTimeZoneIdChanged(TimeZoneIdChangedEventArgs e)
        {
            var handler = TimeZoneIdChanged;

            if(handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeZoneIdProperty()
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
            TimeZoneIdProperty o = new TimeZoneIdProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
