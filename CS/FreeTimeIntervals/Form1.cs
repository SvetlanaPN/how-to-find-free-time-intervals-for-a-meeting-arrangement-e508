
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Tools;

namespace FreeTimeIntervals
{
	public partial class Form1 : Form {
		TimeZoneHelper timeZoneHelper;
        // Specify non-working time interval.
        TimeOfDayInterval freeTime = new TimeOfDayInterval(TimeSpan.FromHours(18), TimeSpan.FromDays(1) + TimeSpan.FromHours(9));
		
		internal TimeZoneHelper TimeZoneHelper { get { return timeZoneHelper; } }
		internal TimeOfDayInterval FreeTime { get { return freeTime; } set { freeTime = value; } }

		public Form1() {
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e) {
			// TODO: This line of code loads data into the 'xtraSchedulingDataSet1.Resources' table. You can move, or remove it, as needed.
			this.resourcesTableAdapter.Fill(this.xtraSchedulingDataSet1.Resources);
			// TODO: This line of code loads data into the 'xtraSchedulingDataSet.Appointments' table. You can move, or remove it, as needed.
			this.appointmentsTableAdapter.Fill(this.xtraSchedulingDataSet.Appointments);

			timeZoneHelper = new TimeZoneHelper(this.schedulerControl1.OptionsBehavior.ClientTimeZoneId);
		}

		private void button1_Click(object sender, EventArgs e) {
			TimeSpan duration = ((DateTime)slotDuration.EditValue).TimeOfDay;
			DateTime start = schedulerControl1.ActiveView.SelectedInterval.End;
			DateTime startOfWeek = DevExpress.XtraScheduler.Native.DateTimeHelper.GetStartOfWeek(start);
			TimeInterval interval = new TimeInterval(start, startOfWeek.AddDays(7));
			TimeInterval freeTime = FindInterval(interval, duration);
			string text;
			if (TimeInterval.Equals(freeTime, TimeInterval.Empty))
				text = "Not found";
			else {
				TimeInterval clientFreeInterval = TimeZoneHelper.ToClientTime(freeTime);
				text = "Free time interval equal to " + clientFreeInterval.Duration.ToString() +
					" is found! \n\r It starts on " + clientFreeInterval.Start.Date.ToShortDateString() +
					" at " + clientFreeInterval.Start.TimeOfDay.ToString() + ".";
				schedulerControl1.ActiveView.SetSelection(clientFreeInterval, Resource.Empty);
			}
			MessageBox.Show(text, "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		private TimeInterval FindInterval(TimeInterval clientInterval, TimeSpan duration) {
			FreeTimeCalculator calculator = new FreeTimeCalculator(schedulerControl1.Storage);
			// Set a handler for the IntervalFound event.
			calculator.IntervalFound += new IntervalFoundEventHandler(OnIntervalFound);
			TimeInterval interval = TimeZoneHelper.FromClientTime(clientInterval);
			// Call the method which raises the event.
			return calculator.FindFreeTimeInterval(interval, duration, true);
		}
		private void OnIntervalFound(object sender, IntervalFoundEventArgs args) {
			TimeIntervalCollectionEx freeIntervals = args.FreeIntervals;
			DateTime start = freeIntervals.Start.Date.AddDays(-1);
			DateTime end = freeIntervals.End;
			while (start < end) {
				RemoveFreeTime(freeIntervals, start);
				RemoveNonworkingDay(freeIntervals, start);
				start += TimeSpan.FromDays(1);
			}
		}
		private void RemoveFreeTime(TimeIntervalCollectionEx freeIntervals, DateTime date) {
			DateTime clientDate = TimeZoneHelper.ToClientTime(date).Date;		
			
			TimeInterval clientFreeTime = new TimeInterval(clientDate + FreeTime.Start, clientDate + FreeTime.End);
			freeIntervals.Remove(TimeZoneHelper.FromClientTime(clientFreeTime));
		}
		private void RemoveNonworkingDay(TimeIntervalCollectionEx freeIntervals, DateTime date) {
			DateTime clientDate = TimeZoneHelper.ToClientTime(date).Date;
			bool isWorkDay = schedulerControl1.WorkDays.IsWorkDay(clientDate);
			if (!isWorkDay) {
				TimeInterval clientInterval = new TimeInterval(clientDate, TimeSpan.FromDays(1));
				freeIntervals.Remove(TimeZoneHelper.FromClientTime(clientInterval));
			}
		}
		private void OnApptChangedInsertedDeleted(object sender, PersistentObjectsEventArgs e) {
			appointmentsTableAdapter.Update(xtraSchedulingDataSet);
			xtraSchedulingDataSet.AcceptChanges();
		}
	}
}
