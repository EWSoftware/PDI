<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LabelControl
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
        Me.chkWork = New System.Windows.Forms.CheckBox()
        Me.chkHome = New System.Windows.Forms.CheckBox()
        Me.chkParcel = New System.Windows.Forms.CheckBox()
        Me.chkPostal = New System.Windows.Forms.CheckBox()
        Me.chkInternational = New System.Windows.Forms.CheckBox()
        Me.chkDomestic = New System.Windows.Forms.CheckBox()
        Me.txtLabelText = New System.Windows.Forms.TextBox()
        Me.label12 = New System.Windows.Forms.Label()
        Me.pnlControls.SuspendLayout
        Me.SuspendLayout
        '
        'pnlControls
        '
        Me.pnlControls.Controls.Add(Me.chkPreferred)
        Me.pnlControls.Controls.Add(Me.chkWork)
        Me.pnlControls.Controls.Add(Me.chkHome)
        Me.pnlControls.Controls.Add(Me.chkParcel)
        Me.pnlControls.Controls.Add(Me.chkPostal)
        Me.pnlControls.Controls.Add(Me.chkInternational)
        Me.pnlControls.Controls.Add(Me.chkDomestic)
        Me.pnlControls.Controls.Add(Me.txtLabelText)
        Me.pnlControls.Controls.Add(Me.label12)
        Me.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControls.Location = New System.Drawing.Point(0, 0)
        Me.pnlControls.Name = "pnlControls"
        Me.pnlControls.Size = New System.Drawing.Size(464, 173)
        Me.pnlControls.TabIndex = 0
        '
        'chkPreferred
        '
        Me.chkPreferred.Location = New System.Drawing.Point(286, 140)
        Me.chkPreferred.Name = "chkPreferred"
        Me.chkPreferred.Size = New System.Drawing.Size(96, 24)
        Me.chkPreferred.TabIndex = 8
        Me.chkPreferred.Text = "Preferred"
        '
        'chkWork
        '
        Me.chkWork.Location = New System.Drawing.Point(166, 140)
        Me.chkWork.Name = "chkWork"
        Me.chkWork.Size = New System.Drawing.Size(72, 24)
        Me.chkWork.TabIndex = 7
        Me.chkWork.Text = "Work"
        '
        'chkHome
        '
        Me.chkHome.Location = New System.Drawing.Point(70, 140)
        Me.chkHome.Name = "chkHome"
        Me.chkHome.Size = New System.Drawing.Size(78, 24)
        Me.chkHome.TabIndex = 6
        Me.chkHome.Text = "Home"
        '
        'chkParcel
        '
        Me.chkParcel.Location = New System.Drawing.Point(372, 108)
        Me.chkParcel.Name = "chkParcel"
        Me.chkParcel.Size = New System.Drawing.Size(78, 24)
        Me.chkParcel.TabIndex = 5
        Me.chkParcel.Text = "Parcel"
        '
        'chkPostal
        '
        Me.chkPostal.Location = New System.Drawing.Point(286, 108)
        Me.chkPostal.Name = "chkPostal"
        Me.chkPostal.Size = New System.Drawing.Size(80, 24)
        Me.chkPostal.TabIndex = 4
        Me.chkPostal.Text = "Postal"
        '
        'chkInternational
        '
        Me.chkInternational.Location = New System.Drawing.Point(166, 108)
        Me.chkInternational.Name = "chkInternational"
        Me.chkInternational.Size = New System.Drawing.Size(114, 24)
        Me.chkInternational.TabIndex = 3
        Me.chkInternational.Text = "International"
        '
        'chkDomestic
        '
        Me.chkDomestic.Location = New System.Drawing.Point(70, 108)
        Me.chkDomestic.Name = "chkDomestic"
        Me.chkDomestic.Size = New System.Drawing.Size(90, 24)
        Me.chkDomestic.TabIndex = 2
        Me.chkDomestic.Text = "Domestic"
        '
        'txtLabelText
        '
        Me.txtLabelText.AcceptsReturn = true
        Me.txtLabelText.Location = New System.Drawing.Point(70, 12)
        Me.txtLabelText.Multiline = true
        Me.txtLabelText.Name = "txtLabelText"
        Me.txtLabelText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLabelText.Size = New System.Drawing.Size(368, 88)
        Me.txtLabelText.TabIndex = 1
        '
        'label12
        '
        Me.label12.Location = New System.Drawing.Point(14, 12)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(48, 23)
        Me.label12.TabIndex = 0
        Me.label12.Text = "&Label"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelControl
        '
        Me.Controls.Add(Me.pnlControls)
        Me.Name = "LabelControl"
        Me.Size = New System.Drawing.Size(464, 200)
        Me.Controls.SetChildIndex(Me.pnlControls, 0)
        Me.pnlControls.ResumeLayout(false)
        Me.pnlControls.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Private WithEvents pnlControls As System.Windows.Forms.Panel
    Private WithEvents chkPreferred As System.Windows.Forms.CheckBox
    Private WithEvents chkWork As System.Windows.Forms.CheckBox
    Private WithEvents chkHome As System.Windows.Forms.CheckBox
    Private WithEvents chkParcel As System.Windows.Forms.CheckBox
    Private WithEvents chkPostal As System.Windows.Forms.CheckBox
    Private WithEvents chkInternational As System.Windows.Forms.CheckBox
    Private WithEvents chkDomestic As System.Windows.Forms.CheckBox
    Private WithEvents txtLabelText As System.Windows.Forms.TextBox
    Private WithEvents label12 As System.Windows.Forms.Label


End Class
