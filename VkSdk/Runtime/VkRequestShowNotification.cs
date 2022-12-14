using UnityEngine;

namespace VkSdk
{
    public class VkRequestShowNotification : MonoBehaviour
    {
        private const string KeyRequested = "SuccessfullyTriedToAllowNotifications";

        private bool CanTryRequest => PlayerPrefs.GetInt(KeyRequested, 0) == 0;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            if (enabled == false)
                return;
            if (!CanTryRequest) return;
            Request();
            RestrictNextAttempts();
        }

        private void RestrictNextAttempts()
        {
            PlayerPrefs.SetInt(KeyRequested, 1);
        }

        private void Request()
        {
            Application.ExternalCall("tryAskShowNotifications");
        }

        [CallExternally]
        public void CancelRestriction() 
        {
            PlayerPrefs.SetInt(KeyRequested, 0);
        }
    }
}