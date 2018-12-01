<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddressControl
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
        Me.btnMap = New System.Windows.Forms.Button()
        Me.chkPreferred = New System.Windows.Forms.CheckBox()
        Me.chkWork = New System.Windows.Forms.CheckBox()
        Me.chkHome = New System.Windows.Forms.CheckBox()
        Me.chkParcel = New System.Windows.Forms.CheckBox()
        Me.chkPostal = New System.Windows.Forms.CheckBox()
        Me.chkInternational = New System.Windows.Forms.CheckBox()
        Me.chkDomestic = New System.Windows.Forms.CheckBox()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.label18 = New System.Windows.Forms.Label()
        Me.txtPostalCode = New System.Windows.Forms.TextBox()
        Me.label17 = New System.Windows.Forms.Label()
        Me.txtRegion = New System.Windows.Forms.TextBox()
        Me.label16 = New System.Windows.Forms.Label()
        Me.txtLocality = New System.Windows.Forms.TextBox()
        Me.label15 = New System.Windows.Forms.Label()
        Me.txtPOBox = New System.Windows.Forms.TextBox()
        Me.label14 = New System.Windows.Forms.Label()
        Me.txtExtendedAddress = New System.Windows.Forms.TextBox()
        Me.label13 = New System.Windows.Forms.Label()
        Me.txtStreetAddress = New System.Windows.Forms.TextBox()
        Me.label12 = New System.Windows.Forms.Label()
        Me.pnlControls.SuspendLayout
        Me.SuspendLayout
        '
        'pnlControls
        '
        Me.pnlControls.Controls.Add(Me.btnMap)
        Me.pnlControls.Controls.Add(Me.chkPreferred)
        Me.pnlControls.Controls.Add(Me.chkWork)
        Me.pnlControls.Controls.Add(Me.chkHome)
        Me.pnlControls.Controls.Add(Me.chkParcel)
        Me.pnlControls.Controls.Add(Me.chkPostal)
        Me.pnlControls.Controls.Add(Me.chkInternational)
        Me.pnlControls.Controls.Add(Me.chkDomestic)
        Me.pnlControls.Controls.Add(Me.txtCountry)
        Me.pnlControls.Controls.Add(Me.label18)
        Me.pnlControls.Controls.Add(Me.txtPostalCode)
        Me.pnlControls.Controls.Add(Me.label17)
        Me.pnlControls.Controls.Add(Me.txtRegion)
        Me.pnlControls.Controls.Add(Me.label16)
        Me.pnlControls.Controls.Add(Me.txtLocality)
        Me.pnlControls.Controls.Add(Me.label15)
        Me.pnlControls.Controls.Add(Me.txtPOBox)
        Me.pnlControls.Controls.Add(Me.label14)
        Me.pnlControls.Controls.Add(Me.txtExtendedAddress)
        Me.pnlControls.Controls.Add(Me.label13)
        Me.pnlControls.Controls.Add(Me.txtStreetAddress)
        Me.pnlControls.Controls.Add(Me.label12)
        Me.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControls.Location = New System.Drawing.Point(0, 0)
        Me.pnlControls.Name = "pnlControls"
        Me.pnlControls.Size = New System.Drawing.Size(685, 212)
        Me.pnlControls.TabIndex = 0
        '
        'btnMap
        '
        Me.btnMap.Location = New System.Drawing.Point(298, 104)
        Me.btnMap.Name = "btnMap"
        Me.btnMap.Size = New System.Drawing.Size(75, 28)
        Me.btnMap.TabIndex = 14
        Me.btnMap.Text = "&Map"
        '
        'chkPreferred
        '
        Me.chkPreferred.Location = New System.Drawing.Point(397, 176)
        Me.chkPreferred.Name = "chkPreferred"
        Me.chkPreferred.Size = New System.Drawing.Size(137, 24)
        Me.chkPreferred.TabIndex = 21
        Me.chkPreferred.Text = "Preferred"
        '
        'chkWork
        '
        Me.chkWork.Location = New System.Drawing.Point(251, 176)
        Me.chkWork.Name = "chkWork"
        Me.chkWork.Size = New System.Drawing.Size(137, 24)
        Me.chkWork.TabIndex = 20
        Me.chkWork.Text = "Work"
        '
        'chkHome
        '
        Me.chkHome.Location = New System.Drawing.Point(105, 176)
        Me.chkHome.Name = "chkHome"
        Me.chkHome.Size = New System.Drawing.Size(137, 24)
        Me.chkHome.TabIndex = 19
        Me.chkHome.Text = "Home"
        '
        'chkParcel
        '
        Me.chkParcel.Location = New System.Drawing.Point(543, 146)
        Me.chkParcel.Name = "chkParcel"
        Me.chkParcel.Size = New System.Drawing.Size(137, 24)
        Me.chkParcel.TabIndex = 18
        Me.chkParcel.Text = "Parcel"
        '
        'chkPostal
        '
        Me.chkPostal.Location = New System.Drawing.Point(397, 146)
        Me.chkPostal.Name = "chkPostal"
        Me.chkPostal.Size = New System.Drawing.Size(137, 24)
        Me.chkPostal.TabIndex = 17
        Me.chkPostal.Text = "Postal"
        '
        'chkInternational
        '
        Me.chkInternational.Location = New System.Drawing.Point(251, 146)
        Me.chkInternational.Name = "chkInternational"
        Me.chkInternational.Size = New System.Drawing.Size(137, 24)
        Me.chkInternational.TabIndex = 16
        Me.chkInternational.Text = "International"
        '
        'chkDomestic
        '
        Me.chkDomestic.Location = New System.Drawing.Point(105, 146)
        Me.chkDomestic.Name = "chkDomestic"
        Me.chkDomestic.Size = New System.Drawing.Size(137, 24)
        Me.chkDomestic.TabIndex = 15
        Me.chkDomestic.Text = "Domestic"
        '
        'txtCountry
        '
        Me.txtCountry.Location = New System.Drawing.Point(105, 105)
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(168, 26)
        Me.txtCountry.TabIndex = 13
        '
        'label18
        '
        Me.label18.Location = New System.Drawing.Point(7, 107)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(92, 23)
        Me.label18.TabIndex = 12
        Me.label18.Text = "Country"
        Me.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPostalCode
        '
        Me.txtPostalCode.Location = New System.Drawing.Point(593, 73)
        Me.txtPostalCode.Name = "txtPostalCode"
        Me.txtPostalCode.Size = New System.Drawing.Size(80, 26)
        Me.txtPostalCode.TabIndex = 11
        '
        'label17
        '
        Me.label17.Location = New System.Drawing.Point(478, 75)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(109, 23)
        Me.label17.TabIndex = 10
        Me.label17.Text = "Postal Code"
        Me.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRegion
        '
        Me.txtRegion.Location = New System.Drawing.Point(392, 73)
        Me.txtRegion.Name = "txtRegion"
        Me.txtRegion.Size = New System.Drawing.Size(80, 26)
        Me.txtRegion.TabIndex = 9
        '
        'label16
        '
        Me.label16.Location = New System.Drawing.Point(279, 75)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(107, 23)
        Me.label16.TabIndex = 8
        Me.label16.Text = "State/Prov"
        Me.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLocality
        '
        Me.txtLocality.Location = New System.Drawing.Point(105, 73)
        Me.txtLocality.Name = "txtLocality"
        Me.txtLocality.Size = New System.Drawing.Size(168, 26)
        Me.txtLocality.TabIndex = 7
        '
        'label15
        '
        Me.label15.Location = New System.Drawing.Point(38, 75)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(61, 23)
        Me.label15.TabIndex = 6
        Me.label15.Text = "City"
        Me.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPOBox
        '
        Me.txtPOBox.Location = New System.Drawing.Point(565, 9)
        Me.txtPOBox.Name = "txtPOBox"
        Me.txtPOBox.Size = New System.Drawing.Size(108, 26)
        Me.txtPOBox.TabIndex = 3
        '
        'label14
        '
        Me.label14.Location = New System.Drawing.Point(482, 11)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(77, 23)
        Me.label14.TabIndex = 2
        Me.label14.Text = "PO Box"
        Me.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtExtendedAddress
        '
        Me.txtExtendedAddress.Location = New System.Drawing.Point(105, 41)
        Me.txtExtendedAddress.Name = "txtExtendedAddress"
        Me.txtExtendedAddress.Size = New System.Drawing.Size(367, 26)
        Me.txtExtendedAddress.TabIndex = 5
        '
        'label13
        '
        Me.label13.Location = New System.Drawing.Point(3, 43)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(96, 23)
        Me.label13.TabIndex = 4
        Me.label13.Text = "Address 2"
        Me.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtStreetAddress
        '
        Me.txtStreetAddress.Location = New System.Drawing.Point(105, 9)
        Me.txtStreetAddress.Name = "txtStreetAddress"
        Me.txtStreetAddress.Size = New System.Drawing.Size(367, 26)
        Me.txtStreetAddress.TabIndex = 1
        '
        'label12
        '
        Me.label12.Location = New System.Drawing.Point(3, 11)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(96, 23)
        Me.label12.TabIndex = 0
        Me.label12.Text = "&Address 1"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AddressControl
        '
        Me.Controls.Add(Me.pnlControls)
        Me.Name = "AddressControl"
        Me.Size = New System.Drawing.Size(685, 240)
        Me.Controls.SetChildIndex(Me.pnlControls, 0)
        Me.pnlControls.ResumeLayout(false)
        Me.pnlControls.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Private WithEvents pnlControls As System.Windows.Forms.Panel
    Private WithEvents btnMap As System.Windows.Forms.Button
    Private WithEvents chkPreferred As System.Windows.Forms.CheckBox
    Private WithEvents chkWork As System.Windows.Forms.CheckBox
    Private WithEvents chkHome As System.Windows.Forms.CheckBox
    Private WithEvents chkParcel As System.Windows.Forms.CheckBox
    Private WithEvents chkPostal As System.Windows.Forms.CheckBox
    Private WithEvents chkInternational As System.Windows.Forms.CheckBox
    Private WithEvents chkDomestic As System.Windows.Forms.CheckBox
    Private WithEvents txtCountry As System.Windows.Forms.TextBox
    Private WithEvents label18 As System.Windows.Forms.Label
    Private WithEvents txtPostalCode As System.Windows.Forms.TextBox
    Private WithEvents label17 As System.Windows.Forms.Label
    Private WithEvents txtRegion As System.Windows.Forms.TextBox
    Private WithEvents label16 As System.Windows.Forms.Label
    Private WithEvents txtLocality As System.Windows.Forms.TextBox
    Private WithEvents label15 As System.Windows.Forms.Label
    Private WithEvents txtPOBox As System.Windows.Forms.TextBox
    Private WithEvents label14 As System.Windows.Forms.Label
    Private WithEvents txtExtendedAddress As System.Windows.Forms.TextBox
    Private WithEvents label13 As System.Windows.Forms.Label
    Private WithEvents txtStreetAddress As System.Windows.Forms.TextBox
    Private WithEvents label12 As System.Windows.Forms.Label


End Class
