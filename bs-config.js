// bs-config.js
module.exports = {
    proxy: "http://localhost:5085", // URL of your ASP.NET Core app
    files: ["./wwwroot/**/*.{html,htm,css,js}", "./Views/**/*.cshtml"],
    reloadDelay: 0,
    notify: false,
    open: false
};
