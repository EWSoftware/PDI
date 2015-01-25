//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RRuleProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/19/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Recurrence Rule property.  It is used with the Personal Data Interchange (PDI)
// vCalendar and iCalendar classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/31/2004  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Recurrence Rule (RRULE) property of a vCalendar or iCalendar.  This
    /// defines a rule or repeating pattern for recurring items.
    /// </summary>
    /// <remarks>The <see cref="BaseProperty.Value"/> property contains the recurrence information in string
    /// form.  The <see cref="Recurrence"/> property can be used to access it as a <c>Recurrence</c> object.</remarks>
    public class RRuleProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private Recurrence recur;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCalendar10 | SpecificationVersions.iCalendar20; }
        }

        /// <summary>
        /// This read-only property defines the tag (RRULE)
        /// </summary>
        public override string Tag
        {
            get { return "RRULE"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as RECUR
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Recur; }
        }

        /// <summary>
        /// This is used to get the recurrence rule information
        /// </summary>
        /// <value>The recurrence object can also be used to generate a set of dates represented by the rule</value>
        public Recurrence Recurrence
        {
            get
            {
                if(recur == null)
                    recur = new Recurrence();

                return recur;
            }
        }

        /// <summary>
        /// This is overridden to handle parsing of the recurrence value to/from its string form
        /// </summary>
        public override string Value
        {
            get
            {
                // If undefined, it won't be saved
                if(recur == null || recur.Frequency == RecurFrequency.Undefined)
                    return null;

                if(this.Version == SpecificationVersions.vCalendar10)
                    return recur.ToVCalendarString();

                return recur.ToString();
            }
            set { this.Recurrence.Parse(value); }
        }

        /// <summary>
        /// This is overridden to handle parsing of the recurrence value to/from its string form
        /// </summary>
        public override string EncodedValue
        {
            get { return this.Value; }
            set { this.Value = value; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Default constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public RRuleProperty()
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
            RRuleProperty o = new RRuleProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
