#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Xml.Linq;
#endregion

namespace FontRenderer
{
    /// <summary>
    /// The class to render font
    /// </summary>
    public static class FontRenderer
    {
        /// <summary>
        /// Draws the given text with the given font.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="position">The upper left position of the text.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to draw with.</param>
        /// <param name="color">The color to draw with.</param>
        public static void DrawText(SpriteBatch spriteBatch, Vector2 position, string text, Font font, Color color)
        {
            int oldkey = -1;

            int width = 0;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null);

            for (int i = 0; i < text.Length; i++)
            {
                int key = (int)text[i];

                spriteBatch.Draw(font.Texture, new Rectangle((int)position.X + width, (int)position.Y, (int)(font.RectangleDictionary[key].Width * font.Scale), (int)(font.RectangleDictionary[key].Height * font.Scale)), font.RectangleDictionary[key], color);
                width += (int)(font.RectangleDictionary[key].Width * font.Scale + font.Spacing);

                oldkey = key;
            }

            spriteBatch.End();
        }
    }

    public class Font
    {
        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <value>
        /// The texture.
        /// </value>
        public Texture2D Texture { get; private set; }
        /// <summary>
        /// Gets the rectangle dictionary.
        /// </summary>
        /// <value>
        /// The rectangle dictionary.
        /// </value>
        public Dictionary<int, Rectangle> RectangleDictionary { get; private set; }
        private XDocument doc;
        private string path;

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public float Scale { get; set; }
        /// <summary>
        /// Gets or sets the spacing in pixels.
        /// </summary>
        /// <value>
        /// The spacing in pixels.
        /// </value>
        public int Spacing { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="font">The file name of the font.</param>
        /// <param name="Content">Content manager.</param>
        public Font(string font, ContentManager Content)
        {
            Scale = 1f;
            Spacing = 3;
            Texture = Content.Load<Texture2D>(font);
            RectangleDictionary = new Dictionary<int, Rectangle>();

            path = Content.RootDirectory;
            doc = XDocument.Load(path + @"\" + font + ".xml");

            Parse();
        }

        private void Parse()
        {
            XElement element = doc.Element("fontMetrics");
            var characters = element.Elements("character");

            RectangleDictionary.Add(-1, new Rectangle(0, 0, 0, 0));

            foreach (XElement character in characters)
            {
                Rectangle rect = new Rectangle();
                rect.X = int.Parse(character.Element("x").Value);
                rect.Y = int.Parse(character.Element("y").Value);
                rect.Width = int.Parse(character.Element("width").Value);
                rect.Height = int.Parse(character.Element("height").Value);
                RectangleDictionary.Add(int.Parse(character.Attribute("key").Value), rect);
            }
        }
    }


}
