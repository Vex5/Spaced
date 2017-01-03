using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Spaced
{
    class Player
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle rectangle;

        public Rectangle front_rect;
        public Rectangle back_rect;

        int boundary;

        KeyboardState oldState = Keyboard.GetState();

        public Player(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            boundary = 800 - texture.Width;
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            front_rect = new Rectangle((int)position.X + 18, (int)position.Y + 8, texture.Width / 2 - 15, texture.Height / 2 + 12);
            back_rect = new Rectangle((int)position.X + 1, (int)position.Y + 25, texture.Width - 1, texture.Height / 2 - 10);

            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X = 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X = -5;
            }
            else
            {
                velocity.X = 0;
            }

            if(rectangle.X + texture.Width >= 800)
            {
                rectangle.X = boundary;
                if (oldState.IsKeyUp(Keys.A) && newState.IsKeyDown(Keys.A))
                {
                    velocity.X = -5;
                }
            }
            else if(position.X <= 0)
            {
                position.X = 0;
            }

            newState = oldState;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);            
        }
    }
}
