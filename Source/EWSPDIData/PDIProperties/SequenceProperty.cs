//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : SequenceProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/19/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Sequence property class used by the Personal Data Interchange (PDI) vCalendar and
// iCalendar classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/31/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Sequence (SEQUENCE) property of a vCalendar or iCalendar object.
    /// This defines the revision sequence number of the calendar component within a sequence of revisions.
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property to allow setting and getting the
    /// value as an integer.</remarks>
    public class SequenceProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private static Regex reNumber = new Regex(@"^\d*$");

        private int sequenceNumber;

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
        /// This read-only property defines the tag (SEQUENCE)
        /// </summary>
        public override string Tag
        {
            get { return "SEQUENCE"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as INTEGER
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Integer; }
        }

        /// <summary>
        /// This allows the sequence number to be set or retrieved as an integer
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">An exception is thrown if the value is less than zero</exception>
        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("value", value, LR.GetString("ExSeqNumbBadValue"));

                sequenceNumber = value;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a numeric value
        /// </summary>
        /// <value>Instead of throwing an exception, the property will convert non-numeric values to the default
        /// sequence number (0).</value>
        public override string Value
        {
            get
            {
                // Return null if not defined
                if(sequenceNumber == 0)
                    return null;

                return sequenceNumber.ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                sequenceNumber = 0;

                if(!String.IsNullOrWhiteSpace(value) && reNumber.IsMatch(value))
                {
                    sequenceNumber = Convert.ToInt32(value, CultureInfo.InvariantCulture);

                    if(sequenceNumber < 0)
                        sequenceNumber = 0;
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a numeric value
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
        /// Default constructor
        /// </summary>
        public SequenceProperty()
        {
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
            SequenceProperty o = new SequenceProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
