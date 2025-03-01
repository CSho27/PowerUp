const webpack = require('webpack');
const rules = require('./webpack.rules');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');

process.loadEnvFile();

module.exports = {
  target: "electron-main",
  devtool: "source-map",
  entry: './src/electronApp/main.tsx',
  output: { 
    filename: '[name].js',
  },
  watchOptions: {
    ignored: ['**/node_modules/', '**/dist']
  },
  module: {
    rules: rules
  },
  resolve: {
    extensions: [".ts", ".tsx", ".js"]
  },
  plugins: [
    new ForkTsCheckerWebpackPlugin({
      logger: 'webpack-infrastructure',
    }),
    /*
    new webpack.DefinePlugin({
      'process.env': JSON.stringify(process.env)
    })
      */
  ],
};