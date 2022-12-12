const vkSdkParameters = {};

window.addEventListener("load", () => {
    vkBridge.send('VKWebAppInit').then(data => {
        vkSdkParameters.vkUserId = getParameterByName("viewer_id");
        vkSdkParameters.accessToken = getParameterByName("access_token");
        window.vkInitialized = true;
        showBannerAdvertising();
    });
});

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function assertVkInitialized() {
    if(window.vkInitialized === undefined || window.vkInitialized === false)
        throw new Error("Vk not initialized yet");
}

function notifyFriendsPointsScored(amount)
{
    assertVkInitialized();
    const levelPointsScoredActivityId = 2;

    const request = vk.method("secure.addAppEvent", {
        access_token: vkSdkParameters.accessToken,
        user_id: vkSdkParameters.vkUserId,
        activity_id: levelPointsScoredActivityId,
        value: amount
    });
    handleUnityVkApiCallback(request, "NotifyPointsScored");
}

function notifyFriendsLevelReached(level) {
    assertVkInitialized();
    const levelUpdatedActivityId = 1;
    
    const request = vk.method("secure.addAppEvent", {
        access_token: vkSdkParameters.accessToken,
        user_id: vkSdkParameters.vkUserId,
        activity_id: levelUpdatedActivityId,
        value: level
    });
    handleUnityVkApiCallback(request, "NotifyLevelReached");
}

function notifyFriendsMissionCompleted(missionIdentifier)
{
    assertVkInitialized();
    
    const request = vk.method("secure.addAppEvent", {
        access_token: vkSdkParameters.accessToken,
        user_id: vkSdkParameters.vkUserId,
        activity_id: missionIdentifier
    });
    handleUnityVkApiCallback(request, "NotifyMissionCompleted");
}

function showBannerAdvertising() {
    assertVkInitialized();
    const bannerShow = vkBridge.send('VKWebAppShowBannerAd', {
        banner_location: 'bottom'
    });
    handleUnityVkBridgeCallback(bannerShow, "BannerAdvertisingShown");
}

function showInterstitial() {
    assertVkInitialized();
    const advertisingShow = vkBridge.send('VKWebAppShowNativeAds', {
        ad_format:"interstitial"
    });
    handleUnityVkBridgeCallbackWithParameter(advertisingShow, "InterstitialAdvertisingShown",
        "OnInterstitialEnded", "OnInterstitialShowError");
}

function showRewarded(name) {
    assertVkInitialized();
    const advertisingShow = vkBridge.send('VKWebAppShowNativeAds', {ad_format: "reward"});
    handleUnityVkBridgeCallback(advertisingShow, "RewardedAdvertisingShown", 
        "placement", name, "OnRewardedEnded", "OnRewardedFailed");
}

function share() {
    var shareRequest = vkBridge.send('VKWebAppShare', {
        link: ""
    });
    handleUnityVkBridgeCallback(shareRequest, "SharedToSomebody", "SharingSuccessful", "SharingFailed")
}

function handleUnityVkBridgeCallback(promise, operation, unityCallbackOnSuccess, unityCallbackOnError)
{
    const sdkRootObject = "VKSDK";
    const response = {};
    response["operation"] = operation;
    const unityCallbackMethod = "VkBridgeCallback";
    promise.then(data => {
        const success = data != null && data.result != null && data.result != false;
        if(success)
        {
            response.result = "success";
            window.unityInstance.SendMessage(sdkRootObject, unityCallbackMethod, JSON.stringify(response));
            console.log("[VKSDK:" + name + "] Operation successful");
            return;
        }
        failureResult("null/false promise result");
    });
    promise.catch(error => {
        failureResult(error);
    });
}

function handleUnityVkApiCallback(promise, operation, unityCallbackOnSuccess, unityCallbackOnError)
{
    const sdkRootObject = "VKSDK";
    const response = {};
    response["operation"] = operation;
    const unityCallbackMethod = "VkApiCallback";
    promise.then(data => {
        response.result = "success";
        window.unityInstance.SendMessage(sdkRootObject, unityCallbackMethod, JSON.stringify(response));
        console.log("[VKSDK:" + operation + "] Operation successful");
    });
    promise.catch(error => {
        vkApiFailureResult(response, error);
    });
}

function handleUnityVkBridgeCallbackWithParameter(promise, operation, parameterName, parameterValue, unityCallbackOnSuccess, unityCallbackOnError)
{
    const sdkRootObject = "VKSDK";
    const response = {};
    response["operation"] = operation;
    response["parameterName"] = parameterName;
    response["parameterValue"] = parameterValue;
    const unityCallbackMethod = "VkBridgeCallbackWithParameter";
    promise.then(data => {
        const success = data != null && data.result != null && data.result != false;
        if(success)
        {
            response.result = "success";
            window.unityInstance.SendMessage(sdkRootObject, unityCallbackMethod, JSON.stringify(response));
            console.log("[VKSDK:" + operation + "] Operation successful");
            return;
        }
        failureResult(response, "null/false promise result");
    });
    promise.catch(error => {
        failureResult(response, error);
    });
}

function vkApiFailureResult(response, error, unityCallbackOnError)
{
    response.result = "failure";
    response.errorCode = error.error.code;
    window.unityInstance.SendMessage(sdkRootObject, unityCallbackMethod, JSON.stringify(response));
    console.log("[VKSDK:" + response.operation + "] Error occured: " + additionToLog);
}

function failureResult(response, additionToLog, unityCallbackMethod)
{
    response.result = "failure";
    window.unityInstance.SendMessage(sdkRootObject, unityCallbackMethod, JSON.stringify(response));
    console.log("[VKSDK:" + response.operation + "] Error occured: " + additionToLog);
}