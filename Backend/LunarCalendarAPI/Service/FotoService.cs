using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Ical.Net;
using Lunar;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace LunarCalendarAPI.Service
{
    public class FotoService
    {
        private Solar Solar { get; set; }

        public FotoService(Solar solar)
        {
            this.Solar = solar;
        }

        public List<DateTime> ZhaiTenListByYear(int year)
        {
            List<DateTime> engDate = new List<DateTime>();

            for (int monthVal = 1; monthVal <= 12; monthVal++)
            {
                // Get the number of days in the specified month
                int daysInMonth = DateTime.DaysInMonth(year, monthVal);
                for (int dayVal = 1; dayVal <= daysInMonth; dayVal++)
                {
                    DateTime date = new DateTime(year, monthVal, dayVal);

                    Foto foto = new Foto(new Solar(date.Year, date.Month, date.Day).Lunar);
                    if (foto.DayZhaiTen)
                    {
                        engDate.Add(date);
                    }
                }
            }

            return engDate;
        }

        public List<DateTime> ZhaiShuoListByYear(int year)
        {
            List<DateTime> engDate = new List<DateTime>();

            for (int monthVal = 1; monthVal <= 12; monthVal++)
            {
                // Get the number of days in the specified month
                int daysInMonth = DateTime.DaysInMonth(year, monthVal);
                for (int dayVal = 1; dayVal <= daysInMonth; dayVal++)
                {
                    DateTime date = new DateTime(year, monthVal, dayVal);

                    Foto foto = new Foto(new Solar(date.Year, date.Month, date.Day).Lunar);
                    if (foto.DayZhaiShuoWang)
                    {
                        engDate.Add(date);
                    }
                }
            }

            return engDate;
        }

        public List<DateTime> ZhaiGuanYinListByYear(int year)
        {
            List<DateTime> engDate = new List<DateTime>();

            for (int monthVal = 1; monthVal <= 12; monthVal++)
            {
                // Get the number of days in the specified month
                int daysInMonth = DateTime.DaysInMonth(year, monthVal);
                for (int dayVal = 1; dayVal <= daysInMonth; dayVal++)
                {
                    DateTime date = new DateTime(year, monthVal, dayVal);

                    Foto foto = new Foto(new Solar(date.Year, date.Month, date.Day).Lunar);
                    if (foto.DayZhaiGuanYin)
                    {
                        engDate.Add(date);
                    }
                }
            }

            return engDate;
        }

        public string Testing(string name, string descritpion)
        {
            var now = DateTime.Now;
            //var later = now.AddHours(1);
            var later = now.AddDays(1);

            ////Repeat daily for 5 days
            //var rrule = new RecurrencePattern(FrequencyType.None, 1) { Count = 5 };

            var e = new CalendarEvent
            {
                Summary = name,
                Start = new CalDateTime(now),
                End = new CalDateTime(later),
                IsAllDay = true,
                //RecurrenceRules = new List<RecurrencePattern> { rrule },
            };

            var calendar = new Calendar();
            calendar.Events.Add(e);

            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);
            return serializedCalendar;
        }

        public string GetCalString(string title, List<DateTime> dateEvent)
        {
            var calendar = new Calendar();

            foreach (var i in dateEvent)
            {
                var e = new CalendarEvent
                {
                    Summary = title,
                    Start = new CalDateTime(i),
                    End = new CalDateTime(i),
                    IsAllDay = true,
                    Description = $"{title} -- descriptions"
                };

                calendar.Events.Add(e);
            }

            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);
            return serializedCalendar;
        }

        public async Task<MemoryStream> GetFileMemoryStream(string datestring)
        {
            // Create an in-memory stream to hold the file data
            var memoryStream = new MemoryStream();

            // Write the data to the memory stream
            using (var writer = new StreamWriter(memoryStream, leaveOpen: true))
            {
                await writer.WriteAsync(datestring);
                await writer.FlushAsync();
            }

            // Reset the position of the stream to the beginning before reading it
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
