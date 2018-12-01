<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PhoneControl
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
        Me.chkPager = New System.Windows.Forms.CheckBox()
        Me.chkPreferred = New System.Windows.Forms.CheckBox()
        Me.chkWork = New System.Windows.Forms.CheckBox()
        Me.chkHome = New System.Windows.Forms.CheckBox()
        Me.chkCell = New System.Windows.Forms.CheckBox()
        Me.chkMessage = New System.Windows.Forms.CheckBox()
        Me.chkFax = New System.Windows.Forms.CheckBox()
        Me.chkVoice = New System.Windows.Forms.CheckBox()
        Me.txtPhoneNumber = New System.Windows.Forms.TextBox()
        Me.label12 = New System.Windows.Forms.Label()
        Me.pnlControls.SuspendLayout
        Me.SuspendLayout
        '
        'pnlControls
        '
        Me.pnlControls.Controls.Add(Me.chkPager)
        Me.pnlControls.Controls.Add(Me.chkPreferred)
        Me.pnlControls.Controls.Add(Me.chkWork)
        Me.pnlControls.Controls.Add(Me.chkHome)
        Me.pnlControls.Controls.Add(Me.chkCell)
        Me.pnlControls.Controls.Add(Me.chkMessage)
        Me.pnlControls.Controls.Add(Me.chkFax)
        Me.pnlControls.Controls.Add(Me.chkVoice)
        Me.pnlControls.Controls.Add(Me.txtPhoneNumber)
        Me.pnlControls.Controls.Add(Me.label12)
        Me.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControls.Location = New System.Drawing.Point(0, 0)
        Me.pnlControls.Name = "pnlControls"
        Me.pnlControls.Size = New System.Drawing.Size(673, 100)
        Me.pnlControls.TabIndex = 0
        '
        'chkPager
        '
        Me.chkPager.Location = New System.Drawing.Point(381, 70)
        Me.chkPager.Name = "chkPager"
        Me.chkPager.Size = New System.Drawing.Size(137, 24)
        Me.chkPager.TabIndex = 8
        Me.chkPager.Text = "Pager"
        '
        'chkPreferred
        '
        Me.chkPreferred.Location = New System.Drawing.Point(524, 70)
        Me.chkPreferred.Name = "chkPreferred"
        Me.chkPreferred.Size = New System.Drawing.Size(137, 24)
        Me.chkPreferred.TabIndex = 9
        Me.chkPreferred.Text = "Preferred"
        '
        'chkWork
        '
        Me.chkWork.Location = New System.Drawing.Point(95, 40)
        Me.chkWork.Name = "chkWork"
        Me.chkWork.Size = New System.Drawing.Size(137, 24)
        Me.chkWork.TabIndex = 2
        Me.chkWork.Text = "Work"
        '
        'chkHome
        '
        Me.chkHome.Location = New System.Drawing.Point(238, 40)
        Me.chkHome.Name = "chkHome"
        Me.chkHome.Size = New System.Drawing.Size(137, 24)
        Me.chkHome.TabIndex = 3
        Me.chkHome.Text = "Home"
        '
        'chkCell
        '
        Me.chkCell.Location = New System.Drawing.Point(238, 70)
        Me.chkCell.Name = "chkCell"
        Me.chkCell.Size = New System.Drawing.Size(137, 24)
        Me.chkCell.TabIndex = 7
        Me.chkCell.Text = "Cell"
        '
        'chkMessage
        '
        Me.chkMessage.Location = New System.Drawing.Point(95, 70)
        Me.chkMessage.Name = "chkMessage"
        Me.chkMessage.Size = New System.Drawing.Size(137, 24)
        Me.chkMessage.TabIndex = 6
        Me.chkMessage.Text = "Message"
        '
        'chkFax
        '
        Me.chkFax.Location = New System.Drawing.Point(524, 40)
        Me.chkFax.Name = "chkFax"
        Me.chkFax.Size = New System.Drawing.Size(137, 24)
        Me.chkFax.TabIndex = 5
        Me.chkFax.Text = "Fax"
        '
        'chkVoice
        '
        Me.chkVoice.Location = New System.Drawing.Point(381, 40)
        Me.chkVoice.Name = "chkVoice"
        Me.chkVoice.Size = New System.Drawing.Size(137, 24)
        Me.chkVoice.TabIndex = 4
        Me.chkVoice.Text = "Voice"
        '
        'txtPhoneNumber
        '
        Me.txtPhoneNumber.Location = New System.Drawing.Point(95, 8)
        Me.txtPhoneNumber.Name = "txtPhoneNumber"
        Me.txtPhoneNumber.Size = New System.Drawing.Size(566, 26)
        Me.txtPhoneNumber.TabIndex = 1
        '
        'label12
        '
        Me.label12.Location = New System.Drawing.Point(5, 10)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(84, 23)
        Me.label12.TabIndex = 0
        Me.label12.Text = "&Phone #"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PhoneControl
        '
        Me.Controls.Add(Me.pnlControls)
        Me.Name = "PhoneControl"
        Me.Size = New System.Drawing.Size(673, 128)
        Me.Controls.SetChildIndex(Me.pnlControls, 0)
        Me.pnlControls.ResumeLayout(false)
        Me.pnlControls.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Private WithEvents pnlControls As System.Windows.Forms.Panel
    Private WithEvents chkPager As System.Windows.Forms.CheckBox
    Private WithEvents chkPreferred As System.Windows.Forms.CheckBox
    Private WithEvents chkWork As System.Windows.Forms.CheckBox
    Private WithEvents chkHome As System.Windows.Forms.CheckBox
    Private WithEvents chkCell As System.Windows.Forms.CheckBox
    Private WithEvents chkMessage As System.Windows.Forms.CheckBox
    Private WithEvents chkFax As System.Windows.Forms.CheckBox
    Private WithEvents chkVoice As System.Windows.Forms.CheckBox
    Private WithEvents txtPhoneNumber As System.Windows.Forms.TextBox
    Private WithEvents label12 As System.Windows.Forms.Label


End Class
