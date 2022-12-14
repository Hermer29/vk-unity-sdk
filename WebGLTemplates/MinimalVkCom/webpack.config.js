const path = require("path");
const NodePolyfillPlugin = require("node-polyfill-webpack-plugin");

module.exports = {
    plugins: [
        new NodePolyfillPlugin()
    ],
    entry: "./src/app.jsx",
    output: {
        filename: "app.js",
        path: path.resolve(__dirname, "dist/TemplateData")
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules\/(?!react)/,
                use: 'babel-loader'
            },
        ]
    }, 
  resolve: {
    extensions: ['', '.js', '.jsx'],
    fallback: {
        "fs": false,
        "child_process": false,
        "worker_threads": false,
        "uglify-js": false,
        "@swc/core": false,
        "esbuild": false
    },
  }
};