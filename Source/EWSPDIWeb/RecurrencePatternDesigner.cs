//===============================================================================================================
// System  : EWSoftware.PDI ASP.NET Web Server Controls
// File    : RecurrencePatternDesigner.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/02/2025
// Note    : Copyright 2005-2025, Eric Woodruff, All rights reserved
//
// This file contains a designer for the recurrence pattern control
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 02/22/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.Globalization;
using System.IO;
using System.Web.UI;

using EWSoftware.PDI.Web.Controls;

namespace EWSoftware.PDI.Web.Design
{
    /// <summary>
    /// This provides design time support for the web server
    /// <see cref="RecurrencePattern"/> control.
    /// </summary>
    internal sealed class RecurrencePatternDesigner : System.Web.UI.Design.ControlDesigner
    {
        /// <summary>
        /// This returns the design time HTML for the <see cref="RecurrencePattern"/>
        /// control.
        /// </summary>
        /// <returns>The design time HTML</returns>
        public override string GetDesignTimeHtml()
        {
            RecurrencePattern rp = (RecurrencePattern)this.Component;
            bool isVisible = rp.Visible;

            try
            {
                using var tw = new StringWriter(CultureInfo.InvariantCulture);
                using var writer = new HtmlTextWriter(tw);
                
                if(!isVisible)
                    rp.Visible = true;

                writer.Write(rp.RenderAtDesignTime());
                return tw.ToString();
            }
            catch(Exception e)
            {
                return GetErrorDesignTimeHtml(e);
            }
            finally
            {
                rp.Visible = isVisible;
            }
        }

        /// <summary>
        /// Render a place holder describing an error that occurred while creating the design time HTML
        /// </summary>
        /// <param name="e">The exception that occurred</param>
        /// <returns>A string describing the error</returns>
        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            return CreatePlaceHolderDesignTimeHtml(String.Format(CultureInfo.InvariantCulture,
                "There was an error and the control cannot be displayed.<br>Exception: {0}<br>{1}", e.Message,
                e.StackTrace));
        }
    }
}
