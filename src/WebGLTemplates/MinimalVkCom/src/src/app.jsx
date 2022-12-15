import React, { useState, useEffect } from 'react'
import { createRoot } from 'react-dom/client'
import { PromoBanner, View, Panel, FixedLayout, Spinner } from "@vkontakte/vkui"
import { QueryClient, QueryClientProvider, useQuery, useQueryClient, useMutation } from '@tanstack/react-query'
import bridge, {applyMiddleware} from "@vkontakte/vk-bridge"

const logger = ({ send, subscribe }) => next => async (method, props) => {
  const result = await next(method, props);
  console.log("Received answer from vkBridge:");
  console.log(result);
  return result;
};

const enhancedBridge = applyMiddleware(logger)(bridge);

const requestAdsMethod = 'VKWebAppGetAds';
const queryClient = new QueryClient();

const fetchBannerData = () => enhancedBridge.send(requestAdsMethod, {});

function Banner() {
  if(enhancedBridge.supports(requestAdsMethod) == false)
  {
    console.error(`${requestAdsMethod} not supported`)
    return null
  }

  const { isLoading, error, data } = useQuery(["banner"], fetchBannerData);

  console.log(`isLoading: ${isLoading}, error: ${error}, data:`);
  console.log(data);
  if (isLoading) return null;

  if (error) return null;

  return <PromoBanner bannerData={data} />;
}

const root = createRoot(
  document.getElementById('root')
);

enhancedBridge.send("VKWebAppInit", {});

enhancedBridge.subscribe(event => {
  if(event == false)
    return;

    console.log(event.detail.type);

    if(event.detail.type === "VKWebAppInitResult")
    {
      root.render(
        <React.StrictMode>
          <QueryClientProvider client={queryClient}>
            <View activePanel="promo">
              <Panel id="promo">
                <FixedLayout vertical="bottom">
                  <Banner />
                </FixedLayout>
              </Panel>
            </View>
          </QueryClientProvider>
        </React.StrictMode>
      );
      return;
    }
});