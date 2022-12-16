using UnityEngine;

namespace VkSdk.Runtime.Utility
{
    public static class VkSdkLogger
    {
        private static string Sign 
        {
            get
            {
#if !UNITY_EDITOR
                return "[VKSDK]";
#else
                return "<color=blue>[VKSDK]</color>";
#endif
            }
        }

        private static string AddTextToSign(string text)
        {
#if !UNITY_EDITOR
            return $"[VKSDK:{text}]";
#else
            return $"<color=blue>[VKSDK:{text}]</color>";
#endif
        }

        public static void LogWarning(string message, UnityEngine.Object caller)
        {
            Debug.LogWarning($"{Sign} {message}", caller);
        }

        public static void LogErrorWithMethodName(string methodName, string message)
        {
            Debug.LogError($"{AddTextToSign(methodName)} {message}");
        }

        public static void LogWarningWithMethodName(string methodName, string message)
        {
            Debug.LogWarning($"{AddTextToSign(methodName)} {message}");
        }

        public static void LogWithMethodName(string methodName, string message)
        {
            Debug.Log($"{AddTextToSign(methodName)} {message}");
        }
    }
}