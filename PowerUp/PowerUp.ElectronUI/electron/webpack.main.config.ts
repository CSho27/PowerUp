import type { Configuration } from 'webpack';

import { baseRules, electronRules } from './webpack.rules';
import { plugins } from './webpack.plugins';

export const mainConfig: Configuration = {
  devtool: 'source-map',
  entry: './src/electronApp/main.ts',
  module: {
    rules: [
      ...baseRules,
      ...electronRules
    ],
  },
  plugins: plugins,
  resolve: {
    extensions: ['.js', '.ts', '.jsx', '.tsx', '.css', '.json'],
  },
};
