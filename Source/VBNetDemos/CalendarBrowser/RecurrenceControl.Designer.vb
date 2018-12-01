<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RecurrenceControl
    Inherits System.Windows.Forms.UserControl

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
        Me.btnClearRRules = New System.Windows.Forms.Button()
        Me.btnRemoveRRule = New System.Windows.Forms.Button()
        Me.btnAddRRule = New System.Windows.Forms.Button()
        Me.lbRRules = New System.Windows.Forms.ListBox()
        Me.btnClearRDates = New System.Windows.Forms.Button()
        Me.btnRemoveRDate = New System.Windows.Forms.Button()
        Me.btnAddRDate = New System.Windows.Forms.Button()
        Me.lbRDates = New System.Windows.Forms.ListBox()
        Me.lblRRules = New System.Windows.Forms.Label()
        Me.lblRDates = New System.Windows.Forms.Label()
        Me.dtpRDate = New System.Windows.Forms.DateTimePicker()
        Me.btnEditRRule = New System.Windows.Forms.Button()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkExcludeDay = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout
        '
        'btnClearRRules
        '
        Me.btnClearRRules.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClearRRules.Location = New System.Drawing.Point(285, 282)
        Me.btnClearRRules.Name = "btnClearRRules"
        Me.btnClearRRules.Size = New System.Drawing.Size(88, 32)
        Me.btnClearRRules.TabIndex = 5
        Me.btnClearRRules.Text = "Clear"
        '
        'btnRemoveRRule
        '
        Me.btnRemoveRRule.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRemoveRRule.Location = New System.Drawing.Point(191, 282)
        Me.btnRemoveRRule.Name = "btnRemoveRRule"
        Me.btnRemoveRRule.Size = New System.Drawing.Size(88, 32)
        Me.btnRemoveRRule.TabIndex = 4
        Me.btnRemoveRRule.Text = "Remove"
        '
        'btnAddRRule
        '
        Me.btnAddRRule.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAddRRule.Location = New System.Drawing.Point(3, 282)
        Me.btnAddRRule.Name = "btnAddRRule"
        Me.btnAddRRule.Size = New System.Drawing.Size(88, 32)
        Me.btnAddRRule.TabIndex = 2
        Me.btnAddRRule.Text = "Add"
        '
        'lbRRules
        '
        Me.lbRRules.HorizontalExtent = 800
        Me.lbRRules.HorizontalScrollbar = true
        Me.lbRRules.IntegralHeight = false
        Me.lbRRules.ItemHeight = 20
        Me.lbRRules.Location = New System.Drawing.Point(3, 34)
        Me.lbRRules.Name = "lbRRules"
        Me.lbRRules.Size = New System.Drawing.Size(370, 242)
        Me.lbRRules.TabIndex = 1
        '
        'btnClearRDates
        '
        Me.btnClearRDates.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClearRDates.Location = New System.Drawing.Point(584, 282)
        Me.btnClearRDates.Name = "btnClearRDates"
        Me.btnClearRDates.Size = New System.Drawing.Size(88, 32)
        Me.btnClearRDates.TabIndex = 13
        Me.btnClearRDates.Text = "Clear"
        '
        'btnRemoveRDate
        '
        Me.btnRemoveRDate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRemoveRDate.Location = New System.Drawing.Point(490, 282)
        Me.btnRemoveRDate.Name = "btnRemoveRDate"
        Me.btnRemoveRDate.Size = New System.Drawing.Size(88, 32)
        Me.btnRemoveRDate.TabIndex = 12
        Me.btnRemoveRDate.Text = "Remove"
        '
        'btnAddRDate
        '
        Me.btnAddRDate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAddRDate.Location = New System.Drawing.Point(396, 282)
        Me.btnAddRDate.Name = "btnAddRDate"
        Me.btnAddRDate.Size = New System.Drawing.Size(88, 32)
        Me.btnAddRDate.TabIndex = 11
        Me.btnAddRDate.Text = "Add"
        '
        'lbRDates
        '
        Me.lbRDates.IntegralHeight = false
        Me.lbRDates.ItemHeight = 20
        Me.lbRDates.Location = New System.Drawing.Point(396, 96)
        Me.lbRDates.Name = "lbRDates"
        Me.lbRDates.Size = New System.Drawing.Size(276, 180)
        Me.lbRDates.TabIndex = 10
        '
        'lblRRules
        '
        Me.lblRRules.Location = New System.Drawing.Point(3, 8)
        Me.lblRRules.Name = "lblRRules"
        Me.lblRRules.Size = New System.Drawing.Size(216, 23)
        Me.lblRRules.TabIndex = 0
        Me.lblRRules.Text = "Recurrence &Rules"
        Me.lblRRules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRDates
        '
        Me.lblRDates.Location = New System.Drawing.Point(397, 8)
        Me.lblRDates.Name = "lblRDates"
        Me.lblRDates.Size = New System.Drawing.Size(174, 23)
        Me.lblRDates.TabIndex = 7
        Me.lblRDates.Text = "Recurrence &Dates"
        Me.lblRDates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpRDate
        '
        Me.dtpRDate.Checked = false
        Me.dtpRDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpRDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRDate.Location = New System.Drawing.Point(395, 34)
        Me.dtpRDate.Name = "dtpRDate"
        Me.dtpRDate.Size = New System.Drawing.Size(235, 26)
        Me.dtpRDate.TabIndex = 8
        '
        'btnEditRRule
        '
        Me.btnEditRRule.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEditRRule.Location = New System.Drawing.Point(97, 282)
        Me.btnEditRRule.Name = "btnEditRRule"
        Me.btnEditRRule.Size = New System.Drawing.Size(88, 32)
        Me.btnEditRRule.TabIndex = 3
        Me.btnEditRRule.Text = "Edit"
        '
        'groupBox1
        '
        Me.groupBox1.Location = New System.Drawing.Point(387, 8)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(2, 300)
        Me.groupBox1.TabIndex = 6
        Me.groupBox1.TabStop = false
        '
        'chkExcludeDay
        '
        Me.chkExcludeDay.Location = New System.Drawing.Point(393, 66)
        Me.chkExcludeDay.Name = "chkExcludeDay"
        Me.chkExcludeDay.Size = New System.Drawing.Size(201, 24)
        Me.chkExcludeDay.TabIndex = 9
        Me.chkExcludeDay.Text = "Exclude whole day"
        '
        'RecurrenceControl
        '
        Me.Controls.Add(Me.chkExcludeDay)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.btnEditRRule)
        Me.Controls.Add(Me.dtpRDate)
        Me.Controls.Add(Me.lblRDates)
        Me.Controls.Add(Me.lblRRules)
        Me.Controls.Add(Me.btnClearRDates)
        Me.Controls.Add(Me.btnRemoveRDate)
        Me.Controls.Add(Me.btnAddRDate)
        Me.Controls.Add(Me.lbRDates)
        Me.Controls.Add(Me.btnClearRRules)
        Me.Controls.Add(Me.btnRemoveRRule)
        Me.Controls.Add(Me.btnAddRRule)
        Me.Controls.Add(Me.lbRRules)
        Me.Name = "RecurrenceControl"
        Me.Size = New System.Drawing.Size(685, 320)
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents btnClearRRules As System.Windows.Forms.Button
    Friend WithEvents btnRemoveRRule As System.Windows.Forms.Button
    Friend WithEvents btnAddRRule As System.Windows.Forms.Button
    Friend WithEvents lbRRules As System.Windows.Forms.ListBox
    Friend WithEvents btnClearRDates As System.Windows.Forms.Button
    Friend WithEvents btnRemoveRDate As System.Windows.Forms.Button
    Friend WithEvents btnAddRDate As System.Windows.Forms.Button
    Friend WithEvents lbRDates As System.Windows.Forms.ListBox
    Friend WithEvents lblRRules As System.Windows.Forms.Label
    Friend WithEvents lblRDates As System.Windows.Forms.Label
    Friend WithEvents dtpRDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnEditRRule As System.Windows.Forms.Button
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkExcludeDay As System.Windows.Forms.CheckBox

End Class
