const HtmlWebpackPlugin = require('html-webpack-plugin'),
   path = require('path'),
   babelCfg = require('./babel.cx.config'),
   p = (p) => path.join(__dirname, '../', p || ''),
   CxScssManifestPlugin = require('./CxScssMainfestPlugin'),
   tailwindConfig = require('../tailwind.config'),
   CopyWebpackPlugin = require('copy-webpack-plugin'),
   tailwindcss = require('tailwindcss');

module.exports = ({ rootCssLoader, tailwindOptions }) => {
   return {
      resolve: {
         alias: {
            app: p('app'),
         },
         extensions: ['.js', '.jsx', '.ts', '.tsx'],
      },

      // externals: {
      //    react: 'React',
      //    'react-dom': 'ReactDOM',
      // },

      module: {
         rules: [
            {
               test: /\.(js|jsx|ts|tsx)$/,
               //add here any ES6 based library
               include: [
                  p('common'),
                  p('app'),
                  /packages[\\\/]cx/,
                  /node_modules[\\\/](cx|cx-react|cx-theme-\w*|cx-google-maps)[\\\/]/,
               ],
               use: [
                  {
                     loader: 'swc-loader',
                     options: {
                        jsc: {
                           //loose: true,
                           parser: {
                              syntax: 'typescript',
                              tsx: true,
                           },
                           transform: {
                              react: {
                                 pragma: 'VDOM.createElement',
                              },
                           },
                        },
                     },
                  },
                  { loader: 'babel-loader', options: babelCfg },
               ],
            },
            {
               test: /\.scss$/,
               use: [rootCssLoader, 'css-loader', 'sass-loader'],
            },
            {
               test: /\.css$/,
               use: [
                  rootCssLoader,
                  'css-loader',
                  {
                     loader: 'postcss-loader',
                     options: {
                        postcssOptions: {
                           ident: 'postcss',
                           plugins: [tailwindcss({ ...tailwindConfig, ...tailwindOptions })],
                           cacheInclude: [/.*\.(css|scss)$/, /.tailwind\.config\.js$/],
                        },
                     },
                  },
               ],
            },
            {
               test: /\.(png|jpg|svg)$/,
               type: 'asset',
            },
         ],
      },
      entry: {
         app: [p('app/index.js'), p('app/index.scss'), p('app/tailwind.css')],
      },
      plugins: [
         new HtmlWebpackPlugin({
            template: p('app/index.html'),
         }),

         new CxScssManifestPlugin({
            outputPath: p('app/manifest.scss'),
         }),
         new CopyWebpackPlugin({
            patterns: [{ from: p('/assets'), to: p('../../Server/Insightinator.Web/wwwroot/assets') }],
         }),
      ],

      optimization: {
         runtimeChunk: 'single',
      },
   };
};
