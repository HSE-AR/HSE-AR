import { UIPanel, UIRow } from './libs/ui.js';

function MenubarSaveAndExit( editor ) { //моеееее

    console.log("Examples нам не нужны")

    let container = new UIPanel();
	container.setClass( 'menu' );

    let title = new UIPanel();
	title.setClass( 'title' );
    title.setTextContent( "SAVE" ); //потом надо будет завести строку в  strings

    title.onClick( async function () {
        console.log(editor.history.undos)
        if(editor.history.undos.length >= 1) {
            editor.ModificationsLoadToBack()
        }
	} );
	container.add( title );

	return container;
}

export { MenubarSaveAndExit };
