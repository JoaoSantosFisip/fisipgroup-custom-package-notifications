// Ignore Spelling: Fisip

using System;
using UnityEngine;

namespace FisipGroup.CustomPackage.Notifications
{
    /// <summary>
    /// This class acts as a middleman to send notifications.
    /// It will create a notification platform depending on the selected platform on Unity or build.
    /// </summary>
    public static class NotificationsManager
    {
        private static INotificationsPlatform Platform;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Platform =
#if UNITY_ANDROID
                new NotificationsAndroid();
#elif UNITY_IOS
                new NotificationsIOS();
#else
                new NotificationsAndroid();
#endif

        }

        public static void RequestAuthorization(Action<bool> callback)
        {
            Platform.RequestAuthorization(callback);
        }
        public static void SendNotification(string channelID, string title, string text, int fireTimeMinutes, bool replace = false)
        {
            Platform.SendNotification(channelID, title, text,fireTimeMinutes, replace);
        }
    }
}