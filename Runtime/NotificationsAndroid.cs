// Ignore Spelling: Fisip

using System;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;
using UnityEngine.Android;

namespace FisipGroup.CustomPackage.Notifications
{
    /// <summary>
    /// Handles the android notifications.
    /// </summary>
    public class NotificationsAndroid : INotificationsPlatform
    {
        private static readonly string Permission = "android.permission.POST_NOTIFICATIONS";

        public void RequestAuthorization(Action<bool> callback)
        {
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(Permission))
            {
                var permissionCallback = new PermissionCallbacks();
                permissionCallback.PermissionGranted += (response) => callback?.Invoke(true);
                permissionCallback.PermissionDenied += (response) => callback?.Invoke(false);
                permissionCallback.PermissionDeniedAndDontAskAgain += (response) => callback?.Invoke(false);

                UnityEngine.Android.Permission.RequestUserPermission(Permission, permissionCallback);
            }
            else
            {
                callback?.Invoke(true);
            }
        }

        public void SendNotification(string channelID, string title, string text, int fireTimeMinutes, bool reschedule = false)
        {
#if UNITY_ANDROID
            // If notification channel doesn't exist create one
            if (string.IsNullOrEmpty(AndroidNotificationCenter.GetNotificationChannel(channelID).Id))
            {
                AndroidNotificationCenter.RegisterNotificationChannel(new AndroidNotificationChannel()
                    {
                        Id = channelID,
                        Name = channelID,
                        Description = text,
                        Importance = Importance.Default,
                    });
            }


            if (reschedule)
            {
                var notificationID = PlayerPrefs.GetInt(channelID);
                
                if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationID) == NotificationStatus.Scheduled)
                {
                    AndroidNotificationCenter.CancelNotification(notificationID);
                }
            }

            var notification = new AndroidNotification
            {
                Title = title,
                Text = text,
                FireTime = DateTime.Now.AddMinutes(fireTimeMinutes),
                SmallIcon = "icon_0",
                LargeIcon = "icon_1"
            };

            PlayerPrefs.SetInt(channelID, AndroidNotificationCenter.SendNotification(notification, channelID));
#endif
        }
    }
}