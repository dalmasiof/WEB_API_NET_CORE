using System;

namespace SmartSchool.WebAPI.Helper
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - dateTime.Year;

            if(currentDate<dateTime.AddYears(age))
                age--;

                return age;
        }
    }
}