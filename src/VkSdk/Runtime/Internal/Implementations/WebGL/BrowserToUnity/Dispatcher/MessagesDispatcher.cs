using UnityEngine;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Requests;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Responses;
using VkSdk.Runtime.Internal.Library.Observables;
using VkSdk.Runtime.Utility;

namespace VkSdk.Runtime.Internal.Implementations.WebGL.BrowserToUnity
{
    internal class MessagesDispatcher : MonoBehaviour
    {
        private IBackendMessenger _messenger;

        internal void Construct(IBackendMessenger messenger)
        {
            _messenger = messenger;

            _messenger.Subscribe(OnMessageReceived);
        }

        internal void OnMessageReceived(RequestMessage message)
        {
            switch (message)
            {
                case NotifyFriendsLevelReachedMessage concrete:
                    Application.ExternalCall("notifyFriendsLevelReached", concrete.Level);
                    break;
                case NotifyFriendsMissionAccomplishedMessage concrete:
                    Application.ExternalCall("notifyFriendsMissionCompleted", concrete.MissionIdentifier);
                    break;
                case NotifyFriendsPointsScoredMessage concrete:
                    Application.ExternalCall("notifyFriendsPointsScored", concrete.Score);
                    break;
                case ShareMessage concrete:
                    Application.ExternalCall("share");
                    break;
                case ShowInterstitialAdvertisingMessage concrete:
                    Application.ExternalCall("showInterstitial");
                    break;
                case ShowRewardedAdvertisingMessage concrete:
                    Application.ExternalCall("showRewarded", concrete.Placement);
                    break;
            }
        }

        [CallExternally]
        public void OnInterstitialEnded()
        {
            _messenger.Put(new InterstitialResponseMessage(ResponseType.Successful));
        }

        [CallExternally]
        public void OnInterstitialShowError()
        { 
            _messenger.Put(new InterstitialResponseMessage(ResponseType.Failed));
            VkSdkLogger.LogErrorWithMethodName("ShowInterstitial", "Interstitial show error");
        }

        [CallExternally]
        public void SharingSuccessful()
        {
            _messenger.Put(new SharingResponseMessage(ResponseType.Successful));
            VkSdkLogger.LogWithMethodName("Share", "Sharing successful");
        }

        [CallExternally]
        public void SharingFailed()
        {
            _messenger.Put(new SharingResponseMessage(ResponseType.Failed));
            VkSdkLogger.LogErrorWithMethodName("Share", "Sharing failed");
        }
    }
}