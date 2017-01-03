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
    class Asteroid
    {

        Texture2D texture;
        public Vector2 velocity;
        public Vector2 position;
        public Rectangle rectangle;
        Vector2 origin;
        float rotation;
        float angular_velocity;

        public Asteroid(Texture2D newTexture, Vector2 newPosition, Vector2 newVelocity, float newAngular_velocity)
        {
            texture = newTexture;
            velocity = newVelocity;
            position = newPosition;
            angular_velocity = newAngular_velocity;
        }

        public void Update()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            if (angular_velocity > 0.3f && angular_velocity < 0.5f)
            {
                angular_velocity = 0.01f;
            }
            else if (angular_velocity > 0.5f)
            {
                angular_velocity = 0.002f;
            }
            else if (angular_velocity > 0.1f && angular_velocity < 0.3f)
            {
                angular_velocity = 0f;
            }
            rotation += angular_velocity;

            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
        }

    }
}
