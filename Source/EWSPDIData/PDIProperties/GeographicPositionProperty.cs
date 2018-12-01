//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : GeographicPositionProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains Geographic Position property class used by the Personal Data Interchange (PDI) classes
// such as vCalendar, iCalendar, and vCard.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the geographic position (GEO) property of a vCard or vCalendar object
    /// </summary>
    /// <remarks><para>This property class parses the <see cref="BaseProperty.Value"/> property to allow access
    /// to its individual latitude and longitude parts as numeric values.</para>
    /// 
    /// <para>For the vCard 2.1 and vCalendar 1.0 specification, the values are separated by a comma.  For the
    /// vCard 3.0 and iCalendar 2.0 specification, they are separated by a semi-colon.</para></remarks>
    public class GeographicPositionProperty : BaseProperty
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports all specifications except IrMC 1.1</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.Any;

        /// <summary>
        /// This read-only property defines the tag (GEO)
        /// </summary>
        public override string Tag => "GEO";

        /// <summary>
        /// This read-only property defines the default value type as FLOAT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Float;

        /// <summary>
        /// This is used to get or set the latitude as a floating point value
        /// </summary>
        /// <value>Positive values indicate positions north of the equator.  Negative values indicate positions
        /// south of the equator.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// This is used to get or set the longitude as a floating point value
        /// </summary>
        /// <value>Positive values indicate positions east of the prime meridian.  Negative values indicate
        /// positions west of the prime meridian.</value>
        public double Longitude { get; set; }

        /// <summary>
        /// This property is overridden to handle parsing the component parts to/from their string form
        /// </summary>
        public override string Value
        {
            get
            {
                // If both are equal to zero, nothing will be saved
                if(this.Latitude == 0.0 && this.Longitude == 0.0)
                    return null;

                // vCard 2.1 and vCalendar 1.0 separates them with a comma.  vCard 3.0 and iCalendar 2.0
                // separates them with a semi-colon.
                return String.Format(CultureInfo.InvariantCulture, "{0:F6}{1}{2:F6}", this.Latitude,
                    ((this.Version == SpecificationVersions.vCard30 ||
                    this.Version == SpecificationVersions.iCalendar20) ? ";" : ","), this.Longitude);
            }
            set
            {
                this.Latitude = this.Longitude = 0.0;

                if(!String.IsNullOrWhiteSpace(value))
                {
                    string[] parts = value.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    if(parts.Length == 2)
                    {
                        this.Latitude = Convert.ToDouble(parts[0], CultureInfo.InvariantCulture);
                        this.Longitude = Convert.ToDouble(parts[1], CultureInfo.InvariantCulture);
                    }
                }
            }
        }

        /// <summary>
        /// This property is overridden to handle parsing the component parts to/from their string form
        /// </summary>
        public override string EncodedValue
        {
            get => this.Value;
            set => this.Value = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the vCard 3.0/iCalendar 2.0
        /// specification.
        /// </summary>
        public GeographicPositionProperty()
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
            GeographicPositionProperty o = new GeographicPositionProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// The specifications do not allow parameters for this property.  Any parameters are ignored.
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
        }

        /// <summary>
        /// The specifications do not allow parameters for this property.  Any parameters are ignored.
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
        }
        #endregion
    }
}
