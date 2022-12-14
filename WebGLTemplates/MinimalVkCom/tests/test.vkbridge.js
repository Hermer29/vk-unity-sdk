import bridge from "@vkontakte/vk-bridge"
import assert from 'assert'

describe('VKBridge', function () {
  describe('#Vk', async function () {
    var initResult = await bridge.send('VKWebAppInit');
    var result = await bridge.send('VKWebAppGetAds');
    it('Should end execution in 5 seconds', function () {
      
    });
  });
});