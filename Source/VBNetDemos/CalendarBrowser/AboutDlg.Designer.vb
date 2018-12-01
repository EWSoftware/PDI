<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutDlg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutDlg))
        Me.lnkHelp = New System.Windows.Forms.LinkLabel()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnSysInfo = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lvComponents = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'lnkHelp
        '
        Me.lnkHelp.Location = New System.Drawing.Point(187, 334)
        Me.lnkHelp.Name = "lnkHelp"
        Me.lnkHelp.Size = New System.Drawing.Size(216, 23)
        Me.lnkHelp.TabIndex = 6
        Me.lnkHelp.TabStop = true
        Me.lnkHelp.Text = "Eric@EWoodruff.us"
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        Me.ColumnHeader1.Width = 275
        '
        'btnSysInfo
        '
        Me.btnSysInfo.Location = New System.Drawing.Point(355, 398)
        Me.btnSysInfo.Name = "btnSysInfo"
        Me.btnSysInfo.Size = New System.Drawing.Size(150, 32)
        Me.btnSysInfo.TabIndex = 9
        Me.btnSysInfo.Text = "System Info..."
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(199, 398)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(150, 32)
        Me.btnOK.TabIndex = 8
        Me.btnOK.Text = "OK"
        '
        'lvComponents
        '
        Me.lvComponents.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvComponents.FullRowSelect = true
        Me.lvComponents.GridLines = true
        Me.lvComponents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvComponents.Location = New System.Drawing.Point(12, 153)
        Me.lvComponents.MultiSelect = false
        Me.lvComponents.Name = "lvComponents"
        Me.lvComponents.Size = New System.Drawing.Size(493, 170)
        Me.lvComponents.TabIndex = 4
        Me.lvComponents.UseCompatibleStateImageBehavior = false
        Me.lvComponents.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Version"
        Me.ColumnHeader2.Width = 150
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 127)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 23)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Product Components"
        '
        'lblName
        '
        Me.lblName.Location = New System.Drawing.Point(80, 8)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(425, 23)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "<Application Name>"
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(80, 64)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(425, 67)
        Me.lblDescription.TabIndex = 2
        Me.lblDescription.Text = "<Description>"
        '
        'lblCopyright
        '
        Me.lblCopyright.Location = New System.Drawing.Point(12, 364)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(493, 23)
        Me.lblCopyright.TabIndex = 7
        Me.lblCopyright.Text = "<Copyright>"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"),System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(16, 16)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = false
        '
        'lblVersion
        '
        Me.lblVersion.Location = New System.Drawing.Point(80, 36)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(425, 23)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "<Version>"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(12, 334)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(178, 23)
        Me.label2.TabIndex = 5
        Me.label2.Text = "For help send e-mail to"
        '
        'AboutDlg
        '
        Me.AcceptButton = Me.btnOK
        Me.CancelButton = Me.btnOK
        Me.ClientSize = New System.Drawing.Size(517, 442)
        Me.Controls.Add(Me.lnkHelp)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.btnSysInfo)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lvComponents)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblVersion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "AboutDlg"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About Calendar Browser"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnSysInfo As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lvComponents As System.Windows.Forms.ListView
    Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label

End Class
