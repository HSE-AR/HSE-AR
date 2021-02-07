#!/usr/bin/env node

import * as fs from 'fs'
import * as path from 'path'
import * as THREE from 'three'
import * as program from "commander";
import {Canvas} from "canvas";
import { Blob, FileReader } from 'vblob'

export const create = (req, res) => {
    console.log("1")


    //const fs = require('fs');
    //const path = require('path');
    //const THREE = require('three');
    //const program = require('commander');
    //const Canvas = require('canvas');
    //const { Blob, FileReader } = require('vblob');

// Patch global scope to imitate browser environment.
    global.window = global;
    global.Blob = Blob;
    global.FileReader = FileReader;
    global.THREE = THREE;

    global.document = {
        createElement: (nodeName) => {

            console.log(nodeName)
            if (nodeName !== 'canvas') throw new Error(`Cannot create node ${nodeName}`);
            const canvas = new Canvas(256, 256);
            console.log("2")
            // This isn't working — currently need to avoid toBlob(), so export to embedded .gltf not .glb.
            // canvas.toBlob = function () {
            //   return new Blob([this.toBuffer()]);
            // };
            return canvas;
        }
    };


// https://github.com/mrdoob/three.js/issues/9562
    require('three/examples/js/exporters/GLTFExporter');

    program
        .version('0.0.1')
        .usage('[options] <file>')
        .option('-o, --output <file>', 'output filename', String)
        // .option('-b, --binary', 'use binary (.glb) format (default false)')
        .option('-m, --myoption', 'my custom option (default 1.0)', Number)
        .parse(process.argv);

    program.binary = !!program.binary;
    program.myoption = program.myoption || 1;

    const inputPath = program.args[0];
    if (!inputPath) { program.help(); }

    if (!program.output) {
        program.output = path.basename(inputPath, '.foo');
        program.output += program.binary ? '.glb' : '.gltf';
    }

    console.log(' → input: %j', program.args);
    console.log(' → output: %j', program.output);
    console.log(' → binary: %j', program.binary);
    console.log(' → my custom option: %j', program.myoption);

    console.log(`Loading "${inputPath}"`);
    let mesh = new THREE.Mesh(req.body); // (load or create mesh here)

    console.log('Converting to glTF');
    const exporter = new THREE.GLTFExporter();
    exporter.parse(mesh, (content) => {

        console.log(`Writing to ${program.output}`);
        if (typeof content === 'object') content = JSON.stringify(content);
        fs.writeFileSync(program.output, content);

    }, {binary: program.binary});






    res.status(200).json({message: 'Заебись все работает'})
}
