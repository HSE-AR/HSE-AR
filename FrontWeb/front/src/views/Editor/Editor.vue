<template>
  <div>

  </div>
</template>


<script>

import {THREE, Editor, Viewport, Toolbar, Player, Sidebar, Menubar, Resizer, Dialog,} from '../../main.js'
import { Loader } from '../../../public/threejs/build/three.module.js';

import axios from 'axios'
import {mapGetters} from "vuex";
import {AddObjectCommand} from "../../../public/threejs/editor/js/commands/AddObjectCommand";
import {GLTFLoader} from "../../../public/threejs/examples/jsm/loaders/GLTFLoader";

Number.prototype.format = function () {
	return this.toString().replace( /(\d)(?=(\d{3})+(?!\d))/g, "$1," );

};


export default {
  name: 'Editor',
  data(){
      return{
          workspace: null,
          editor:null,
          viewport:null,
          toolbar:null,
          sidebar:null,
          menubar:null,
          resizer:null,
          isLoadingFromHash: false,
          hash:null,
          companyActions: localStorage.getItem('company_actions')
      }
  },
  props: (route) => ({ query: route.query.floorId }) ,
    computed: {
        ...mapGetters({
            buildingInfo: 'building_info',
            token: 'token',
        })
    },

  async created(){
    window.URL = window.URL || window.webkitURL;
    window.BlobBuilder = window.BlobBuilder || window.WebKitBlobBuilder || window.MozBlobBuilder;
    
    this.editor = new Editor();
    this.editor.portBack = this.$store.state.port;
    window.editor = this.editor; // Expose editor to Console
	window.THREE = THREE; // Expose THREE to APP Scripts and Console

    this.editor.progres = this.$Progress

    this.viewport = new Viewport( this.editor );
    document.body.appendChild( this.viewport.dom );

	 this.toolbar = new Toolbar( this.editor );
    document.body.appendChild( this.toolbar.dom );

	this.sidebar = new Sidebar( this.editor );
	document.body.appendChild( this.sidebar.dom );

	this.menubar = new Menubar( this.editor );
	document.body.appendChild( this.menubar.dom );

    this.resizer = new Resizer( this.editor );
	document.body.appendChild( this.resizer.dom );

    document.addEventListener( 'dragover', function ( event ) {
		event.preventDefault();
		event.dataTransfer.dropEffect = 'copy';
	}, false );

	document.addEventListener( 'drop', function ( event ) {
		event.preventDefault();
        if ( event.dataTransfer.types[ 0 ] === 'text/plain' ) 
            return; // Outliner drop

		if ( event.dataTransfer.items ) {
					// DataTransferItemList supports folders
			this.editor.loader.loadItemList( event.dataTransfer.items );
		} else {
			this.editor.loader.loadFiles( event.dataTransfer.files );
        }   
	}, false );


	window.addEventListener( 'resize', this.onWindowResize, false );

    this.onWindowResize();

    var isLoadingFromHash = false;
	this.hash = window.location.hash;

	await this.LoadSceneFromBack();	
  },

  destroyed(){
    location.reload()
  },

  methods:{

	onWindowResize()
	{
        this.editor.signals.windowResize.dispatch();
	},
	
	async LoadSceneFromBack()
	{
        this.$Progress.start()
      	await axios.get(this.$store.state.port + `wapi/editor/${this.$route.query.floorId}`, {
      	    headers: {
                'X-Company-Key': JSON.parse(localStorage.getItem('company_actions'))[0].id
            }
        })
            .then(response =>{

              console.log(response)
              const token = this.token
              axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
              this.editor.idFromBack = response.data.scene.id;
              this.editor.floorId = this.$route.query.floorId;
              this.editor.companyId = JSON.parse(localStorage.getItem('company_actions'))[0].id;
              this.editor.loader.MyLoader(response.data.scene);

              this.InitializeFloorPlan(response.data.floorPlan);
              this.$Progress.finish()
              this.editor.select( null );
            })
            .catch(err => {
                console.log(err)
                this.$Progress.fail()
            })
	},
    InitializeFloorPlan(floorPlan)
    {
      let geometry = new THREE.PlaneBufferGeometry( 1, 1, 1, 1 );
      let material = new THREE.MeshStandardMaterial();
      let loader = new THREE.TextureLoader();
      let img = this.$store.state.port + floorPlan.floorPlanImg;
      console.log(img)
      loader.load(img,
          function(texture) {
            console.log(img + ' downloaded successfully');
            material.map = texture;
            material.needsUpdate = true
          }
      );
      let  mesh = new THREE.Mesh( geometry, material );

      mesh.uuid = this.editor.floorPlaneUuid;
      mesh.rotation.x = 270 * Math.PI/180;
      mesh.scale.set(floorPlan.imgWidth /10,floorPlan.imgHeight/10,1)
      mesh.name = 'FloorPlan Plane';
      this.editor.execute( new AddObjectCommand( this.editor, mesh ) );

      this.InitializeFloorPlanGltf(floorPlan.floorPlanGltf)
    },

    InitializeFloorPlanGltf(path)
    {
      let gltfLoader = new GLTFLoader();
      gltfLoader.load(this.$store.state.port + path, (gltf) => {

        var scene = gltf.scene.children[0];
        scene.name = "Floorplan3d";
        scene.rotation.y = 180 * Math.PI/180;
        scene.position.y = -0.2
        scene.scale.set(10,10,10)

        scene.uuid =  this.editor.floorPlaneGltfUuid

        this.editor.execute( new AddObjectCommand( editor, scene ) );
      });
    }


  },


}
</script>

<style lang="css" scoped>
  @import 'editor.css';
</style>
