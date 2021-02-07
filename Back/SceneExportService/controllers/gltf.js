
/*
@reuest.body -  scene three.js Json format with id from mongodb
*/
function create(req, res){
    const fs = require('fs');
    const THREE = require('three');
    const Canvas = require('canvas');
    const { Blob, FileReader } = require('vblob');

    console.log(process.cwd())

    global.window = global;
    global.Blob = Blob;
    global.FileReader = FileReader;
    global.THREE = THREE;
    global.document = {
        createElement: (nodeName) => {
            if (nodeName !== 'canvas') throw new Error(`Cannot create node ${nodeName}`);
            const canvas = new Canvas(256, 256);
            return canvas;
        }
    };

    require('three/examples/js/exporters/GLTFExporter');

    const filePath =  `../data/scenes/gltfs/${req.body.id}.gltf`

    var loader = new THREE.ObjectLoader();

    try{
        loader.parse(req.body, ( resultScene ) => {

            const exporter = new THREE.GLTFExporter();

            exporter.parse(resultScene, (content) => {
                if (typeof content === 'object') content = JSON.stringify(content);
                fs.writeFileSync(filePath, content);
            });
        })
        res.status(200).json({status: true })
    } catch (error){
        res.status(500).json({status: false})
    }
}


module.exports.create = create;

