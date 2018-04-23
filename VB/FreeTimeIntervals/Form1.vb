Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Tools

Namespace FreeTimeIntervals
	Partial Public Class Form1
		Inherits Form
		Private timeZoneHelper_Renamed As TimeZoneHelper
		' Specify non-working time interval.
		Private nonWorkingTime_Renamed As New TimeOfDayInterval(TimeSpan.FromHours(18), TimeSpan.FromDays(1) + TimeSpan.FromHours(9))

		Friend ReadOnly Property TimeZoneHelper() As TimeZoneHelper
			Get
				Return timeZoneHelper_Renamed
			End Get
		End Property
		Friend Property NonWorkingTime() As TimeOfDayInterval
			Get
				Return nonWorkingTime_Renamed
			End Get
			Set(ByVal value As TimeOfDayInterval)
				nonWorkingTime_Renamed = value
			End Set
		End Property

		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'xtraSchedulingDataSet1.Resources' table. You can move, or remove it, as needed.
			Me.resourcesTableAdapter.Fill(Me.xtraSchedulingDataSet1.Resources)
			' TODO: This line of code loads data into the 'xtraSchedulingDataSet.Appointments' table. You can move, or remove it, as needed.
			Me.appointmentsTableAdapter.Fill(Me.xtraSchedulingDataSet.Appointments)

			timeZoneHelper_Renamed = New TimeZoneHelper(Me.schedulerControl1.OptionsBehavior.ClientTimeZoneId)
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			Dim duration As TimeSpan = (CDate(slotDuration.EditValue)).TimeOfDay
			Dim start As DateTime = schedulerControl1.ActiveView.SelectedInterval.End
			Dim startOfWeek As DateTime = DevExpress.XtraScheduler.Native.DateTimeHelper.GetStartOfWeek(start)
			Dim interval As New TimeInterval(start, startOfWeek.AddDays(7))
			Dim freeTime As TimeInterval = FindInterval(interval, duration)
			Dim text As String
			If TimeInterval.Equals(freeTime, TimeInterval.Empty) Then
				text = "Not found"
			Else
				Dim clientFreeInterval As TimeInterval = TimeZoneHelper.ToClientTime(freeTime)
                text = "Free time interval with duration " & clientFreeInterval.Duration.ToString() & " is found! " & Constants.vbLf + Constants.vbCr & " It starts on " & clientFreeInterval.Start.Date.ToShortDateString() & " at " & clientFreeInterval.Start.TimeOfDay.ToString() & "."
				schedulerControl1.ActiveView.SetSelection(clientFreeInterval, Resource.Empty)
			End If
			MessageBox.Show(text, "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End Sub
		Private Function FindInterval(ByVal clientInterval As TimeInterval, ByVal duration As TimeSpan) As TimeInterval
			Dim calculator As New FreeTimeCalculator(schedulerControl1.Storage)
			' Set a handler for the IntervalFound event.
			AddHandler calculator.IntervalFound, AddressOf OnIntervalFound
			Dim interval As TimeInterval = TimeZoneHelper.FromClientTime(clientInterval)
			' Call the method which raises the event.
			Return calculator.FindFreeTimeInterval(interval, duration, True)
		End Function
		Private Sub OnIntervalFound(ByVal sender As Object, ByVal args As IntervalFoundEventArgs)
			Dim freeIntervals As TimeIntervalCollectionEx = args.FreeIntervals
			Dim start As DateTime = freeIntervals.Start.Date.AddDays(-1)
			Dim [end] As DateTime = freeIntervals.End
			Do While start < [end]
				RemoveNonWorkingTime(freeIntervals, start)
				RemoveNonWorkingDay(freeIntervals, start)
				start = start + TimeSpan.FromDays(1)
			Loop
		End Sub
		Private Sub RemoveNonWorkingTime(ByVal freeIntervals As TimeIntervalCollectionEx, ByVal [date] As DateTime)
			Dim clientDate As DateTime = TimeZoneHelper.ToClientTime([date]).Date

			Dim clientNonWorkingTime As New TimeInterval(clientDate + NonWorkingTime.Start, clientDate + NonWorkingTime.End)
			freeIntervals.Remove(TimeZoneHelper.FromClientTime(clientNonWorkingTime))
		End Sub
		Private Sub RemoveNonWorkingDay(ByVal freeIntervals As TimeIntervalCollectionEx, ByVal [date] As DateTime)
			Dim clientDate As DateTime = TimeZoneHelper.ToClientTime([date]).Date
			Dim isWorkDay As Boolean = schedulerControl1.WorkDays.IsWorkDay(clientDate)
			If (Not isWorkDay) Then
				Dim clientInterval As New TimeInterval(clientDate, TimeSpan.FromDays(1))
				freeIntervals.Remove(TimeZoneHelper.FromClientTime(clientInterval))
			End If
		End Sub
		Private Sub OnApptChangedInsertedDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles schedulerStorage1.AppointmentsChanged, schedulerStorage1.AppointmentsInserted, schedulerStorage1.AppointmentsDeleted
			appointmentsTableAdapter.Update(xtraSchedulingDataSet)
			xtraSchedulingDataSet.AcceptChanges()
		End Sub
	End Class
End Namespace
