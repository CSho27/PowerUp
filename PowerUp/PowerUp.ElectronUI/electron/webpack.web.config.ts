import path from 'path';
import { baseRules, renderingRules } from "./webpack.rules";
import { plugins } from "./webpack.plugins";
import HtmlWebPackPlugin from 'html-webpack-plugin';
import type { Configuration } from "webpack";

const config: Configuration = {
  devtool: 'source-map',
  entry: './src/webApp/index.tsx',
  output: {
    filename: 'index.js',
    path: path.resolve('../wwwroot')
  },
  module: {
    rules: [
      ...baseRules,
      ...renderingRules,
    ]
  },
  plugins: [
    ...plugins,
    new HtmlWebPackPlugin({
      template: "./src/index.html",
    })
  ],
  resolve: {
    extensions: ['.js', '.ts', '.jsx', '.tsx', '.css']
  },
}

export default config;