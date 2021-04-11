import {UIButton, UIDiv, UIPanel} from './libs/ui.js';

import { MenubarAdd } from './Menubar.Add.js';
import { MenubarEdit } from './Menubar.Edit.js';
import { MenubarFile } from './Menubar.File.js';
import { MenubarSaveAndExit } from './Menubar.SaveAndExit.js';

// import { MenubarExamples } from './Menubar.Examples.js';
// import { MenubarHelp } from './Menubar.Help.js';
// import { MenubarPlay } from './Menubar.Play.js';
// import { MenubarStatus } from './Menubar.Status.js';

function Menubar( editor ) {

	var container = new UIPanel();
	container.setId( 'menubar' );

	var c = new UIPanel();
	c.setClass( 'menu' );

	var translateIcon = document.createElement( 'img' );
	translateIcon.src = '/logo.svg';

	var translate = new UIPanel();
	translate.setClass('logodiv')
	translate.dom.appendChild( translateIcon );

	c.add( translate );


	container.add(c)

	container.add( new MenubarFile( editor ) );
	container.add( new MenubarEdit( editor ) );
	container.add( new MenubarAdd( editor ) );
	//container.add( new MenubarSaveAndExit( editor ) );



	// container.add( new MenubarPlay( editor ) );
	// container.add( new MenubarExamples( editor ) );
	// container.add( new MenubarHelp( editor ) );

	// container.add( new MenubarStatus( editor ) );

	return container;

}

export { Menubar };
