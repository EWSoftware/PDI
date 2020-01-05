namespace vCardBrowser
{
    partial class VCardPropertiesDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.pgName = new System.Windows.Forms.TabPage();
            this.txtGenderIdentity = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cboSex = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSortString = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFormattedName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNickname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pgAddresses = new System.Windows.Forms.TabPage();
            this.ucAddresses = new vCardBrowser.AddressControl();
            this.pgLabels = new System.Windows.Forms.TabPage();
            this.ucLabels = new vCardBrowser.LabelControl();
            this.pgPhoneEMail = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucEMail = new vCardBrowser.EMailControl();
            this.ucPhones = new vCardBrowser.PhoneControl();
            this.pgWork = new System.Windows.Forms.TabPage();
            this.txtRole = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtCategories = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtJobTitle = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtUnits = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtOrganization = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.pgOther = new System.Windows.Forms.TabPage();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnWebPage = new System.Windows.Forms.Button();
            this.txtWebPage = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTimeZone = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pgPhoto = new System.Windows.Forms.TabPage();
            this.ucPhoto = new vCardBrowser.PhotoControl();
            this.pgLogo = new System.Windows.Forms.TabPage();
            this.ucLogo = new vCardBrowser.PhotoControl();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtUniqueId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLastRevised = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cboVersion = new System.Windows.Forms.ComboBox();
            this.epErrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabInfo.SuspendLayout();
            this.pgName.SuspendLayout();
            this.pgAddresses.SuspendLayout();
            this.pgLabels.SuspendLayout();
            this.pgPhoneEMail.SuspendLayout();
            this.pgWork.SuspendLayout();
            this.pgOther.SuspendLayout();
            this.pgPhoto.SuspendLayout();
            this.pgLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.pgName);
            this.tabInfo.Controls.Add(this.pgAddresses);
            this.tabInfo.Controls.Add(this.pgLabels);
            this.tabInfo.Controls.Add(this.pgPhoneEMail);
            this.tabInfo.Controls.Add(this.pgWork);
            this.tabInfo.Controls.Add(this.pgOther);
            this.tabInfo.Controls.Add(this.pgPhoto);
            this.tabInfo.Controls.Add(this.pgLogo);
            this.tabInfo.Location = new System.Drawing.Point(12, 92);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(721, 322);
            this.tabInfo.TabIndex = 8;
            // 
            // pgName
            // 
            this.pgName.Controls.Add(this.txtGenderIdentity);
            this.pgName.Controls.Add(this.label25);
            this.pgName.Controls.Add(this.cboSex);
            this.pgName.Controls.Add(this.label24);
            this.pgName.Controls.Add(this.groupBox1);
            this.pgName.Controls.Add(this.txtSortString);
            this.pgName.Controls.Add(this.label8);
            this.pgName.Controls.Add(this.txtFormattedName);
            this.pgName.Controls.Add(this.label7);
            this.pgName.Controls.Add(this.txtNickname);
            this.pgName.Controls.Add(this.label4);
            this.pgName.Controls.Add(this.txtSuffix);
            this.pgName.Controls.Add(this.label5);
            this.pgName.Controls.Add(this.txtTitle);
            this.pgName.Controls.Add(this.label6);
            this.pgName.Controls.Add(this.txtMiddleName);
            this.pgName.Controls.Add(this.label3);
            this.pgName.Controls.Add(this.txtFirstName);
            this.pgName.Controls.Add(this.label2);
            this.pgName.Controls.Add(this.txtLastName);
            this.pgName.Controls.Add(this.label1);
            this.pgName.Location = new System.Drawing.Point(4, 29);
            this.pgName.Name = "pgName";
            this.pgName.Size = new System.Drawing.Size(713, 289);
            this.pgName.TabIndex = 0;
            this.pgName.Text = "Name";
            this.pgName.UseVisualStyleBackColor = true;
            // 
            // txtGenderIdentity
            // 
            this.txtGenderIdentity.Location = new System.Drawing.Point(438, 132);
            this.txtGenderIdentity.Name = "txtGenderIdentity";
            this.txtGenderIdentity.Size = new System.Drawing.Size(195, 26);
            this.txtGenderIdentity.TabIndex = 15;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(289, 134);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(143, 23);
            this.label25.TabIndex = 14;
            this.label25.Text = "Gender Identity";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSex
            // 
            this.cboSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSex.Location = new System.Drawing.Point(181, 134);
            this.cboSex.Name = "cboSex";
            this.cboSex.Size = new System.Drawing.Size(101, 28);
            this.cboSex.TabIndex = 13;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(118, 136);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(57, 23);
            this.label24.TabIndex = 12;
            this.label24.Text = "&Sex";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(16, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 8);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // txtSortString
            // 
            this.txtSortString.Location = new System.Drawing.Point(525, 88);
            this.txtSortString.Name = "txtSortString";
            this.txtSortString.Size = new System.Drawing.Size(128, 26);
            this.txtSortString.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(410, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 23);
            this.label8.TabIndex = 10;
            this.label8.Text = "Sort String";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFormattedName
            // 
            this.txtFormattedName.Location = new System.Drawing.Point(181, 231);
            this.txtFormattedName.Name = "txtFormattedName";
            this.txtFormattedName.Size = new System.Drawing.Size(336, 26);
            this.txtFormattedName.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 23);
            this.label7.TabIndex = 19;
            this.label7.Text = "&Formatted Name";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNickname
            // 
            this.txtNickname.Location = new System.Drawing.Point(181, 199);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Size = new System.Drawing.Size(176, 26);
            this.txtNickname.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(65, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 17;
            this.label4.Text = "&Nickname";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(525, 56);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(128, 26);
            this.txtSuffix.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(437, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Suffix";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(525, 24);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(128, 26);
            this.txtTitle.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(433, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "Title";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(181, 88);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(208, 26);
            this.txtMiddleName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(61, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Middle Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(181, 56);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(208, 26);
            this.txtFirstName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(61, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "First Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(181, 24);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(208, 26);
            this.txtLastName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(61, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Last Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgAddresses
            // 
            this.pgAddresses.Controls.Add(this.ucAddresses);
            this.pgAddresses.Location = new System.Drawing.Point(4, 29);
            this.pgAddresses.Name = "pgAddresses";
            this.pgAddresses.Size = new System.Drawing.Size(713, 289);
            this.pgAddresses.TabIndex = 1;
            this.pgAddresses.Text = "Addresses";
            this.pgAddresses.UseVisualStyleBackColor = true;
            // 
            // ucAddresses
            // 
            this.ucAddresses.Location = new System.Drawing.Point(9, 20);
            this.ucAddresses.Name = "ucAddresses";
            this.ucAddresses.Size = new System.Drawing.Size(690, 248);
            this.ucAddresses.TabIndex = 0;
            // 
            // pgLabels
            // 
            this.pgLabels.Controls.Add(this.ucLabels);
            this.pgLabels.Location = new System.Drawing.Point(4, 29);
            this.pgLabels.Name = "pgLabels";
            this.pgLabels.Size = new System.Drawing.Size(713, 289);
            this.pgLabels.TabIndex = 7;
            this.pgLabels.Text = "Labels";
            this.pgLabels.UseVisualStyleBackColor = true;
            // 
            // ucLabels
            // 
            this.ucLabels.Location = new System.Drawing.Point(18, 40);
            this.ucLabels.Name = "ucLabels";
            this.ucLabels.Size = new System.Drawing.Size(672, 208);
            this.ucLabels.TabIndex = 0;
            // 
            // pgPhoneEMail
            // 
            this.pgPhoneEMail.Controls.Add(this.groupBox2);
            this.pgPhoneEMail.Controls.Add(this.ucEMail);
            this.pgPhoneEMail.Controls.Add(this.ucPhones);
            this.pgPhoneEMail.Location = new System.Drawing.Point(4, 29);
            this.pgPhoneEMail.Name = "pgPhoneEMail";
            this.pgPhoneEMail.Size = new System.Drawing.Size(713, 289);
            this.pgPhoneEMail.TabIndex = 3;
            this.pgPhoneEMail.Text = "Phone/E-Mail";
            this.pgPhoneEMail.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(9, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(694, 8);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // ucEMail
            // 
            this.ucEMail.Location = new System.Drawing.Point(8, 173);
            this.ucEMail.Name = "ucEMail";
            this.ucEMail.Size = new System.Drawing.Size(689, 110);
            this.ucEMail.TabIndex = 2;
            // 
            // ucPhones
            // 
            this.ucPhones.Location = new System.Drawing.Point(8, 8);
            this.ucPhones.Name = "ucPhones";
            this.ucPhones.Size = new System.Drawing.Size(689, 143);
            this.ucPhones.TabIndex = 0;
            // 
            // pgWork
            // 
            this.pgWork.Controls.Add(this.txtRole);
            this.pgWork.Controls.Add(this.label22);
            this.pgWork.Controls.Add(this.groupBox7);
            this.pgWork.Controls.Add(this.txtCategories);
            this.pgWork.Controls.Add(this.label21);
            this.pgWork.Controls.Add(this.txtJobTitle);
            this.pgWork.Controls.Add(this.label20);
            this.pgWork.Controls.Add(this.groupBox6);
            this.pgWork.Controls.Add(this.txtUnits);
            this.pgWork.Controls.Add(this.label19);
            this.pgWork.Controls.Add(this.txtOrganization);
            this.pgWork.Controls.Add(this.label18);
            this.pgWork.Location = new System.Drawing.Point(4, 29);
            this.pgWork.Name = "pgWork";
            this.pgWork.Size = new System.Drawing.Size(713, 289);
            this.pgWork.TabIndex = 2;
            this.pgWork.Text = "Work";
            this.pgWork.UseVisualStyleBackColor = true;
            // 
            // txtRole
            // 
            this.txtRole.Location = new System.Drawing.Point(170, 137);
            this.txtRole.Name = "txtRole";
            this.txtRole.Size = new System.Drawing.Size(320, 26);
            this.txtRole.TabIndex = 8;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(71, 139);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(93, 23);
            this.label22.TabIndex = 7;
            this.label22.Text = "Role";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(14, 168);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(684, 8);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            // 
            // txtCategories
            // 
            this.txtCategories.Location = new System.Drawing.Point(170, 193);
            this.txtCategories.Name = "txtCategories";
            this.txtCategories.Size = new System.Drawing.Size(440, 26);
            this.txtCategories.TabIndex = 11;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(42, 195);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(122, 23);
            this.label21.TabIndex = 10;
            this.label21.Text = "&Categories";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtJobTitle
            // 
            this.txtJobTitle.Location = new System.Drawing.Point(170, 105);
            this.txtJobTitle.Name = "txtJobTitle";
            this.txtJobTitle.Size = new System.Drawing.Size(320, 26);
            this.txtJobTitle.TabIndex = 6;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(67, 107);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 23);
            this.label20.TabIndex = 5;
            this.label20.Text = "&Job Title";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(14, 80);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(684, 8);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            // 
            // txtUnits
            // 
            this.txtUnits.Location = new System.Drawing.Point(170, 48);
            this.txtUnits.Name = "txtUnits";
            this.txtUnits.Size = new System.Drawing.Size(440, 26);
            this.txtUnits.TabIndex = 3;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(88, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(76, 23);
            this.label19.TabIndex = 2;
            this.label19.Text = "Units";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrganization
            // 
            this.txtOrganization.Location = new System.Drawing.Point(170, 16);
            this.txtOrganization.Name = "txtOrganization";
            this.txtOrganization.Size = new System.Drawing.Size(320, 26);
            this.txtOrganization.TabIndex = 1;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(42, 18);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(122, 23);
            this.label18.TabIndex = 0;
            this.label18.Text = "&Organization";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgOther
            // 
            this.pgOther.Controls.Add(this.btnFind);
            this.pgOther.Controls.Add(this.txtComments);
            this.pgOther.Controls.Add(this.label17);
            this.pgOther.Controls.Add(this.groupBox5);
            this.pgOther.Controls.Add(this.btnWebPage);
            this.pgOther.Controls.Add(this.txtWebPage);
            this.pgOther.Controls.Add(this.label16);
            this.pgOther.Controls.Add(this.groupBox4);
            this.pgOther.Controls.Add(this.groupBox3);
            this.pgOther.Controls.Add(this.label15);
            this.pgOther.Controls.Add(this.dtpBirthDate);
            this.pgOther.Controls.Add(this.txtLongitude);
            this.pgOther.Controls.Add(this.label14);
            this.pgOther.Controls.Add(this.txtLatitude);
            this.pgOther.Controls.Add(this.label13);
            this.pgOther.Controls.Add(this.txtTimeZone);
            this.pgOther.Controls.Add(this.label12);
            this.pgOther.Location = new System.Drawing.Point(4, 29);
            this.pgOther.Name = "pgOther";
            this.pgOther.Size = new System.Drawing.Size(713, 289);
            this.pgOther.TabIndex = 4;
            this.pgOther.Text = "Other";
            this.pgOther.UseVisualStyleBackColor = true;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(542, 95);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 28);
            this.btnFind.TabIndex = 9;
            this.btnFind.Text = "&Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtComments
            // 
            this.txtComments.AcceptsReturn = true;
            this.txtComments.Location = new System.Drawing.Point(150, 198);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComments.Size = new System.Drawing.Size(528, 72);
            this.txtComments.TabIndex = 16;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(34, 200);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 23);
            this.label17.TabIndex = 15;
            this.label17.Text = "&Comments";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(17, 182);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(688, 8);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            // 
            // btnWebPage
            // 
            this.btnWebPage.Location = new System.Drawing.Point(542, 149);
            this.btnWebPage.Name = "btnWebPage";
            this.btnWebPage.Size = new System.Drawing.Size(75, 28);
            this.btnWebPage.TabIndex = 13;
            this.btnWebPage.Text = "&Go";
            this.btnWebPage.Click += new System.EventHandler(this.btnWebPage_Click);
            // 
            // txtWebPage
            // 
            this.txtWebPage.Location = new System.Drawing.Point(150, 150);
            this.txtWebPage.Name = "txtWebPage";
            this.txtWebPage.Size = new System.Drawing.Size(368, 26);
            this.txtWebPage.TabIndex = 12;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(34, 150);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(110, 23);
            this.label16.TabIndex = 11;
            this.label16.Text = "&Web Page";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(17, 126);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(688, 8);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(17, 46);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(688, 8);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(38, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(106, 23);
            this.label15.TabIndex = 0;
            this.label15.Text = "&Birth Date";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Checked = false;
            this.dtpBirthDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBirthDate.Location = new System.Drawing.Point(150, 14);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.ShowCheckBox = true;
            this.dtpBirthDate.Size = new System.Drawing.Size(255, 26);
            this.dtpBirthDate.TabIndex = 1;
            // 
            // txtLongitude
            // 
            this.txtLongitude.Location = new System.Drawing.Point(398, 96);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(120, 26);
            this.txtLongitude.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(290, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 23);
            this.label14.TabIndex = 7;
            this.label14.Text = "Longitude";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLatitude
            // 
            this.txtLatitude.Location = new System.Drawing.Point(150, 96);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(120, 26);
            this.txtLatitude.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(51, 96);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 23);
            this.label13.TabIndex = 5;
            this.label13.Text = "&Latitude";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTimeZone
            // 
            this.txtTimeZone.Location = new System.Drawing.Point(150, 64);
            this.txtTimeZone.Name = "txtTimeZone";
            this.txtTimeZone.Size = new System.Drawing.Size(368, 26);
            this.txtTimeZone.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(34, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 23);
            this.label12.TabIndex = 3;
            this.label12.Text = "&Time Zone";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgPhoto
            // 
            this.pgPhoto.Controls.Add(this.ucPhoto);
            this.pgPhoto.Location = new System.Drawing.Point(4, 29);
            this.pgPhoto.Name = "pgPhoto";
            this.pgPhoto.Size = new System.Drawing.Size(713, 289);
            this.pgPhoto.TabIndex = 5;
            this.pgPhoto.Text = "Photo";
            this.pgPhoto.UseVisualStyleBackColor = true;
            // 
            // ucPhoto
            // 
            this.ucPhoto.Cursor = System.Windows.Forms.Cursors.Default;
            this.ucPhoto.ImageType = null;
            this.ucPhoto.Location = new System.Drawing.Point(22, 40);
            this.ucPhoto.Name = "ucPhoto";
            this.ucPhoto.Size = new System.Drawing.Size(664, 208);
            this.ucPhoto.TabIndex = 0;
            // 
            // pgLogo
            // 
            this.pgLogo.Controls.Add(this.ucLogo);
            this.pgLogo.Location = new System.Drawing.Point(4, 29);
            this.pgLogo.Name = "pgLogo";
            this.pgLogo.Size = new System.Drawing.Size(713, 289);
            this.pgLogo.TabIndex = 6;
            this.pgLogo.Text = "Logo";
            this.pgLogo.UseVisualStyleBackColor = true;
            // 
            // ucLogo
            // 
            this.ucLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.ucLogo.ImageType = null;
            this.ucLogo.Location = new System.Drawing.Point(22, 40);
            this.ucLogo.Name = "ucLogo";
            this.ucLogo.Size = new System.Drawing.Size(664, 208);
            this.ucLogo.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 424);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 32);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(645, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            // 
            // txtUniqueId
            // 
            this.txtUniqueId.Location = new System.Drawing.Point(115, 12);
            this.txtUniqueId.Name = "txtUniqueId";
            this.txtUniqueId.ReadOnly = true;
            this.txtUniqueId.Size = new System.Drawing.Size(376, 26);
            this.txtUniqueId.TabIndex = 1;
            this.txtUniqueId.TabStop = false;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "Unique ID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClass
            // 
            this.txtClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClass.Location = new System.Drawing.Point(584, 46);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(145, 26);
            this.txtClass.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(500, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 23);
            this.label10.TabIndex = 6;
            this.label10.Text = "Class";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLastRevised
            // 
            this.txtLastRevised.Location = new System.Drawing.Point(115, 46);
            this.txtLastRevised.Name = "txtLastRevised";
            this.txtLastRevised.ReadOnly = true;
            this.txtLastRevised.Size = new System.Drawing.Size(207, 26);
            this.txtLastRevised.TabIndex = 5;
            this.txtLastRevised.TabStop = false;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(16, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 23);
            this.label11.TabIndex = 4;
            this.label11.Text = "Revised";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(498, 14);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 23);
            this.label23.TabIndex = 2;
            this.label23.Text = "&Version";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboVersion
            // 
            this.cboVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVersion.Items.AddRange(new object[] {
            "2.1",
            "3.0",
            "4.0"});
            this.cboVersion.Location = new System.Drawing.Point(584, 12);
            this.cboVersion.Name = "cboVersion";
            this.cboVersion.Size = new System.Drawing.Size(65, 28);
            this.cboVersion.TabIndex = 3;
            this.cboVersion.SelectedIndexChanged += new System.EventHandler(this.cboVersion_SelectedIndexChanged);
            // 
            // epErrors
            // 
            this.epErrors.ContainerControl = this;
            // 
            // VCardPropertiesDlg
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(745, 468);
            this.Controls.Add(this.cboVersion);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtLastRevised);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.txtUniqueId);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VCardPropertiesDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit vCard Properties";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.VCardPropertiesDlg_Closing);
            this.tabInfo.ResumeLayout(false);
            this.pgName.ResumeLayout(false);
            this.pgName.PerformLayout();
            this.pgAddresses.ResumeLayout(false);
            this.pgLabels.ResumeLayout(false);
            this.pgPhoneEMail.ResumeLayout(false);
            this.pgWork.ResumeLayout(false);
            this.pgWork.PerformLayout();
            this.pgOther.ResumeLayout(false);
            this.pgOther.PerformLayout();
            this.pgPhoto.ResumeLayout(false);
            this.pgLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.epErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMiddleName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNickname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFormattedName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSortString;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUniqueId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLastRevised;
        private System.Windows.Forms.Label label11;
        private vCardBrowser.AddressControl ucAddresses;
        private System.Windows.Forms.TabPage pgPhoto;
        private System.Windows.Forms.TabPage pgLogo;
        private System.Windows.Forms.TabPage pgName;
        private System.Windows.Forms.TabPage pgWork;
        private vCardBrowser.PhotoControl ucPhoto;
        private vCardBrowser.PhotoControl ucLogo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private vCardBrowser.EMailControl ucEMail;
        private vCardBrowser.PhoneControl ucPhones;
        private System.Windows.Forms.TextBox txtTimeZone;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtWebPage;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnWebPage;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.TextBox txtOrganization;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtUnits;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtJobTitle;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtCategories;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtRole;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cboVersion;
        private System.Windows.Forms.TabPage pgAddresses;
        private System.Windows.Forms.TabPage pgPhoneEMail;
        private System.Windows.Forms.TabPage pgLabels;
        private vCardBrowser.LabelControl ucLabels;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ErrorProvider epErrors;
        private System.Windows.Forms.TabPage pgOther;
        private System.Windows.Forms.TextBox txtGenderIdentity;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cboSex;
        private System.Windows.Forms.Label label24;
    }
}
