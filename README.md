In order to work, prefab from VkSdk folder should be added to first launched scene in game.

### Quick start:

```
var sdkInstance = VkSdkFunctions.Instance; 
```
Contained in in VkSdk.Runtime.Root namespace

Instance field assigned in Awake of the scene, in which VkSdk prefab has added. Contains all functions of sdk


Functions of VkSdkFunctions:
---------------
```
sdkInstance.ShowInterstitial();

```
Shows interstitial advertisment, can be handled by events: InterstitialStarted, InterstitialEnded, InterstitialFailed

---------------
```
sdkInstance.ShowRewarded(string placement);
```
Shows rewarded advertisment, can be handled by events: RewardedStarted, RewardedEnded, RewardedFailed

---------------

VkSharingButton: attaches to passed to parameter button, on click it will launch vk sharing modal window
