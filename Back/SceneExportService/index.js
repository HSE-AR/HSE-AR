var express = require("express");

const fs = require('fs');
const path = require('path');
const THREE = require('three');
const program = require('commander');
const Canvas = require('canvas');
const { Blob, FileReader } = require('vblob');
const routes = require('./routes/api.js')


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

app.post("/gapi/gltf", (req, res) => {


    global.window = global;
    global.Blob = Blob;
    global.FileReader = FileReader;
    global.THREE = THREE;
    global.document = {
        createElement: (nodeName) => {
            if (nodeName !== 'canvas') throw new Error(`Cannot create node ${nodeName}`);
            const canvas = new Canvas(256, 256);
            // This isn't working — currently need to avoid toBlob(), so export to embedded .gltf not .glb.
            // canvas.toBlob = function () {
            //   return new Blob([this.toBuffer()]);
            // };
            return canvas;
        }
    };

    require('three/examples/js/exporters/GLTFExporter');


    const fileName =  'example.gltf'

    var loader = new THREE.ObjectLoader();

    loader.parse(req.body, ( resultScene ) => {

        console.log('Converting to glTF');
        const exporter = new THREE.GLTFExporter();
        exporter.parse(resultScene, (content) => {

            console.log(`Writing to ${fileName}`);
            if (typeof content === 'object') content = JSON.stringify(content);
            fs.writeFileSync(fileName, content);

        });
    })


    res.status(200).json({message: 'Заебись все работает'})
});



app.listen(PORT, () => {
    console.log(`Server has been started on port ${PORT}...`)
});



