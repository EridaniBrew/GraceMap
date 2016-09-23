<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnSelectFolder = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDataFolder = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstIncidentType = New System.Windows.Forms.ListBox()
        Me.btnStartCollection = New System.Windows.Forms.Button()
        Me.txtLog = New System.Windows.Forms.TextBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Select Folder for Data files"
        '
        'btnSelectFolder
        '
        Me.btnSelectFolder.Location = New System.Drawing.Point(93, 50)
        Me.btnSelectFolder.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSelectFolder.Name = "btnSelectFolder"
        Me.btnSelectFolder.Size = New System.Drawing.Size(176, 21)
        Me.btnSelectFolder.TabIndex = 0
        Me.btnSelectFolder.Text = "Select Data Destination Folder..."
        Me.btnSelectFolder.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnTest)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDataFolder)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstIncidentType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnStartCollection)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSelectFolder)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtLog)
        Me.SplitContainer1.Size = New System.Drawing.Size(291, 363)
        Me.SplitContainer1.SplitterDistance = 96
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 2
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(235, 11)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(61, 25)
        Me.btnTest.TabIndex = 8
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        Me.btnTest.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Enabled = False
        Me.Label2.Location = New System.Drawing.Point(234, 35)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Incident Types"
        Me.Label2.Visible = False
        '
        'txtDataFolder
        '
        Me.txtDataFolder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDataFolder.Location = New System.Drawing.Point(12, 76)
        Me.txtDataFolder.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDataFolder.Name = "txtDataFolder"
        Me.txtDataFolder.Size = New System.Drawing.Size(256, 13)
        Me.txtDataFolder.TabIndex = 6
        Me.txtDataFolder.Text = "This is a longer text which need to be right aligned"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 54)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "File Destination"
        '
        'lstIncidentType
        '
        Me.lstIncidentType.Enabled = False
        Me.lstIncidentType.FormattingEnabled = True
        Me.lstIncidentType.Location = New System.Drawing.Point(134, 8)
        Me.lstIncidentType.Margin = New System.Windows.Forms.Padding(2)
        Me.lstIncidentType.Name = "lstIncidentType"
        Me.lstIncidentType.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstIncidentType.Size = New System.Drawing.Size(96, 30)
        Me.lstIncidentType.TabIndex = 4
        Me.lstIncidentType.Visible = False
        '
        'btnStartCollection
        '
        Me.btnStartCollection.Location = New System.Drawing.Point(12, 11)
        Me.btnStartCollection.Margin = New System.Windows.Forms.Padding(2)
        Me.btnStartCollection.Name = "btnStartCollection"
        Me.btnStartCollection.Size = New System.Drawing.Size(118, 27)
        Me.btnStartCollection.TabIndex = 2
        Me.btnStartCollection.Text = "Start Data Collection"
        Me.btnStartCollection.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLog.Location = New System.Drawing.Point(0, 0)
        Me.txtLog.Margin = New System.Windows.Forms.Padding(2)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(291, 264)
        Me.txtLog.TabIndex = 0
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(291, 363)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "MainForm"
        Me.Text = "GrabData "
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnSelectFolder As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnStartCollection As System.Windows.Forms.Button
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents lstIncidentType As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDataFolder As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnTest As System.Windows.Forms.Button

End Class
