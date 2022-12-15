xcopy "./src/VkSdk" "./dist/VkSdk" /e /i /h
cd "src/WebGLTemplates/MinimalVkCom"
npx webpack --config ./webpack.config.js