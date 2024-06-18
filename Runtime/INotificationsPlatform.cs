// Ignore Spelling: Fisip

using System;

namespace FisipGroup.CustomPackage.Notifications
{
    /// <summary>
    /// Interface for the notification platforms.
    /// Each platform iOS/Android, uses this platform as base for requesting notifications and sending them.
    /// </summary>
    public interface INotificationsPlatform
    {
        public void RequestAuthorization(Action<bool> callback);
        public void SendNotification(string channelID, string title, string text, int fireTimeMinutes, bool reschedule = false);
    }
}