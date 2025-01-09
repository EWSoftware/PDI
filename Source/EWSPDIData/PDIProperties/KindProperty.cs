//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : KindProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/03/2025
// Note    : Copyright 2019-2025, Eric Woodruff, All rights reserved
//
// This file contains the kind property class used by the Personal Data Interchange (PDI) vCard class
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/20/2019  EFW  Created the code
//===============================================================================================================

// Ignore Spelling: org

using System;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the kind (KIND) property of a vCard.  This specifies the type of entity
    /// represented by the vCard (see <see cref="CardKind" />).
    /// </summary>
    public class KindProperty : BaseProperty
    {
        #region Private data members
        //=====================================================================

        private CardKind cardKind;
        private string? otherKind;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCard 4.0 only</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

        /// <summary>
        /// This read-only property defines the tag (KIND)
        /// </summary>
        public override string Tag => "KIND";

        /// <summary>
        /// This read-only property defines the default value type as TEXT
        /// </summary>
        public override string DefaultValueLocation => ValLocValue.Text;

        /// <summary>
        /// This property is used to set or get the vCard kind value
        /// </summary>
        /// <value>Setting this parameter to <c>Other</c> sets the <see cref="OtherKind"/> to <c>X-UNKNOWN</c>
        /// if not already set to something else.  It is set to null if set to any other calendar method.</value>
        public CardKind CardKind
        {
            get => cardKind;
            set
            {
                cardKind = value;

                if(cardKind != CardKind.Other)
                    otherKind = null;
                else
                {
                    if(String.IsNullOrWhiteSpace(otherKind))
                        otherKind = "X-UNKNOWN";
                }
            }
        }

        /// <summary>
        /// This property is used to set or get the card kind string when the type is set to <c>Other</c>
        /// </summary>
        /// <value>Setting this parameter automatically sets the <see cref="CardKind"/> property to
        /// <c>Other</c>.</value>
        public string? OtherKind
        {
            get => otherKind;
            set
            {
                cardKind = CardKind.Other;

                if(!String.IsNullOrWhiteSpace(value))
                    otherKind = value;
                else
                    otherKind = "X-UNKNOWN";
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a <see cref="CardKind"/>
        /// value.
        /// </summary>
        public override string? Value
        {
            get
            {
                string? kind;

                switch(cardKind)
                {
                    case CardKind.Other:
                        kind = otherKind;
                        break;

                    case CardKind.Organization:
                        kind = "org";
                        break;

                    default:
                        kind = cardKind.ToString().ToLowerInvariant();
                        break;
                }

                return kind;
            }
            set
            {
                string kind;

                if(value != null)
                {
                    kind = value.Trim().ToLowerInvariant();
                    otherKind = null;

                    switch(kind)
                    {
                        case "individual":
                            cardKind = CardKind.Individual;
                            break;

                        case "group":
                            cardKind = CardKind.Group;
                            break;

                        case "org":
                            cardKind = CardKind.Organization;
                            break;

                        case "location":
                            cardKind = CardKind.Location;
                            break;

                        default:
                            this.OtherKind= kind;
                            break;
                    }
                }
                else
                    this.CardKind = CardKind.None;
            }
        }

        /// <summary>
        /// This property is overridden to handle converting the text value to a <see cref="CalendarMethod"/>
        /// value.
        /// </summary>
        public override string? EncodedValue
        {
            get => this.Value;
            set => this.Value = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public KindProperty()
        {
            this.Version = SpecificationVersions.vCard40;
            this.CardKind = CardKind.None;
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
            KindProperty o = new();
            o.Clone(this);
            return o;
        }
        #endregion
    }
}
