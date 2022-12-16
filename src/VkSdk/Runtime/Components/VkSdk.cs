using UnityEngine;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.UnityToImplementation;
using VkSdk.Runtime.Internal.Implementations.WebGL.BrowserToUnity;
using VkSdk.Runtime.Root;

namespace VkSdk.Runtime.Components
{
    [RequireComponent(typeof(MessagesDispatcher), typeof(VkSdkFunctions))]
    public class VkSdk : MonoBehaviour
    {
        private MessagesDispatcher _webGlDispatcher;
        private MessagesFrontend _frontend;
        private Bus _bus;
        private IFrontendMessenger _frontendMessengerImplementation;
        private VkSdkFunctions _functions;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _bus = new Bus();
            _frontend = new MessagesFrontend(_bus);
            _webGlDispatcher.Construct(_bus);
            _functions.Construct(_frontend);
        }
    }
}