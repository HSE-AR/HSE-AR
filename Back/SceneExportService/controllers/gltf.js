import fs from 'fs'
import * as THREE from '../three/src/Three.js'

import pkg from 'canvas';
const {Canvas, Image: ImageCanvas} = pkg;

import {GLTFExporter} from '../three/examples/jsm/exporters/GLTFExporter.js';
import {Image} from 'image-js'
import atob from 'atob'

import { Blob, FileReader } from 'vblob';
/*
@reuest.body -  scene three.js Json format with id from mongodb
*/
function  create(req, res){
    global.atob = atob
    global.window = global;
    global.Blob = Blob;
    global.FileReader = FileReader;
    global.THREE = THREE;
    global.document = {
        createElement: nodeName => {
            if (nodeName !== "canvas")
            {
                throw new Error(`Cannot create node ${nodeName}`)
            }

            const canvas = new Canvas(256, 256)

            // This isn't working — currently need to avoid toBlob(), so export to embedded .gltf not .glb.
             canvas.toBlob = function () {
               return new Blob([this.toBuffer()]);
             };
            canvas.getMyImage = function () {
                return new ImageCanvas()
            };
            return canvas
        },
        createElementNS: (namespaceURI, qualifiedName) => {

            if (qualifiedName == "img") {
                const img = new Image()
                img.removeEventListener = (name, fn) => {

                     console.log(`img.removeEventListener(${name},${fn})`)
                }
                img.addEventListener = (name, fn) => {
                     console.log(`img.addEventListener(${name},${fn})`)
                    setTimeout(fn, 10)
                }
                return img
            }
            throw new Error(`Cannot create node ${qualifiedName}`)
        },
    };

    const filePath =  `../data/scenes/gltfs/${req.body.id}.gltf`

    var loader = new THREE.ObjectLoader();


    try{

        let RESULT = loader.parse(req.body)

        const exporter = new GLTFExporter();

        var animations = getAnimations( RESULT );

        exporter.parse(RESULT, (content) => {

            //if (typeof content === 'object') content = JSON.stringify(content);
            let saveString = JSON.stringify( content, null, 2 )
            fs.writeFileSync(filePath, saveString);
        },{animations:animations});

        res.status(200).json({status: true })
    } catch (error){
        res.status(500).json({status: false})
    }
}

function getAnimations( scene ) {

    var animations = [];

    scene.traverse( function ( object ) {
        animations.push( ... object.animations );
    } );

    return animations;

}


export {create}

