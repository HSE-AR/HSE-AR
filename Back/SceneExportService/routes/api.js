var express = require('express')
var methods = require('../controllers/gltf.js')
var router = express.Router()

router.post('/exporter/gltf', methods.create)

module.exports.router = router