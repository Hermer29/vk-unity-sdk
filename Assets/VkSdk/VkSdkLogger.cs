using UnityEngine;

namespace VkSdk
{
    public static class VkSdkLogger
    {
        public static void Log(string message)
        {
            Debug.Log($"[VkSdk] {message}");
        }
    }
}