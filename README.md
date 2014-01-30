# What is it?
MonoGameFontRenderer is exactly what it sounds like; a tool to render texts (fonts) in monogame (and XNA).

It makes use of the lovely tool I found here: [http://www.nubik.com/SpriteFont/], which is required to generate the files for the font.

## How do I use it?
1. First go to [http://www.nubik.com/SpriteFont/] and download the SpriteFont generator tool.
2. Use the generator tool to cuztomize you font however you want.
3. Generate the files by clicking "Export" on the export page. Make sure you generate the 'font metrics' file as well.
4. Name both of the files, the image file and the xml (font metrics) file to the same name, excluding the file extension, and add them to your MonoGames project content.
5. Go ahead to download/clone MonoGameFontRenderer (if you haven't already), compile and/or add it as a reference to your MonoGame project.
6. Create an object of the `Font` class. In the constructor, give the name of the added font (the file name that is, without file extenstions) and give a pointer to the ContentManager.
7. Draw the font with the static `FontRenderer` class using the method `DrawText`. The parameters are the SpriteBatch, position of where to draw the text to draw, the `Font` object you just created and the color in which to draw it with. Done!

## Scale/Spacing
You can edit the scale of the text (to make it larger) and change the spacing between characters. You can find these in the `Font` class properties and their default values are `1f` and `3` respectively. 