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

```
sdkInstance.NotifyFriendsLevelReached(int level);
```
Updates last completed level, which shows in vk notifications and friend list in games page

---------------

```
sdkInstance.NotifyFriendsPointsScored(int points);
```
Like last method, but updates score value

----------------

```
sdkInstance.NotifyFriendsMissionAccomplishedMessage(int missionIdentifier);
```
Like last method, but shows that user completes mission with specified identifier. Mission list for game configured in vk

----------------

VkSharingButton: attaches to passed to parameter button, on click it will launch vk sharing modal window
