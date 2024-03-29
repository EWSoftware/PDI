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

<Assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId:="EMail", Scope:="type", Target:="vCardBrowser.EMailControl")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId:="Filename", Scope:="member", Target:="vCardBrowser.PhotoControl.#ImageFilename")>
<Assembly: SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope:="member", Target:="vCardBrowser.PhotoControl.#SetImageBytes(System.Byte[])")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.VCardPropertiesDlg.#GetValues(EWSoftware.PDI.Objects.VCard)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.AboutDlg.#btnSysInfo_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.AboutDlg.#lnkHelp_LinkClicked(System.Object,System.Windows.Forms.LinkLabelLinkClickedEventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.AddressControl.#btnMap_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.PhotoControl.#ImageFilename")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.PhotoControl.#SetImageBytes(System.Byte[])")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.VCardBrowserForm.#miOpen_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.VCardBrowserForm.#miSave_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.VCardPropertiesDlg.#btnFind_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope:="member", Target:="vCardBrowser.VCardPropertiesDlg.#btnWebPage_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="v", Scope:="member", Target:="vCardBrowser.VCardPropertiesDlg.#GetValues(EWSoftware.PDI.Objects.VCard)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId:="v")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId:="v", Scope:="namespace", Target:="vCardBrowser")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="Dlg", Scope:="type", Target:="vCardBrowser.AboutDlg")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="Dlg", Scope:="type", Target:="vCardBrowser.VCardPropertiesDlg")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="v", Scope:="member", Target:="vCardBrowser.VCardPropertiesDlg.#SetValues(EWSoftware.PDI.Objects.VCard)")>
<Assembly: SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId:="bytes", Scope:="member", Target:="vCardBrowser.PhotoControl.#SetImageBytes(System.Byte[])")>
<Assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification:="<Pending>", Scope:="member", Target:="~M:vCardBrowser.VCardBrowserForm.Main")>
<Assembly: SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification:="<Pending>", Scope:="member", Target:="~M:vCardBrowser.VCardBrowserForm.miOpen_Click(System.Object,System.EventArgs)")>
<Assembly: SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification:="<Pending>", Scope:="member", Target:="~M:vCardBrowser.VCardBrowserForm.miSave_Click(System.Object,System.EventArgs)")>
