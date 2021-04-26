const fs = require('fs');

module.exports = {
    devServer: {
        disableHostCheck: true,
        open: process.platform === 'darwin',
        host: '0.0.0.0',
        port: 2000, // CHANGE YOUR PORT HERE!
        key: fs.readFileSync('./localhost.key', 'utf8'),
        cert: fs.readFileSync('./localhost.crt', 'utf8'),
        https: true,
        hotOnly: false,
    },
}