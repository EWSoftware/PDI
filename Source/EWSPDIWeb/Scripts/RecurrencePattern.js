//===============================================================================================================
// System : EWSoftware.PDI ASP.NET Web Server Controls
// File   : RecurrencePattern.js
// Author : Eric Woodruff  (Eric@EWoodruff.us)
// Updated: 12/31/2014
// Note   : Copyright 2004-2014, Eric Woodruff, All Rights Reserved
//
// This contains the client-side code for the EWSoftware Recurrence Pattern web control
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/PDI.
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//===============================================================================================================

// Constructor.  Pass it the control ID to which the object is related.
function EWSRP_RecurrencePattern(controlId)
{
    // Instance data
    this.ControlId = controlId;

    // 1 = Yearly, 2 = Monthly, 3 = Weekly, 4 = Daily, 5 = Hourly, 6 = Minutely, 7 = Secondly
    this.CurrentPattern = 1;

    // 0 = Never ends, 1 = End after N occurrences, 2 = End by specific date
    this.CurrentRange = 0;

    this.DayList = new Array();     // Advanced pattern By Day instance lists
    this.DayInstanceList = new Array();

    // Function prototypes
    this.GetControl = EWSRP_GetControl;
    this.LoadByDayValues = EWSRP_LoadByDayValues;

    this.SetInterval = EWSRP_SetInterval;
    this.SetDailyInterval = EWSRP_SetDailyInterval;
    this.SetWeeklyInterval = EWSRP_SetWeeklyInterval;
    this.SetMonthlyInterval = EWSRP_SetMonthlyInterval;
    this.SetYearlyInterval = EWSRP_SetYearlyInterval;
    this.SetAdvancedInterval = EWSRP_SetAdvancedInterval;
    this.SetRangeOfRecurrence = EWSRP_SetRangeOfRecurrence;
    this.ShowWeekStartDay = EWSRP_ShowWeekStartDay;
    this.ShowCanOccurOnHoliday = EWSRP_ShowCanOccurOnHoliday;
    this.ShowAdvanced = EWSRP_ShowAdvanced;
    this.ShowPatternOptions = EWSRP_ShowPatternOptions;

    this.SelectPattern = EWSRP_SelectPattern;
    this.SetAdvancedState = EWSRP_SetAdvancedState;
    this.SetRangeCtlState = EWSRP_SetRangeCtlState;
    this.SetDailyCtlState = EWSRP_SetDailyCtlState;
    this.SetMonthlyCtlState = EWSRP_SetMonthlyCtlState;
    this.SetYearlyCtlState = EWSRP_SetYearlyCtlState;
    this.SetDayInstanceState = EWSRP_SetDayInstanceState;

    this.AddDayInstance = EWSRP_AddDayInstance;
    this.RemoveDayInstance = EWSRP_RemoveDayInstance;
    this.ClearDayInstances = EWSRP_ClearDayInstances;

    this.ValidateRecurrence = EWSRP_ValidateRecurrence;
    this.ValidateNumeric = EWSRP_ValidateNumeric;
    this.ValidateEndDate = EWSRP_ValidateEndDate;

    // Initialize the controls to a default state
    this.SelectPattern(1);
    this.SetDailyCtlState(true);
    this.SetMonthlyCtlState(true);
    this.SetYearlyCtlState(true);

    this.GetControl("RP_WP_" + (new Date()).getDay().toString()).checked = true;

    this.GetControl("RP_SP_INT").value = this.GetControl("RP_MINP_INT").value =
        this.GetControl("RP_HP_INT").value = this.GetControl("RP_DP_INT").value =
        this.GetControl("RP_WP_INT").value = this.GetControl("RP_MP_DAY").value =
        this.GetControl("RP_MP_INT").value = this.GetControl("RP_MP_NTHINT").value =
        this.GetControl("RP_YP_DAY").value = this.GetControl("RP_YP_INT").value =
        this.GetControl("RP_YP_NTHINT").value = this.GetControl("RP_AP_INT").value =
        this.GetControl("RP_AP_DAY").value = "1";

    this.SetRangeOfRecurrence(0, false, 0, 10, "");
}

// Get a sub-control reference for the specified recurrence control
function EWSRP_GetControl(subId)
{
    return document.getElementById(this.ControlId + "_" + subId);
}

// This is used to load the By Day list box with values from the ByDay arrays
function EWSRP_LoadByDayValues()
{
    var instList, selIdx, idx, opt;

    instList = this.GetControl("RP_AP_INSTLST");
    selIdx = instList.selectedIndex;

    while(instList.options.length != 0)
        instList.remove(0);

    // Certain configurations only uses the day, not the instance
    if(this.GetControl("RP_AP_DAY").disabled == "")
        for(idx = 0; idx < this.DayInstanceList.length; idx++)
        {
            opt = document.createElement("OPTION");
            opt.text = this.DayInstanceList[idx].Description();

            try
            {
                instList.add(opt, null);    // Standards compliant
            }
            catch(ex)
            {
                instList.add(opt);  // IE only
            }
        }
    else
        for(idx = 0; idx < this.DayList.length; idx++)
        {
            opt = document.createElement("OPTION");
            opt.text = this.DayList[idx].DayName();

            try
            {
                instList.add(opt, null);    // Standards compliant
            }
            catch(ex)
            {
                instList.add(opt);  // IE only
            }
        }

    if(instList.options.length > 0)
    {
        this.GetControl("RP_AP_DELINST").disabled = this.GetControl("RP_AP_CLRINST").disabled = "";
        instList.selectedIndex = (selIdx < instList.options.length) ? selIdx : instList.options.length - 1;
    }
    else
        this.GetControl("RP_AP_DELINST").disabled = this.GetControl("RP_AP_CLRINST").disabled = "disabled";
}

// Set the interval for the secondly through hourly patterns
function EWSRP_SetInterval(pattern, interval)
{
    this.SelectPattern(pattern);

    if(interval < 1)
        interval = 1;
    else
        if(interval > 999)
            interval = 999;

    switch(pattern)
    {
        case 5:
            this.GetControl("RP_HP_INT").value = interval.toString();
            break;

        case 6:
            this.GetControl("RP_MINP_INT").value = interval.toString();
            break;

        case 7:
            this.GetControl("RP_SP_INT").value = interval.toString();
            break;
    }
}

// Set the daily interval
function EWSRP_SetDailyInterval(interval, everyWeekday)
{
    this.SelectPattern(4);

    if(interval < 1)
        interval = 1;
    else
        if(interval > 999)
            interval = 999;

    this.GetControl("RP_DP_INT").value = interval.toString();
    this.SetDailyCtlState(!everyWeekday);
}

// Set the weekly interval
function EWSRP_SetWeeklyInterval(interval, dayList)
{
    var idx;

    this.SelectPattern(3);

    if(interval < 1)
        interval = 1;
    else
        if(interval > 999)
            interval = 999;

    this.GetControl("RP_WP_INT").value = interval.toString();

    for(idx = 0; idx < 7; idx++)
        this.GetControl("RP_WP_" + idx.toString()).checked = false;

    for(idx = 0; idx < dayList.length; idx++)
        this.GetControl("RP_WP_" + dayList[idx].DayOfWeek.toString()).checked = true;
}

// Set the monthly interval
function EWSRP_SetMonthlyInterval(dayOfMonth, interval, dayList, occurrence)
{
    var idx, dow, daysUsed = 0;

    this.SelectPattern(2);

    if(dayOfMonth < 1)
        dayOfMonth = 1;
    else
        if(dayOfMonth > 31)
            dayOfMonth = 31;

    if(interval < 1)
        interval = 1;
    else
        if(interval > 999)
            interval = 999;

    this.GetControl("RP_MP_DAY").value = dayOfMonth.toString();
    this.GetControl("RP_MP_INT").value = interval.toString();
    this.GetControl("RP_MP_NTHINT").value = interval.toString();

    if(dayList.length == 0)
        this.SetMonthlyCtlState(true);
    else
    {
        this.SetMonthlyCtlState(false);

        // If there's only one entry, use it.  Otherwise, use the first one plus the indicated occurrence.
        if(dayList.length == 1)
        {
            if(dayList[0].Instance < 1 || dayList[0].Instance > 4)
                this.GetControl("RP_MP_INST").selectedIndex = 4;
            else
                this.GetControl("RP_MP_INST").selectedIndex = dayList[0].Instance - 1;

            this.GetControl("RP_MP_DOW").selectedIndex = dayList[0].DayOfWeek;
        }
        else
        {
            if(occurrence < 1 || occurrence > 5)
                this.GetControl("RP_MP_INST").selectedIndex = 4;
            else
                this.GetControl("RP_MP_INST").selectedIndex = occurrence - 1;

            // Figure out which day or combination of days to use
            for(idx = 0; idx < dayList.length; idx++)
                switch(dayList[idx].DayOfWeek)
                {
                    case 0:
                        daysUsed |= 1;
                        break;

                    case 1:
                        daysUsed |= 2;
                        break;

                    case 2:
                        daysUsed |= 4;
                        break;

                    case 3:
                        daysUsed |= 8;
                        break;

                    case 4:
                        daysUsed |= 16;
                        break;

                    case 5:
                        daysUsed |= 32;
                        break;

                    case 6:
                        daysUsed |= 64;
                        break;
                }

            // If it isn't a combination we can use, it'll use the first day of the week from the list
            if(daysUsed == 127)
                dow = 9;    // All days
            else
                if(daysUsed == 65)
                    dow = 8;    // Weekend day
                else
                    if(daysUsed == 62)
                        dow = 7;    // Weekday
                    else
                        dow = dayList[0].DayOfWeek;

            this.GetControl("RP_MP_DOW").selectedIndex = dow;
        }
    }
}

// Set the yearly interval
function EWSRP_SetYearlyInterval(month, dayOfMonth, interval, dayList, occurrence)
{
    var idx, dow, daysUsed = 0;

    this.SelectPattern(1);

    if(month < 1)
        month = 1;
    else
        if(month > 12)
            month = 12;

    if(dayOfMonth < 1)
        dayOfMonth = 1;
    else
        if(dayOfMonth > 31)
            dayOfMonth = 31;

    if(interval < 1)
        interval = 1;
    else
        if(interval > 999)
            interval = 999;

    this.GetControl("RP_YP_MON").selectedIndex = this.GetControl("RP_YP_INMON").selectedIndex = month - 1;
    this.GetControl("RP_YP_DAY").value = dayOfMonth.toString();
    this.GetControl("RP_YP_INT").value = interval.toString();
    this.GetControl("RP_YP_NTHINT").value = interval.toString();

    if(dayList.length == 0)
        this.SetYearlyCtlState(true);
    else
    {
        this.SetYearlyCtlState(false);

        // If there's only one entry, use it.  Otherwise, use the first one plus the indicated occurrence.
        if(dayList.length == 1)
        {
            if(dayList[0].Instance < 1 || dayList[0].Instance > 4)
                this.GetControl("RP_YP_INST").selectedIndex = 4;
            else
                this.GetControl("RP_YP_INST").selectedIndex =
                    dayList[0].Instance - 1;

            this.GetControl("RP_YP_DOW").selectedIndex = dayList[0].DayOfWeek;
        }
        else
        {
            if(occurrence < 1 || occurrence > 5)
                this.GetControl("RP_YP_INST").selectedIndex = 4;
            else
                this.GetControl("RP_YP_INST").selectedIndex = occurrence - 1;

            // Figure out which day or combination of days to use
            for(idx = 0; idx < dayList.length; idx++)
                switch(dayList[idx].DayOfWeek)
                {
                    case 0:
                        daysUsed |= 1;
                        break;

                    case 1:
                        daysUsed |= 2;
                        break;

                    case 2:
                        daysUsed |= 4;
                        break;

                    case 3:
                        daysUsed |= 8;
                        break;

                    case 4:
                        daysUsed |= 16;
                        break;

                    case 5:
                        daysUsed |= 32;
                        break;

                    case 6:
                        daysUsed |= 64;
                        break;
                }

            // If it isn't a combination we can use, it'll use the first day of the week from the list
            if(daysUsed == 127)
                dow = 9;    // All days
            else
                if(daysUsed == 65)
                    dow = 8;    // Weekend day
                else
                    if(daysUsed == 62)
                        dow = 7;    // Weekday
                    else
                        dow = dayList[0].DayOfWeek;

            this.GetControl("RP_YP_DOW").selectedIndex = dow;
        }
    }
}

// Set the advanced pattern interval and options
function EWSRP_SetAdvancedInterval(pattern, interval, dayList, months, byWeekNo, byYearDay, byMonthDay, byHour,
  byMinute, bySecond, bySetPos)
{
    var advChk, opts, idx;

    this.GetControl("RP_AP_BYWKNO").value = this.GetControl("RP_AP_BYYRDAY").value =
        this.GetControl("RP_AP_BYMODAY").value = this.GetControl("RP_AP_BYHOUR").value =
        this.GetControl("RP_AP_BYMIN").value = this.GetControl("RP_AP_BYSEC").value =
        this.GetControl("RP_AP_BYPOS").value = "";

    opts = this.GetControl("RP_AP_MOS").options;

    for(idx = 0; idx < opts.length; idx++)
        opts[idx].selected = false;

    this.SelectPattern(pattern);

    advChk = this.GetControl("RP_CHK_ADV");

    if(!advChk.checked)
    {
        advChk.checked = true;
        this.SetAdvancedState();

        // Clear the months again as it tends to select one when the checked state changes
        for(idx = 0; idx < opts.length; idx++)
            opts[idx].selected = false;
    }

    this.GetControl("RP_AP_INT").value = interval.toString();

    if(typeof(dayList) != "undefined")
    {
        if(typeof(months) != "undefined" && months.length != 0)
            for(idx = 0; idx < months.length; idx++)
                opts[months[idx] - 1].selected = true;

        if(typeof(byWeekNo) != "undefined")
        {
            this.GetControl("RP_AP_BYWKNO").value = byWeekNo;
            this.GetControl("RP_AP_BYYRDAY").value = byYearDay;
            this.GetControl("RP_AP_BYMODAY").value = byMonthDay;
            this.GetControl("RP_AP_BYHOUR").value = byHour;
            this.GetControl("RP_AP_BYMIN").value = byMinute;
            this.GetControl("RP_AP_BYSEC").value = bySecond;
            this.GetControl("RP_AP_BYPOS").value = bySetPos;
        }

        // Set the Day Instance control state which depends on the above settings
        this.SetDayInstanceState();

        if(this.GetControl("RP_AP_DAY").disabled == "")
            this.DayInstanceList = dayList;
        else
            this.DayList = dayList;

        this.LoadByDayValues();
    }
}

// Set the range of recurrence options
function EWSRP_SetRangeOfRecurrence(weekStart, canOccurOnHoliday, range, endOcc, endDate)
{
	if(endOcc < 1)
		endOcc = 1;
	else
		if(endOcc > 999)
			endOcc = 999;

    this.GetControl("RP_RNG_WSD").selectedIndex = weekStart;
    this.GetControl("RP_RNG_COH").checked = canOccurOnHoliday;
    this.GetControl("RP_RNG_ENDOCC").value = endOcc;
    this.GetControl("RP_RNG_ENDDT").value = endDate;
    this.SetRangeCtlState(range);
}

// Show or hide the "Week start day" option
function EWSRP_ShowWeekStartDay(show)
{
    this.GetControl("RP_SPN_WSD").style.display = show ? "inline" : "none";
}

// Show or hide the "Can occur on holidays" option
function EWSRP_ShowCanOccurOnHoliday(show)
{
    this.GetControl("RP_SPN_COH").style.display = show ? "inline" : "none";
}

// Show or hide the "Advanced" option
function EWSRP_ShowAdvanced(show)
{
    this.GetControl("RP_SPN_ADV").style.display = show ? "inline" : "none";
}

// Show or hide the pattern options based on the maximum available pattern
function EWSRP_ShowPatternOptions(maxPattern)
{
    this.GetControl("RP_RB_SEC").style.display = (maxPattern > 6) ? "inline" : "none";
    this.GetControl("RP_RB_SECLBL").style.display = (maxPattern > 6) ? "inline" : "none";
    this.GetControl("RP_RB_MIN").style.display = (maxPattern > 5) ? "inline" : "none";
    this.GetControl("RP_RB_MINLBL").style.display = (maxPattern > 5) ? "inline" : "none";
    this.GetControl("RP_RB_HOUR").style.display = (maxPattern > 4) ? "inline" : "none";
    this.GetControl("RP_RB_HOURLBL").style.display = (maxPattern > 4) ? "inline" : "none";
    this.GetControl("RP_RB_DAY").style.display = (maxPattern > 3) ? "inline" : "none";
    this.GetControl("RP_RB_DAYLBL").style.display = (maxPattern > 3) ? "inline" : "none";
    this.GetControl("RP_RB_WEEK").style.display = (maxPattern > 2) ? "inline" : "none";
    this.GetControl("RP_RB_WEEKLBL").style.display = (maxPattern > 2) ? "inline" : "none";
    this.GetControl("RP_RB_MON").style.display = (maxPattern > 1) ? "inline" : "none";
    this.GetControl("RP_RB_MONLBL").style.display = (maxPattern > 1) ? "inline" : "none";
}

// Show and hide the appropriate pattern options
function EWSRP_SelectPattern(pattern)
{
    // Only validate when the pattern changes
    if(this.CurrentPattern != pattern && !this.ValidateRecurrence())
    {
        // Keep the old radio button checked if validation fails
        pattern = this.CurrentPattern;

        this.GetControl("RP_RB_YEAR").checked = (pattern == 1);
        this.GetControl("RP_RB_MON").checked = (pattern == 2);
        this.GetControl("RP_RB_WEEK").checked = (pattern == 3);
        this.GetControl("RP_RB_DAY").checked = (pattern == 4);
        this.GetControl("RP_RB_HOUR").checked = (pattern == 5);
        this.GetControl("RP_RB_MIN").checked = (pattern == 6);
        this.GetControl("RP_RB_SEC").checked = (pattern == 7);

        return;
    }

    var advanced = this.GetControl("RP_CHK_ADV").checked;

    this.GetControl("RP_RB_YEAR").checked = (pattern == 1);
    this.GetControl("RP_RB_MON").checked = (pattern == 2);
    this.GetControl("RP_RB_WEEK").checked = (pattern == 3);
    this.GetControl("RP_RB_DAY").checked = (pattern == 4);
    this.GetControl("RP_RB_HOUR").checked = (pattern == 5);
    this.GetControl("RP_RB_MIN").checked = (pattern == 6);
    this.GetControl("RP_RB_SEC").checked = (pattern == 7);

    this.CurrentPattern = pattern;

    if(advanced)
    {
        this.GetControl("RP_ADV_PAT").style.display = "block";
        this.GetControl("RP_YEAR_PAT").style.display = this.GetControl("RP_MON_PAT").style.display =
            this.GetControl("RP_WEEK_PAT").style.display = this.GetControl("RP_DAY_PAT").style.display =
            this.GetControl("RP_HOUR_PAT").style.display = this.GetControl("RP_MIN_PAT").style.display =
            this.GetControl("RP_SEC_PAT").style.display = "none";

        switch(pattern)
        {
            case 1:     // Yearly
                this.GetControl("RP_AP_INTLBL").innerHTML = "year(s)";
                break;

            case 2:     // Monthly
                this.GetControl("RP_AP_INTLBL").innerHTML = "month(s)";
                break;

            case 3:     // Weekly
                this.GetControl("RP_AP_INTLBL").innerHTML = "week(s)";
                break;

            case 4:     // Daily
                this.GetControl("RP_AP_INTLBL").innerHTML = "day(s)";
                break;

            case 5:     // Hourly
                this.GetControl("RP_AP_INTLBL").innerHTML = "hour(s)";
                break;

            case 6:     // Minutely
                this.GetControl("RP_AP_INTLBL").innerHTML = "minute(s)";
                break;

            default:    // Secondly
                this.GetControl("RP_AP_INTLBL").innerHTML = "second(s)";
                break;
        }

        // By Week # is only used by the Yearly frequency
        if(pattern == 1)
            this.GetControl("RP_AP_BYWKNO").disabled = "";
        else
            this.GetControl("RP_AP_BYWKNO").disabled = "disabled";

        if(pattern < 3)
            this.SetDayInstanceState();
        else
        {
            // Day Instance isn't used by the weekly through secondly patterns
            this.GetControl("RP_AP_DAY").disabled = "disabled";
            this.LoadByDayValues();
        }
    }
    else
    {
        this.GetControl("RP_ADV_PAT").style.display = "none";
        this.GetControl("RP_YEAR_PAT").style.display = (pattern == 1) ? "block" : "none";
        this.GetControl("RP_MON_PAT").style.display = (pattern == 2) ? "block" : "none";
        this.GetControl("RP_WEEK_PAT").style.display = (pattern == 3) ? "block" : "none";
        this.GetControl("RP_DAY_PAT").style.display = (pattern == 4) ? "block" : "none";
        this.GetControl("RP_HOUR_PAT").style.display = (pattern == 5) ? "block" : "none";
        this.GetControl("RP_MIN_PAT").style.display = (pattern == 6) ? "block" : "none";
        this.GetControl("RP_SEC_PAT").style.display = (pattern == 7) ? "block" : "none";
    }
}

// Set the advanced pattern control states
function EWSRP_SetAdvancedState()
{
    var interval, advChk, idx, daysUsed, dayList, monthDay;
    var occurrence, opts, months;

    advChk = this.GetControl("RP_CHK_ADV");

    // Invert the checked state for validation
    advChk.checked = !advChk.checked;

    if(!this.ValidateRecurrence())
        return false;

    // Validation passed so accept the new state
    advChk.checked = !advChk.checked;

    // Copy settings between the advanced and simple panels based on the prior setting
    if(!advChk.checked)
    {
        interval = parseInt(this.GetControl("RP_AP_INT").value, 10);

        monthDay = this.GetControl("RP_AP_BYMODAY").value;
        monthDay = monthDay.split(/[,\-]/);

        if(monthDay.length == 0)
            monthDay = 1;
        else
        {
            monthDay = parseInt(monthDay[0], 10);

            if(isNaN(monthDay))
                monthDay = 1;
        }

        occurrence = this.GetControl("RP_AP_BYPOS").value;

        if(occurrence.substr(0, 1) != "-")
			occurrence = occurrence.split(/[,\-]/);
		else
			occurrence = occurrence.substr(1).split(/,/);

        if(occurrence.length == 0)
            occurrence = 1;
        else
        {
            occurrence = parseInt(occurrence[0], 10);
            if(isNaN(occurrence))
                occurrence = 1;
        }

        if(this.GetControl("RP_AP_DAY").disabled == "")
            dayList = this.DayInstanceList;
        else
            dayList = this.DayList;

        // Set the simple pattern based on the advanced pattern's options
        switch(this.CurrentPattern)
        {
            case 1:     // Yearly
                opts = this.GetControl("RP_AP_MOS").options;

                for(idx = 0; idx < opts.length; idx++)
                    if(opts[idx].selected)
                        break;

                if(idx >= opts.length)
                    idx = 0;

                this.SetYearlyInterval(idx + 1, monthDay, interval, dayList, occurrence);
                break;

            case 2:     // Monthly
                this.SetMonthlyInterval(monthDay, interval, dayList, occurrence);
                break;

            case 3:     // Weekly
                this.SetWeeklyInterval(interval, this.DayList);
                break;

            case 4:     // Daily
                // See if it is all weekdays
                for(idx = 0; idx < dayList.length; idx++)
                    switch(dayList[idx].DayOfWeek)
                    {
                        case 0:
                            daysUsed |= 1;
                            break;

                        case 1:
                            daysUsed |= 2;
                            break;

                        case 2:
                            daysUsed |= 4;
                            break;

                        case 3:
                            daysUsed |= 8;
                            break;

                        case 4:
                            daysUsed |= 16;
                            break;

                        case 5:
                            daysUsed |= 32;
                            break;

                        case 6:
                            daysUsed |= 64;
                            break;
                    }

                // Use Every Week Day if that's what we found
                if(daysUsed == 62)
                    this.SetDailyInterval(1, true);
                else
                    this.SetDailyInterval(interval, false);
                break;

            case 5:     // Hourly
                this.SetInterval(5, interval);
                break;

            case 6:     // Minutely
                this.SetInterval(6, interval);
                break;

            default:    // Secondly
                this.SetInterval(7, interval);
                break;
        }

        this.DayInstanceList = new Array();
        this.DayList = new Array();
    }
    else
    {
        // Set the advanced pattern's options based on the simple pattern
        switch(this.CurrentPattern)
        {
            case 1:     // Yearly
                if(this.GetControl("RP_YP_XOFY").checked)
                {
                    months = new Array();
                    months[0] = this.GetControl("RP_YP_MON").selectedIndex + 1;
                    monthDay = this.GetControl("RP_YP_DAY").value;
                    interval = parseInt(this.GetControl("RP_YP_INT").value, 10);
                    dayList = new Array();
                    occurrence = "";
                }
                else
                {
                    monthDay = "";
                    months = new Array();
                    months[0] = this.GetControl("RP_YP_INMON").selectedIndex + 1;
                    interval = parseInt(this.GetControl("RP_YP_NTHINT").value, 10);

                    // If it's a single day, use ByDay.  If it's a combination, use ByDay with BySetPos.
                    occurrence = this.GetControl("RP_YP_INST").selectedIndex + 1;

                    // Convert "last" to 1st from end
                    if(occurrence > 4)
                        occurrence = -1;

                    idx = this.GetControl("RP_YP_DOW").selectedIndex;

                    switch(idx)
                    {
                        case 7:     // Any weekday
                            occurrence = occurrence.toString();
                            dayList = new Array(new EWSRP_DayInstance(0, 1), new EWSRP_DayInstance(0, 2),
                                new EWSRP_DayInstance(0, 3), new EWSRP_DayInstance(0, 4),
                                new EWSRP_DayInstance(0, 5));
                            break;

                        case 8:     // Any weekend day
                            occurrence = occurrence.toString();
                            dayList = new Array(new EWSRP_DayInstance(0, 0), new EWSRP_DayInstance(0, 6));
                            break;

                        case 9:     // Every day
                            occurrence = occurrence.toString();
                            dayList = new Array(new EWSRP_DayInstance(0, 0), new EWSRP_DayInstance(0, 1),
                                new EWSRP_DayInstance(0, 2), new EWSRP_DayInstance(0, 3),
                                new EWSRP_DayInstance(0, 4), new EWSRP_DayInstance(0, 5),
                                new EWSRP_DayInstance(0, 6));
                            break;

                        default:	// Specific day of the week
                            dayList = new Array(new EWSRP_DayInstance(occurrence, idx));
                            occurrence = "";
                            break;
                    }
                }

                this.SetAdvancedInterval(1, interval, dayList, months, "", "", monthDay, "", "", "", occurrence);
                break;

            case 2:     // Monthly
                if(this.GetControl("RP_MP_XOFY").checked)
                {
                    monthDay = this.GetControl("RP_MP_DAY").value;
                    interval = parseInt(this.GetControl("RP_MP_INT").value, 10);
                    dayList = new Array();
                    occurrence = "";
                }
                else
                {
                    monthDay = "";
                    interval = parseInt(this.GetControl("RP_MP_NTHINT").value, 10);

                    // If it's a single day, use ByDay.  If it's a combination, use ByDay with BySetPos.
                    occurrence = this.GetControl("RP_MP_INST").selectedIndex + 1;

                    // Convert "last" to 1st from end
                    if(occurrence > 4)
                        occurrence = -1;

                    idx = this.GetControl("RP_MP_DOW").selectedIndex;

                    switch(idx)
                    {
                        case 7:     // Any weekday
                            occurrence = occurrence.toString();
                            dayList = new Array(new EWSRP_DayInstance(0, 1), new EWSRP_DayInstance(0, 2),
                                new EWSRP_DayInstance(0, 3), new EWSRP_DayInstance(0, 4),
                                new EWSRP_DayInstance(0, 5));
                            break;

                        case 8:     // Any weekend day
                            occurrence = occurrence.toString();
                            dayList = new Array(new EWSRP_DayInstance(0, 0), new EWSRP_DayInstance(0, 6));
                            break;

                        case 9:     // Every day
                            occurrence = occurrence.toString();
                            dayList = new Array(new EWSRP_DayInstance(0, 0), new EWSRP_DayInstance(0, 1),
                                new EWSRP_DayInstance(0, 2), new EWSRP_DayInstance(0, 3),
                                new EWSRP_DayInstance(0, 4), new EWSRP_DayInstance(0, 5),
                                new EWSRP_DayInstance(0, 6));
                            break;

                        default:     // Specific day of the week
                            dayList = new Array(new EWSRP_DayInstance(occurrence, idx));
                            occurrence = "";
                            break;
                    }
                }

                this.SetAdvancedInterval(2, interval, dayList, new Array(), "", "", monthDay, "", "", "",
                    occurrence);
                break;

            case 3:     // Weekly
                dayList = new Array();

                // Figure out which days to add to the By Day list
                for(idx = 0; idx < 7; idx++)
                    if(this.GetControl("RP_WP_" + idx.toString()).checked)
                        dayList[dayList.length] = new EWSRP_DayInstance(0, idx);

                this.SetAdvancedInterval(3, parseInt(this.GetControl("RP_WP_INT").value, 10), dayList);
                break;

            case 4:     // Daily
                if(this.GetControl("RP_DP_INST").checked)
                    this.SetAdvancedInterval(4, parseInt(this.GetControl("RP_DP_INT").value, 10));
                else
                {
                    this.SetAdvancedInterval(4, 1, new Array(new EWSRP_DayInstance(0, 1),
                        new EWSRP_DayInstance(0, 2), new EWSRP_DayInstance(0, 3),
                        new EWSRP_DayInstance(0, 4), new EWSRP_DayInstance(0, 5)));
                }
                break;

            case 5:     // Hourly
                this.SetAdvancedInterval(5, parseInt(this.GetControl("RP_HP_INT").value, 10));
                break;

            case 6:     // Minutely
                this.SetAdvancedInterval(6, parseInt(this.GetControl("RP_MINP_INT").value, 10));
                break;

            default:    // Secondly
                this.SetAdvancedInterval(7, parseInt(this.GetControl("RP_SP_INT").value, 10));
                break;
        }
    }

    this.SelectPattern(this.CurrentPattern);
}

// Set the range of recurrence control states
function EWSRP_SetRangeCtlState(range)
{
    this.GetControl("RP_RNG_OCCVAL").style.display = "none";
    this.GetControl("RP_RNG_DTVAL").style.display = "none";

    switch(range)
    {
        case 0:     // Never ends
            this.GetControl("RP_RNG_NONE").checked = true;
            this.GetControl("RP_RNG_OCC").checked = this.GetControl("RP_RNG_DATE").checked = false;

            this.GetControl("RP_RNG_ENDOCC").disabled = this.GetControl("RP_RNG_ENDDT").disabled = "disabled";
            break;

        case 1:     // End after N occurrences
            this.GetControl("RP_RNG_OCC").checked = true;
            this.GetControl("RP_RNG_NONE").checked = this.GetControl("RP_RNG_DATE").checked = false;

            this.GetControl("RP_RNG_ENDOCC").disabled = "";
            this.GetControl("RP_RNG_ENDDT").disabled = "disabled";
            break;

        default:    // End by specific date
            this.GetControl("RP_RNG_DATE").checked = true;
            this.GetControl("RP_RNG_NONE").checked = this.GetControl("RP_RNG_OCC").checked = false;

            this.GetControl("RP_RNG_ENDOCC").disabled = "disabled";
            this.GetControl("RP_RNG_ENDDT").disabled = "";
            break;
    }

    this.CurrentRange = range;
}

// Set the daily pattern's control states
function EWSRP_SetDailyCtlState(dayInstance)
{
    this.GetControl("RP_DP_VAL").style.display = "none";

    if(dayInstance)
    {
        this.GetControl("RP_DP_INST").checked = true;
        this.GetControl("RP_DP_INT").disabled = "";

        this.GetControl("RP_DP_WD").checked = false;
    }
    else
    {
        this.GetControl("RP_DP_INST").checked = false;
        this.GetControl("RP_DP_INT").disabled = "disabled";

        this.GetControl("RP_DP_WD").checked = true;
    }
}

// Set the monthly pattern's control states
function EWSRP_SetMonthlyCtlState(dayXofY)
{
    this.GetControl("RP_MP_DAY_VAL").style.display = "none";
    this.GetControl("RP_MP_INT_VAL").style.display = "none";

    if(dayXofY)
    {
        this.GetControl("RP_MP_XOFY").checked = true;
        this.GetControl("RP_MP_DAY").disabled = this.GetControl("RP_MP_INT").disabled = "";

        this.GetControl("RP_MP_NTH").checked = false;
        this.GetControl("RP_MP_INST").disabled = this.GetControl("RP_MP_DOW").disabled =
            this.GetControl("RP_MP_NTHINT").disabled = "disabled";
    }
    else
    {
        this.GetControl("RP_MP_XOFY").checked = false;
        this.GetControl("RP_MP_DAY").disabled = this.GetControl("RP_MP_INT").disabled = "disabled";

        this.GetControl("RP_MP_NTH").checked = true;
        this.GetControl("RP_MP_INST").disabled = this.GetControl("RP_MP_DOW").disabled =
            this.GetControl("RP_MP_NTHINT").disabled = "";
    }
}

// Set the yearly pattern's control states
function EWSRP_SetYearlyCtlState(dayXofY)
{
    this.GetControl("RP_YP_DAY_VAL").style.display = "none";
    this.GetControl("RP_YP_INT_VAL").style.display = "none";

    if(dayXofY)
    {
        this.GetControl("RP_YP_XOFY").checked = true;
        this.GetControl("RP_YP_MON").disabled = this.GetControl("RP_YP_DAY").disabled =
            this.GetControl("RP_YP_INT").disabled = "";

        this.GetControl("RP_YP_NTH").checked = false;
        this.GetControl("RP_YP_INST").disabled = this.GetControl("RP_YP_DOW").disabled =
            this.GetControl("RP_YP_INMON").disabled = this.GetControl("RP_YP_NTHINT").disabled = "disabled";
    }
    else
    {
        this.GetControl("RP_YP_XOFY").checked = false;
        this.GetControl("RP_YP_MON").disabled = this.GetControl("RP_YP_DAY").disabled =
            this.GetControl("RP_YP_INT").disabled = "disabled";

        this.GetControl("RP_YP_NTH").checked = true;
        this.GetControl("RP_YP_INST").disabled = this.GetControl("RP_YP_DOW").disabled =
            this.GetControl("RP_YP_INMON").disabled = this.GetControl("RP_YP_NTHINT").disabled = "";
    }
}

// Set the state of the advanced pattern's day instance control based on the other recurrence settings
function EWSRP_SetDayInstanceState()
{
    var dayInstCtl, newState, oldState, opts, idx, count;

    dayInstCtl = this.GetControl("RP_AP_DAY");
    newState = oldState = (dayInstCtl.disabled == "");

    // If yearly, see how many months are selected
    if(this.CurrentPattern == 1)
    {
        opts = this.GetControl("RP_AP_MOS").options;
        count = opts.length;

        for(idx = 0; idx < count; idx++)
            if(opts[idx].selected)
                break;

        // If any months are selected, check By Month Day.  If no months are selected check By Week No.
        if(idx < count)
            newState = (this.GetControl("RP_AP_BYMODAY").value.length == 0);
        else
            newState = (this.GetControl("RP_AP_BYWKNO").value.length == 0);
    }
    else
        if(this.CurrentPattern == 2)  // If monthly, check By Month Day
            newState = (this.GetControl("RP_AP_BYMODAY").value.length == 0);

    if(oldState != newState)
        dayInstCtl.disabled = (newState) ? "" : "disabled";

    this.LoadByDayValues();
}

// Add a day instance to the instance list
function EWSRP_AddDayInstance()
{
    var dayCtl, instList, opt, di, idx;

    dayCtl = this.GetControl("RP_AP_DAY");

    if(dayCtl.disabled == "" && !this.ValidateNumeric("RP_AP_DAY", "RP_AP_DAYVAL", -53, 53))
        return;

    instList = this.GetControl("RP_AP_INSTLST");
    opt = document.createElement("OPTION");

    if(dayCtl.disabled == "")
    {
        di = new EWSRP_DayInstance(parseInt(dayCtl.value, 10), this.GetControl("RP_AP_DOW").selectedIndex);

        if(this.DayInstanceList.length == 0)
            this.DayInstanceList[0] = di;
        else
        {
            for(idx = 0; idx < this.DayInstanceList.length; idx++)
                if(this.DayInstanceList[idx].Instance == di.Instance &&
                  this.DayInstanceList[idx].DayOfWeek == di.DayOfWeek)
                    break;

            // Don't add it if it's already there
            if(idx < this.DayInstanceList.length)
                return;

            this.DayInstanceList[this.DayInstanceList.length] = di;
        }

        opt.text = di.Description();
    }
    else
    {
        di = new EWSRP_DayInstance(0, this.GetControl("RP_AP_DOW").selectedIndex);

        if(this.DayList.length == 0)
            this.DayList[0] = di;
        else
        {
            for(idx = 0; idx < this.DayList.length; idx++)
                if(this.DayList[idx].DayOfWeek == di.DayOfWeek)
                    break;

            // Don't add it if it's already there
            if(idx < this.DayList.length)
                return;

            this.DayList[this.DayList.length] = di;
        }

        opt.text = di.DayName();
    }

    try
    {
        instList.add(opt, null); // standards compliant
    }
    catch(ex)
    {
        instList.add(opt); // IE only
    }

    this.GetControl("RP_AP_DELINST").disabled =
        this.GetControl("RP_AP_CLRINST").disabled = "";
}

// Remove a day instance from the instance list
function EWSRP_RemoveDayInstance()
{
    var instList, selIdx;

    this.GetControl("RP_AP_DAYVAL").style.display = "none";

    instList = this.GetControl("RP_AP_INSTLST");
    selIdx = instList.selectedIndex;

    if(selIdx == -1)
        instList.selectedIndex = 0;
    else
    {
        if(this.GetControl("RP_AP_DAY").disabled == "")
            this.DayInstanceList.splice(selIdx, 1);
        else
            this.DayList.splice(selIdx, 1);

        instList.remove(selIdx);

        if(selIdx < instList.options.length)
            instList.selectedIndex = selIdx;
        else
            if(instList.options.length != 0)
                instList.selectedIndex = instList.options.length - 1;
            else
                this.GetControl("RP_AP_DELINST").disabled = this.GetControl("RP_AP_CLRINST").disabled = "disabled";
    }
}

// Clear all day instances from the instance list
function EWSRP_ClearDayInstances()
{
    var instList;

    this.GetControl("RP_AP_DAYVAL").style.display = "none";

    instList = this.GetControl("RP_AP_INSTLST");

    while(instList.options.length != 0)
        instList.remove(0);

    this.DayList = new Array();
    this.DayInstanceList = new Array();

    instList.selectedIndex = -1;
    this.GetControl("RP_AP_DELINST").disabled = this.GetControl("RP_AP_CLRINST").disabled = "disabled";
}

// Validate the recurrence.  Returns true if valid, false if not valid.
function EWSRP_ValidateRecurrence()
{
    var month, maxDay, opts, idx, isValid = true, pattern = "";

    if(this.GetControl("RP_CHK_ADV").checked)
    {
        isValid = this.ValidateNumeric("RP_AP_INT", "RP_AP_INTVAL", 1, 999);
        
        // Build the advanced pattern data
		pattern = "1\xFF" + this.CurrentPattern.toString() + "\xFF";
		pattern += this.GetControl("RP_AP_INT").value + "\xFF";
		
		// Add state flags for each month
	    opts = this.GetControl("RP_AP_MOS").options;

		for(idx = 0; idx < opts.length; idx++)
			pattern += opts[idx].selected ? "1" : "0";
			
		pattern += "\xFF";
		
		// Add the day instances
	    if(this.GetControl("RP_AP_DAY").disabled == "")
			for(idx = 0; idx < this.DayInstanceList.length; idx++)
		    {
				if(idx != 0)
					pattern += ",";
					
				pattern += this.DayInstanceList[idx].toString();
			}
		else
			for(idx = 0; idx < this.DayList.length; idx++)
			{
				if(idx != 0)
					pattern += ",";
					
				pattern += this.DayList[idx].toString();
			}
			
		pattern += "\xFF";
		
		if(this.GetControl("RP_AP_BYWKNO").disabled == "")
			pattern += this.GetControl("RP_AP_BYWKNO").value + "\xFF";
		else
			pattern += "\xFF";
			
		pattern += this.GetControl("RP_AP_BYYRDAY").value + "\xFF";
		pattern += this.GetControl("RP_AP_BYMODAY").value + "\xFF";
		pattern += this.GetControl("RP_AP_BYHOUR").value + "\xFF";
		pattern += this.GetControl("RP_AP_BYMIN").value + "\xFF";
		pattern += this.GetControl("RP_AP_BYSEC").value + "\xFF";
		pattern += this.GetControl("RP_AP_BYPOS").value + "\xFF";
    }
    else
    {
		pattern = "0\xFF" + this.CurrentPattern.toString() + "\xFF";
		
        switch(this.CurrentPattern)
        {
            case 1:     // Yearly
                // For the yearly pattern, Nth day of month, validate that the day value is valid for the
                // selected month.  Feb 29th is never valid so use a non-leap year.
                month = this.GetControl("RP_YP_MON").selectedIndex;
                maxDay = new Date(2003, month + 1, 1);
                maxDay = new Date(maxDay.getTime() - 86400000);
                maxDay = maxDay.getDate();
                this.GetControl("RP_YP_MAXDAYS").innerHTML = maxDay;

                if(this.GetControl("RP_YP_XOFY").checked)
                {
                    if(!this.ValidateNumeric("RP_YP_DAY", "RP_YP_DAY_VAL", 1, maxDay))
                        isValid = false;

                    if(!this.ValidateNumeric("RP_YP_INT", "RP_YP_INT_VAL", 1, 999))
                        isValid = false;
                    
                    pattern += "0\xFF";    
                    pattern += this.GetControl("RP_YP_MON").selectedIndex.toString() + "\xFF";
                    pattern += this.GetControl("RP_YP_DAY").value + "\xFF";
                    pattern += this.GetControl("RP_YP_INT").value + "\xFF";
                }
                else
                {
                    isValid = this.ValidateNumeric("RP_YP_NTHINT", "RP_YP_INT_VAL", 1, 999);

                    pattern += "1\xFF";    
					pattern += this.GetControl("RP_YP_INST").selectedIndex.toString() + "\xFF";
					pattern += this.GetControl("RP_YP_DOW").selectedIndex.toString() + "\xFF";
					pattern += this.GetControl("RP_YP_INMON").selectedIndex.toString() + "\xFF";
                    pattern += this.GetControl("RP_YP_NTHINT").value + "\xFF";
                }
                break;

            case 2:     // Monthly
                if(this.GetControl("RP_MP_XOFY").checked)
                {
                    if(!this.ValidateNumeric("RP_MP_DAY", "RP_MP_DAY_VAL", 1, 31))
                        isValid = false;

                    if(!this.ValidateNumeric("RP_MP_INT", "RP_MP_INT_VAL", 1, 999))
                        isValid = false;
                    
                    pattern += "0\xFF";    
                    pattern += this.GetControl("RP_MP_DAY").value + "\xFF";
                    pattern += this.GetControl("RP_MP_INT").value + "\xFF";
                }
                else
                {
                    isValid = this.ValidateNumeric("RP_MP_NTHINT", "RP_MP_INT_VAL", 1, 999);

                    pattern += "1\xFF";    
					pattern += this.GetControl("RP_MP_INST").selectedIndex.toString() + "\xFF";
					pattern += this.GetControl("RP_MP_DOW").selectedIndex.toString() + "\xFF";
                    pattern += this.GetControl("RP_MP_NTHINT").value + "\xFF";
                }
                break;

            case 3:     // Weekly
                isValid = this.ValidateNumeric("RP_WP_INT", "RP_WP_VAL", 1, 999);
                
                pattern += this.GetControl("RP_WP_INT").value + "\xFF";
                pattern += ((this.GetControl("RP_WP_0").checked) ? "1" : "0");
                pattern += ((this.GetControl("RP_WP_1").checked) ? "1" : "0");
                pattern += ((this.GetControl("RP_WP_2").checked) ? "1" : "0");
                pattern += ((this.GetControl("RP_WP_3").checked) ? "1" : "0");
                pattern += ((this.GetControl("RP_WP_4").checked) ? "1" : "0");
                pattern += ((this.GetControl("RP_WP_5").checked) ? "1" : "0");
                pattern += ((this.GetControl("RP_WP_6").checked) ? "1" : "0");
                pattern += "\xFF";
                break;

            case 4:     // Daily
                if(this.GetControl("RP_DP_INST").checked)
                {
                    isValid = this.ValidateNumeric("RP_DP_INT", "RP_DP_VAL", 1, 999);
	                pattern += "0\xFF" + this.GetControl("RP_DP_INT").value + "\xFF";
                }
                else
					pattern += "1\xFF";
                break;

            case 5:     // Hourly
                isValid = this.ValidateNumeric("RP_HP_INT", "RP_HP_VAL", 1, 999);
                pattern += this.GetControl("RP_HP_INT").value + "\xFF";
                break;

            case 6:     // Minutely
                isValid = this.ValidateNumeric("RP_MINP_INT", "RP_MINP_VAL", 1, 999);
                pattern += this.GetControl("RP_MINP_INT").value + "\xFF";
                break;

            default:    // Secondly
                isValid = this.ValidateNumeric("RP_SP_INT", "RP_SP_VAL", 1, 999);
                pattern += this.GetControl("RP_SP_INT").value + "\xFF";
                break;
        }
    }

	// Add week start day and can occur on holiday flag
	pattern += this.GetControl("RP_RNG_WSD").selectedIndex.toString() + "\xFF";
	pattern += ((this.GetControl("RP_RNG_COH").checked) ? "1" : "0") + "\xFF";

	// Add range information
	pattern += this.CurrentRange.toString() + "\xFF";

    switch(this.CurrentRange)
    {
        case 1:     // End after N occurrences
            if(!this.ValidateNumeric("RP_RNG_ENDOCC", "RP_RNG_OCCVAL", 1, 999))
                isValid = false;
            else    
				pattern += this.GetControl("RP_RNG_ENDOCC").value;
            break;

        case 2:     // End after a specific date
            if(!this.ValidateEndDate())
            {
                isValid = false;
                this.GetControl("RP_RNG_DTVAL").style.display = "inline";
            }
            else
            {
                this.GetControl("RP_RNG_DTVAL").style.display = "none";
                pattern += this.GetControl("RP_RNG_ENDDT").value;
            }
            break;

        default:    // Never ends
            break;
    }

	// If valid, store the pattern data in the associated hidden field for posting back to the server
	if(!isValid)
		pattern = "";
		
	document.getElementsByName(this.ControlId + "_Value")[0].value = pattern;

    return isValid;
}

// Validate a numeric value.  Returns true if valid, false if not valid.
function EWSRP_ValidateNumeric(textBoxCtlId, msgSpanId, minVal, maxVal)
{
    var isValid = true;

    var numberCtl = this.GetControl(textBoxCtlId);
    var msgSpan = this.GetControl(msgSpanId);
    var number, numberText = numberCtl.value.replace(/\s*/g, "");

    numberCtl.value = numberText;

    if(numberText == "" || !/^\-?[0-9]+$/.test(numberText))
        number = -32767;
    else
        number = parseInt(numberText, 10);

    if(number < minVal || number > maxVal)
    {
        msgSpan.style.display = "inline";
        isValid = false;
    }
    else
        msgSpan.style.display = "none";

    return isValid;
}

// This is used to parse the end date value and see if it is valid.  Returns true if valid, false if not.
function EWSRP_ValidateEndDate()
{
    var endDateCtl, endDate, dateText, parts, month, day, year, sep;

    endDateCtl = this.GetControl("RP_RNG_ENDDT");
    dateText = endDateCtl.value.toLowerCase().replace(/[\s*]/g, "");
    endDate = Date.parse(dateText);

    if(isNaN(endDate))
        return false;

    parts = dateText.split(/[^0-9]/);

    // We should have enough parts.  If not, return.  It's valid as we made it this far.
    if(parts.length != 3)
        return true;

    sep = dateText.charAt(parts[0].length);

	// Make sure we got the same month and day.  The order will vary based on the culture so check both ways.
	endDate = new Date(endDate);
    month = parseInt(parts[0], 10);
    day = parseInt(parts[1], 10);

	if(month != endDate.getMonth() + 1 && month != endDate.getDate())
		return false;

	if(day != endDate.getMonth() + 1 && day != endDate.getDate())
		return false;

	// Format the parts with leading zeros and add a full 4 digit year.  We make no assumption about the order
    // of the first two parts other than they are month and day in either order.
    if(parts[0].length < 2)
        parts[0] = "0" + parts[0];

    if(parts[1].length < 2)
        parts[1] = "0" + parts[1];

    year = parseInt(parts[2], 10);

    if(year < 1000)
        if(year < 30 || year > 99)
            year += 2000;
        else
            year += 1900;

    if(year != endDate.getFullYear())
		return false;

    endDateCtl.value = parts[0] + sep + parts[1] + sep + year.toString();
    return true;
}
