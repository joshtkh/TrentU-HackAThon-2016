﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp1
{
         public class Toolbar
        {
            private Texture2D texture;
            // A class to access the font we created
            private SpriteFont font;

            // The position of the toolbar
            private Vector2 position;
            // The position of the text
            private Vector2 textPosition;

            public Toolbar(Texture2D texture, SpriteFont font, Vector2 position)
            {
                this.texture = texture;
                this.font = font;

                this.position = position;
                // Offset the text to the bottom right corner
                textPosition = new Vector2(130, position.Y + 10);
            }

            public void Draw(SpriteBatch spriteBatch, Player player)
            {
                spriteBatch.Draw(texture, position, Color.White);

                string text = string.Format("Gold : {0} Lives : {1}", player.Money, player.Lives);
                spriteBatch.DrawString(font, text, textPosition, Color.White);
            }
        }
    }




