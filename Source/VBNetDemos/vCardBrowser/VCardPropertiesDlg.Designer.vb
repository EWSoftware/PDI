<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VCardPropertiesDlg
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
        Me.tabInfo = New System.Windows.Forms.TabControl()
        Me.pgName = New System.Windows.Forms.TabPage()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSortString = New System.Windows.Forms.TextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.txtFormattedName = New System.Windows.Forms.TextBox()
        Me.label7 = New System.Windows.Forms.Label()
        Me.txtNickname = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.txtSuffix = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.label6 = New System.Windows.Forms.Label()
        Me.txtMiddleName = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.pgAddresses = New System.Windows.Forms.TabPage()
        Me.ucAddresses = New vCardBrowser.AddressControl()
        Me.pgLabels = New System.Windows.Forms.TabPage()
        Me.ucLabels = New vCardBrowser.LabelControl()
        Me.pgPhoneEMail = New System.Windows.Forms.TabPage()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.ucEMail = New vCardBrowser.EMailControl()
        Me.ucPhones = New vCardBrowser.PhoneControl()
        Me.pgWork = New System.Windows.Forms.TabPage()
        Me.txtRole = New System.Windows.Forms.TextBox()
        Me.label22 = New System.Windows.Forms.Label()
        Me.groupBox7 = New System.Windows.Forms.GroupBox()
        Me.txtCategories = New System.Windows.Forms.TextBox()
        Me.label21 = New System.Windows.Forms.Label()
        Me.txtJobTitle = New System.Windows.Forms.TextBox()
        Me.label20 = New System.Windows.Forms.Label()
        Me.groupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtUnits = New System.Windows.Forms.TextBox()
        Me.label19 = New System.Windows.Forms.Label()
        Me.txtOrganization = New System.Windows.Forms.TextBox()
        Me.label18 = New System.Windows.Forms.Label()
        Me.pgOther = New System.Windows.Forms.TabPage()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.label17 = New System.Windows.Forms.Label()
        Me.groupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnWebPage = New System.Windows.Forms.Button()
        Me.txtWebPage = New System.Windows.Forms.TextBox()
        Me.label16 = New System.Windows.Forms.Label()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.label15 = New System.Windows.Forms.Label()
        Me.dtpBirthDate = New System.Windows.Forms.DateTimePicker()
        Me.txtLongitude = New System.Windows.Forms.TextBox()
        Me.label14 = New System.Windows.Forms.Label()
        Me.txtLatitude = New System.Windows.Forms.TextBox()
        Me.label13 = New System.Windows.Forms.Label()
        Me.txtTimeZone = New System.Windows.Forms.TextBox()
        Me.label12 = New System.Windows.Forms.Label()
        Me.pgPhoto = New System.Windows.Forms.TabPage()
        Me.ucPhoto = New vCardBrowser.PhotoControl()
        Me.pgLogo = New System.Windows.Forms.TabPage()
        Me.ucLogo = New vCardBrowser.PhotoControl()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtUniqueId = New System.Windows.Forms.TextBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.txtClass = New System.Windows.Forms.TextBox()
        Me.label10 = New System.Windows.Forms.Label()
        Me.txtLastRevised = New System.Windows.Forms.TextBox()
        Me.label11 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.cboVersion = New System.Windows.Forms.ComboBox()
        Me.epErrors = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.txtGenderIdentity = New System.Windows.Forms.TextBox()
        Me.label25 = New System.Windows.Forms.Label()
        Me.cboSex = New System.Windows.Forms.ComboBox()
        Me.label24 = New System.Windows.Forms.Label()
        Me.tabInfo.SuspendLayout()
        Me.pgName.SuspendLayout()
        Me.pgAddresses.SuspendLayout()
        Me.pgLabels.SuspendLayout()
        Me.pgPhoneEMail.SuspendLayout()
        Me.pgWork.SuspendLayout()
        Me.pgOther.SuspendLayout()
        Me.pgPhoto.SuspendLayout()
        Me.pgLogo.SuspendLayout()
        CType(Me.epErrors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabInfo
        '
        Me.tabInfo.Controls.Add(Me.pgName)
        Me.tabInfo.Controls.Add(Me.pgAddresses)
        Me.tabInfo.Controls.Add(Me.pgLabels)
        Me.tabInfo.Controls.Add(Me.pgPhoneEMail)
        Me.tabInfo.Controls.Add(Me.pgWork)
        Me.tabInfo.Controls.Add(Me.pgOther)
        Me.tabInfo.Controls.Add(Me.pgPhoto)
        Me.tabInfo.Controls.Add(Me.pgLogo)
        Me.tabInfo.Location = New System.Drawing.Point(12, 92)
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.SelectedIndex = 0
        Me.tabInfo.Size = New System.Drawing.Size(721, 322)
        Me.tabInfo.TabIndex = 8
        '
        'pgName
        '
        Me.pgName.Controls.Add(Me.txtGenderIdentity)
        Me.pgName.Controls.Add(Me.label25)
        Me.pgName.Controls.Add(Me.cboSex)
        Me.pgName.Controls.Add(Me.label24)
        Me.pgName.Controls.Add(Me.groupBox1)
        Me.pgName.Controls.Add(Me.txtSortString)
        Me.pgName.Controls.Add(Me.label8)
        Me.pgName.Controls.Add(Me.txtFormattedName)
        Me.pgName.Controls.Add(Me.label7)
        Me.pgName.Controls.Add(Me.txtNickname)
        Me.pgName.Controls.Add(Me.label4)
        Me.pgName.Controls.Add(Me.txtSuffix)
        Me.pgName.Controls.Add(Me.label5)
        Me.pgName.Controls.Add(Me.txtTitle)
        Me.pgName.Controls.Add(Me.label6)
        Me.pgName.Controls.Add(Me.txtMiddleName)
        Me.pgName.Controls.Add(Me.label3)
        Me.pgName.Controls.Add(Me.txtFirstName)
        Me.pgName.Controls.Add(Me.label2)
        Me.pgName.Controls.Add(Me.txtLastName)
        Me.pgName.Controls.Add(Me.label1)
        Me.pgName.Location = New System.Drawing.Point(4, 29)
        Me.pgName.Name = "pgName"
        Me.pgName.Size = New System.Drawing.Size(713, 289)
        Me.pgName.TabIndex = 0
        Me.pgName.Text = "Name"
        Me.pgName.UseVisualStyleBackColor = True
        '
        'groupBox1
        '
        Me.groupBox1.Location = New System.Drawing.Point(16, 174)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(680, 8)
        Me.groupBox1.TabIndex = 16
        Me.groupBox1.TabStop = False
        '
        'txtSortString
        '
        Me.txtSortString.Location = New System.Drawing.Point(525, 88)
        Me.txtSortString.Name = "txtSortString"
        Me.txtSortString.Size = New System.Drawing.Size(128, 26)
        Me.txtSortString.TabIndex = 11
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(410, 90)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(109, 23)
        Me.label8.TabIndex = 10
        Me.label8.Text = "Sort String"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFormattedName
        '
        Me.txtFormattedName.Location = New System.Drawing.Point(181, 231)
        Me.txtFormattedName.Name = "txtFormattedName"
        Me.txtFormattedName.Size = New System.Drawing.Size(336, 26)
        Me.txtFormattedName.TabIndex = 20
        '
        'label7
        '
        Me.label7.Location = New System.Drawing.Point(16, 233)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(159, 23)
        Me.label7.TabIndex = 19
        Me.label7.Text = "&Formatted Name"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNickname
        '
        Me.txtNickname.Location = New System.Drawing.Point(181, 199)
        Me.txtNickname.Name = "txtNickname"
        Me.txtNickname.Size = New System.Drawing.Size(176, 26)
        Me.txtNickname.TabIndex = 18
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(65, 201)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(110, 23)
        Me.label4.TabIndex = 17
        Me.label4.Text = "&Nickname"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSuffix
        '
        Me.txtSuffix.Location = New System.Drawing.Point(525, 56)
        Me.txtSuffix.Name = "txtSuffix"
        Me.txtSuffix.Size = New System.Drawing.Size(128, 26)
        Me.txtSuffix.TabIndex = 9
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(437, 58)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(82, 23)
        Me.label5.TabIndex = 8
        Me.label5.Text = "Suffix"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(525, 24)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(128, 26)
        Me.txtTitle.TabIndex = 7
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(433, 26)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(86, 23)
        Me.label6.TabIndex = 6
        Me.label6.Text = "Title"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMiddleName
        '
        Me.txtMiddleName.Location = New System.Drawing.Point(181, 88)
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.Size = New System.Drawing.Size(208, 26)
        Me.txtMiddleName.TabIndex = 5
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(61, 90)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(114, 23)
        Me.label3.TabIndex = 4
        Me.label3.Text = "Middle Name"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(181, 56)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(208, 26)
        Me.txtFirstName.TabIndex = 3
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(61, 56)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(114, 23)
        Me.label2.TabIndex = 2
        Me.label2.Text = "First Name"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(181, 24)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(208, 26)
        Me.txtLastName.TabIndex = 1
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(61, 26)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(114, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Last Name"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pgAddresses
        '
        Me.pgAddresses.Controls.Add(Me.ucAddresses)
        Me.pgAddresses.Location = New System.Drawing.Point(4, 29)
        Me.pgAddresses.Name = "pgAddresses"
        Me.pgAddresses.Size = New System.Drawing.Size(713, 289)
        Me.pgAddresses.TabIndex = 1
        Me.pgAddresses.Text = "Addresses"
        Me.pgAddresses.UseVisualStyleBackColor = True
        '
        'ucAddresses
        '
        Me.ucAddresses.Location = New System.Drawing.Point(9, 20)
        Me.ucAddresses.Name = "ucAddresses"
        Me.ucAddresses.Size = New System.Drawing.Size(690, 248)
        Me.ucAddresses.TabIndex = 15
        '
        'pgLabels
        '
        Me.pgLabels.Controls.Add(Me.ucLabels)
        Me.pgLabels.Location = New System.Drawing.Point(4, 29)
        Me.pgLabels.Name = "pgLabels"
        Me.pgLabels.Size = New System.Drawing.Size(713, 289)
        Me.pgLabels.TabIndex = 7
        Me.pgLabels.Text = "Labels"
        Me.pgLabels.UseVisualStyleBackColor = True
        '
        'ucLabels
        '
        Me.ucLabels.Location = New System.Drawing.Point(18, 40)
        Me.ucLabels.Name = "ucLabels"
        Me.ucLabels.Size = New System.Drawing.Size(672, 208)
        Me.ucLabels.TabIndex = 0
        '
        'pgPhoneEMail
        '
        Me.pgPhoneEMail.Controls.Add(Me.groupBox2)
        Me.pgPhoneEMail.Controls.Add(Me.ucEMail)
        Me.pgPhoneEMail.Controls.Add(Me.ucPhones)
        Me.pgPhoneEMail.Location = New System.Drawing.Point(4, 29)
        Me.pgPhoneEMail.Name = "pgPhoneEMail"
        Me.pgPhoneEMail.Size = New System.Drawing.Size(713, 289)
        Me.pgPhoneEMail.TabIndex = 3
        Me.pgPhoneEMail.Text = "Phone/E-Mail"
        Me.pgPhoneEMail.UseVisualStyleBackColor = True
        '
        'groupBox2
        '
        Me.groupBox2.Location = New System.Drawing.Point(9, 157)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(694, 8)
        Me.groupBox2.TabIndex = 2
        Me.groupBox2.TabStop = False
        '
        'ucEMail
        '
        Me.ucEMail.Location = New System.Drawing.Point(8, 173)
        Me.ucEMail.Name = "ucEMail"
        Me.ucEMail.Size = New System.Drawing.Size(689, 110)
        Me.ucEMail.TabIndex = 1
        '
        'ucPhones
        '
        Me.ucPhones.Location = New System.Drawing.Point(8, 8)
        Me.ucPhones.Name = "ucPhones"
        Me.ucPhones.Size = New System.Drawing.Size(689, 143)
        Me.ucPhones.TabIndex = 0
        '
        'pgWork
        '
        Me.pgWork.Controls.Add(Me.txtRole)
        Me.pgWork.Controls.Add(Me.label22)
        Me.pgWork.Controls.Add(Me.groupBox7)
        Me.pgWork.Controls.Add(Me.txtCategories)
        Me.pgWork.Controls.Add(Me.label21)
        Me.pgWork.Controls.Add(Me.txtJobTitle)
        Me.pgWork.Controls.Add(Me.label20)
        Me.pgWork.Controls.Add(Me.groupBox6)
        Me.pgWork.Controls.Add(Me.txtUnits)
        Me.pgWork.Controls.Add(Me.label19)
        Me.pgWork.Controls.Add(Me.txtOrganization)
        Me.pgWork.Controls.Add(Me.label18)
        Me.pgWork.Location = New System.Drawing.Point(4, 29)
        Me.pgWork.Name = "pgWork"
        Me.pgWork.Size = New System.Drawing.Size(713, 289)
        Me.pgWork.TabIndex = 2
        Me.pgWork.Text = "Work"
        Me.pgWork.UseVisualStyleBackColor = True
        '
        'txtRole
        '
        Me.txtRole.Location = New System.Drawing.Point(170, 137)
        Me.txtRole.Name = "txtRole"
        Me.txtRole.Size = New System.Drawing.Size(320, 26)
        Me.txtRole.TabIndex = 8
        '
        'label22
        '
        Me.label22.Location = New System.Drawing.Point(71, 139)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(93, 23)
        Me.label22.TabIndex = 7
        Me.label22.Text = "Role"
        Me.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'groupBox7
        '
        Me.groupBox7.Location = New System.Drawing.Point(14, 168)
        Me.groupBox7.Name = "groupBox7"
        Me.groupBox7.Size = New System.Drawing.Size(684, 8)
        Me.groupBox7.TabIndex = 9
        Me.groupBox7.TabStop = False
        '
        'txtCategories
        '
        Me.txtCategories.Location = New System.Drawing.Point(170, 193)
        Me.txtCategories.Name = "txtCategories"
        Me.txtCategories.Size = New System.Drawing.Size(440, 26)
        Me.txtCategories.TabIndex = 11
        '
        'label21
        '
        Me.label21.Location = New System.Drawing.Point(42, 195)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(122, 23)
        Me.label21.TabIndex = 10
        Me.label21.Text = "&Categories"
        Me.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtJobTitle
        '
        Me.txtJobTitle.Location = New System.Drawing.Point(170, 105)
        Me.txtJobTitle.Name = "txtJobTitle"
        Me.txtJobTitle.Size = New System.Drawing.Size(320, 26)
        Me.txtJobTitle.TabIndex = 6
        '
        'label20
        '
        Me.label20.Location = New System.Drawing.Point(67, 107)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(97, 23)
        Me.label20.TabIndex = 5
        Me.label20.Text = "&Job Title"
        Me.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'groupBox6
        '
        Me.groupBox6.Location = New System.Drawing.Point(14, 80)
        Me.groupBox6.Name = "groupBox6"
        Me.groupBox6.Size = New System.Drawing.Size(684, 8)
        Me.groupBox6.TabIndex = 4
        Me.groupBox6.TabStop = False
        '
        'txtUnits
        '
        Me.txtUnits.Location = New System.Drawing.Point(170, 48)
        Me.txtUnits.Name = "txtUnits"
        Me.txtUnits.Size = New System.Drawing.Size(440, 26)
        Me.txtUnits.TabIndex = 3
        '
        'label19
        '
        Me.label19.Location = New System.Drawing.Point(88, 50)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(76, 23)
        Me.label19.TabIndex = 2
        Me.label19.Text = "Units"
        Me.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOrganization
        '
        Me.txtOrganization.Location = New System.Drawing.Point(170, 16)
        Me.txtOrganization.Name = "txtOrganization"
        Me.txtOrganization.Size = New System.Drawing.Size(320, 26)
        Me.txtOrganization.TabIndex = 1
        '
        'label18
        '
        Me.label18.Location = New System.Drawing.Point(42, 18)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(122, 23)
        Me.label18.TabIndex = 0
        Me.label18.Text = "&Organization"
        Me.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pgOther
        '
        Me.pgOther.Controls.Add(Me.btnFind)
        Me.pgOther.Controls.Add(Me.txtComments)
        Me.pgOther.Controls.Add(Me.label17)
        Me.pgOther.Controls.Add(Me.groupBox5)
        Me.pgOther.Controls.Add(Me.btnWebPage)
        Me.pgOther.Controls.Add(Me.txtWebPage)
        Me.pgOther.Controls.Add(Me.label16)
        Me.pgOther.Controls.Add(Me.groupBox4)
        Me.pgOther.Controls.Add(Me.groupBox3)
        Me.pgOther.Controls.Add(Me.label15)
        Me.pgOther.Controls.Add(Me.dtpBirthDate)
        Me.pgOther.Controls.Add(Me.txtLongitude)
        Me.pgOther.Controls.Add(Me.label14)
        Me.pgOther.Controls.Add(Me.txtLatitude)
        Me.pgOther.Controls.Add(Me.label13)
        Me.pgOther.Controls.Add(Me.txtTimeZone)
        Me.pgOther.Controls.Add(Me.label12)
        Me.pgOther.Location = New System.Drawing.Point(4, 29)
        Me.pgOther.Name = "pgOther"
        Me.pgOther.Size = New System.Drawing.Size(713, 289)
        Me.pgOther.TabIndex = 4
        Me.pgOther.Text = "Other"
        Me.pgOther.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(542, 95)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(75, 28)
        Me.btnFind.TabIndex = 9
        Me.btnFind.Text = "&Find"
        '
        'txtComments
        '
        Me.txtComments.AcceptsReturn = True
        Me.txtComments.Location = New System.Drawing.Point(150, 198)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(528, 72)
        Me.txtComments.TabIndex = 16
        '
        'label17
        '
        Me.label17.Location = New System.Drawing.Point(34, 200)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(110, 23)
        Me.label17.TabIndex = 15
        Me.label17.Text = "&Comments"
        Me.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'groupBox5
        '
        Me.groupBox5.Location = New System.Drawing.Point(17, 182)
        Me.groupBox5.Name = "groupBox5"
        Me.groupBox5.Size = New System.Drawing.Size(688, 8)
        Me.groupBox5.TabIndex = 14
        Me.groupBox5.TabStop = False
        '
        'btnWebPage
        '
        Me.btnWebPage.Location = New System.Drawing.Point(542, 149)
        Me.btnWebPage.Name = "btnWebPage"
        Me.btnWebPage.Size = New System.Drawing.Size(75, 28)
        Me.btnWebPage.TabIndex = 13
        Me.btnWebPage.Text = "&Go"
        '
        'txtWebPage
        '
        Me.txtWebPage.Location = New System.Drawing.Point(150, 150)
        Me.txtWebPage.Name = "txtWebPage"
        Me.txtWebPage.Size = New System.Drawing.Size(368, 26)
        Me.txtWebPage.TabIndex = 12
        '
        'label16
        '
        Me.label16.Location = New System.Drawing.Point(34, 150)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(110, 23)
        Me.label16.TabIndex = 11
        Me.label16.Text = "&Web Page"
        Me.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'groupBox4
        '
        Me.groupBox4.Location = New System.Drawing.Point(17, 126)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(688, 8)
        Me.groupBox4.TabIndex = 10
        Me.groupBox4.TabStop = False
        '
        'groupBox3
        '
        Me.groupBox3.Location = New System.Drawing.Point(17, 46)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(688, 8)
        Me.groupBox3.TabIndex = 2
        Me.groupBox3.TabStop = False
        '
        'label15
        '
        Me.label15.Location = New System.Drawing.Point(38, 14)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(106, 23)
        Me.label15.TabIndex = 0
        Me.label15.Text = "&Birth Date"
        Me.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpBirthDate
        '
        Me.dtpBirthDate.Checked = False
        Me.dtpBirthDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBirthDate.Location = New System.Drawing.Point(150, 14)
        Me.dtpBirthDate.Name = "dtpBirthDate"
        Me.dtpBirthDate.ShowCheckBox = True
        Me.dtpBirthDate.Size = New System.Drawing.Size(255, 26)
        Me.dtpBirthDate.TabIndex = 1
        '
        'txtLongitude
        '
        Me.txtLongitude.Location = New System.Drawing.Point(398, 96)
        Me.txtLongitude.Name = "txtLongitude"
        Me.txtLongitude.Size = New System.Drawing.Size(120, 26)
        Me.txtLongitude.TabIndex = 8
        '
        'label14
        '
        Me.label14.Location = New System.Drawing.Point(290, 98)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(102, 23)
        Me.label14.TabIndex = 7
        Me.label14.Text = "Longitude"
        Me.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLatitude
        '
        Me.txtLatitude.Location = New System.Drawing.Point(150, 96)
        Me.txtLatitude.Name = "txtLatitude"
        Me.txtLatitude.Size = New System.Drawing.Size(120, 26)
        Me.txtLatitude.TabIndex = 6
        '
        'label13
        '
        Me.label13.Location = New System.Drawing.Point(51, 96)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(93, 23)
        Me.label13.TabIndex = 5
        Me.label13.Text = "&Latitude"
        Me.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTimeZone
        '
        Me.txtTimeZone.Location = New System.Drawing.Point(150, 64)
        Me.txtTimeZone.Name = "txtTimeZone"
        Me.txtTimeZone.Size = New System.Drawing.Size(368, 26)
        Me.txtTimeZone.TabIndex = 4
        '
        'label12
        '
        Me.label12.Location = New System.Drawing.Point(34, 66)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(110, 23)
        Me.label12.TabIndex = 3
        Me.label12.Text = "&Time Zone"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pgPhoto
        '
        Me.pgPhoto.Controls.Add(Me.ucPhoto)
        Me.pgPhoto.Location = New System.Drawing.Point(4, 29)
        Me.pgPhoto.Name = "pgPhoto"
        Me.pgPhoto.Size = New System.Drawing.Size(713, 289)
        Me.pgPhoto.TabIndex = 5
        Me.pgPhoto.Text = "Photo"
        Me.pgPhoto.UseVisualStyleBackColor = True
        '
        'ucPhoto
        '
        Me.ucPhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.ucPhoto.ImageType = Nothing
        Me.ucPhoto.Location = New System.Drawing.Point(22, 40)
        Me.ucPhoto.Name = "ucPhoto"
        Me.ucPhoto.Size = New System.Drawing.Size(664, 208)
        Me.ucPhoto.TabIndex = 0
        '
        'pgLogo
        '
        Me.pgLogo.Controls.Add(Me.ucLogo)
        Me.pgLogo.Location = New System.Drawing.Point(4, 29)
        Me.pgLogo.Name = "pgLogo"
        Me.pgLogo.Size = New System.Drawing.Size(713, 289)
        Me.pgLogo.TabIndex = 6
        Me.pgLogo.Text = "Logo"
        Me.pgLogo.UseVisualStyleBackColor = True
        '
        'ucLogo
        '
        Me.ucLogo.Cursor = System.Windows.Forms.Cursors.Default
        Me.ucLogo.ImageType = Nothing
        Me.ucLogo.Location = New System.Drawing.Point(22, 40)
        Me.ucLogo.Name = "ucLogo"
        Me.ucLogo.Size = New System.Drawing.Size(664, 208)
        Me.ucLogo.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(12, 424)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 32)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(645, 424)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 32)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        '
        'txtUniqueId
        '
        Me.txtUniqueId.Location = New System.Drawing.Point(115, 12)
        Me.txtUniqueId.Name = "txtUniqueId"
        Me.txtUniqueId.ReadOnly = True
        Me.txtUniqueId.Size = New System.Drawing.Size(376, 26)
        Me.txtUniqueId.TabIndex = 1
        Me.txtUniqueId.TabStop = False
        '
        'label9
        '
        Me.label9.Location = New System.Drawing.Point(12, 14)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(97, 23)
        Me.label9.TabIndex = 0
        Me.label9.Text = "Unique ID"
        Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtClass
        '
        Me.txtClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtClass.Location = New System.Drawing.Point(584, 46)
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(145, 26)
        Me.txtClass.TabIndex = 7
        '
        'label10
        '
        Me.label10.Location = New System.Drawing.Point(500, 48)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(78, 23)
        Me.label10.TabIndex = 6
        Me.label10.Text = "Class"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLastRevised
        '
        Me.txtLastRevised.Location = New System.Drawing.Point(115, 46)
        Me.txtLastRevised.Name = "txtLastRevised"
        Me.txtLastRevised.ReadOnly = True
        Me.txtLastRevised.Size = New System.Drawing.Size(207, 26)
        Me.txtLastRevised.TabIndex = 5
        Me.txtLastRevised.TabStop = False
        '
        'label11
        '
        Me.label11.Location = New System.Drawing.Point(16, 48)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(93, 23)
        Me.label11.TabIndex = 4
        Me.label11.Text = "Revised"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label23
        '
        Me.label23.Location = New System.Drawing.Point(498, 14)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(80, 23)
        Me.label23.TabIndex = 2
        Me.label23.Text = "&Version"
        Me.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboVersion
        '
        Me.cboVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVersion.Items.AddRange(New Object() {"2.1", "3.0", "4.0"})
        Me.cboVersion.Location = New System.Drawing.Point(584, 12)
        Me.cboVersion.Name = "cboVersion"
        Me.cboVersion.Size = New System.Drawing.Size(65, 28)
        Me.cboVersion.TabIndex = 3
        '
        'epErrors
        '
        Me.epErrors.ContainerControl = Me
        '
        'txtGenderIdentity
        '
        Me.txtGenderIdentity.Location = New System.Drawing.Point(438, 132)
        Me.txtGenderIdentity.Name = "txtGenderIdentity"
        Me.txtGenderIdentity.Size = New System.Drawing.Size(195, 26)
        Me.txtGenderIdentity.TabIndex = 15
        '
        'label25
        '
        Me.label25.Location = New System.Drawing.Point(289, 134)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(143, 23)
        Me.label25.TabIndex = 14
        Me.label25.Text = "Gender Identity"
        Me.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboSex
        '
        Me.cboSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSex.Location = New System.Drawing.Point(181, 134)
        Me.cboSex.Name = "cboSex"
        Me.cboSex.Size = New System.Drawing.Size(101, 28)
        Me.cboSex.TabIndex = 13
        '
        'label24
        '
        Me.label24.Location = New System.Drawing.Point(118, 136)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(57, 23)
        Me.label24.TabIndex = 12
        Me.label24.Text = "&Sex"
        Me.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'VCardPropertiesDlg
        '
        Me.AcceptButton = Me.btnOK
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(745, 468)
        Me.Controls.Add(Me.cboVersion)
        Me.Controls.Add(Me.label23)
        Me.Controls.Add(Me.txtLastRevised)
        Me.Controls.Add(Me.txtClass)
        Me.Controls.Add(Me.txtUniqueId)
        Me.Controls.Add(Me.label11)
        Me.Controls.Add(Me.label10)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.tabInfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "VCardPropertiesDlg"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit vCard Information"
        Me.tabInfo.ResumeLayout(false)
        Me.pgName.ResumeLayout(false)
        Me.pgName.PerformLayout
        Me.pgAddresses.ResumeLayout(false)
        Me.pgLabels.ResumeLayout(false)
        Me.pgPhoneEMail.ResumeLayout(false)
        Me.pgWork.ResumeLayout(false)
        Me.pgWork.PerformLayout
        Me.pgOther.ResumeLayout(false)
        Me.pgOther.PerformLayout
        Me.pgPhoto.ResumeLayout(false)
        Me.pgLogo.ResumeLayout(false)
        CType(Me.epErrors,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents tabInfo As System.Windows.Forms.TabControl
    Friend WithEvents pgOther As System.Windows.Forms.TabPage
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents txtMiddleName As System.Windows.Forms.TextBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents txtNickname As System.Windows.Forms.TextBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents txtSuffix As System.Windows.Forms.TextBox
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents txtFormattedName As System.Windows.Forms.TextBox
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents txtSortString As System.Windows.Forms.TextBox
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents txtUniqueId As System.Windows.Forms.TextBox
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents txtClass As System.Windows.Forms.TextBox
    Friend WithEvents label10 As System.Windows.Forms.Label
    Friend WithEvents txtLastRevised As System.Windows.Forms.TextBox
    Friend WithEvents label11 As System.Windows.Forms.Label
    Friend WithEvents ucAddresses As vCardBrowser.AddressControl
    Friend WithEvents pgPhoto As System.Windows.Forms.TabPage
    Friend WithEvents pgLogo As System.Windows.Forms.TabPage
    Friend WithEvents pgName As System.Windows.Forms.TabPage
    Friend WithEvents pgWork As System.Windows.Forms.TabPage
    Friend WithEvents ucPhoto As vCardBrowser.PhotoControl
    Friend WithEvents ucLogo As vCardBrowser.PhotoControl
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ucEMail As vCardBrowser.EMailControl
    Friend WithEvents ucPhones As vCardBrowser.PhoneControl
    Friend WithEvents txtTimeZone As System.Windows.Forms.TextBox
    Friend WithEvents label12 As System.Windows.Forms.Label
    Friend WithEvents txtLatitude As System.Windows.Forms.TextBox
    Friend WithEvents label13 As System.Windows.Forms.Label
    Friend WithEvents txtLongitude As System.Windows.Forms.TextBox
    Friend WithEvents label14 As System.Windows.Forms.Label
    Friend WithEvents dtpBirthDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents label15 As System.Windows.Forms.Label
    Friend WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents groupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtWebPage As System.Windows.Forms.TextBox
    Friend WithEvents label16 As System.Windows.Forms.Label
    Friend WithEvents btnWebPage As System.Windows.Forms.Button
    Friend WithEvents groupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents label17 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents txtOrganization As System.Windows.Forms.TextBox
    Friend WithEvents label18 As System.Windows.Forms.Label
    Friend WithEvents txtUnits As System.Windows.Forms.TextBox
    Friend WithEvents label19 As System.Windows.Forms.Label
    Friend WithEvents groupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtJobTitle As System.Windows.Forms.TextBox
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents groupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCategories As System.Windows.Forms.TextBox
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents txtRole As System.Windows.Forms.TextBox
    Friend WithEvents label22 As System.Windows.Forms.Label
    Friend WithEvents label23 As System.Windows.Forms.Label
    Friend WithEvents cboVersion As System.Windows.Forms.ComboBox
    Friend WithEvents pgAddresses As System.Windows.Forms.TabPage
    Friend WithEvents pgPhoneEMail As System.Windows.Forms.TabPage
    Friend WithEvents pgLabels As System.Windows.Forms.TabPage
    Friend WithEvents ucLabels As vCardBrowser.LabelControl
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents epErrors As System.Windows.Forms.ErrorProvider
    Private WithEvents txtGenderIdentity As TextBox
    Private WithEvents label25 As Label
    Private WithEvents cboSex As ComboBox
    Private WithEvents label24 As Label
End Class
