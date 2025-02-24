import HtmlWebPackPlugin from "html-webpack-plugin";
import CopyWebpackPlugin from "copy-webpack-plugin";

export default getConfig;

function getConfig(env, argv) {
  const entries = {
    main: { entry: './src/electronApp/main.tsx' },
    preload: { entry: './src/electronApp/preload.tsx' },
    renderer: { entry: './src/electronApp/renderer.tsx', html: true },
    index: { entry: './src/webApp/renderer.tsx', html: true },
  }

  const htmlPlugins = Object.keys(entries).filter(name => !!entries[name].html).map(name => {
    return new HtmlWebPackPlugin({
      template: "./src/index.html",
      filename: `./${name}.html`,
      chunks: [name],
    });
  });

  const copyPatterns = [
    { from: 'dist/index.js', to: '../../wwwroot/index.js' },
    { from: 'dist/index.html', to: '../../wwwroot/index.html' }
  ];
  if(argv.mode === 'development') copyPatterns.push({ from: 'dist/index.js.map', to: '../../wwwroot/index.js.map' },)

  return {
    mode: argv.mode,
    target: "electron-renderer",
    devtool: "source-map",
    entry: Object.fromEntries(Object.entries(entries).map(([name, { entry }]) => [name, entry])),
    output: { 
      filename: '[name].js',
      clean: true,
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
      ...htmlPlugins,
      new CopyWebpackPlugin({ patterns: copyPatterns }),
    ]
  };
}