const HtmlWebPackPlugin = require("html-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const rules = require('./webpack.rules');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');

const entries = {
  preload: { entry: './src/electronApp/preload.tsx' },
  renderer: { entry: './src/electronApp/renderer.tsx', html: true },
};

const htmlPlugins = Object.keys(entries).filter(name => !!entries[name].html).map(name => {
  return new HtmlWebPackPlugin({
    template: "./src/index.html",
    filename: `./${name}.html`,
    chunks: [name],
  });
});

module.exports = {
  target: "electron-renderer",
  devtool: "source-map",
  entry: Object.fromEntries(Object.entries(entries).map(([name, { entry }]) => [name, entry])),
  output: { 
    filename: '[name].js',
  },
  watchOptions: {
    ignored: ['**/node_modules/', '**/dist']
  },
  module: {
    rules: rules,
  },
  resolve: {
    extensions: [".ts", ".tsx", ".js"]
  },
  plugins: [
    new ForkTsCheckerWebpackPlugin({
      logger: 'webpack-infrastructure',
    }),
    ...htmlPlugins
  ],
};