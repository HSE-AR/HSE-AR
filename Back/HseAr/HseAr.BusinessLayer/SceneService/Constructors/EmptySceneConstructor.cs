using Newtonsoft.Json.Linq;

namespace HseAr.BusinessLayer.SceneService.Constructors
{
    public class EmptySceneConstructor
    {
        public static Data.DataProjections.Scene CreateEmptyScene()
        {
            var metadata = @"{
                version: 4.5,
                type: 'Object',
                generator: 'Object3D.toJSON'
            }";

            var geometries = @"[
                {
			        uuid: 'B6E9034C-6530-4C40-9120-E744DC3B1BEA',
                    type: 'BoxBufferGeometry',
                    width: 1,
                    height: 1,
                    depth: 1,
                    widthSegments: 1,
                    heightSegments: 1,
                    depthSegments: 1
                }
            ]";

            var materials = @"[
                {
			        uuid: '4EFFA909-B302-40B9-B45F-3D2AA7E9D147',
                    type: 'MeshStandardMaterial',
                    color: 16777215,
                    roughness: 1,
                    metalness: 0,
                    emissive: 0,
                    depthFunc: 3,
                    depthTest: true,
                    depthWrite: true,
                    stencilWrite: false,
                    stencilWriteMask: 255,
                    stencilFunc: 519,
                    stencilRef: 0,
                    stencilFuncMask: 255,
                    stencilFail: 7680,
                    stencilZFail: 7680,
                    stencilZPass: 7680
                }
            ]";
            
            var sceneObject = @"{
                uuid: '52DB20E6-4144-461A-BA37-0D41CCD4E73D',
                type: 'Scene',
                name: 'Scene',
                layers: 1,
                matrix: [1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1],
                children: [
                {
                    uuid: '97A45826-D60C-410A-B749-A4F0D3AF8A13',
                    type: 'Mesh',
                    name: 'Box',
                    layers: 1,
                    matrix: [1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1],
                    geometry: 'B6E9034C-6530-4C40-9120-E744DC3B1BEA',
                    material: '4EFFA909-B302-40B9-B45F-3D2AA7E9D147'
                }]
            }";

            return new Data.DataProjections.Scene()
            {
                Metadata = JObject.Parse(metadata),
                Geometries = JArray.Parse(geometries),
                Materials = JArray.Parse(materials),
                Object = JObject.Parse(sceneObject),
            };
        }
    }
}