module.exports = {
    devServer: {
        open: process.platform === 'darwin',
        host: '0.0.0.0',
        port: 2000, // CHANGE YOUR PORT HERE!
        https: true,
        hotOnly: false,
    },
}