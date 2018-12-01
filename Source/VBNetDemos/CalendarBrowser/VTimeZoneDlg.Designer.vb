<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VTimeZoneDlg
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtTimeZoneId = New System.Windows.Forms.TextBox()
        Me.txtTimeZoneUrl = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtLastModified = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.grpObRules = New System.Windows.Forms.GroupBox()
        Me.ucRules = New CalendarBrowser.ObservanceRuleControl()
        Me.epErrors = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.grpObRules.SuspendLayout
        CType(Me.epErrors,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(12, 575)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 32)
        Me.btnOK.TabIndex = 7
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.CausesValidation = false
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(652, 575)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 32)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(38, 14)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(106, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Time Zone ID"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTimeZoneId
        '
        Me.txtTimeZoneId.Location = New System.Drawing.Point(150, 12)
        Me.txtTimeZoneId.Name = "txtTimeZoneId"
        Me.txtTimeZoneId.Size = New System.Drawing.Size(570, 26)
        Me.txtTimeZoneId.TabIndex = 1
        '
        'txtTimeZoneUrl
        '
        Me.txtTimeZoneUrl.Location = New System.Drawing.Point(150, 44)
        Me.txtTimeZoneUrl.Name = "txtTimeZoneUrl"
        Me.txtTimeZoneUrl.Size = New System.Drawing.Size(570, 26)
        Me.txtTimeZoneUrl.TabIndex = 3
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(12, 46)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(132, 23)
        Me.label2.TabIndex = 2
        Me.label2.Text = "Time Zone &URL"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLastModified
        '
        Me.txtLastModified.Location = New System.Drawing.Point(150, 76)
        Me.txtLastModified.Name = "txtLastModified"
        Me.txtLastModified.ReadOnly = true
        Me.txtLastModified.Size = New System.Drawing.Size(207, 26)
        Me.txtLastModified.TabIndex = 5
        Me.txtLastModified.TabStop = false
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(16, 78)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(128, 23)
        Me.label3.TabIndex = 4
        Me.label3.Text = "Last Modified"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpObRules
        '
        Me.grpObRules.Controls.Add(Me.ucRules)
        Me.grpObRules.Location = New System.Drawing.Point(12, 121)
        Me.grpObRules.Name = "grpObRules"
        Me.grpObRules.Size = New System.Drawing.Size(728, 448)
        Me.grpObRules.TabIndex = 6
        Me.grpObRules.TabStop = false
        Me.grpObRules.Text = "Observance Rules"
        '
        'ucRules
        '
        Me.ucRules.Location = New System.Drawing.Point(14, 28)
        Me.ucRules.Name = "ucRules"
        Me.ucRules.Size = New System.Drawing.Size(700, 404)
        Me.ucRules.TabIndex = 0
        '
        'epErrors
        '
        Me.epErrors.ContainerControl = Me
        '
        'VTimeZoneDlg
        '
        Me.AcceptButton = Me.btnOK
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(752, 619)
        Me.Controls.Add(Me.grpObRules)
        Me.Controls.Add(Me.txtLastModified)
        Me.Controls.Add(Me.txtTimeZoneUrl)
        Me.Controls.Add(Me.txtTimeZoneId)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "VTimeZoneDlg"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Time Zone Properties"
        Me.grpObRules.ResumeLayout(false)
        CType(Me.epErrors,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents txtTimeZoneId As System.Windows.Forms.TextBox
    Friend WithEvents txtTimeZoneUrl As System.Windows.Forms.TextBox
    Friend WithEvents txtLastModified As System.Windows.Forms.TextBox
    Friend WithEvents grpObRules As System.Windows.Forms.GroupBox
    Friend WithEvents ucRules As CalendarBrowser.ObservanceRuleControl
    Friend WithEvents epErrors As System.Windows.Forms.ErrorProvider

End Class
