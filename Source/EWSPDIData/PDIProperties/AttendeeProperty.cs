//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : AttendeeProperty.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/24/2018
// Note    : Copyright 2004-2018, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the Attendee property used by the vCalendar and iCalendar classes
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
using System.Text;

using EWSoftware.PDI.Parser;

namespace EWSoftware.PDI.Properties
{
    /// <summary>
    /// This class is used to represent the Attendee (ATTENDEE) property of a vCalendar or iCalendar component.
    /// This property defines an attendee within a calendar component.
    /// </summary>
    /// <remarks><para>The <see cref="BaseProperty.Value"/> property contains the attendee information.  There
    /// are several additional parameters that may appear on the object.  These are parsed and can be accessed
    /// via the class properties.</para>
    /// 
    /// <para>The class is derived from <see cref="OrganizerProperty"/> which contains some common parameters.
    /// However, unlike the organizer property, the attendee property is used in vCalendar objects as well.  It's
    /// format differs significantly between the vCalendar 1.0 and iCalendar 2.0 specification.  It takes this
    /// into account when writing itself to a PDI data stream.  This property can also appear multiple times
    /// within a calendar object and has an associated collection class.</para></remarks>
    public class AttendeeProperty : OrganizerProperty
    {
        #region Private data members
        //=====================================================================

        private StringCollection delFrom, delTo, member;

        // This is used to map parameter name and value strings to a ParameterType enumeration
        private static NameToValue<ParameterType>[] ntv = {
            new NameToValue<ParameterType>(ParameterNames.Role, ParameterType.Role),
            new NameToValue<ParameterType>(ParameterNames.Rsvp, ParameterType.Rsvp),
            new NameToValue<ParameterType>(ParameterNames.Expect, ParameterType.Expect),
            new NameToValue<ParameterType>(ParameterNames.CalendarUserType, ParameterType.CalendarUserType),
            new NameToValue<ParameterType>(ParameterNames.DelegatedFrom, ParameterType.DelegatedFrom),
            new NameToValue<ParameterType>(ParameterNames.DelegatedTo, ParameterType.DelegatedTo),
            new NameToValue<ParameterType>(ParameterNames.Member, ParameterType.Member),
            new NameToValue<ParameterType>(ParameterNames.Status, ParameterType.Status),
            new NameToValue<ParameterType>(ParameterNames.PartStatus, ParameterType.Status),
            new NameToValue<ParameterType>("ATTENDEE", ParameterType.Role, true),
            new NameToValue<ParameterType>("ORGANIZER", ParameterType.Role, true),
            new NameToValue<ParameterType>("OWNER", ParameterType.Role, true),
            new NameToValue<ParameterType>("DELEGATE", ParameterType.Role, true),
            new NameToValue<ParameterType>("ACCEPTED", ParameterType.Status, true),
            new NameToValue<ParameterType>("NEEDS ACTION", ParameterType.Status, true),
            new NameToValue<ParameterType>("NEEDS-ACTION", ParameterType.Status, true),
            new NameToValue<ParameterType>("NEEDSACTION", ParameterType.Status, true),
            new NameToValue<ParameterType>("SENT", ParameterType.Status, true),
            new NameToValue<ParameterType>("TENTATIVE", ParameterType.Status, true),
            new NameToValue<ParameterType>("CONFIRMED", ParameterType.Status, true),
            new NameToValue<ParameterType>("DECLINED", ParameterType.Status, true),
            new NameToValue<ParameterType>("COMPLETED", ParameterType.Status, true),
            new NameToValue<ParameterType>("DELEGATED", ParameterType.Status, true),
            new NameToValue<ParameterType>("YES", ParameterType.Rsvp, true),
            new NameToValue<ParameterType>("NO", ParameterType.Rsvp, true),
            new NameToValue<ParameterType>("FYI", ParameterType.Expect, true),
            new NameToValue<ParameterType>("REQUIRE", ParameterType.Expect, true),
            new NameToValue<ParameterType>("REQUEST", ParameterType.Expect, true),
            new NameToValue<ParameterType>("IMMEDIATE", ParameterType.Expect, true)
        };
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>Supports vCalendar 1.0 and iCalendar 2.0</value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCalendar10 |
            SpecificationVersions.iCalendar20;

        /// <summary>
        /// This read-only property defines the tag (ATTENDEE)
        /// </summary>
        public override string Tag => "ATTENDEE";

        /// <summary>
        /// This property is used to set or get the calendar user type (CUTYPE) parameter for the calendar user
        /// specified by the property value.
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects.  It is used to define the type of
        /// calendar user specified by the property such as INDIVIDUAL, GROUP, RESOURCE, etc.</value>
        public string CalendarUserType { get; set; }

        /// <summary>
        /// This property is used to set or get the calendar user expectation (EXPECT) parameter for the calendar
        /// user specified by the property value.
        /// </summary>
        /// <value>This parameter is only applicable to vCalendar 1.0 objects</value>
        public string Expectation { get; set; }

        /// <summary>
        /// This property is used to set or get the "delegated from" (DELEGATED-FROM) parameters for the calendar
        /// user specified by the property value.  There can be more than one delegate.
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects.  If the collection is empty, there
        /// are no delegates.</value>
        public StringCollection DelegatedFrom
        {
            get
            {
                if(delFrom == null)
                    delFrom = new StringCollection();

                return delFrom;
            }
        }

        /// <summary>
        /// This property is used to set or get the "delegated to" (DELEGATED-TO) parameters for the calendar
        /// user specified by the property value.  There can be more than one delegate.
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects.  If the collection is empty, there
        /// are no delegates.</value>
        public StringCollection DelegatedTo
        {
            get
            {
                if(delTo == null)
                    delTo = new StringCollection();

                return delTo;
            }
        }

        /// <summary>
        /// This property is used to set or get the group or list membership (MEMBER) parameters for the calendar
        /// user specified by the property value.  There can be more than one.
        /// </summary>
        /// <value>This parameter is only applicable to iCalendar 2.0 objects.  If the collection is empty, there
        /// are no members.</value>
        public StringCollection Membership
        {
            get
            {
                if(member == null)
                    member = new StringCollection();

                return member;
            }
        }

        /// <summary>
        /// This property is used to set or get the role (ROLE) parameter for the calendar user specified by the
        /// property value.
        /// </summary>
        /// <value>This parameter is applicable to vCalendar and iCalendar objects</value>
        public string Role { get; set; }

        /// <summary>
        /// This property is used to set or get the participation status parameter (STATUS for vCalendar,
        /// PART-STATUS for iCalendar) for the calendar user specified by the property value.
        /// </summary>
        /// <value>This parameter is applicable to vCalendar and iCalendar objects</value>
        public string ParticipationStatus { get; set; }

        /// <summary>
        /// This property is used to set or get the RSVP (RSVP) parameter for the calendar user specified by the
        /// property value.
        /// </summary>
        /// <value>This parameter is applicable to vCalendar and iCalendar objects</value>
        public bool Rsvp { get; set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AttendeeProperty()
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
            AttendeeProperty o = new AttendeeProperty();
            o.Clone(this);
            return o;
        }

        /// <summary>
        /// This is overridden to allow copying of the additional properties
        /// </summary>
        /// <param name="p">The PDI object from which the settings are to be copied</param>
        protected override void Clone(PDIObject p)
        {
            AttendeeProperty a = (AttendeeProperty)p;

            this.CalendarUserType = a.CalendarUserType;
            this.Expectation = a.Expectation;
            this.Role = a.Role;
            this.ParticipationStatus = a.ParticipationStatus;
            this.Rsvp = a.Rsvp;

            this.DelegatedFrom.Clear();
            this.DelegatedTo.Clear();
            this.Membership.Clear();

            if(a.DelegatedFrom.Count != 0)
                this.DelegatedFrom.AddRange(a.DelegatedFrom);

            if(a.DelegatedTo.Count != 0)
                this.DelegatedTo.AddRange(a.DelegatedTo);

            if(a.Membership.Count != 0)
                this.Membership.AddRange(a.Membership);

            base.Clone(p);
        }

        /// <summary>
        /// This is overridden to provide custom handling of the extra parameters
        /// </summary>
        /// <param name="sb">The StringBuilder to which the parameters are appended</param>
        public override void SerializeParameters(StringBuilder sb)
        {
            base.SerializeParameters(sb);

            // Serialize the extra parameters if necessary
            if(this.Version == SpecificationVersions.iCalendar20)
            {
                if(this.CalendarUserType != null && this.CalendarUserType.Length != 0)
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.CalendarUserType);
                    sb.Append('=');
                    sb.Append(this.CalendarUserType);
                }

                // Delegated From values are always enclosed in quotes
                if(delFrom != null && delFrom.Count != 0)
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.DelegatedFrom);
                    sb.Append('=');

                    for(int nIdx = 0; nIdx < delFrom.Count; nIdx++)
                    {
                        if(nIdx != 0)
                            sb.Append(',');

                        sb.Append('\"');
                        sb.Append(delFrom);
                        sb.Append('\"');
                    }
                }

                // Delegated To values are always enclosed in quotes
                if(delTo != null && delTo.Count != 0)
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.DelegatedTo);
                    sb.Append('=');

                    for(int nIdx = 0; nIdx < delTo.Count; nIdx++)
                    {
                        if(nIdx != 0)
                            sb.Append(',');

                        sb.Append('\"');
                        sb.Append(delTo);
                        sb.Append('\"');
                    }
                }

                // Membership values are always enclosed in quotes
                if(member != null && member.Count != 0)
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.Member);
                    sb.Append('=');

                    for(int nIdx = 0; nIdx < member.Count; nIdx++)
                    {
                        if(nIdx != 0)
                            sb.Append(',');

                        sb.Append('\"');
                        sb.Append(member);
                        sb.Append('\"');
                    }
                }

                if(this.ParticipationStatus != null && this.ParticipationStatus.Length != 0)
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.PartStatus);
                    sb.Append('=');
                    sb.Append(this.ParticipationStatus);
                }

                if(this.Rsvp)
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.Rsvp);
                    sb.Append("=TRUE");
                }
            }
            else
            {
                if(this.Expectation != null && this.Expectation.Length != 0)
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.Expect);
                    sb.Append('=');
                    sb.Append(this.Expectation);
                }

                if(this.ParticipationStatus != null && this.ParticipationStatus.Length != 0)
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.Status);
                    sb.Append('=');
                    sb.Append(this.ParticipationStatus);
                }

                if(this.Rsvp)
                {
                    sb.Append(';');
                    sb.Append(ParameterNames.Rsvp);
                    sb.Append("=YES");
                }
            }

            if(this.Role != null && this.Role.Length != 0)
            {
                sb.Append(';');
                sb.Append(ParameterNames.Role);
                sb.Append('=');
                sb.Append(this.Role);
            }
        }

        /// <summary>
        /// This is overridden to provide custom handling of the extra parameters
        /// </summary>
        /// <param name="parameters">The parameters for the property.</param>
        public override void DeserializeParameters(StringCollection parameters)
        {
            string[] delVals, memberVals;
            string tempVal;
            int idx;

            if(parameters == null || parameters.Count == 0)
                return;

            for(int paramIdx = 0; paramIdx < parameters.Count; paramIdx++)
            {
                for(idx = 0; idx < ntv.Length; idx++)
                    if(ntv[idx].IsMatch(parameters[paramIdx]))
                        break;

                if(idx == ntv.Length)
                {
                    // If it was a parameter name, skip the value too
                    if(parameters[paramIdx].EndsWith("=", StringComparison.Ordinal))
                        paramIdx++;

                    continue;   // Not an attendee parameter
                }

                // Parameters may appear as a pair (name followed by value) or by value alone
                switch(ntv[idx].EnumValue)
                {
                    case ParameterType.Role:
                        if(!ntv[idx].IsParameterValue)
                            parameters.RemoveAt(paramIdx);

                        if(paramIdx < parameters.Count)
                            this.Role = parameters[paramIdx];
                        break;

                    case ParameterType.Rsvp:
                        if(!ntv[idx].IsParameterValue)
                            parameters.RemoveAt(paramIdx);

                        if(paramIdx < parameters.Count)
                        {
                            tempVal = parameters[paramIdx].Trim();

                            this.Rsvp = (String.Compare(tempVal, "YES", StringComparison.OrdinalIgnoreCase) == 0 ||
                              String.Compare(tempVal, "TRUE", StringComparison.OrdinalIgnoreCase) == 0);
                        }
                        break;

                    case ParameterType.Expect:
                        if(!ntv[idx].IsParameterValue)
                            parameters.RemoveAt(paramIdx);

                        if(paramIdx < parameters.Count)
                            this.Expectation = parameters[paramIdx];
                        break;

                    case ParameterType.CalendarUserType:
                        if(!ntv[idx].IsParameterValue)
                            parameters.RemoveAt(paramIdx);

                        if(paramIdx < parameters.Count)
                            this.CalendarUserType = parameters[paramIdx];
                        break;

                    case ParameterType.DelegatedFrom:
                        if(!ntv[idx].IsParameterValue)
                            parameters.RemoveAt(paramIdx);

                        if(paramIdx < parameters.Count)
                        {
                            delVals = parameters[paramIdx].Split(',');

                            delFrom.Clear();

                            foreach(string s in delVals)
                            {
                                tempVal = s.Trim();

                                if(tempVal.Length > 0)
                                    delFrom.Add(tempVal);
                            }
                        }
                        break;

                    case ParameterType.DelegatedTo:
                        if(!ntv[idx].IsParameterValue)
                            parameters.RemoveAt(paramIdx);

                        if(paramIdx < parameters.Count)
                        {
                            delVals = parameters[paramIdx].Split(',');

                            delTo.Clear();

                            foreach(string s in delVals)
                            {
                                tempVal = s.Trim();

                                if(tempVal.Length > 0)
                                    delTo.Add(tempVal);
                            }
                        }
                        break;

                    case ParameterType.Member:
                        if(!ntv[idx].IsParameterValue)
                            parameters.RemoveAt(paramIdx);

                        if(paramIdx < parameters.Count)
                        {
                            memberVals = parameters[paramIdx].Split(',');

                            member.Clear();

                            foreach(string s in memberVals)
                            {
                                tempVal = s.Trim();

                                if(tempVal.Length > 0)
                                    member.Add(tempVal);
                            }
                        }
                        break;

                    case ParameterType.Status:
                        if(!ntv[idx].IsParameterValue)
                            parameters.RemoveAt(paramIdx);

                        if(paramIdx < parameters.Count)
                            this.ParticipationStatus = parameters[paramIdx];
                        break;
                }

                parameters.RemoveAt(paramIdx);
                paramIdx--;
            }

            // Let the base class handle all other parameters
            base.DeserializeParameters(parameters);
        }
        #endregion
    }
}
