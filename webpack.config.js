var path = require("path");
var webpack = require("webpack");
var fableUtils = require("fable-utils");
var md = require('markdown-it')();


// ==== NOT USED ====
// var highlight = require('highlight.js');
// const marked = require("8fold-marked");
// const renderer = new marked.Renderer();


// renderer.heading = function (text, level) {
   
//     return '<h' + level + ' class="title is-'+level+'">' +  text +'</h' + level + '>';
//   }

// renderer.paragraph = function (text) {
    
//     var replacer = '<img src="/graphics/emojis/$1.png" class="emoji" align="absmiddle" height="20" width="20">';
//     var emoji = /:([A-Za-z0-9_\-\+]+?):/g;
//     return "<p>"+text.replace(emoji, replacer)+"</p>";
// }

// // Synchronous highlighting with highlight.js
// marked.setOptions({
//     highlight: function (code) {
//       return highlight.highlightAuto(code).value;
//     }
//   });

var message = {
    
      validate: function(params) {
        return params.trim().match(/^message\s+(.*)$/);
      },
    
      render: function (tokens, idx) {
        var m = tokens[idx].info.trim().match(/^message\s+(.*)$/);
    
        if (tokens[idx].nesting === 1) {
          // opening tag
          return '<article class="message"><div class="message-header"><p>' + md.utils.escapeHtml(m[1]) + '</p> </div><div class="message-body">\n';
    
        } else {
          // closing tag
          return '</div></article>\n';
        }
      }
    }


  

function resolve(filePath) {
    return path.join(__dirname, filePath)
}

var babelOptions = fableUtils.resolveBabelOptions({
    presets: [["es2015", { "modules": false }]],
    plugins: [["transform-runtime", {
        "helpers": true,
        // We don't need the polyfills as we're already calling
        // cdn.polyfill.io/v2/polyfill.js in index.html
        "polyfill": false,
        "regenerator": false
    }]]
});

var isProduction = process.argv.indexOf("-p") >= 0;
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

module.exports = {
    devtool: "source-map",
    entry: resolve('./src/makimono.fsproj'),
    output: {
        filename: 'bundle.js',
        path: resolve('./public'),
    },
    resolve: {
        modules: [
            "node_modules", resolve("./node_modules/")
        ]
    },
    devServer: {
        contentBase: resolve('./public'),
        port: 8080,
        hot: true,
        inline: true
    },
    module: {
        rules: [
            {
                test: /\.fs(x|proj)?$/,
                use: {
                    loader: "fable-loader",
                    options: {
                        babel: babelOptions,
                        define: isProduction ? [] : ["DEBUG"]
                    }
                }
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: babelOptions
                },
            },
            {
                test: /\.sass$/,
                use: [
                    "style-loader",
                    "css-loader",
                    "sass-loader"
                ]
            },
            // {
            //     test: /\.md$/, 
            //     use: [
            //             {
            //                 loader: "html-loader"
            //             },
            //             {
            //                 loader: "markdown-loader",
            //                 options: {
            //                     renderer,
            //                     gfm: true,
            //                     emoji: true
            //                 }
            //             }
            //     ]
            // },
            {
                test: /\.md$/, 
                use: [
                        {
                            loader: "html-loader"
                        },
                        {
                            loader: "markdownit-loader",
                            options: {
                                preset: 'default',
                                breaks: true,
                                html: true,
                                linkify: true,
                                typographer: true,
                                use: [
                                    'markdown-it-deflist',
                                    'markdown-it-abbr',
                                    ['markdown-it-container', 'message', message ],
                                    'markdown-it-footnote',
                                    'markdown-it-ins',
                                    'markdown-it-sub',
                                    'markdown-it-sup',
                                    'markdown-it-emoji',
                                    'markdown-it-mark'
                                ]

                            }
                        }
                ]
            }
        ]
    },
    plugins: isProduction ? [] : [
        new webpack.HotModuleReplacementPlugin(),
        new webpack.NamedModulesPlugin()
    ]
};
