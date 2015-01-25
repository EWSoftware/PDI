//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AgentProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/18/2014
// Note    : Copyright 2004-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Agent property.  It is used with the Personal Data Interchange (PDI) classes such as
// vCalendar, iCalendar, and vCard.
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

using EWSoftware.PDI.Objects;
using EWSoftware.PDI.Parser;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Agent (AGENT) property of a vCard.  This specifies supplemental
    /// information or a comment that is associated with the vCard.
    /// </summary>
    /// <remarks>With the use of property grouping, the association can be limited to a group of properties.  It
    /// has no special requirements or handling.  The <see cref="BaseProperty.Value"/> property contains the
    /// agent information in string form.  The <see cref="VCard"/> property can be used to access it as a vCard
    /// object if it is inline.</remarks>
    public class AgentProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private VCard agent;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 2.1 and vCard 3.0</value>
        public override SpecificationVersions VersionsSupported
        {
            get { return SpecificationVersions.vCard21 | SpecificationVersions.vCard30; }
        }

        /// <summary>
        /// This read-only property defines the tag (AGENT)
        /// </summary>
        public override string Tag
        {
            get { return "AGENT"; }
        }

        /// <summary>
        /// This read-only property defines the default value location as INLINE
        /// </summary>
        public override string DefaultValueLocation
        {
            get { return ValLocValue.Inline; }
        }

        /// <summary>
        /// This is used to set or get the vCard represented by the agent property
        /// </summary>
        /// <value>If the location of the property is inline, it is assumed to be the text that represents a
        /// vCard.  This property returns it as a parsed vCard object.  It can also be used to set or modify the
        /// vCard object.  Null is returned if no agent is defined or the value is not inline.  If set to a vCard
        /// object, the location is forced to be inline.</value>
        public VCard VCard
        {
            get { return agent; }
            set
            {
                agent = value;
                this.ValueLocation = ValLocValue.Inline;
            }
        }

        /// <summary>
        /// This is overridden to handle parsing of the vCard value
        /// </summary>
        /// <value>If inline, the value is stored as a vCard object.  If not inline, it is stored as a text
        /// string.</value>
        /// <exception cref="PDIParserException">This is thrown if the vCard data is not valid</exception>
        public override string Value
        {
            get
            {
                if(this.ValueLocation != ValLocValue.Inline)
                    return base.Value;

                if(agent == null)
                    return null;

                return agent.ToString();
            }
            set
            {
                // Store it as text if not inline
                if(this.ValueLocation != ValLocValue.Inline)
                    base.Value = value;
                else
                    agent = VCardParser.ParseFromString(value);
            }
        }

        /// <summary>
        /// This is overridden to handle parsing of the vCard value in its encoded form
        /// </summary>
        /// <value>If inline, the value is stored as a vCard object.  If not inline, it is stored as a text
        /// string.</value>
        /// <exception cref="PDIParserException">This is thrown if the vCard data is not valid</exception>
        public override string EncodedValue
        {
            get
            {
                if(this.ValueLocation != ValLocValue.Inline)
                    return base.EncodedValue;

                if(agent == null)
                    return null;

                return this.Encode(agent.ToString());
            }
            set
            {
                // Store it as text if not inline
                if(this.ValueLocation != ValLocValue.Inline)
                    base.EncodedValue = value;
                else
                    agent = VCardParser.ParseFromString(this.Decode(value));
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AgentProperty()
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
            AgentProperty o = new AgentProperty();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
