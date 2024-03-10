import react from '@vitejs/plugin-react';
import path from 'path';
import { defineConfig, loadEnv } from 'vite';

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '');
  
  return {
    plugins: [react()],
    server: {
      host: '0.0.0.0',
      port: Number(env.VITE_APP_PORT),
      open: true,
      watch: {
        usePolling: true,
      },
    },
    build: {
      outDir: 'build',
    },
    define: {
      // By default, Vite doesn't include shims for NodeJS/
      // necessary for segment analytics lib to work
      global: {},
    },
    resolve: {
      alias: [
        { find: 'app', replacement: path.resolve(__dirname, 'src/app') },
        {
          find: '@assets',
          replacement: path.resolve(__dirname, 'src/share/assets'),
        },
        {
          find: '@layouts',
          replacement: path.resolve(__dirname, 'src/share/layouts'),
        },
        {
          find: '@components',
          replacement: path.resolve(__dirname, 'src/share/components'),
        },
        {
          find: '@configs',
          replacement: path.resolve(__dirname, 'src/share/configs'),
        },
        {
          find: '@constants',
          replacement: path.resolve(__dirname, 'src/share/constants'),
        },
        {
          find: '@hooks',
          replacement: path.resolve(__dirname, 'src/share/hooks'),
        },
        {
          find: '@contexts',
          replacement: path.resolve(__dirname, 'src/share/contexts'),
        },
        {
          find: '@interfaces',
          replacement: path.resolve(__dirname, 'src/share/interfaces'),
        },
        {
          find: '@models',
          replacement: path.resolve(__dirname, 'src/share/models'),
        },
        {
          find: '@utils',
          replacement: path.resolve(__dirname, 'src/share/utils'),
        },
        {
          find: '@styles',
          replacement: path.resolve(__dirname, 'src/share/styles'),
        },
        {
          find: '@features',
          replacement: path.resolve(__dirname, 'src/features'),
        },
        {
          find: '@pages',
          replacement: path.resolve(__dirname, 'src/pages'),
        },
        {
          find: '@store',
          replacement: path.resolve(__dirname, 'src/store'),
        },
        {
          find: '@http-client',
          replacement: path.resolve(__dirname, 'src/share/http-client'),
        },
        {
          find: '@api',
          replacement: path.resolve(__dirname, 'src/share/api'),
        },
      ],
    },
  };
});
