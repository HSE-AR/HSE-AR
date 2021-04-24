module.exports = {
    devServer: {
        open: process.platform === 'darwin',
        host: '0.0.0.0',
        port: 2000, // CHANGE YOUR PORT HERE!
        key: fs.readFileSync('./hsear.ru.key', 'utf8'),
        cert: fs.readFileSync('./hsear.ru.cert', 'utf8'),
        https: true,
        hotOnly: false,
    },
}