const WriteFilePlugin = require('write-file-webpack-plugin');
const path = require("path");
const NodePolyfillPlugin = require("node-polyfill-webpack-plugin");
const CopyPlugin = require("copy-webpack-plugin");

const replicationRelativeFolder = "\\..\\..\\..\\dist\\WebGLTemplates\\MinimalVkCom";

const checkingFunction = (value, commentary = null) => {
    console.log(commentary + value);
    return value;
};

const patterns = [
    {
        from: "./src/TemplateData/media", 
        to: checkingFunction(path.join(__dirname, replicationRelativeFolder, "TemplateData", "media"), `[static media] `),
        noErrorOnMissing: true,
        context: __dirname
    },
    {
        from: "./src/TemplateData/css", 
        to: checkingFunction(path.join(__dirname, replicationRelativeFolder, "TemplateData", "css"), `[static css] `),
        noErrorOnMissing: true,
        context: __dirname
    },
    {
        from: "./src/index.html", 
        to: checkingFunction(path.join(__dirname, replicationRelativeFolder), `[index.html]`),
        noErrorOnMissing: true,
        context: __dirname
    },
    { 
        from: "./src/TemplateData/js",
        to: checkingFunction(path.join(__dirname, replicationRelativeFolder, "TemplateData", "js"), "[static js]"),
        noErrorOnMissing: true,
        context: __dirname
    }
];

const copyPlugin = new CopyPlugin({patterns: patterns});

const writeFilesPlugin = new WriteFilePlugin();
 
module.exports = {
    plugins: [
        new NodePolyfillPlugin(),
        copyPlugin
    ],
    optimization: {minimize: false},
    entry: "./src/src/app.jsx",
    output: {
        filename: "app.js",
        path: path.resolve(__dirname, "..\\..\\..\\dist\\WebGLTemplates\\MinimalVkCom\\TemplateData\\js")
    },
    module: {
        rules: [
            {
                test: /\.(jsx|js)$/,
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