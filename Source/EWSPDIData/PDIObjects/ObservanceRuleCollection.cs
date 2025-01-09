//===============================================================================================================
// System  : Personal Data Interchange Classes
// File    : ObservanceRuleCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2004-2025, Eric Woodruff, All rights reserved
//
// This file contains a collection class for ObservanceRule objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/20/2004  EFW  Created the code
// 03/21/2007  EFW  Converted to use a generic base class
//===============================================================================================================

using System.Collections.Generic;
using System.ComponentModel;

using EWSoftware.PDI.Binding;
using EWSoftware.PDI.Properties;

namespace EWSoftware.PDI.Objects
{
    /// <summary>
    /// A type-safe collection of <see cref="ObservanceRule"/> objects
    /// </summary>
    /// <remarks>The class has a type-safe enumerator.</remarks>
    public class ObservanceRuleCollection : ExtendedBindingList<ObservanceRule>
    {
        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public ObservanceRuleCollection()
        {
        }

        /// <summary>
        /// Construct the collection using a list of <see cref="ObservanceRule"/> objects
        /// </summary>
        /// <param name="rules">The <see cref="IList{T}"/> of rules to add</param>
        public ObservanceRuleCollection(IList<ObservanceRule> rules) : base(rules)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Add an <see cref="ObservanceRule"/> of the specified type to the collection
        /// </summary>
        /// <param name="ruleType">The type of observance rule to add</param>
        /// <returns>Returns the new rule that was created and added to the collection</returns>
        public ObservanceRule Add(ObservanceRuleType ruleType)
        {
            ObservanceRule rule = new(ruleType);
            this.Add(rule);

            return rule;
        }

        /// <summary>
        /// This is used to sort the collection in ascending or descending order by rule type and start date
        /// </summary>
        /// <param name="ascending">Pass true for ascending order, false for descending order</param>
        public void Sort(bool ascending)
        {
            ((List<ObservanceRule>)this.Items).Sort((x, y) =>
            {
                int result;

                // Flip result for descending order
                if(ascending)
                {
                    result = (int)x.RuleType - (int)y.RuleType;

                    if(result == 0)
                    {
                        result = (x.StartDateTime.DateTimeValue == y.StartDateTime.DateTimeValue) ? 0 :
                            (x.StartDateTime.DateTimeValue < y.StartDateTime.DateTimeValue) ? -1 : 1;
                    }
                }
                else
                {
                    result = (int)y.RuleType - (int)x.RuleType;

                    if(result == 0)
                    {
                        result = (y.StartDateTime.DateTimeValue == x.StartDateTime.DateTimeValue) ? 0 :
                            (y.StartDateTime.DateTimeValue < x.StartDateTime.DateTimeValue) ? -1 : 1;
                    }
                }

                return result;
            });

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// This is used to propagate a common version to all objects in the collection
        /// </summary>
        /// <param name="version">The version to use</param>
        public void PropagateVersion(SpecificationVersions version)
        {
            foreach(PDIObject o in this)
                o.Version = version;

            base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        #endregion
    }
}
