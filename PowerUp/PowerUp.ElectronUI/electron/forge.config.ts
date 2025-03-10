import type { ForgeConfig } from '@electron-forge/shared-types';
import { MakerSquirrel } from '@electron-forge/maker-squirrel';
import { MakerZIP } from '@electron-forge/maker-zip';
import { MakerDeb } from '@electron-forge/maker-deb';
import { MakerRpm } from '@electron-forge/maker-rpm';
import { MakerDMG } from '@electron-forge/maker-dmg';
import { AutoUnpackNativesPlugin } from '@electron-forge/plugin-auto-unpack-natives';
import { WebpackPlugin } from '@electron-forge/plugin-webpack';
import { FusesPlugin } from '@electron-forge/plugin-fuses';
import { FuseV1Options, FuseVersion } from '@electron/fuses';
import { PublisherGithub } from '@electron-forge/publisher-github';
import dotenv from 'dotenv';
import { mainConfig } from './webpack.main.config';
import { rendererConfig } from './webpack.renderer.config';

dotenv.config();

const config: ForgeConfig = {
  packagerConfig: {
    asar: true,
    extraResource: 'config.json',
    icon: 'public/PowerUpIcon'
  },
  rebuildConfig: {},
  makers: [
    new MakerSquirrel({
      setupIcon: 'public/PowerUpIcon.ico'
    }),
    new MakerDMG({
      icon: 'public/PowerUpIcon.icns'
    }),
    new MakerZIP({}, ['darwin']),
    new MakerRpm({
      options: {
        icon: '/path/to/icon.png'
      }
    }), 
    new MakerDeb({
      options: {
        icon: 'public/PowerUpIcon.png'
      }
    })
  ],
  publishers: [
    new PublisherGithub({
      repository: { owner: 'CSho27', name: 'PowerUp' },
      authToken: process.env.GITHUB_TOKEN,
      force: true
    })
  ],
  plugins: [
    new AutoUnpackNativesPlugin({}),
    new WebpackPlugin({
      mainConfig: mainConfig,
      packageSourceMaps: true,
      renderer: {
        config: rendererConfig,
        entryPoints: [
          {
            html: './src/index.html',
            js: './src/electronApp/renderer.tsx',
            name: 'main_window',
            preload: {
              js: './src/electronApp/preload.ts',
            },
          },
        ],
      },
    }),
    // Fuses are used to enable/disable various Electron functionality
    // at package time, before code signing the application
    new FusesPlugin({
      version: FuseVersion.V1,
      [FuseV1Options.RunAsNode]: false,
      [FuseV1Options.EnableCookieEncryption]: true,
      [FuseV1Options.EnableNodeOptionsEnvironmentVariable]: false,
      [FuseV1Options.EnableNodeCliInspectArguments]: false,
      [FuseV1Options.EnableEmbeddedAsarIntegrityValidation]: true,
      [FuseV1Options.OnlyLoadAppFromAsar]: true,
    }),
  ],
};

export default config;
