'
'
' This file is used by Code Analysis to maintain SuppressMessage 
' attributes that are applied to this project.
' Project-level suppressions either have no target or are given 
' a specific target and scoped to a namespace, type, member, etc.
'
' To add a suppression to this file, right-click the message in the 
' Code Analysis results, point to "Suppress Message", and click 
' "In Suppression File".
' You do not need to add suppressions to this file manually.


Imports System.Diagnostics.CodeAnalysis

<Assembly: SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope:="member", Target:="CalendarBrowser.CalendarObjectDlg.#SetValues(EWSoftware.PDI.Objects.CalendarObject)")>
<Assembly: SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope:="member", Target:="CalendarBrowser.CalendarObjectDlg.#GetValues(EWSoftware.PDI.Objects.CalendarObject)")>
<Assembly: SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope:="member", Target:="CalendarBrowser.CalendarObjectDlg.#CalendarObjectDlg_Closing(System.Object,System.ComponentModel.CancelEventArgs)")>
<Assembly: SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId:="vCal", Scope:="member", Target:="CalendarBrowser.CalendarBrowserForm.#Dispose(System.Boolean)")>
<Assembly: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope:="member", Target:="CalendarBrowser.CalendarBrowserForm.#dgvCalendar_CellPainting(System.Object,System.Windows.Forms.DataGridViewCellPaintingEventArgs)")>
<Assembly: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope:="member", Target:="CalendarBrowser.CalendarBrowserForm.#CalendarSorter(EWSoftware.PDI.Objects.CalendarObject,EWSoftware.PDI.Objects.CalendarObject)")>
<Assembly: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope:="member", Target:="CalendarBrowser.CalendarObjectDlg.#SetValues(EWSoftware.PDI.Objects.CalendarObject)")>
<Assembly: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope:="member", Target:="CalendarBrowser.CalendarObjectDlg.#GetValues(EWSoftware.PDI.Objects.CalendarObject)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="CalendarBrowser.RecurrenceControl.#btnEditRRule_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="CalendarBrowser.CalendarObjectDlg.#btnFind_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="CalendarBrowser.CalendarBrowserForm.#miSave_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="CalendarBrowser.CalendarBrowserForm.#miOpen_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="CalendarBrowser.AttachmentsControl.#btnDetach_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="CalendarBrowser.AttachmentsControl.#btnAdd_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="CalendarBrowser.AboutDlg.#lnkHelp_LinkClicked(System.Object,System.Windows.Forms.LinkLabelLinkClickedEventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="CalendarBrowser.AboutDlg.#btnSysInfo_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="Dlg", Scope:="type", Target:="CalendarBrowser.AboutDlg")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="Dlg", Scope:="type", Target:="CalendarBrowser.CalendarObjectDlg")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="o", Scope:="member", Target:="CalendarBrowser.CalendarObjectDlg.#SetValues(EWSoftware.PDI.Objects.CalendarObject)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="o", Scope:="member", Target:="CalendarBrowser.CalendarObjectDlg.#GetValues(EWSoftware.PDI.Objects.CalendarObject)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="rr", Scope:="member", Target:="CalendarBrowser.RecurrenceControl.#SetValues(EWSoftware.PDI.Properties.RRulePropertyCollection,EWSoftware.PDI.Properties.RDatePropertyCollection)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="er", Scope:="member", Target:="CalendarBrowser.RecurrenceControl.#SetValues(EWSoftware.PDI.Properties.RRulePropertyCollection,EWSoftware.PDI.Properties.ExDatePropertyCollection)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="rr", Scope:="member", Target:="CalendarBrowser.RecurrenceControl.#GetValues(EWSoftware.PDI.Properties.RRulePropertyCollection,EWSoftware.PDI.Properties.RDatePropertyCollection)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="er", Scope:="member", Target:="CalendarBrowser.RecurrenceControl.#GetValues(EWSoftware.PDI.Properties.RRulePropertyCollection,EWSoftware.PDI.Properties.ExDatePropertyCollection)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="Dlg", Scope:="type", Target:="CalendarBrowser.TimeZoneListDlg")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="Dlg", Scope:="type", Target:="CalendarBrowser.VFreeBusyDlg")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="fb", Scope:="member", Target:="CalendarBrowser.VFreeBusyDlg.#SetValues(EWSoftware.PDI.Objects.VFreeBusy)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="fb", Scope:="member", Target:="CalendarBrowser.VFreeBusyDlg.#GetValues(EWSoftware.PDI.Objects.VFreeBusy)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="Dlg", Scope:="type", Target:="CalendarBrowser.VTimeZoneDlg")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="tz", Scope:="member", Target:="CalendarBrowser.VTimeZoneDlg.#SetValues(EWSoftware.PDI.Objects.VTimeZone)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="tz", Scope:="member", Target:="CalendarBrowser.VTimeZoneDlg.#GetValues(EWSoftware.PDI.Objects.VTimeZone)")>
<Assembly: SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification:="<Pending>", Scope:="member", Target:="~M:CalendarBrowser.AttachmentsControl.btnAdd_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification:="<Pending>", Scope:="member", Target:="~M:CalendarBrowser.AttachmentsControl.btnDetach_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification:="<Pending>", Scope:="member", Target:="~F:CalendarBrowser.CalendarBrowserForm.vCal")>
<Assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification:="<Pending>", Scope:="member", Target:="~M:CalendarBrowser.CalendarBrowserForm.Main")>
<Assembly: SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification:="<Pending>", Scope:="member", Target:="~M:CalendarBrowser.CalendarBrowserForm.miOpen_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification:="<Pending>", Scope:="member", Target:="~M:CalendarBrowser.CalendarBrowserForm.miSave_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Globalization", "CA1307:Specify StringComparison for clarity", Justification:="<Pending>", Scope:="member", Target:="~M:CalendarBrowser.CalendarBrowserForm.dgvCalendar_CellPainting(System.Object,System.Windows.Forms.DataGridViewCellPaintingEventArgs)")>
