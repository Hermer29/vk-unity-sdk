using UnityEngine;
using UnityEngine.UI;

namespace VkSdk
{
    public class VkSharingButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private void Start()
        {
            _button.onClick.AddListener(Share);
        }

        private void Share()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalCall("share");
            Debug.Log($"[{nameof(VkSharingButton)}] Sharing to vk requested");
#else       
            Debug.Log($"[{nameof(VkSharingButton)}] Vk sharing will work only in web gl build");
#endif
        }
    }
}