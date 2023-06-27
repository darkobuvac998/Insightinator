const webpack = require('webpack'),
   MiniCssExtractPlugin = require('mini-css-extract-plugin'),
   { CleanWebpackPlugin } = require('clean-webpack-plugin'),
   CopyWebpackPlugin = require('copy-webpack-plugin'),
   { merge } = require('webpack-merge'),
   common = require('./webpack.config'),
   path = require('path'),
   p = (p) => path.join(__dirname, '../', p || ''),
   { EsbuildPlugin } = require('esbuild-loader');

module.exports = merge(common({ rootCssLoader: MiniCssExtractPlugin.loader }), {
   mode: 'production',

   output: {
      path: path.resolve(__dirname, '../../Server/Insightinator.Web/wwwroot/dist'),
      publicPath: '../../Server/Insightinator.Web/wwwroot/dist',
      filename: '[name].ltc.[contenthash].js',
      chunkFilename: '[name].ltc.[contenthash].js',
      hashDigestLength: 6,
   },

   optimization: {
      minimizer: [
         new EsbuildPlugin({
            target: 'es2022',
            css: true,
         }),
      ],
      concatenateModules: true,
      minimize: true,
   },

   plugins: [
      new webpack.DefinePlugin({
         'process.env.NODE_ENV': JSON.stringify('production'),
      }),
      new MiniCssExtractPlugin({
         filename: '[name].ltc.[contenthash].css',
         chunkFilename: '[name].ltc.[contenthash].css',
      }),
      new CopyWebpackPlugin({
         patterns: [
            {
               from: p('/assets'),
               to: path.resolve(__dirname, '../../Server/Insightinator.Web/wwwroot/assets'),
            },
         ],
      }),
      new CleanWebpackPlugin({
         dry: false,
         dangerouslyAllowCleanPatternsOutsideProject: true,
      }),
   ],
});
