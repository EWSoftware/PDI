<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AttendeeControl
    Inherits EWSoftware.PDI.Windows.Forms.BrowseControl

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
        Me.pnlControls = New System.Windows.Forms.Panel()
        Me.txtSentBy = New System.Windows.Forms.TextBox()
        Me.label6 = New System.Windows.Forms.Label()
        Me.txtAttendee = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.chkRSVP = New System.Windows.Forms.CheckBox()
        Me.cboRole = New System.Windows.Forms.ComboBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.label10 = New System.Windows.Forms.Label()
        Me.txtCommonName = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtUserType = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.pnlControls.SuspendLayout
        Me.SuspendLayout
        '
        'pnlControls
        '
        Me.pnlControls.Controls.Add(Me.txtSentBy)
        Me.pnlControls.Controls.Add(Me.label6)
        Me.pnlControls.Controls.Add(Me.txtAttendee)
        Me.pnlControls.Controls.Add(Me.label4)
        Me.pnlControls.Controls.Add(Me.chkRSVP)
        Me.pnlControls.Controls.Add(Me.cboRole)
        Me.pnlControls.Controls.Add(Me.label3)
        Me.pnlControls.Controls.Add(Me.cboStatus)
        Me.pnlControls.Controls.Add(Me.label10)
        Me.pnlControls.Controls.Add(Me.txtCommonName)
        Me.pnlControls.Controls.Add(Me.label1)
        Me.pnlControls.Controls.Add(Me.txtUserType)
        Me.pnlControls.Controls.Add(Me.label5)
        Me.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControls.Location = New System.Drawing.Point(0, 0)
        Me.pnlControls.Name = "pnlControls"
        Me.pnlControls.Size = New System.Drawing.Size(496, 173)
        Me.pnlControls.TabIndex = 0
        '
        'txtSentBy
        '
        Me.txtSentBy.Location = New System.Drawing.Point(128, 107)
        Me.txtSentBy.Name = "txtSentBy"
        Me.txtSentBy.Size = New System.Drawing.Size(248, 22)
        Me.txtSentBy.TabIndex = 9
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(58, 107)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(64, 23)
        Me.label6.TabIndex = 8
        Me.label6.Text = "Sent By"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAttendee
        '
        Me.txtAttendee.Location = New System.Drawing.Point(128, 11)
        Me.txtAttendee.Name = "txtAttendee"
        Me.txtAttendee.Size = New System.Drawing.Size(248, 22)
        Me.txtAttendee.TabIndex = 1
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(50, 11)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(72, 23)
        Me.label4.TabIndex = 0
        Me.label4.Text = "Attendee"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkRSVP
        '
        Me.chkRSVP.Location = New System.Drawing.Point(328, 139)
        Me.chkRSVP.Name = "chkRSVP"
        Me.chkRSVP.Size = New System.Drawing.Size(72, 24)
        Me.chkRSVP.TabIndex = 12
        Me.chkRSVP.Text = "RSVP"
        '
        'cboRole
        '
        Me.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRole.Items.AddRange(New Object() {"", "CHAIR", "REQ-PARTICIPANT", "OPT-PARTICIPANT", "NON-PARTICIPANT"})
        Me.cboRole.Location = New System.Drawing.Point(328, 75)
        Me.cboRole.MaxDropDownItems = 16
        Me.cboRole.Name = "cboRole"
        Me.cboRole.Size = New System.Drawing.Size(160, 24)
        Me.cboRole.TabIndex = 7
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(276, 75)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(46, 23)
        Me.label3.TabIndex = 6
        Me.label3.Text = "Role"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Items.AddRange(New Object() {"", "NEEDS-ACTION", "ACCEPTED", "DECLINED", "TENTATIVE", "DELEGATED", "COMPLETED", "IN-PROCESS"})
        Me.cboStatus.Location = New System.Drawing.Point(128, 139)
        Me.cboStatus.MaxDropDownItems = 16
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(176, 24)
        Me.cboStatus.TabIndex = 11
        '
        'label10
        '
        Me.label10.Location = New System.Drawing.Point(63, 139)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(59, 23)
        Me.label10.TabIndex = 10
        Me.label10.Text = "Status"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCommonName
        '
        Me.txtCommonName.Location = New System.Drawing.Point(128, 43)
        Me.txtCommonName.Name = "txtCommonName"
        Me.txtCommonName.Size = New System.Drawing.Size(248, 22)
        Me.txtCommonName.TabIndex = 3
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(8, 43)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(114, 23)
        Me.label1.TabIndex = 2
        Me.label1.Text = "Common Name"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUserType
        '
        Me.txtUserType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserType.Location = New System.Drawing.Point(128, 75)
        Me.txtUserType.Name = "txtUserType"
        Me.txtUserType.Size = New System.Drawing.Size(128, 22)
        Me.txtUserType.TabIndex = 5
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(36, 75)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(86, 23)
        Me.label5.TabIndex = 4
        Me.label5.Text = "User Type"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AttendeeControl
        '
        Me.Controls.Add(Me.pnlControls)
        Me.Name = "AttendeeControl"
        Me.Size = New System.Drawing.Size(496, 200)
        Me.Controls.SetChildIndex(Me.pnlControls, 0)
        Me.pnlControls.ResumeLayout(false)
        Me.pnlControls.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Private WithEvents pnlControls As System.Windows.Forms.Panel
    Private WithEvents txtSentBy As System.Windows.Forms.TextBox
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents txtAttendee As System.Windows.Forms.TextBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents chkRSVP As System.Windows.Forms.CheckBox
    Private WithEvents cboRole As System.Windows.Forms.ComboBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents cboStatus As System.Windows.Forms.ComboBox
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents txtCommonName As System.Windows.Forms.TextBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents txtUserType As System.Windows.Forms.TextBox
    Private WithEvents label5 As System.Windows.Forms.Label


End Class
