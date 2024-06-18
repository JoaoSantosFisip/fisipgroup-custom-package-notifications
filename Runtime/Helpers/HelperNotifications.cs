// Ignore Spelling: Fisip

namespace FisipGroup.CustomPackage.Notifications.Helpers
{
    /// <summary>
    /// Contains methods to help manage notifications.
    /// </summary>
    public static class HelperNotifications
    {
        /// <summary>
        /// Returns the amount of minutes in "x" days.
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static int GetMinutesFromDays(int days)
        {
            return days * 1440;
        }

        /// <summary>
        /// Returns the amount of minutes in "x" hours.
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int GetMinutesFromHours(int hours)
        {
            return hours * 60;
        }
    }
}