var express = require("express");
const routes = require('./routes/api.js')

process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";

const PORT = process.env.PORT ?? 3000
var app = express();

app.use(express.json({
    limit: '50mb'
}));

app.use(express.urlencoded({
    limit: '50mb',
    parameterLimit: 100000,
    extended: true
}));

app.use(routes.router)

app.listen(PORT, () => {
    console.log(`Server has been started on port ${PORT}...`)
});
