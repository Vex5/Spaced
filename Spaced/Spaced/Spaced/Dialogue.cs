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
    class Dialogue
    {

        Texture2D adi_say;
        Texture2D anes_say;
        Texture2D vedad_say;

        bool adi=false, anes=false, vedad=false;

        string sanes, sadi, svedad;
        SpriteFont font;

        public void Load(ContentManager Content)
        {
            adi_say = Content.Load<Texture2D>("Resources/Story/Dialogue/AdiSay");
            anes_say = Content.Load<Texture2D>("Resources/Story/Dialogue/AnesSay");
            vedad_say = Content.Load<Texture2D>("Resources/Story/Dialogue/VedadSay");
            font = Content.Load<SpriteFont>("Resources/Fonts/Score");
        }

        public void Update(GameTime gameTime, float timer, int difficulty, bool show_dialogue, SoundEffect dialogue_sound)
        {
            anes = false;
            adi = false;
            vedad = false;
            if(timer > 3 && timer < 6)
            {
                anes = true;
                adi = false;
                vedad = false;
                sanes = "Captain, we are approaching the asteroid field.";
            }
            if (timer > 6 && timer < 7.5)
            {
                anes = false;
                adi = true;
                vedad = false;
                sadi = "Vedad...";
            }
            if (timer > 9 && timer < 12)
            {
                anes = false;
                adi = false;
                vedad = true;
                svedad = "Already on it, disabling burn for easier manuverability.";
            }
            if(timer > 12 && timer < 15)
            {
                anes = false;
                adi = true;
                vedad = false;
                sadi = "Anes, ready the lasers.";
            }
            if(difficulty == 1 || difficulty == 2)
            {
                if (timer > 15 && timer < 18)
                {
                    anes = true;
                    adi = false;
                    vedad = false;
                    sanes = "Understood.";
                }
            }
            if (difficulty == 3)
            {
                if (timer > 15 && timer < 18)
                {
                    anes = true;
                    adi = false;
                    vedad = false;
                    sanes = "Our lasers are malfunctioning, I'm unable to repair them.";
                }
            }
            if (timer > 18 && timer < 21)
            {
                anes = false;
                adi = true;
                vedad = false;
                sadi = "Vedad, check our left and right thrusters.";
            }
            if (timer > 21 && timer < 24)
            {
                anes = false;
                adi = false;
                vedad = true;
                svedad = "Both are working fine captain.";
            }
            if (timer > 27 && timer < 30)
            {
                anes = false;
                adi = true;
                vedad = false;
                sadi = "Now put some music on.";
            }
            if (timer > 30 && timer < 33)
            {
                anes = true;
                adi = false;
                vedad = false;
                sanes = "We are approaching the asteroid field!";
            }
            if (timer > 60 && timer < 63)
            {
                anes = false;
                adi = true;
                vedad = false;
                sadi = "What kind of music is this?? Anes, drop the beat!";
            }
            if (timer > 63 && timer < 66)
            {
                anes = true;
                adi = false;
                vedad = false;
                sanes = "I like the way you think!";
            }
            if (timer > 250)
            {
                anes = false;
                adi = true;
                vedad = false;
                sadi = "Good work people!";
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (adi == true)
            {
                spriteBatch.Draw(adi_say, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, "" + sadi, new Vector2(90, 10), Color.White);
            }
            else if (anes == true)
            {
                spriteBatch.Draw(anes_say, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, "" + sanes, new Vector2(90, 10), Color.White);
                
            }
            else if (vedad == true)
            {
                spriteBatch.Draw(vedad_say, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, "" + svedad, new Vector2(90, 10), Color.White);
            }
        }

    }
}
