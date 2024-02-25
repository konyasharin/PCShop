import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      assets: '/src/assets',
      components: '/src/components',
      hooks: '/src/hooks',
      api: '/src/api',
      store: '/src/store',
      types: '/src/types',
      utils: '/src/utils',
      enums: '/src/enums',
    },
  },
});
