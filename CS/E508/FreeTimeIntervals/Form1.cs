using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Tools;
using System;
using System.Windows.Forms;

namespace FreeTimeIntervals
{
	public partial class Form1 : Form {
		TimeZoneHelper timeZoneHelper;
        // Specify non-working time interval.
        TimeOfDayInterval nonWorkingTime = new TimeOfDayInterval(TimeSpan.FromHours(18), TimeSpan.FromDays(1) + TimeSpan.FromHours(9));
		
		internal TimeZoneHelper TimeZoneHelper { get { return timeZoneHelper; } }
		internal TimeOfDayInterval NonWorkingTime { get { return nonWorkingTime; } set { nonWorkingTime = value; } }

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
				text = "Free time interval with duration " + clientFreeInterval.Duration.ToString() +
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
				RemoveNonWorkingTime(freeIntervals, start);
				RemoveNonWorkingDay(freeIntervals, start);
				start += TimeSpan.FromDays(1);
			}
		}
		private void RemoveNonWorkingTime(TimeIntervalCollectionEx freeIntervals, DateTime date) {
			DateTime clientDate = TimeZoneHelper.ToClientTime(date).Date;		
			
			TimeInterval clientNonWorkingTime = new TimeInterval(clientDate + NonWorkingTime.Start, clientDate + NonWorkingTime.End);
			freeIntervals.Remove(TimeZoneHelper.FromClientTime(clientNonWorkingTime));
		}
		private void RemoveNonWorkingDay(TimeIntervalCollectionEx freeIntervals, DateTime date) {
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
