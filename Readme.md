# How to find free time intervals for a meeting arrangement


<p>This example illustrates the use of <a href="http://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraSchedulerToolsFreeTimeCalculatortopic"><u>FreeTimeCalculator</u></a> class to find all available free intervals within the specified period of time. Also, it can be used to find the nearest free slot with the specified duration.<br />
When the <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraSchedulerToolsFreeTimeCalculator_FindFreeTimeIntervaltopic1125"><u>FindFreeTimeInterval</u></a> method finds an interval that does not intersect with existing appointments, the <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraSchedulerToolsFreeTimeCalculator_IntervalFoundtopic"><u>IntervalFound</u></a> event occurs. We handle this event to exclude intervals which fall into restricted areas, such as non-working hours or holidays.</p><p>This application finds a free slot with specified duration within the work time before the end of the current work week. The non-working time is defined in code as an interval from 6 PM current day to 8 AM next day.The project uses SQL Server so the corresponding detached database files - XtraScheduling.mdf and XtraScheduling_log.ldf are included. You should attach the databases to the MS SQL server and change a connection string in the app.config file before running the project.</p>


<h3>Description</h3>

<p>For v2010 vol. 1 and higher: <br />
The database is located in Data subfolder. A connection is established automatically by SQL Express data engine when the project is run. The xtrascheduler.sql file contains scripts used to create the database and populate tables with sample data.</p>

<br/>


