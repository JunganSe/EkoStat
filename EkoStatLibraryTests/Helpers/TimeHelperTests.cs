using EkoStatLibrary.Helpers;
using EkoStatLibrary.Models;
using Microsoft.VisualBasic;
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

    [TestMethod()]
    [DataRow("2020-01-01", "2020-01-07", 1)]
    [DataRow("2020-01-01", "2020-01-31", 1)]
    [DataRow("2020-01-01", "2020-02-01", 1)]
    [DataRow("2020-01-01", "2020-02-02", 2)]
    [DataRow("2020-01-01", "2020-02-29", 2)]
    [DataRow("2020-01-01", "2020-03-01", 2)]
    [DataRow("2020-01-01", "2020-03-02", 3)]
    [DataRow("2020-01-22", "2020-02-01", 1)]
    [DataRow("2020-01-22", "2020-02-05", 2)]
    [DataRow("2020-01-22", "2020-03-01", 2)]
    [DataRow("2020-01-22", "2020-03-31", 3)]
    [DataRow("2020-01-22", "2020-04-01", 3)]
    [DataRow("2020-01-22", "2020-04-02", 4)]
    public void GetTimePeriodsByMonthTest(string start, string end, int expectedPeriodsCount)
    {
        var timeHelper = new TimeHelper();
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var timePeriods = timeHelper.GetTimePeriodsByMonth(startDate, endDate);
        Assert.AreEqual(expectedPeriodsCount, timePeriods.Count);
    }

    [TestMethod()]
    [DataRow("2020-01-01", "2020-05-07", 1)]
    [DataRow("2020-01-01", "2021-01-01", 1)]
    [DataRow("2020-01-01", "2021-01-02", 2)]
    [DataRow("2020-01-01", "2021-12-31", 2)]
    [DataRow("2020-01-01", "2022-01-01", 2)]
    [DataRow("2020-01-01", "2022-01-02", 3)]
    [DataRow("2019-12-31", "2022-01-01", 3)]
    [DataRow("2019-12-31", "2022-01-02", 4)]
    public void GetTimePeriodsByYearTest(string start, string end, int expectedPeriodsCount)
    {
        var timeHelper = new TimeHelper();
        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var timePeriods = timeHelper.GetTimePeriodsByYear(startDate, endDate);
        Assert.AreEqual(expectedPeriodsCount, timePeriods.Count);
    }
}
