!function(){"use strict";var t=tinymce.util.Tools.resolve("tinymce.PluginManager"),a=tinymce.util.Tools.resolve("tinymce.util.Tools"),s=function(t,e,n){var r="UL"===e?"InsertUnorderedList":"InsertOrderedList";t.execCommand(r,!1,!1===n?null:{"list-style-type":n})},o=function(n){n.addCommand("ApplyUnorderedListStyle",function(t,e){s(n,"UL",e["list-style-type"])}),n.addCommand("ApplyOrderedListStyle",function(t,e){s(n,"OL",e["list-style-type"])})},e=function(t){var e=t.getParam("advlist_number_styles","default,lower-alpha,lower-greek,lower-roman,upper-alpha,upper-roman");return e?e.split(/[ ,]/):[]},n=function(t){var e=t.getParam("advlist_bullet_styles","default,circle,disc,square");return e?e.split(/[ ,]/):[]},u=function(t){return t&&/^(TH|TD)$/.test(t.nodeName)},c=function(r){return function(t){return t&&/^(OL|UL|DL)$/.test(t.nodeName)&&(n=t,(e=r).$.contains(e.getBody(),n));var e,n}},d=function(t){var e=t.dom.getParent(t.selection.getNode(),"ol,ul");return t.dom.getStyle(e,"listStyleType")||""},p=function(t){return a.map(t,function(t){return{text:t.replace(/\-/g," ").replace(/\b\w/g,function(t){return t.toUpperCase()}),data:"default"===t?"":t}})},f=function(i,l){return function(t){var o=t.control;i.on("NodeChange",function(t){var e=function(t,e){for(var n=0;n<t.length;n++)if(e(t[n]))return n;return-1}(t.parents,u),n=-1!==e?t.parents.slice(0,e):t.parents,r=a.grep(n,c(i));o.active(0<r.length&&r[0].nodeName===l)})}},m=function(e,t,n,r,o,i){var l;e.addButton(t,{active:!1,type:"splitbutton",tooltip:n,menu:p(i),onPostRender:f(e,o),onshow:(l=e,function(t){var e=d(l);t.control.items().each(function(t){t.active(t.settings.data===e)})}),onselect:function(t){s(e,o,t.control.settings.data)},onclick:function(){e.execCommand(r)}})},r=function(t,e,n,r,o,i){var l,a,s,u,c;0<i.length?m(t,e,n,r,o,i):(a=e,s=n,u=r,c=o,(l=t).addButton(a,{active:!1,type:"button",tooltip:s,onPostRender:f(l,c),onclick:function(){l.execCommand(u)}}))},i=function(t){r(t,"numlist","Numbered list","InsertOrderedList","OL",e(t)),r(t,"bullist","Bullet list","InsertUnorderedList","UL",n(t))};t.add("advlist",function(t){var e,n,r;n="lists",r=(e=t).settings.plugins?e.settings.plugins:"",-1!==a.inArray(r.split(/[ ,]/),n)&&(i(t),o(t))})}();