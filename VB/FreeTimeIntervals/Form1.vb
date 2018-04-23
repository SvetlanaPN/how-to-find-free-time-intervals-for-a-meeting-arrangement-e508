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
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'xtraSchedulingDataSet1.Resources' table. You can move, or remove it, as needed.
			Me.resourcesTableAdapter.Fill(Me.xtraSchedulingDataSet1.Resources)
			' TODO: This line of code loads data into the 'xtraSchedulingDataSet.Appointments' table. You can move, or remove it, as needed.
			Me.appointmentsTableAdapter.Fill(Me.xtraSchedulingDataSet.Appointments)
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
				text = freeTime.ToString()
				schedulerControl1.ActiveView.SetSelection(freeTime, Resource.Empty)
			End If
			MessageBox.Show(text)
		End Sub
	Private Function FindInterval(ByVal interval As TimeInterval, ByVal duration As TimeSpan) As TimeInterval
		Dim calculator As New FreeTimeCalculator(schedulerControl1.Storage)
		' Set a handler for the IntervalFound event.
		AddHandler calculator.IntervalFound, AddressOf OnIntervalFound
		' Call the method which raises the event.
		Dim freeInterval As TimeInterval = calculator.FindFreeTimeInterval(interval, duration, True)
		Return freeInterval
	End Function
	Private Sub OnIntervalFound(ByVal sender As Object, ByVal args As IntervalFoundEventArgs)
		Dim freeIntervals As TimeIntervalCollectionEx = args.FreeIntervals
		Dim start As DateTime = freeIntervals.Start.Date.AddDays(-1)
		Dim [end] As DateTime = freeIntervals.End
		Do While start < [end]
			RemoveSpareTime(freeIntervals, start)
			RemoveNonworkingDays(freeIntervals, start)
			start = start + TimeSpan.FromDays(1)
		Loop
	End Sub
	Private Sub RemoveSpareTime(ByVal freeIntervals As TimeIntervalCollectionEx, ByVal [date] As DateTime)
		Dim spareTime As New TimeInterval([date].AddHours(18), TimeSpan.FromHours(15))
		freeIntervals.Remove(spareTime)
	End Sub
	Private Sub RemoveNonworkingDays(ByVal freeIntervals As TimeIntervalCollectionEx, ByVal [date] As DateTime)
		Dim isWorkDay As Boolean = schedulerControl1.WorkDays.IsWorkDay([date])
		If (Not isWorkDay) Then
			freeIntervals.Remove(New TimeInterval([date], TimeSpan.FromDays(1)))
		End If
	End Sub


		Private Sub OnApptChangedInsertedDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles schedulerStorage1.AppointmentsChanged, schedulerStorage1.AppointmentsInserted, schedulerStorage1.AppointmentsDeleted
			appointmentsTableAdapter.Update(xtraSchedulingDataSet)
			xtraSchedulingDataSet.AcceptChanges()
		End Sub



	End Class


End Namespace
