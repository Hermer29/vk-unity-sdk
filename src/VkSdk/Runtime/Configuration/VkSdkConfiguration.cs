using UnityEngine;
using VkSdk.Runtime.Utility;

namespace VkSdk.Runtime.Configuration
{
    public class VkSdkConfiguration : ScriptableObject
    {
        [SerializeField] private SerializedTime _interstitialShowFrequency;

        public SerializedTime InterstitialShowFrequency => _interstitialShowFrequency;
    }
}