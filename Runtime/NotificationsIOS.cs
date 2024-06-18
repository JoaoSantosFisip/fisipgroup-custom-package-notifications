// Ignore Spelling: Fisip

using System;
using System.Threading.Tasks;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

namespace FisipGroup.CustomPackage.Notifications
{
    /// <summary>
    /// Handles the iOS notifications.
    /// </summary>
    public class NotificationsIOS : INotificationsPlatform
    {
        public void RequestAuthorization(Action<bool> callback)
        {
#if UNITY_IOS
            var request = new AuthorizationRequest(AuthorizationOption.Alert | AuthorizationOption.Badge, true);

            AuthorizationRequestCallback(request, callback);
#endif        
        }
        public void SendNotification(string channelID, string title, string text, int fireTimeMinutes, bool reschedule = false)
        {
#if UNITY_IOS
            // Checks if notification exists, and if yes, reschedule
            if (reschedule)
            {
                foreach (var notif in iOSNotificationCenter.GetScheduledNotifications())
                {
                    if(notif.Identifier == channelID) 
                    {
                        iOSNotificationCenter.RemoveScheduledNotification(channelID);
                        
                        break;
                    }
                }
            }

            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = TimeSpan.FromMinutes(fireTimeMinutes),
                Repeats = false
            };

            var notification = new iOSNotification()
            {
                Identifier = channelID,
                Title = title,
                Body = text,
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = channelID,
                ThreadIdentifier = channelID,
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
#endif
        }

#if UNITY_IOS
        // Since the iOS permission request doesn't have a callback, we create our own.
        private async void AuthorizationRequestCallback(AuthorizationRequest request, Action<bool> callback) 
        {
            while (!request.IsFinished)
            {
                await Task.Yield();
            }

            callback?.Invoke(request.Granted);
        }
#endif
    }
}