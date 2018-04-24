Namespace FreeTimeIntervals
    Partial Public Class Form1
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim timeRuler1 As New DevExpress.XtraScheduler.TimeRuler()
            Dim timeScaleYear1 As New DevExpress.XtraScheduler.TimeScaleYear()
            Dim timeScaleQuarter1 As New DevExpress.XtraScheduler.TimeScaleQuarter()
            Dim timeScaleMonth1 As New DevExpress.XtraScheduler.TimeScaleMonth()
            Dim timeScaleWeek1 As New DevExpress.XtraScheduler.TimeScaleWeek()
            Dim timeScaleDay1 As New DevExpress.XtraScheduler.TimeScaleDay()
            Dim timeScaleHour1 As New DevExpress.XtraScheduler.TimeScaleHour()
            Dim timeScaleFixedInterval1 As New DevExpress.XtraScheduler.TimeScaleFixedInterval()
            Dim timeRuler2 As New DevExpress.XtraScheduler.TimeRuler()
            Me.schedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
            Me.schedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
            Me.appointmentsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.xtraSchedulingDataSet = New FreeTimeIntervals.XtraSchedulingDataSet()
            Me.resourcesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.xtraSchedulingDataSet1 = New FreeTimeIntervals.XtraSchedulingDataSet1()
            Me.appointmentsTableAdapter = New FreeTimeIntervals.XtraSchedulingDataSetTableAdapters.AppointmentsTableAdapter()
            Me.resourcesTableAdapter = New FreeTimeIntervals.XtraSchedulingDataSet1TableAdapters.ResourcesTableAdapter()
            Me.button1 = New System.Windows.Forms.Button()
            Me.dateNavigator1 = New DevExpress.XtraScheduler.DateNavigator()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.label1 = New System.Windows.Forms.Label()
            Me.slotDuration = New DevExpress.XtraEditors.TimeEdit()
            CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.appointmentsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.xtraSchedulingDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.resourcesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.xtraSchedulingDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dateNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.groupBox1.SuspendLayout()
            CType(Me.slotDuration.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' schedulerControl1
            ' 
            Me.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.WorkWeek
            Me.schedulerControl1.Location = New System.Drawing.Point(12, 12)
            Me.schedulerControl1.Name = "schedulerControl1"
            Me.schedulerControl1.Size = New System.Drawing.Size(592, 462)
            Me.schedulerControl1.Start = New Date(2007, 12, 17, 0, 0, 0, 0)
            Me.schedulerControl1.Storage = Me.schedulerStorage1
            Me.schedulerControl1.TabIndex = 0
            Me.schedulerControl1.Text = "schedulerControl1"
            Me.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1)
            Me.schedulerControl1.Views.TimelineView.AppointmentDisplayOptions.AppointmentAutoHeight = True
            timeScaleYear1.Enabled = False
            timeScaleQuarter1.Enabled = False
            timeScaleMonth1.Enabled = False
            timeScaleDay1.Width = 100
            timeScaleHour1.Enabled = False
            timeScaleHour1.Width = 100
            timeScaleFixedInterval1.Enabled = False
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleYear1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleQuarter1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleMonth1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleWeek1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleDay1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleHour1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleFixedInterval1)
            Me.schedulerControl1.Views.WorkWeekView.ShowWorkTimeOnly = True
            Me.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler2)
            ' 
            ' schedulerStorage1
            ' 
            Me.schedulerStorage1.Appointments.DataSource = Me.appointmentsBindingSource
            Me.schedulerStorage1.Appointments.Mappings.AllDay = "AllDay"
            Me.schedulerStorage1.Appointments.Mappings.Description = "Description"
            Me.schedulerStorage1.Appointments.Mappings.End = "EndDate"
            Me.schedulerStorage1.Appointments.Mappings.Label = "Label"
            Me.schedulerStorage1.Appointments.Mappings.Location = "Location"
            Me.schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo"
            Me.schedulerStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo"
            Me.schedulerStorage1.Appointments.Mappings.ResourceId = "ResourceID"
            Me.schedulerStorage1.Appointments.Mappings.Start = "StartDate"
            Me.schedulerStorage1.Appointments.Mappings.Status = "Status"
            Me.schedulerStorage1.Appointments.Mappings.Subject = "Subject"
            Me.schedulerStorage1.Appointments.Mappings.Type = "Type"
            Me.schedulerStorage1.Resources.DataSource = Me.resourcesBindingSource
            Me.schedulerStorage1.Resources.Mappings.Caption = "ResourceName"
            Me.schedulerStorage1.Resources.Mappings.Color = "Color"
            Me.schedulerStorage1.Resources.Mappings.Id = "ResourceID"
            ' 
            ' appointmentsBindingSource
            ' 
            Me.appointmentsBindingSource.DataMember = "Appointments"
            Me.appointmentsBindingSource.DataSource = Me.xtraSchedulingDataSet
            ' 
            ' xtraSchedulingDataSet
            ' 
            Me.xtraSchedulingDataSet.DataSetName = "XtraSchedulingDataSet"
            Me.xtraSchedulingDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            ' 
            ' resourcesBindingSource
            ' 
            Me.resourcesBindingSource.DataMember = "Resources"
            Me.resourcesBindingSource.DataSource = Me.xtraSchedulingDataSet1
            ' 
            ' xtraSchedulingDataSet1
            ' 
            Me.xtraSchedulingDataSet1.DataSetName = "XtraSchedulingDataSet1"
            Me.xtraSchedulingDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            ' 
            ' appointmentsTableAdapter
            ' 
            Me.appointmentsTableAdapter.ClearBeforeFill = True
            ' 
            ' resourcesTableAdapter
            ' 
            Me.resourcesTableAdapter.ClearBeforeFill = True
            ' 
            ' button1
            ' 
            Me.button1.Location = New System.Drawing.Point(10, 64)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(107, 23)
            Me.button1.TabIndex = 1
            Me.button1.Text = "Find Next"
            Me.button1.UseVisualStyleBackColor = True
            ' 
            ' dateNavigator1
            ' 
            Me.dateNavigator1.Location = New System.Drawing.Point(625, 158)
            Me.dateNavigator1.Name = "dateNavigator1"
            Me.dateNavigator1.CellPadding = New System.Windows.Forms.Padding(2)
            Me.dateNavigator1.SchedulerControl = Me.schedulerControl1
            Me.dateNavigator1.Size = New System.Drawing.Size(179, 316)
            Me.dateNavigator1.TabIndex = 3
            Me.dateNavigator1.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.MonthInfo
            ' 
            ' groupBox1
            ' 
            Me.groupBox1.Controls.Add(Me.label1)
            Me.groupBox1.Controls.Add(Me.slotDuration)
            Me.groupBox1.Controls.Add(Me.button1)
            Me.groupBox1.Location = New System.Drawing.Point(625, 12)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(179, 100)
            Me.groupBox1.TabIndex = 4
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Free Time Slots in WorkTime"
            ' 
            ' label1
            ' 
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(7, 22)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(127, 13)
            Me.label1.TabIndex = 3
            Me.label1.Text = "Duration (Hours:Minutes):"
            ' 
            ' slotDuration
            ' 
            Me.slotDuration.EditValue = New Date(2008, 9, 26, 1, 30, 0, 0)
            Me.slotDuration.Location = New System.Drawing.Point(10, 38)
            Me.slotDuration.Name = "slotDuration"
            Me.slotDuration.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton()})
            Me.slotDuration.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default
            Me.slotDuration.Properties.Mask.EditMask = "HH:mm"
            Me.slotDuration.Size = New System.Drawing.Size(107, 20)
            Me.slotDuration.TabIndex = 2
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(826, 486)
            Me.Controls.Add(Me.groupBox1)
            Me.Controls.Add(Me.dateNavigator1)
            Me.Controls.Add(Me.schedulerControl1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.appointmentsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.xtraSchedulingDataSet, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.resourcesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.xtraSchedulingDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dateNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox1.PerformLayout()
            CType(Me.slotDuration.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private schedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
        Private WithEvents schedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
        Private xtraSchedulingDataSet As XtraSchedulingDataSet
        Private appointmentsBindingSource As System.Windows.Forms.BindingSource
        Private appointmentsTableAdapter As FreeTimeIntervals.XtraSchedulingDataSetTableAdapters.AppointmentsTableAdapter
        Private xtraSchedulingDataSet1 As XtraSchedulingDataSet1
        Private resourcesBindingSource As System.Windows.Forms.BindingSource
        Private resourcesTableAdapter As FreeTimeIntervals.XtraSchedulingDataSet1TableAdapters.ResourcesTableAdapter
        Private WithEvents button1 As System.Windows.Forms.Button
        Private dateNavigator1 As DevExpress.XtraScheduler.DateNavigator
        Private groupBox1 As System.Windows.Forms.GroupBox
        Private label1 As System.Windows.Forms.Label
        Private slotDuration As DevExpress.XtraEditors.TimeEdit
    End Class
End Namespace

