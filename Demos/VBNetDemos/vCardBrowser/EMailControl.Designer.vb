<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EMailControl
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
        Me.chkPreferred = New System.Windows.Forms.CheckBox()
        Me.chkX400 = New System.Windows.Forms.CheckBox()
        Me.chkInternet = New System.Windows.Forms.CheckBox()
        Me.chkAOL = New System.Windows.Forms.CheckBox()
        Me.txtEMailAddress = New System.Windows.Forms.TextBox()
        Me.label12 = New System.Windows.Forms.Label()
        Me.pnlControls.SuspendLayout
        Me.SuspendLayout
        '
        'pnlControls
        '
        Me.pnlControls.Controls.Add(Me.chkPreferred)
        Me.pnlControls.Controls.Add(Me.chkX400)
        Me.pnlControls.Controls.Add(Me.chkInternet)
        Me.pnlControls.Controls.Add(Me.chkAOL)
        Me.pnlControls.Controls.Add(Me.txtEMailAddress)
        Me.pnlControls.Controls.Add(Me.label12)
        Me.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControls.Location = New System.Drawing.Point(0, 0)
        Me.pnlControls.Name = "pnlControls"
        Me.pnlControls.Size = New System.Drawing.Size(470, 77)
        Me.pnlControls.TabIndex = 0
        '
        'chkPreferred
        '
        Me.chkPreferred.Location = New System.Drawing.Point(346, 44)
        Me.chkPreferred.Name = "chkPreferred"
        Me.chkPreferred.Size = New System.Drawing.Size(98, 24)
        Me.chkPreferred.TabIndex = 5
        Me.chkPreferred.Text = "Preferred"
        '
        'chkX400
        '
        Me.chkX400.Location = New System.Drawing.Point(256, 44)
        Me.chkX400.Name = "chkX400"
        Me.chkX400.Size = New System.Drawing.Size(72, 24)
        Me.chkX400.TabIndex = 4
        Me.chkX400.Text = "X400"
        '
        'chkInternet
        '
        Me.chkInternet.Location = New System.Drawing.Point(136, 44)
        Me.chkInternet.Name = "chkInternet"
        Me.chkInternet.Size = New System.Drawing.Size(104, 24)
        Me.chkInternet.TabIndex = 3
        Me.chkInternet.Text = "Internet"
        '
        'chkAOL
        '
        Me.chkAOL.Location = New System.Drawing.Point(36, 44)
        Me.chkAOL.Name = "chkAOL"
        Me.chkAOL.Size = New System.Drawing.Size(84, 24)
        Me.chkAOL.TabIndex = 2
        Me.chkAOL.Text = "AOL"
        '
        'txtEMailAddress
        '
        Me.txtEMailAddress.Location = New System.Drawing.Point(76, 12)
        Me.txtEMailAddress.Name = "txtEMailAddress"
        Me.txtEMailAddress.Size = New System.Drawing.Size(368, 22)
        Me.txtEMailAddress.TabIndex = 1
        '
        'label12
        '
        Me.label12.Location = New System.Drawing.Point(15, 12)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(55, 23)
        Me.label12.TabIndex = 0
        Me.label12.Text = "&E-Mail"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'EMailControl
        '
        Me.Controls.Add(Me.pnlControls)
        Me.Name = "EMailControl"
        Me.Size = New System.Drawing.Size(470, 104)
        Me.Controls.SetChildIndex(Me.pnlControls, 0)
        Me.pnlControls.ResumeLayout(false)
        Me.pnlControls.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Private WithEvents pnlControls As System.Windows.Forms.Panel
    Private WithEvents chkPreferred As System.Windows.Forms.CheckBox
    Private WithEvents chkX400 As System.Windows.Forms.CheckBox
    Private WithEvents chkInternet As System.Windows.Forms.CheckBox
    Private WithEvents chkAOL As System.Windows.Forms.CheckBox
    Private WithEvents txtEMailAddress As System.Windows.Forms.TextBox
    Private WithEvents label12 As System.Windows.Forms.Label

End Class
