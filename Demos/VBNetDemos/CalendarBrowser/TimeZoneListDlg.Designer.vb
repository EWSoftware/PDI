<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimeZoneListDlg
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TimeZoneListDlg))
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.chkLimitToCalendar = New System.Windows.Forms.CheckBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.dgvCalendar = New System.Windows.Forms.DataGridView()
        Me.tbcTimeZoneId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbcLastModified = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvCalendar,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(200, 516)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(88, 32)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "&Delete"
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnEdit.Location = New System.Drawing.Point(106, 516)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(88, 32)
        Me.btnEdit.TabIndex = 3
        Me.btnEdit.Text = "&Edit"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(12, 516)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(88, 32)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "&Add"
        '
        'chkLimitToCalendar
        '
        Me.chkLimitToCalendar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.chkLimitToCalendar.Location = New System.Drawing.Point(12, 486)
        Me.chkLimitToCalendar.Name = "chkLimitToCalendar"
        Me.chkLimitToCalendar.Size = New System.Drawing.Size(345, 24)
        Me.chkLimitToCalendar.TabIndex = 1
        Me.chkLimitToCalendar.Text = "&Limit to time zones used in the loaded calendar"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(687, 516)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 32)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnApply.Location = New System.Drawing.Point(349, 516)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(88, 32)
        Me.btnApply.TabIndex = 5
        Me.btnApply.Text = "A&pply"
        '
        'dgvCalendar
        '
        Me.dgvCalendar.AllowUserToAddRows = false
        Me.dgvCalendar.AllowUserToDeleteRows = false
        Me.dgvCalendar.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dgvCalendar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCalendar.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCalendar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.tbcTimeZoneId, Me.tbcLastModified})
        Me.dgvCalendar.Location = New System.Drawing.Point(12, 12)
        Me.dgvCalendar.MultiSelect = false
        Me.dgvCalendar.Name = "dgvCalendar"
        Me.dgvCalendar.ReadOnly = true
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCalendar.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCalendar.RowHeadersWidth = 25
        Me.dgvCalendar.RowTemplate.Height = 24
        Me.dgvCalendar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCalendar.Size = New System.Drawing.Size(763, 468)
        Me.dgvCalendar.StandardTab = true
        Me.dgvCalendar.TabIndex = 7
        '
        'tbcTimeZoneId
        '
        Me.tbcTimeZoneId.DataPropertyName = "TimeZoneId_Value"
        Me.tbcTimeZoneId.HeaderText = "Time Zone ID"
        Me.tbcTimeZoneId.Name = "tbcTimeZoneId"
        Me.tbcTimeZoneId.ReadOnly = true
        Me.tbcTimeZoneId.Width = 500
        '
        'tbcLastModified
        '
        Me.tbcLastModified.DataPropertyName = "LastModified_DateTimeValue"
        Me.tbcLastModified.HeaderText = "Last Modified"
        Me.tbcLastModified.Name = "tbcLastModified"
        Me.tbcLastModified.ReadOnly = true
        Me.tbcLastModified.Width = 200
        '
        'TimeZoneListDlg
        '
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(787, 560)
        Me.Controls.Add(Me.dgvCalendar)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.chkLimitToCalendar)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAdd)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(600, 200)
        Me.Name = "TimeZoneListDlg"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Time Zone List"
        CType(Me.dgvCalendar,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents chkLimitToCalendar As System.Windows.Forms.CheckBox
    Friend WithEvents dgvCalendar As System.Windows.Forms.DataGridView
    Friend WithEvents tbcTimeZoneId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tbcLastModified As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnApply As System.Windows.Forms.Button

End Class
