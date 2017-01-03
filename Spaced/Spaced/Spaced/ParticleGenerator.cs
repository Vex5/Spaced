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
    class ParticleGenerator
    {
        Texture2D Texture;
        List<Stars> stars = new List<Stars>();
        
        Random r = new Random();

        public bool is_game_running = true;

        float SpawnWidth, Density, timer;

        public ParticleGenerator(Texture2D newTexture, float newSpawnWidth, float newDensity)
        {
            Texture = newTexture;
            SpawnWidth = newSpawnWidth;
            Density = newDensity;
        }

        public void CreateParticle()
        {

            stars.Add(new Stars(Texture, new Vector2((float)r.NextDouble() * SpawnWidth, -50), new Vector2(0, 1)));

        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer>0)
            {
                timer -= 10 / Density;
                CreateParticle();
            }

            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].Update();

                if (stars[i].position.Y > graphics.Viewport.Height)
                {
                    stars.RemoveAt(i);
                    i--;
                }

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Stars star in stars)
            {
                star.Draw(spriteBatch);
            }
        }

    }
}
