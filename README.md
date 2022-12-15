# vk-unity-sdk

Advertising component must be added to clean object in first scene

## Contains: 

### Interstitial advertising call time
#### Events:
* AdvertisingStarted
* AdvertisingEnded
* InterstitialStarted
* InterstitialEnded
* RewardedStarted
* RewardedEnded
* RewardedClosed

#### Methods:
*   bool TryShowInterstitial() // false if interstitial advertising call time is not ended
*   void ShowRewarded(string key) // after end of ad, you will receive Rewarded*** with this key

#### Components:
*   Advertising: has everything descripted above
*   VkSharingButton: attaches to passed to parameter button, on click it will launch vk sharing modal window
