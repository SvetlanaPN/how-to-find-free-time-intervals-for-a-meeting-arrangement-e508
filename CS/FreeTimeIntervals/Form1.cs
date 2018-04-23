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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'xtraSchedulingDataSet1.Resources' table. You can move, or remove it, as needed.
            this.resourcesTableAdapter.Fill(this.xtraSchedulingDataSet1.Resources);
            // TODO: This line of code loads data into the 'xtraSchedulingDataSet.Appointments' table. You can move, or remove it, as needed.
            this.appointmentsTableAdapter.Fill(this.xtraSchedulingDataSet.Appointments);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            TimeSpan duration = ((DateTime)slotDuration.EditValue).TimeOfDay;
            DateTime start = schedulerControl1.ActiveView.SelectedInterval.End;
            DateTime startOfWeek = DevExpress.XtraScheduler.Native.DateTimeHelper.GetStartOfWeek(start);
            TimeInterval interval = new TimeInterval(start, startOfWeek.AddDays(7));
            TimeInterval freeTime = FindInterval(interval, duration);
            string text;
            if (TimeInterval.Equals(freeTime, TimeInterval.Empty))
                text = "Not found";
            else {
                text = freeTime.ToString();
                schedulerControl1.ActiveView.SetSelection(freeTime, Resource.Empty);
            }            
            MessageBox.Show(text);
        }
    private TimeInterval FindInterval(TimeInterval interval, TimeSpan duration) {            
        FreeTimeCalculator calculator = new FreeTimeCalculator(schedulerControl1.Storage);
        // Set a handler for the IntervalFound event.
        calculator.IntervalFound += new IntervalFoundEventHandler(OnIntervalFound);            
        // Call the method which raises the event.
        TimeInterval freeInterval = calculator.FindFreeTimeInterval(interval, duration, true);
        return freeInterval;
    }
    private void OnIntervalFound(object sender, IntervalFoundEventArgs args)
    {   TimeIntervalCollectionEx freeIntervals = args.FreeIntervals;
        DateTime start = freeIntervals.Start.Date.AddDays(-1);
        DateTime end = freeIntervals.End;
        while (start < end) {
            RemoveSpareTime(freeIntervals, start);
            RemoveNonworkingDays(freeIntervals, start);
            start += TimeSpan.FromDays(1);
        }
    }        
    private void RemoveSpareTime(TimeIntervalCollectionEx freeIntervals, DateTime date) {
        TimeInterval spareTime = new TimeInterval(date.AddHours(18), TimeSpan.FromHours(15));            
        freeIntervals.Remove(spareTime);
    }
    private void RemoveNonworkingDays(TimeIntervalCollectionEx freeIntervals, DateTime date) {
        bool isWorkDay = schedulerControl1.WorkDays.IsWorkDay(date);
        if (!isWorkDay)
            freeIntervals.Remove(new TimeInterval(date, TimeSpan.FromDays(1)));            
    }


        private void OnApptChangedInsertedDeleted(object sender, PersistentObjectsEventArgs e)
        {
            appointmentsTableAdapter.Update(xtraSchedulingDataSet);
            xtraSchedulingDataSet.AcceptChanges();
        }



    }


}
