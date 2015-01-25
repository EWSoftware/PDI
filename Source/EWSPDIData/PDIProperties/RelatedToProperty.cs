//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : RelatedToProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/21/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Related To property.  It is used with the vCalendar and iCalendar Personal Data
// Interchange (PDI) classes.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/04/2004  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.Text;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the RelatedTo (RELATED-TO) property of an iCalendar component.  This is
    /// used to indicate a relationship between the object and another object identified by the unique ID in this
    /// property's value.
    /// </summary>
    /// <remarks>This class parses the <see cref="BaseProperty.Value"/> property to allow setting and getting the
    /// value as an enumerated <see cref="RelationshipType"/> value.</remarks>
    public class RelatedToProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private RelationshipType relType;
        private string otherType;
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
        /// This read-only property defines the tag (RELATED-TO)
        /// </summary>
        public override string Tag
        {
            get { return "RELATED-TO"; }
        }

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Text; }
        }

        /// <summary>
        /// This property is used to set or get the relationship type (RELTYPE) parameter
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects.  Setting this parameter to
        /// <c>Other</c> sets the <see cref="OtherRelationship"/> to <c>X-UNKNOWN</c> if not already set to
        /// something else.  It is set to null if set to any other calendar method.</value>
        public RelationshipType RelationshipType
        {
            get { return relType; }
            set
            {
                relType = value;

                if(relType != RelationshipType.Other)
                    otherType = null;
                else
                    if(String.IsNullOrWhiteSpace(otherType))
                        otherType = "X-UNKNOWN";
            }
        }

        /// <summary>
        /// This property is used to set or get the relationship type string when the relationship type is set to
        /// <c>Other</c>.
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects.  Setting this parameter
        /// automatically sets the <see cref="RelationshipType"/> property to <c>Other</c>.</value>
        public string OtherRelationship
        {
            get { return otherType; }
            set
            {
                relType = RelationshipType.Other;

                if(!String.IsNullOrWhiteSpace(value))
                    otherType = value;
                else
                    otherType = "X-UNKNOWN";
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor.  Unless the version is changed, the object will conform to the iCalendar 2.0
        /// specification.
        /// </summary>
        public RelatedToProperty()
        {
            this.Version = SpecificationVersions.iCalendar20;
            relType = RelationshipType.Parent;
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
            RelatedToProperty o = new RelatedToProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            RelatedToProperty rtp = (RelatedToProperty)p;

            relType = rtp.RelationshipType;

            if(relType == RelationshipType.Other)
                otherType = rtp.OtherRelationship;
            else
                otherType = null;

            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the RELTYPE parameter
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the relationship type if necessary
            if(this.Version == SpecificationVersions.iCalendar20 && relType != RelationshipType.Parent)
            {
                sb.Append(';');
                sb.Append(ParameterNames.RelationshipType);
                sb.Append('=');

                switch(relType)
                {
                    case RelationshipType.Parent:
                        sb.Append("PARENT");
                        break;

                    case RelationshipType.Child:
                        sb.Append("CHILD");
                        break;

                    case RelationshipType.Sibling:
                        sb.Append("SIBLING");
                        break;

                    default:
                        if(!String.IsNullOrWhiteSpace(otherType))
                            sb.Append(otherType);
                        else
                            sb.Append("X-UNKNOWN");
                        break;
                }
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the RELTYPE parameter
        /// </summary>
        /// <param name="parameters">The parameters for the property</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            string type;

            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
                if(String.Compare(parameters[paramIdx], "RELTYPE=", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Remove the parameter name
                    parameters.RemoveAt(paramIdx);

                    if(paramIdx < parameters.Count)
                    {
                        type = parameters[paramIdx].Trim().ToUpperInvariant();

                        switch(type)
                        {
                            case "PARENT":
                                this.RelationshipType = RelationshipType.Parent;
                                break;

                            case "CHILD":
                                this.RelationshipType = RelationshipType.Child;
                                break;

                            case "SIBLING":
                                this.RelationshipType = RelationshipType.Sibling;
                                break;

                            default:
                                this.OtherRelationship = parameters[paramIdx];
                                break;
                        }

                        // As above, remove the value
                        parameters.RemoveAt(paramIdx);
                    }
                    break;
                }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
