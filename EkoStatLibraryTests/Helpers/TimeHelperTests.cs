using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EkoStatLibrary.Helpers.Tests;

[TestClass()]
public class TimeHelperTests
{
    [TestMethod()]
    [DataRow("2023-05-01", "2023-05-07", DayOfWeek.Monday, 1)]
    [DataRow("2023-05-01", "2023-05-08", DayOfWeek.Monday, 1)]
    [DataRow("2023-05-01", "2023-05-09", DayOfWeek.Monday, 2)]
    [DataRow("2023-04-30", "2023-05-06", DayOfWeek.Monday, 2)]
    [DataRow("2023-04-30", "2023-05-08", DayOfWeek.Monday, 2)]
    [DataRow("2023-05-02", "2023-05-08", DayOfWeek.Monday, 1)]
    [DataRow("2023-05-02", "2023-05-09", DayOfWeek.Monday, 2)]
    [DataRow("2023-05-02", "2023-05-10", DayOfWeek.Monday, 2)]
    [DataRow("2023-05-02", "2023-05-14", DayOfWeek.Monday, 2)]
    [DataRow("2023-05-02", "2023-05-15", DayOfWeek.Monday, 2)]
    [DataRow("2023-05-02", "2023-05-16", DayOfWeek.Monday, 3)]

    [DataRow("2023-05-01", "2023-05-09", DayOfWeek.Tuesday, 2)]
    [DataRow("2023-05-01", "2023-05-10", DayOfWeek.Tuesday, 3)]
    [DataRow("2023-05-01", "2023-05-16", DayOfWeek.Tuesday, 3)]
    [DataRow("2023-05-01", "2023-05-17", DayOfWeek.Tuesday, 4)]

    [DataRow("2023-05-01", "2023-05-13", DayOfWeek.Sunday, 2)]
    [DataRow("2023-05-01", "2023-05-14", DayOfWeek.Sunday, 2)]
    [DataRow("2023-05-01", "2023-05-15", DayOfWeek.Sunday, 3)]
    public void GetTimePeriodsByCalendarWeekTest(string start, string end, DayOfWeek firstDayOfWeek, int expectedPeriodsCount)
    {
        var timeHelper = new TimeHelper();
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var timePeriods = timeHelper.GetTimePeriodsByWeek(startDate, endDate, firstDayOfWeek);
        Assert.AreEqual(expectedPeriodsCount, timePeriods.Count);
    }
}
