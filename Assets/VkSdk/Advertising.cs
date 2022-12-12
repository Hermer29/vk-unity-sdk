using System;
using System.Security.Cryptography;
using UnityEngine;

namespace VkSdk
{
    public class Advertising : MonoBehaviour
    {
        [SerializeField] private SerializedTime _interstitialAdvertisingTimeInterval;
        
        private static TimeSpan InterstitialAdvertisingInterval;
        private static DateTime? LastInterstitialAdvertisingTime;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            var doubleOf = FindObjectOfType<Advertising>(); 
            if(doubleOf != null && doubleOf.gameObject != gameObject)
                Destroy(doubleOf.gameObject);
            InterstitialAdvertisingInterval = new TimeSpan(_interstitialAdvertisingTimeInterval.hours, _interstitialAdvertisingTimeInterval.minutes,
                _interstitialAdvertisingTimeInterval.seconds);
        }

        public static event Action AdvertisingStarted;
        public static event Action AdvertisingEnded;

        #region Interstitial advertising members

        public static event Action InterstitialEnded;

        public static event Action InterstitialStarted;
        
        public static bool TryShowInterstitial()
        {
            if (IsCooldownPassed() == false)
                return false;
            
#if !UNITY_EDITOR && UNITY_WEBGL 
            Application.ExternalCall("showInterstitial");
#else
            OnInterstitialPlayingOnWrongPlatform();
#endif  
            InterstitialStarted?.Invoke();
            AdvertisingStarted?.Invoke();
            LastInterstitialAdvertisingTime = DateTime.Now;
            return true;
        }

        private static bool IsCooldownPassed()
        {
            if (LastInterstitialAdvertisingTime == null)
                return true;
            var nextAvailableAdvertisingTime = LastInterstitialAdvertisingTime.Value + InterstitialAdvertisingInterval;
            return DateTime.Now > nextAvailableAdvertisingTime;
        }
        
        public static void OnInterstitialPlayingOnWrongPlatform()
        {
            Debug.Log($"[{nameof(Advertising)}] Interstitial can't be played not in WEBGL build. Interstitial ended");
            InterstitialEnded?.Invoke();
            AdvertisingEnded?.Invoke();
        }
        #endregion

        #region Rewarded advertising members

        public static event Action<string> RewardedStarted;

        public static event Action<string> RewardedEnded;

        public static event Action<string> RewardedClosed;
        
        public static void ShowRewarded(string key)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            Application.ExternalCall("showRewarded", key);
#else
            OnRewardedPlayingOnWrongPlatform(key);
#endif
            RewardedStarted?.Invoke(key);
            AdvertisingStarted?.Invoke();
        }
        
        public static void OnRewardedPlayingOnWrongPlatform(string name)
        {
            VkSdkLogger.Log("Rewarded can't be played not in WEBGL build. Rewarded ended");
            RewardedEnded?.Invoke(name);
            AdvertisingEnded?.Invoke();
        }
        #endregion

        #region External callbacks

        [CallExternally]
        public void OnRewardedFailed(string name)
        {
            RewardedClosed?.Invoke(name);
            AdvertisingEnded?.Invoke();
        }

        [CallExternally]
        public void OnRewardedEnded(string name)
        {
            RewardedEnded?.Invoke(name);
            AdvertisingEnded?.Invoke();
        }

        [CallExternally]
        public void OnInterstitialEnded()
        {
            InterstitialEnded?.Invoke();
            AdvertisingEnded?.Invoke();
        }

        [CallExternally]
        public void OnInterstitialShowError()
        {
            VkSdkLogger.Log("Interstitial show error");
            InterstitialEnded?.Invoke();
            AdvertisingEnded?.Invoke();
        }

        [CallExternally]
        public void SharingSuccessful()
        {
            VkSdkLogger.Log("Sharing successful");
        }

        [CallExternally]
        public void SharingFailed()
        {
            VkSdkLogger.Log("Sharing failed");
        }
        #endregion
    }
}