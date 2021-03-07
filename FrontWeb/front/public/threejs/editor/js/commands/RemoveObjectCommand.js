
import { Command } from '../Command.js';

import * as THREE from '../../../build/three.module.js';

/**
 * @param editor Editor
 * @param object THREE.Object3D
 * @constructor
 */
function RemoveObjectCommand( editor, object ) {

	this.sceneUuid = editor.sceneUuid
	this.parent = (object !== undefined ) ? object.parent : undefined;

	this.isDisable = this.parent !== undefined && this.parent.uuid !== editor.sceneUuid;
	if (this.isDisable) {
		alert("хоба, низя")
		this.type = 'ForbiddenCommand';
		this.object = object;
		this.name = 'Remove Object';
		return;
	}

	Command.call( this, editor );


	this.type = 'RemoveObjectCommand';
	this.name = 'Remove Object';
	this.object = object;

	if ( this.parent !== undefined  ) {

		this.index = this.parent.children.indexOf( this.object );

	}

}

RemoveObjectCommand.prototype = {

	execute: function () {

		if (!this.isDisable){
			this.editor.removeObject( this.object );
			this.editor.deselect();
		}

	},

	undo: function () {

		this.editor.addObject( this.object, this.parent, this.index );
		this.editor.select( this.object );

	},

	toJSON: function () {

		var output = Command.prototype.toJSON.call( this );
		output.object = this.object.toJSON();
		output.index = this.index;
		output.parentUuid = this.parent.uuid;

		return output;

	},

	fromJSON: function ( json ) {

		Command.prototype.fromJSON.call( this, json );

		this.parent = this.editor.objectByUuid( json.parentUuid );
		if ( this.parent === undefined ) {

			this.parent = this.editor.scene;

		}

		this.index = json.index;

		this.object = this.editor.objectByUuid( json.object.object.uuid );
		if ( this.object === undefined ) {

			var loader = new THREE.ObjectLoader();
			this.object = loader.parse( json.object );

		}

	}

};

export { RemoveObjectCommand };
