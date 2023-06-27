module.exports = {
   presets: [
      '@babel/preset-typescript',
      '@babel/preset-env',
      [
         '@babel/preset-react',
         {
            runtime: 'automatic',
         },
      ],
   ],
   plugins: [
      ['babel-plugin-transform-cx-jsx', { trimWhitespace: true, autoImportHtmlElement: true }],
      ['babel-plugin-transform-cx-imports', { useSrc: true }],
   ],
};
