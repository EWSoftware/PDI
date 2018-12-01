//===============================================================================================================
// System : EWSoftware.PDI ASP.NET Web Server Controls
// File   : DayInstance.js
// Author : Eric Woodruff  (Eric@EWoodruff.us)
// Updated: 11/22/2018
// Note   : Copyright 2004-2018, Eric Woodruff, All Rights Reserved
//
// This contains the client-side code for the EWSoftware DayInstance object
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//===============================================================================================================

// Ignore Spelling: nd

var EWSRP_dayList = new Array("SU", "MO", "TU", "WE", "TH", "FR", "SA");
var EWSRP_dayNameList = new Array("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");

// Constructor.  Pass it a day instance between -53 and +53 and a day of week value (0 = Sunday, 1 = Monday, etc)
function EWSRP_DayInstance(instance, dayOfWeek)
{
    // Instance data
    this.Instance = instance;
    this.DayOfWeek = dayOfWeek;

    // Function prototypes
    this.Equals = EWSRP_DayInstanceEquals;
    this.Description = EWSRP_DayInstanceDescription;
    this.DayName = EWSRP_DayInstanceDayName;
    this.toString = EWSRP_DayInstanceToString;
}

// See if another instance equals this one.  Returns true if it does, false if it doesn't
function EWSRP_DayInstanceEquals(otherInstance)
{
    return (this.Instance === otherInstance.Instance && this.DayOfWeek === otherInstance.DayOfWeek);
}

// Get a description of the day instance
function EWSRP_DayInstanceDescription()
{
    var digits, idx, suffix;

    if(this.Instance === 0)
        return "Any " + EWSRP_dayNameList[this.DayOfWeek];

    digits = (this.Instance % 100) * ((this.Instance < 0) ? -1 :  1);

    if((digits >= 10 && digits <= 19) || digits % 10 === 0)
        idx = 4;
    else
        idx = digits % 10;

    switch(idx)
    {
        case 1:
            suffix = "st";
            break;

        case 2:
            suffix = "nd";
            break;

        case 3:
            suffix = "rd";
            break;

        default:
            suffix = "th";
            break;
    }

    if(this.Instance < 0)
        return (this.Instance * -1).toString() + suffix + " " + EWSRP_dayNameList[this.DayOfWeek] + " from end";

    return this.Instance.toString() + suffix + " " + EWSRP_dayNameList[this.DayOfWeek];
}

// Get the day name for the current day of the week setting
function EWSRP_DayInstanceDayName()
{
    return EWSRP_dayNameList[this.DayOfWeek];
}

// Convert the day instance to a string
function EWSRP_DayInstanceToString()
{
    if(this.Instance === 0)
        return EWSRP_dayList[this.DayOfWeek];

    return this.Instance.toString() + EWSRP_dayList[this.DayOfWeek];
}
