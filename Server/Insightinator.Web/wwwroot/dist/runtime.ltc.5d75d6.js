(()=>{(()=>{"use strict";var h={},g={};function r(e){var f=g[e];if(f!==void 0)return f.exports;var i=g[e]={id:e,loaded:!1,exports:{}};return h[e].call(i.exports,i,i.exports,r),i.loaded=!0,i.exports}r.m=h,(()=>{var e=[];r.O=(f,i,s,n)=>{if(i){n=n||0;for(var a=e.length;a>0&&e[a-1][2]>n;a--)e[a]=e[a-1];e[a]=[i,s,n];return}for(var t=1/0,a=0;a<e.length;a++){for(var[i,s,n]=e[a],u=!0,l=0;l<i.length;l++)(n&!1||t>=n)&&Object.keys(r.O).every(p=>r.O[p](i[l]))?i.splice(l--,1):(u=!1,n<t&&(t=n));if(u){e.splice(a--,1);var o=s();o!==void 0&&(f=o)}}return f}})(),r.n=e=>{var f=e&&e.__esModule?()=>e.default:()=>e;return r.d(f,{a:f}),f},r.d=(e,f)=>{for(var i in f)r.o(f,i)&&!r.o(e,i)&&Object.defineProperty(e,i,{enumerable:!0,get:f[i]})},r.f={},r.e=e=>Promise.all(Object.keys(r.f).reduce((f,i)=>(r.f[i](e,f),f),[])),r.u=e=>""+({236:"user-routes",277:"overlays",379:"rich-text"}[e]||e)+".ltc."+{131:"57a803",236:"84799b",277:"7438c3",379:"2da560",417:"3cb59a"}[e]+".js",r.miniCssF=e=>"rich-text.ltc.b1e905.css",r.g=function(){if(typeof globalThis=="object")return globalThis;try{return this||new Function("return this")()}catch{if(typeof window=="object")return window}}(),r.hmd=e=>(e=Object.create(e),e.children||(e.children=[]),Object.defineProperty(e,"exports",{enumerable:!0,set:()=>{throw new Error("ES Modules may not assign module.exports or exports.*, Use ESM export syntax, instead: "+e.id)}}),e),r.o=(e,f)=>Object.prototype.hasOwnProperty.call(e,f),(()=>{var e={},f="cxjs-tailwind-template:";r.l=(i,s,n,a)=>{if(e[i]){e[i].push(s);return}var t,u;if(n!==void 0)for(var l=document.getElementsByTagName("script"),o=0;o<l.length;o++){var d=l[o];if(d.getAttribute("src")==i||d.getAttribute("data-webpack")==f+n){t=d;break}}t||(u=!0,t=document.createElement("script"),t.charset="utf-8",t.timeout=120,r.nc&&t.setAttribute("nonce",r.nc),t.setAttribute("data-webpack",f+n),t.src=i),e[i]=[s];var c=(b,p)=>{t.onerror=t.onload=null,clearTimeout(v);var m=e[i];if(delete e[i],t.parentNode&&t.parentNode.removeChild(t),m&&m.forEach(y=>y(p)),b)return b(p)},v=setTimeout(c.bind(null,void 0,{type:"timeout",target:t}),12e4);t.onerror=c.bind(null,t.onerror),t.onload=c.bind(null,t.onload),u&&document.head.appendChild(t)}})(),r.r=e=>{typeof Symbol<"u"&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},r.p="../../Server/Insightinator.Web/wwwroot/dist",(()=>{if(!(typeof document>"u")){var e=(n,a,t,u,l)=>{var o=document.createElement("link");o.rel="stylesheet",o.type="text/css";var d=c=>{if(o.onerror=o.onload=null,c.type==="load")u();else{var v=c&&(c.type==="load"?"missing":c.type),b=c&&c.target&&c.target.href||a,p=new Error("Loading CSS chunk "+n+` failed.
(`+b+")");p.code="CSS_CHUNK_LOAD_FAILED",p.type=v,p.request=b,o.parentNode&&o.parentNode.removeChild(o),l(p)}};return o.onerror=o.onload=d,o.href=a,t?t.parentNode.insertBefore(o,t.nextSibling):document.head.appendChild(o),o},f=(n,a)=>{for(var t=document.getElementsByTagName("link"),u=0;u<t.length;u++){var l=t[u],o=l.getAttribute("data-href")||l.getAttribute("href");if(l.rel==="stylesheet"&&(o===n||o===a))return l}for(var d=document.getElementsByTagName("style"),u=0;u<d.length;u++){var l=d[u],o=l.getAttribute("data-href");if(o===n||o===a)return l}},i=n=>new Promise((a,t)=>{var u=r.miniCssF(n),l=r.p+u;if(f(u,l))return a();e(n,l,null,a,t)}),s={666:0};r.f.miniCss=(n,a)=>{var t={379:1};s[n]?a.push(s[n]):s[n]!==0&&t[n]&&a.push(s[n]=i(n).then(()=>{s[n]=0},u=>{throw delete s[n],u}))}}})(),(()=>{var e={666:0};r.f.j=(s,n)=>{var a=r.o(e,s)?e[s]:void 0;if(a!==0)if(a)n.push(a[2]);else if(s!=666){var t=new Promise((d,c)=>a=e[s]=[d,c]);n.push(a[2]=t);var u=r.p+r.u(s),l=new Error,o=d=>{if(r.o(e,s)&&(a=e[s],a!==0&&(e[s]=void 0),a)){var c=d&&(d.type==="load"?"missing":d.type),v=d&&d.target&&d.target.src;l.message="Loading chunk "+s+` failed.
(`+c+": "+v+")",l.name="ChunkLoadError",l.type=c,l.request=v,a[1](l)}};r.l(u,o,"chunk-"+s,s)}else e[s]=0},r.O.j=s=>e[s]===0;var f=(s,n)=>{var[a,t,u]=n,l,o,d=0;if(a.some(v=>e[v]!==0)){for(l in t)r.o(t,l)&&(r.m[l]=t[l]);if(u)var c=u(r)}for(s&&s(n);d<a.length;d++)o=a[d],r.o(e,o)&&e[o]&&e[o][0](),e[o]=0;return r.O(c)},i=self.webpackChunkcxjs_tailwind_template=self.webpackChunkcxjs_tailwind_template||[];i.forEach(f.bind(null,0)),i.push=f.bind(null,i.push.bind(i))})()})();})();
