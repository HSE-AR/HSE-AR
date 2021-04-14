import express from 'express'
import * as methods from '../controllers/gltf.js'

var router = express.Router()

router.post('/exporter/gltf', methods.create)

export {router}