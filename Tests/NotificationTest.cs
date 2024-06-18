// Ignore Spelling: Fisip

using UnityEngine;

namespace FisipGroup.CustomPackage.Notifications
{
    /// <summary>
    /// Sends a test notification if the build is on development mode.
    /// </summary>
    public static class NotificationTest
    {
        private static readonly string Channel = "TestNotification";
        private static readonly int FireTimeInMinutes = 5;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
#if DEVELOPMENT_BUILD
            SendTestNotification();
#endif
        }

        private static void SendTestNotification()
        {
            NotificationsManager.RequestAuthorization((granted) =>
            {
                if (granted)
                {
                    NotificationsManager.SendNotification(
                        Channel,
                        "This is a test notification",
                        "Testing if notifications are working",
                        FireTimeInMinutes,
                        replace: true);
                }
            });
        }
    }
}