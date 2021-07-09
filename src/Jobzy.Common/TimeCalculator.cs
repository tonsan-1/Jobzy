namespace Jobzy.Common
{
    using System;

    public static class TimeCalculator
    {
        public static string GetTimeAgo(DateTime time)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - time.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }

            if (delta < 60 * 2)
            {
                return "a minute ago";
            }

            if (delta < 45 * 60)
            {
                return ts.Minutes + " minutes ago";
            }

            if (delta < 90 * 60)
            {
                return "an hour ago";
            }

            if (delta < 24 * 60 * 60)
            {
                return ts.Hours + " hours ago";
            }

            if (delta < 48 * 60 * 60)
            {
                return "yesterday";
            }

            if (delta < 30 * 24 * 60 * 60)
            {
                return ts.Days + " days ago";
            }

            if (delta < 12 * 30 * 24 * 60 * 60)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }

            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }
}
