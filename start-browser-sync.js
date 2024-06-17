const browserSync = require('browser-sync').create();

browserSync.init({
    proxy: "http://localhost:5085",
    files: ["./wwwroot/**/*.{html,htm,css,js}", "./Views/**/*.cshtml"],
    reloadDelay: 0,
    notify: false,
    open: false
});