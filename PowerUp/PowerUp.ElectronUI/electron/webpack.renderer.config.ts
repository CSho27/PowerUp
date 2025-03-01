import type { Configuration } from 'webpack';

import { baseRules, electronRules, renderingRules } from './webpack.rules';
import { plugins } from './webpack.plugins';

export const rendererConfig: Configuration = {
  devtool: 'source-map',
  module: {
    rules: [
      ...baseRules,
      ...electronRules,
      ...renderingRules,
    ],
  },
  plugins: plugins,
  resolve: {
    extensions: ['.js', '.ts', '.jsx', '.tsx', '.css', '.json'],
  },
};
