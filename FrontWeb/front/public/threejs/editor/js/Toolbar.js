import {UIPanel, UIButton, UICheckbox, UISpan, UIElement} from './libs/ui.js';
import router from "@/router/router";

function Toolbar( editor ) {

	var d = document.createElement( 'div' );
	d.id = "dialog"

	var dialog_content = document.createElement( 'div' );
	dialog_content.className ='dialog_content'

	var ask = document.createElement( 'span' );
	ask.className = 'ask'
	ask.textContent ="SAVE CHANGES"
	dialog_content.appendChild(ask)

	var selectL = document.createElement( 'div' );
	selectL.className ='select_dialog_left'

	var spany = document.createElement( 'button' );
	spany.className = 'selected'
	spany.textContent ="Yes"

	selectL.appendChild(spany)

	var selectR = document.createElement( 'div' );
	selectR.className ='select_dialog_right'

	var spann = document.createElement( 'button' );
	spann.textContent ="No"
	selectR.appendChild(spann)


	dialog_content.appendChild(selectL)
	dialog_content.appendChild(selectR)
	d.appendChild(dialog_content)

	window.onclick = function (event) {
		if (event.target == d) {
			d.style.display = "none";
		}
	}

	spany.onclick = async function () {
		d.style.display = "none";
		console.log(editor.history.undos)
		if(editor.history.undos.length >= 1) {
			await editor.ModificationsLoadToBack(true)
		}
		else {
			router.back()
		}
	}

	spann.onclick = function () {
		d.style.display = "none";
		router.back()
	}


	var signals = editor.signals;
	var strings = editor.strings;

	var container = new UIPanel();
	container.setId( 'toolbar' );

	container.add(new UIElement(d))

	//home
	var homeIcon = document.createElement( 'img' );
	homeIcon.title = "home";
	homeIcon.src = editor.portBack + 'staticImgs/home.svg';

	var home = new UIButton();
	home.setClass('myToolButton')
	home.dom.appendChild( homeIcon );
	home.onClick( function () {
		d.style.display ='block'

	} );
	container.add( home );

	// translate / rotate / scale
	var translateIcon = document.createElement( 'img' );
	translateIcon.title = strings.getKey( 'toolbar/translate' );
	translateIcon.src = editor.portBack + 'staticImgs/translate.svg';

	var translate = new UIButton();
	translate.dom.className = 'myToolButton selected';
	translate.dom.appendChild( translateIcon );
	translate.onClick( function () {

		signals.transformModeChanged.dispatch( 'translate' );

	} );
	container.add( translate );

	var rotateIcon = document.createElement( 'img' );


	rotateIcon.title = strings.getKey( 'toolbar/rotate' );
	rotateIcon.src = editor.portBack + 'staticImgs/rotate.svg';

	var rotate = new UIButton();
	rotate.setClass('myToolButton')
	rotate.dom.appendChild( rotateIcon );
	rotate.onClick( function () {

		signals.transformModeChanged.dispatch( 'rotate' );

	} );
	container.add( rotate );

	var scaleIcon = document.createElement( 'img' );
	scaleIcon.title = strings.getKey( 'toolbar/scale' );
	scaleIcon.src = editor.portBack + 'staticImgs/scale.svg';

	var scale = new UIButton();
	scale.setClass('myToolButton')
	scale.dom.appendChild( scaleIcon );
	scale.onClick( function () {

		signals.transformModeChanged.dispatch( 'scale' );

	} );
	container.add( scale );

	//save
	var saveIcon = document.createElement( 'img' );
	saveIcon.title = "save";
	saveIcon.src = editor.portBack + 'staticImgs/save.svg';

	var save = new UIButton();
	save.setClass('myToolButton')
	save.dom.appendChild( saveIcon );
	save.onClick( async function () {
		 await editor.ModificationsLoadToBack(false)
	} );
	container.add( save );

	//delete
	/*var deleteIcon = document.createElement( 'img' );
	deleteIcon.title = "delete";
	deleteIcon.src = '/delete.svg';

	var delet = new UIButton();
	delet.setClass('myToolButton')
	delet.dom.appendChild( deleteIcon );
	delet.onClick( function () {

	} );
	container.add( delet );*/



	/*var local = new UICheckbox( false );
	local.dom.title = strings.getKey( 'toolbar/local' );
	local.onChange( function () {

		signals.spaceChanged.dispatch( this.getValue() === true ? 'local' : 'world' );

	} );
	container.add( local );*/

	//

	signals.transformModeChanged.add( function ( mode ) {

		translate.dom.classList.remove( 'selected' );
		rotate.dom.classList.remove( 'selected' );
		scale.dom.classList.remove( 'selected' );

		switch ( mode ) {

			case 'translate': translate.dom.classList.add( 'selected' ); break;
			case 'rotate': rotate.dom.classList.add( 'selected' ); break;
			case 'scale': scale.dom.classList.add( 'selected' ); break;

		}

	} );

	return container;

}

export { Toolbar };
