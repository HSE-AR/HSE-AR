
import * as Commands from './commands/Commands.js';
import {SetPositionCommand} from "./commands/Commands";

function History( editor ) {

	this.editor = editor;
	this.undos = [];
	this.redos = [];
	this.lastCmdTime = new Date();
	this.idCounter = 0;

	this.historyDisabled = false;
	this.config = editor.config;

	// signals

	var scope = this;

	this.editor.signals.startPlayer.add( function () {

		scope.historyDisabled = true;

	} );

	this.editor.signals.stopPlayer.add( function () {

		scope.historyDisabled = false;

	} );

}

History.prototype = {

	execute: function ( cmd, optionalName ) {

		var lastCmd = this.undos[ this.undos.length - 1 ];
		var timeDifference = new Date().getTime() - this.lastCmdTime.getTime();

		var isUpdatableCmd = lastCmd &&
			lastCmd.updatable &&
			cmd.updatable &&
			lastCmd.object === cmd.object &&
			lastCmd.type === cmd.type &&
			lastCmd.script === cmd.script &&
			lastCmd.attributeName === cmd.attributeName;

		if ( isUpdatableCmd && cmd.type === "SetScriptValueCommand" ) {

			// When the cmd.type is "SetScriptValueCommand" the timeDifference is ignored

			lastCmd.update( cmd );
			cmd = lastCmd;

		} else if ( isUpdatableCmd && timeDifference < 500 ) {

			lastCmd.update( cmd );
			cmd = lastCmd;

		} else {

			// the command is not updatable and is added as a new part of the history

			this.undos.push( cmd );
			cmd.id = ++ this.idCounter;

		}

		cmd.name = ( optionalName !== undefined ) ? optionalName : cmd.name;
		cmd.execute();
		cmd.inMemory = true;

		if ( this.config.getKey( 'settings/history' ) ) {

			cmd.json = cmd.toJSON();	// serialize the cmd immediately after execution and append the json to the cmd

		}

		this.lastCmdTime = new Date();

		// clearing all the redo-commands

		this.redos = [];
		this.editor.signals.historyChanged.dispatch( cmd );

	},

	undo: function () {

		if ( this.historyDisabled ) {

			alert( "Undo/Redo disabled while scene is playing." );
			return;

		}

		var cmd = undefined;

		if ( this.undos.length > 0 ) {

			cmd = this.undos.pop();

			if ( cmd.inMemory === false ) {

				cmd.fromJSON( cmd.json );

			}

		}

		if ( cmd !== undefined ) {

			cmd.undo();
			this.redos.push( cmd );
			this.editor.signals.historyChanged.dispatch( cmd );

		}

		return cmd;

	},

	redo: function () {

		if ( this.historyDisabled ) {

			alert( "Undo/Redo disabled while scene is playing." );
			return;

		}

		var cmd = undefined;

		if ( this.redos.length > 0 ) {

			cmd = this.redos.pop();

			if ( cmd.inMemory === false ) {

				cmd.fromJSON( cmd.json );

			}

		}

		if ( cmd !== undefined ) {

			cmd.execute();
			this.undos.push( cmd );
			this.editor.signals.historyChanged.dispatch( cmd );

		}

		return cmd;

	},

	toJSON: function () {

		var history = {};
		history.undos = [];
		history.redos = [];

		if ( ! this.config.getKey( 'settings/history' ) ) {

			return history;

		}

		// Append Undos to History

		for ( var i = 0; i < this.undos.length; i ++ ) {

			if ( this.undos[ i ].hasOwnProperty( "json" ) ) {

				history.undos.push( this.undos[ i ].json );

			}

		}

		// Append Redos to History

		for ( var i = 0; i < this.redos.length; i ++ ) {

			if ( this.redos[ i ].hasOwnProperty( "json" ) ) {

				history.redos.push( this.redos[ i ].json );

			}

		}

		return history;

	},

	fromJSON: function ( json ) {

		if ( json === undefined ) return;

		for ( var i = 0; i < json.undos.length; i ++ ) {

			var cmdJSON = json.undos[ i ];
			var cmd = new Commands[ cmdJSON.type ]( this.editor ); // creates a new object of type "json.type"
			cmd.json = cmdJSON;
			cmd.id = cmdJSON.id;
			cmd.name = cmdJSON.name;
			this.undos.push( cmd );
			this.idCounter = ( cmdJSON.id > this.idCounter ) ? cmdJSON.id : this.idCounter; // set last used idCounter

		}

		for ( var i = 0; i < json.redos.length; i ++ ) {

			var cmdJSON = json.redos[ i ];
			var cmd = new Commands[ cmdJSON.type ]( this.editor ); // creates a new object of type "json.type"
			cmd.json = cmdJSON;
			cmd.id = cmdJSON.id;
			cmd.name = cmdJSON.name;
			this.redos.push( cmd );
			this.idCounter = ( cmdJSON.id > this.idCounter ) ? cmdJSON.id : this.idCounter; // set last used idCounter

		}

		// Select the last executed undo-command
		this.editor.signals.historyChanged.dispatch( this.undos[ this.undos.length - 1 ] );

	},

	clear: function () {

		this.undos = [];
		this.redos = [];
		this.idCounter = 0;

		this.editor.signals.historyChanged.dispatch();

	},

	goToState: function ( id ) {

		if ( this.historyDisabled ) {

			alert( "Undo/Redo disabled while scene is playing." );
			return;

		}

		this.editor.signals.sceneGraphChanged.active = false;
		this.editor.signals.historyChanged.active = false;

		var cmd = this.undos.length > 0 ? this.undos[ this.undos.length - 1 ] : undefined;	// next cmd to pop

		if ( cmd === undefined || id > cmd.id ) {

			cmd = this.redo();
			while ( cmd !== undefined && id > cmd.id ) {

				cmd = this.redo();

			}

		} else {

			while ( true ) {

				cmd = this.undos[ this.undos.length - 1 ];	// next cmd to pop

				if ( cmd === undefined || id === cmd.id ) break;

				this.undo();

			}

		}

		this.editor.signals.sceneGraphChanged.active = true;
		this.editor.signals.historyChanged.active = true;

		this.editor.signals.sceneGraphChanged.dispatch();
		this.editor.signals.historyChanged.dispatch( cmd );

	},

	enableSerialization: function ( id ) {

		/**
		 * because there might be commands in this.undos and this.redos
		 * which have not been serialized with .toJSON() we go back
		 * to the oldest command and redo one command after the other
		 * while also calling .toJSON() on them.
		 */

		this.goToState( - 1 );

		this.editor.signals.sceneGraphChanged.active = false;
		this.editor.signals.historyChanged.active = false;

		var cmd = this.redo();
		while ( cmd !== undefined ) {

			if ( ! cmd.hasOwnProperty( "json" ) ) {

				cmd.json = cmd.toJSON();

			}

			cmd = this.redo();

		}

		this.editor.signals.sceneGraphChanged.active = true;
		this.editor.signals.historyChanged.active = true;

		this.goToState( id );

	},

	GetArrayOfModification: function() {
		let arrayOfModifications = []
		this.undos.forEach((mod) => {
			const objectModificationData = this.GetCustomModificationObject(mod)
			if (objectModificationData.ModelId !== null) {
				arrayOfModifications.push(objectModificationData)
				this.clear()
			}
		})
		return arrayOfModifications
	},

	GetCustomModificationObject: function(modificationType) {
		const objectModificationData = {
			Type: 'Update',
			ObjectType: 'ObjectChild',
			PropertyModificationType: 'Update',
			ModelId: null,
			ObjectChild: null
		}
		this.SetObjectModification(objectModificationData,modificationType)
		this.AddObjectModification(objectModificationData,modificationType)
		this.RemoveObjectModification(objectModificationData,modificationType)

		return objectModificationData
	},

	SetObjectModification: function(objectModificationData,modificationType) {
		if(modificationType.type === 'SetPositionCommand'|| modificationType.type === 'SetRotationCommand' || modificationType.type === 'SetScaleCommand') {
			objectModificationData.ObjectChild = {
				uuid: modificationType.toJSON().objectUuid,
				matrix: modificationType.object.matrix.elements
			}
			objectModificationData.ModelId = editor.idFromBack
		}
	},

	AddObjectModification: function(objectModificationData, modificationType) {
		if (modificationType.type === 'AddObjectCommand') {
			objectModificationData.Type = "Add"
			objectModificationData.ObjectType = 'ObjectChild'
			objectModificationData.PropertyModificationType = 'Add'
			objectModificationData.ModelId = editor.idFromBack
			objectModificationData.ObjectChild = {
				uuid: modificationType.object.uuid,
				type: modificationType.object.type,
				name: modificationType.object.name,
				layers: modificationType.object.layers.mask,
				matrix: modificationType.object.matrix.elements,
				geometry: modificationType.object.geometry.uuid,
				material: modificationType.object.material.uuid,
			}
			objectModificationData.Geometry = modificationType.object.geometry
			objectModificationData.Material = modificationType.object.material

			// if(modificationType.object.type.includes('Light')) { // добавление света на сцену
			// 	objectModificationData.Type = "Add"
			// 	objectModificationData.ObjectType = 'ObjectChild'
			// 	objectModificationData.PropertyModificationType = 'Add'
			// 	objectModificationData.ModelId = editor.idFromBack
			// 	objectModificationData.ObjectChild = modificationType.object
			// }
		}
	},

	RemoveObjectModification: function(objectModificationData, modificationType) {
		if(modificationType.type === 'RemoveObjectCommand') {
			objectModificationData.Type = "Delete"
			objectModificationData.PropertyModificationType = "Delete"
			objectModificationData.ModelId = editor.idFromBack
			objectModificationData.Object = {
				uuid: modificationType.object.uuid,
				type: modificationType.object.type,
				name: modificationType.object.name,
				layers: modificationType.object.layers.mask,
				matrix: modificationType.object.matrix.elements,
				geometry: modificationType.object.geometry.uuid,
				material: modificationType.object.material.uuid,
			}
		}
	}

};

export { History };
