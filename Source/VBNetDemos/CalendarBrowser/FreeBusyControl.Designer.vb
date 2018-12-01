<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FreeBusyControl
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
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.label1 = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.label2 = New System.Windows.Forms.Label()
        Me.cboBusyType = New System.Windows.Forms.ComboBox()
        Me.label10 = New System.Windows.Forms.Label()
        Me.txtOtherType = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Checked = false
        Me.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(95, 53)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.ShowCheckBox = true
        Me.dtpStartDate.Size = New System.Drawing.Size(255, 26)
        Me.dtpStartDate.TabIndex = 5
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(19, 57)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(70, 23)
        Me.label1.TabIndex = 4
        Me.label1.Text = "Start"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Checked = false
        Me.dtpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(95, 90)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.ShowCheckBox = true
        Me.dtpEndDate.Size = New System.Drawing.Size(255, 26)
        Me.dtpEndDate.TabIndex = 7
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(23, 94)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(66, 23)
        Me.label2.TabIndex = 6
        Me.label2.Text = "End"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboBusyType
        '
        Me.cboBusyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBusyType.Items.AddRange(New Object() {"None", "Free", "Busy", "Unavailable", "Tentative", "Other"})
        Me.cboBusyType.Location = New System.Drawing.Point(95, 14)
        Me.cboBusyType.Name = "cboBusyType"
        Me.cboBusyType.Size = New System.Drawing.Size(128, 28)
        Me.cboBusyType.TabIndex = 1
        '
        'label10
        '
        Me.label10.Location = New System.Drawing.Point(15, 16)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(74, 23)
        Me.label10.TabIndex = 0
        Me.label10.Text = "Type"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOtherType
        '
        Me.txtOtherType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOtherType.Location = New System.Drawing.Point(342, 14)
        Me.txtOtherType.Name = "txtOtherType"
        Me.txtOtherType.Size = New System.Drawing.Size(136, 26)
        Me.txtOtherType.TabIndex = 3
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(229, 16)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(107, 23)
        Me.label4.TabIndex = 2
        Me.label4.Text = "Other Type"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FreeBusyControl
        '
        Me.Controls.Add(Me.txtOtherType)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.cboBusyType)
        Me.Controls.Add(Me.label10)
        Me.Controls.Add(Me.dtpEndDate)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.dtpStartDate)
        Me.Controls.Add(Me.label1)
        Me.Name = "FreeBusyControl"
        Me.Size = New System.Drawing.Size(503, 161)
        Me.Controls.SetChildIndex(Me.label1, 0)
        Me.Controls.SetChildIndex(Me.dtpStartDate, 0)
        Me.Controls.SetChildIndex(Me.label2, 0)
        Me.Controls.SetChildIndex(Me.dtpEndDate, 0)
        Me.Controls.SetChildIndex(Me.label10, 0)
        Me.Controls.SetChildIndex(Me.cboBusyType, 0)
        Me.Controls.SetChildIndex(Me.label4, 0)
        Me.Controls.SetChildIndex(Me.txtOtherType, 0)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label10 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents cboBusyType As System.Windows.Forms.ComboBox
    Friend WithEvents txtOtherType As System.Windows.Forms.TextBox

End Class
