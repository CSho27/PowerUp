const webpack = require('webpack');
const path = require('path');

module.exports = getConfig;

process.loadEnvFile();
function getConfig(env, argv) {
  return {
    mode: argv.mode,
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
      rules: [
        {
          test: /\.(ts|tsx)$/,
          exclude: /node_modules/,
          use: {
            loader: "ts-loader"
          }
        },
        {
          test: /\.css$/i,
          use: ["style-loader", "css-loader"],
        },
      ]
    },
    resolve: {
      extensions: [".ts", ".tsx", ".js"]
    },
    plugins: [
      new webpack.DefinePlugin({
        'process.env': JSON.stringify(process.env)
      })
    ],
    node: {
      __dirname: true
    }
  };
}