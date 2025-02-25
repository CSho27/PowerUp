const webpack = require('webpack');
const path = require('path');
const rules = require('./webpack.rules');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');

process.loadEnvFile();

module.exports = {
  target: "electron-renderer",
  devtool: "source-map",
  entry: './src/electronApp/main.tsx',
  output: { 
    filename: '[name].js',
    path: path.resolve(__dirname, 'dist/main'),
    clean: true,
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
    new webpack.DefinePlugin({
      'process.env': JSON.stringify(process.env)
    })
  ],
};