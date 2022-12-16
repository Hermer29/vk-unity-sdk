using System;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Requests;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Responses;
using VkSdk.Runtime.Internal.Library.Observables;
using VkSdk.Runtime.Utility;

namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging.UnityToImplementation
{
    internal class MessagesFrontend : IMessagesFrontend
    {
        private readonly IFrontendMessenger _sender;

        public MessagesFrontend(IFrontendMessenger sender)
        {
            _sender = sender;

            _sender.Subscribe(OnMessageReceived);
        }
        
        public event Action<string> RewardedEnded;
        public event Action<string> RewardedShowing;
        public event Action<string> RewardedError;
        public event Action InterstitialShowing;
        public event Action InterstitialEnded;
        public event Action InterstitialError;

        private void OnMessageReceived(ResponseMessage responseMessage)
        {
            switch (responseMessage)
            {
                case InterstitialResponseMessage msg:
                    switch (msg.ResponseType)
                    {
                        case ResponseType.Failed:
                            VkSdkLogger.LogErrorWithMethodName(nameof(ShowInterstitial), "Interstitial show failed");
                            InterstitialError?.Invoke();
                            break;
                        case ResponseType.Successful:
                            VkSdkLogger.LogWithMethodName(nameof(ShowInterstitial), "Interstitial successfully ended");
                            InterstitialEnded?.Invoke();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case RewardedResponseMessage msg:
                    switch (msg.ResponseType)
                    {
                        case ResponseType.Failed:
                            VkSdkLogger.LogErrorWithMethodName(nameof(ShowRewarded), "Rewarded show failed");
                            RewardedError?.Invoke(msg.AdvertisingPlacement);
                            break;
                        case ResponseType.Successful:
                            VkSdkLogger.LogWithMethodName(nameof(ShowRewarded), "Rewarded show successful");
                            RewardedEnded?.Invoke(msg.AdvertisingPlacement);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
            }
        }
        
        public void ShowRewarded(string placement)
        {
            _sender.Put(new ShowRewardedAdvertisingMessage(placement));
        }

        public void ShowInterstitial()
        {
            _sender.Put(new ShowInterstitialAdvertisingMessage());
        }

        public void NotifyFriendsLevelReached(int level)
        {
            _sender.Put(new NotifyFriendsLevelReachedMessage(level));
        }

        public void NotifyFriendsPointsScored(int score)
        {
            _sender.Put(new NotifyFriendsPointsScoredMessage(score));
        }

        public void NotifyFriendsMissionAccomplishedMessage(int missionIdentifier)
        {
            _sender.Put(new NotifyFriendsMissionAccomplishedMessage(missionIdentifier));
        }

        public void Share()
        {
            _sender.Put(new ShareMessage());
        }
    }
}