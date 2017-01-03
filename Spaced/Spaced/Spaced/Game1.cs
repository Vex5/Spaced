using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Spaced
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //www.microsoft.com/en-us/download/details.aspx?id=24872
        //www.microsoft.com/en-us/download/details.aspx?id=20914
        //support.microsoft.com/kb/893803

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState
        {
            Splash,
            MainMenu,
            Controls,
            Level,
            Win
        }
        GameState CurrentState = GameState.Splash;

        bool increment_score = true;

        Texture2D splash;
        int splash_timer;

        //STORY
        int difficulty = 0;/*
        Texture2D mission_info;
        Texture2D pilotcard_adi;
        Texture2D pilotcard_anes;
        Texture2D pilotcard_vedad;
        
        public bool pilotcard_adi_show = false;
        public bool pilotcard_anes_show = false;
        public bool pilotcard_vedad_show = false;*/
        bool show_story = false;
        public bool mission_info_show = false;
        /*Dialogue dialogue;
        bool show_dialogue = true;*/
        //STORY

        bool asteroid_add;

        //MAIN MENU
        Texture2D title;
        Texture2D play_button;
        Texture2D controls_button;
        Texture2D exit_button; 
        
        Texture2D play_button_hover;
        Texture2D controls_button_hover;
        Texture2D exit_button_hover;

        Rectangle play_button_rect;
        Rectangle controls_button_rect;
        Rectangle exit_button_rect;

        bool play_button_hover_bool;
        bool controls_button_hover_bool;
        bool exit_button_hover_bool;

        Texture2D easy_button;
        Texture2D normal_button;
        Texture2D hard_button;

        Texture2D easy_button_hover;
        Texture2D normal_button_hover;
        Texture2D hard_button_hover;

        Rectangle easy_button_rect;
        Rectangle normal_button_rect;
        Rectangle hard_button_rect;

        bool easy_button_hover_bool;
        bool normal_button_hover_bool;
        bool hard_button_hover_bool;
        bool diff_buttons_visible = false;

        Texture2D play_background;
        SpriteFont ammo_font;

        Texture2D controls_background;
        bool control_background_visible = false;
        //MAIN MENU

        //SOUNDS
        SoundEffect laser_sound;
        SoundEffect asteroid_explosion_sound;
        SoundEffect ship_explosion_sound;
        SoundEffect new_dialogue_sound;
        Song menu_music;
        Song level_music;
        Song beat_song;
        bool songstart_menu = false;
        bool songstart_level = false;
        bool songstart_beat = false;
        //SOUNDS

        //PAUSE
        bool game_paused = false;
        Texture2D paused_background;
        Texture2D resume_button;
        Texture2D resume_button_hover;

        Rectangle resume_button_rect;

        bool resume_button_hover_bool;
        //PAUSE

        //RANDOM STUFF
        Player player;
        Texture2D player_explode;
        bool player_dead = false;
        int dead_timer = 0;
        bool show_fade_texture = false;
        Texture2D fade_texture;

        Texture2D finish_line;
        Vector2 finish_line_pos;
        Vector2 finish_line_vel;
        Rectangle finish_line_rect;
        Texture2D win_window;
        bool show_finish_line = false;
        bool show_win_window = false;
        //RANDOM STUFF

        //EXPLOSION
        Texture2D explosion;
        bool show_explosion = false;
        int explosion_timer = 0;
        List<Explosion> explosion_list = new List<Explosion>();
        Vector2 explosion_pos;
        //EXPLOSION

        //SHOOTING VARS
        Texture2D bullet;
        Vector2 bullet_pos;
        Vector2 bullet_vel;
        Rectangle bullet_rect;
        bool show_bullet = false;
        int ammo = 100;
        int temp = 0;
        KeyboardState oldState;
        int range = 0;
        bool first_bullet_fix = true;
        bool disable_shoot_input = false;
        bool unlimited_ammo = false;
        //SHOOTING VARS

        Texture2D background;
        Texture2D asteroid_texture;

        int infinite_ammo_num = 0;

        //TEMPORARY COLLISION VARS
        Texture2D fr;
        Texture2D br;
        //TMP

        SpriteFont EGT;
        SpriteFont PP;
        SpriteFont S;
        SpriteFont FS;
        int score, tmp, final_score;
        bool show_score = true;

        ParticleGenerator white_stars;
        ParticleGenerator yellow_stars;
        ParticleGenerator planet;

        List<Asteroid> asteroids = new List<Asteroid>();
        long asteroid_add_time = 0;

        float asteroid_pos_x;
        Random r1 = new Random();
        Random r2 = new Random();

        float timer = 150;

        bool show_details = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            //dialogue = new Dialogue();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            splash = Content.Load<Texture2D>("Resources/Backgrounds/Splash");

            //MAIN MENU
            title = Content.Load<Texture2D>("Resources/MainMenu/Title");
            play_button = Content.Load<Texture2D>("Resources/MainMenu/MainButtons/playbuttonstatic");
            controls_button = Content.Load<Texture2D>("Resources/MainMenu/MainButtons/controlsbuttonstatic");
            exit_button = Content.Load<Texture2D>("Resources/MainMenu/MainButtons/exitbuttonstatic");
            play_button_hover = Content.Load<Texture2D>("Resources/MainMenu/MainButtons/playbuttonhover");
            controls_button_hover = Content.Load<Texture2D>("Resources/MainMenu/MainButtons/controlsbuttonhover");
            exit_button_hover = Content.Load<Texture2D>("Resources/MainMenu/MainButtons/exitbuttonhover");

            play_button_rect = new Rectangle(50, 150, play_button.Width, play_button.Height);
            controls_button_rect = new Rectangle(50, 200, controls_button.Width, controls_button.Height);
            exit_button_rect = new Rectangle(50, 250, exit_button.Width, exit_button.Height);

            easy_button = Content.Load<Texture2D>("Resources/MainMenu/Difficulty/easybutton");
            normal_button = Content.Load<Texture2D>("Resources/MainMenu/Difficulty/normalbutton");
            hard_button = Content.Load<Texture2D>("Resources/MainMenu/Difficulty/hardbutton");
            easy_button_hover = Content.Load<Texture2D>("Resources/MainMenu/Difficulty/easybuttonhover");
            normal_button_hover = Content.Load<Texture2D>("Resources/MainMenu/Difficulty/normalbuttonhover");
            hard_button_hover = Content.Load<Texture2D>("Resources/MainMenu/Difficulty/hardbuttonhover");

            easy_button_rect = new Rectangle(300, 150, easy_button.Width, easy_button.Height);
            normal_button_rect = new Rectangle(300, 200, normal_button.Width, normal_button.Height);
            hard_button_rect = new Rectangle(300, 250, hard_button.Width, hard_button.Height);

            play_background = Content.Load<Texture2D>("Resources/MainMenu/Difficulty/PlayBackground");
            ammo_font = Content.Load<SpriteFont>("Resources/Fonts/Ammo");

            controls_background = Content.Load<Texture2D>("Resources/MainMenu/ControlsBackground");
            //MAIN MENU

            //SOUND
            laser_sound = Content.Load<SoundEffect>("Resources/SoundEffects/Laser");
            asteroid_explosion_sound = Content.Load<SoundEffect>("Resources/SoundEffects/AsteroidExplosion");
            ship_explosion_sound = Content.Load<SoundEffect>("Resources/SoundEffects/ShipExplosion");
            new_dialogue_sound = Content.Load<SoundEffect>("Resources/SoundEffects/NewDialogue");
            level_music = Content.Load<Song>("Resources/Music/FirstSong");
            menu_music = Content.Load<Song>("Resources/Music/MainMenu");
            beat_song = Content.Load<Song>("Resources/Music/LevelD");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
            SoundEffect.MasterVolume = 0.1f;
            //SOUND

            //PAUSE
            paused_background = Content.Load<Texture2D>("Resources/MainMenu/PausedBackground");
            resume_button = Content.Load<Texture2D>("Resources/MainMenu/resumebuttonstatic");
            resume_button_hover = Content.Load<Texture2D>("Resources/MainMenu/resumebuttonhover");

            resume_button_rect = new Rectangle(315, 170, resume_button.Width, resume_button.Height);
            //PAUSE

            //TEMPORARY LAOD COLLISIION
            fr = new Texture2D(GraphicsDevice, 1, 1);
            fr.SetData(new[] { Color.White });

            br = new Texture2D(GraphicsDevice, 1, 1);
            br.SetData(new[] { Color.White });
            //TMP

            background = Content.Load<Texture2D>("Resources/Backgrounds/black");
            asteroid_texture = Content.Load<Texture2D>("Resources/Characters/asteroid");
            white_stars = new ParticleGenerator(Content.Load<Texture2D>("Resources/Particles/WhiteStar"), GraphicsDevice.Viewport.Width, 100);
            yellow_stars = new ParticleGenerator(Content.Load<Texture2D>("Resources/Particles/YellowStar"), GraphicsDevice.Viewport.Width, 1.5f);
            planet = new ParticleGenerator(Content.Load<Texture2D>("Resources/Particles/Planet"), GraphicsDevice.Viewport.Width, 0.1f);
            explosion = Content.Load<Texture2D>("Resources/Particles/Explosion");
            
            player = new Player(Content.Load<Texture2D>("Resources/Characters/player"), new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2 + 150));
            player_explode = Content.Load<Texture2D>("Resources/Particles/ShipExplosion");
            fade_texture = Content.Load<Texture2D>("Resources/Backgrounds/FadeTexture");


            finish_line = Content.Load<Texture2D>("Resources/Backgrounds/FinishLine");
            win_window = Content.Load<Texture2D>("Resources/Backgrounds/WinWindow");

            //SHOOTING
            bullet = Content.Load<Texture2D>("Resources/Ammo/Laser");
            bullet_pos = new Vector2(-20, -20);
            bullet_rect = new Rectangle((int)bullet_pos.X, (int)bullet_pos.Y, bullet.Width, bullet.Height);
            //SHOOTING

            EGT = Content.Load<SpriteFont>("Resources/Fonts/ElapsedGameTime");
            PP = Content.Load<SpriteFont>("Resources/Fonts/PlayerPosition");
            S = Content.Load<SpriteFont>("Resources/Fonts/Score");
            FS = Content.Load<SpriteFont>("Resources/Fonts/FinalScore");
        }
        
        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();            

            MouseState mouseState = Mouse.GetState();
            if (CurrentState == GameState.Splash)
            {
                splash_timer++;
                if (splash_timer > 150)
                {
                    CurrentState = GameState.MainMenu;
                }
            }
            else if (CurrentState == GameState.MainMenu)
            {
                KeyboardState newState = Keyboard.GetState();
                if (show_story == true)
                {                    
                    if (oldState.IsKeyUp(Keys.Enter) && newState.IsKeyDown(Keys.Enter)/* && mission_info_show == true*/)
                    {
                        mission_info_show = false;
                        /*pilotcard_adi_show = true;
                        pilotcard_anes_show = false;
                        pilotcard_vedad_show = false;*/
                    }
                    else if (oldState.IsKeyUp(Keys.Enter) && newState.IsKeyDown(Keys.Enter) /*&& pilotcard_adi_show == true*/)
                    {
                        mission_info_show = false;
                    /*    pilotcard_adi_show = false;
                        pilotcard_anes_show = true;
                        pilotcard_vedad_show = false;*/
                    }
                    else if (oldState.IsKeyUp(Keys.Enter) && newState.IsKeyDown(Keys.Enter) /*&& pilotcard_anes_show == true*/)
                    {
                        mission_info_show = false;
                        /*pilotcard_adi_show = false;
                        pilotcard_anes_show = false;
                        pilotcard_vedad_show = true;*/
                    }
                    else if (oldState.IsKeyUp(Keys.Enter) && newState.IsKeyDown(Keys.Enter) /*&& pilotcard_vedad_show == true*/)
                    {
                        CurrentState = GameState.Level;
                        show_story = false;
                    }
                    newState = oldState;                    
                }
                timer = 0;
                show_fade_texture = false;
                player_dead = false;
                for (int i = 0; i < asteroids.Count; i++)
                {
                    asteroids.RemoveAt(i);
                }

                songstart_level = false;
                if (!songstart_menu)
                {
                    MediaPlayer.Play(menu_music);
                    songstart_menu = true;
                }

                white_stars.Update(gameTime, graphics.GraphicsDevice);

                play_button_rect = new Rectangle(50, 150, play_button.Width, play_button.Height);
                controls_button_rect = new Rectangle(50, 200, controls_button.Width, controls_button.Height);
                exit_button_rect = new Rectangle(50, 250, exit_button.Width, exit_button.Height);

                newState = Keyboard.GetState();

                if (oldState.IsKeyUp(Keys.Insert) && newState.IsKeyDown(Keys.Insert) && infinite_ammo_num == 0)
                {
                    unlimited_ammo = true;
                    infinite_ammo_num = 1;
                }
                else if (oldState.IsKeyUp(Keys.Insert) && newState.IsKeyDown(Keys.Insert) && infinite_ammo_num == 1)
                {
                    unlimited_ammo = false;
                    infinite_ammo_num = 0;
                }

                oldState = newState;

                if (play_button_rect.Contains(mouseState.X, mouseState.Y))
                {
                    play_button_hover_bool = true;
                    controls_button_hover_bool = false;
                    exit_button_hover_bool = false;
                    diff_buttons_visible = true;
                    control_background_visible = false;
                }
                else if (controls_button_rect.Contains(mouseState.X, mouseState.Y))
                {
                    play_button_hover_bool = false;
                    controls_button_hover_bool = true;
                    exit_button_hover_bool = false;
                    diff_buttons_visible = false;
                    control_background_visible = true;
                }
                else if (exit_button_rect.Contains(mouseState.X, mouseState.Y))
                {
                    play_button_hover_bool = false;
                    controls_button_hover_bool = false;
                    exit_button_hover_bool = true;
                    diff_buttons_visible = false;
                    control_background_visible = false;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Exit();
                    }

                }
                else
                {
                    play_button_hover_bool = false;
                    controls_button_hover_bool = false;
                    exit_button_hover_bool = false;
                }

                if (diff_buttons_visible == true)
                {

                    if (easy_button_rect.Contains(mouseState.X, mouseState.Y))
                    {
                        easy_button_hover_bool = true;
                        normal_button_hover_bool = false;
                        hard_button_hover_bool = false;

                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            difficulty = 1;
                            ammo = 20;
                            score = 0;
                            MediaPlayer.Stop();
                            //STORY
                            if (show_story == true)
                            {
                                mission_info_show = true;
                            }
                            else
                            {
                                CurrentState = GameState.Level;
                            }
                            newState = oldState;                         
                        }

                    }
                    else if (normal_button_rect.Contains(mouseState.X, mouseState.Y))
                    {
                        easy_button_hover_bool = false;
                        normal_button_hover_bool = true;
                        hard_button_hover_bool = false;

                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            difficulty = 2;
                            ammo = 10;
                            score = 0;
                            MediaPlayer.Stop();
                            //STORY
                            if (show_story == true)
                            {
                                mission_info_show = true;
                            }
                            else
                            {
                                CurrentState = GameState.Level;
                            }
                            newState = oldState;
                        }
                    }
                    else if (hard_button_rect.Contains(mouseState.X, mouseState.Y))
                    {
                        easy_button_hover_bool = false;
                        normal_button_hover_bool = false;
                        hard_button_hover_bool = true;

                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            difficulty = 3;
                            ammo = 0;
                            score = 0;
                            MediaPlayer.Stop();
                            //STORY
                            if (show_story == true)
                            {
                                mission_info_show = true;
                            }
                            else
                            {                                
                                CurrentState = GameState.Level;
                            }
                            newState = oldState;
                        }
                    }
                    else
                    {
                        easy_button_hover_bool = false;
                        normal_button_hover_bool = false;
                        hard_button_hover_bool = false;
                    }
                }
            }
            else if (CurrentState == GameState.Level)
            {
                asteroid_add = true;
                songstart_menu = false;
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if(show_score == true){
                    //dialogue.Update(gameTime, timer, difficulty, show_dialogue, new_dialogue_sound);
                }
                finish_line_pos += finish_line_vel;

                /*pilotcard_adi_show = false;
                pilotcard_anes_show = false;
                pilotcard_vedad_show = false;*/
                mission_info_show = false;

                if (player_dead)
                {                    
                    show_fade_texture = true;
                    dead_timer++;                    
                    if (dead_timer > 500)
                    {                        
                        player_dead = false;
                        dead_timer = 0;
                        CurrentState = GameState.MainMenu;
                    }
                }

                //Adding asteroid
                asteroid_add_time++;
                if (asteroid_add == true)
                {
                    if (asteroid_add_time > 300)
                    {
                        asteroid_pos_x = (float)r2.NextDouble() * graphics.GraphicsDevice.Viewport.Width - asteroid_texture.Width;
                        asteroids.Add(new Asteroid(asteroid_texture, new Vector2(asteroid_pos_x, r1.Next(-1000, -250)), new Vector2(0, (float)r2.Next(4, 5)), (float)r2.NextDouble()));
                        asteroid_add_time = 0;
                    }
                }
                
                if (timer > 29)
                {                    
                    if (!songstart_level)
                    {
                        MediaPlayer.Play(level_music);
                        songstart_level = true;
                    }
                }
                if (timer > 64)
                {
                    if (!songstart_beat)
                    {
                        MediaPlayer.Play(beat_song);
                        songstart_beat = true;
                    }
                }
                

                KeyboardState newState = Keyboard.GetState();
                if (game_paused == false)
                {
                    if (oldState.IsKeyUp(Keys.Escape) && newState.IsKeyDown(Keys.Escape))
                    {
                        game_paused = true;
                    }

                    if (player_dead == false && show_score == true)
                    {
                        white_stars.Update(gameTime, graphics.GraphicsDevice);
                        yellow_stars.Update(gameTime, graphics.GraphicsDevice);
                        planet.Update(gameTime, graphics.GraphicsDevice);
                    }                   

                    tmp++;
                    if (tmp == 100 && increment_score == true)
                    {
                        score++;
                        tmp = 0;
                    }

                    for (int i = 0; i < asteroids.Count; i++)
                    {
                        if (timer > 0)
                        {
                            if (player_dead == false)
                            {
                                asteroids[i].Update();
                            }

                            if (asteroids[i].rectangle.Intersects(player.front_rect) || asteroids[i].rectangle.Intersects(player.back_rect))
                            {
                                asteroids.RemoveAt(i);
                                ship_explosion_sound.Play();
                                increment_score = false;
                                final_score = score;
                                player_dead = true;
                            }
                            if (asteroids[i].position.Y > graphics.GraphicsDevice.Viewport.Height)
                            {
                                asteroids[i].position.X = (float)r1.NextDouble() * graphics.GraphicsDevice.Viewport.Width;
                                asteroids[i].position.Y = -50;
                            }
                        }
                    }

                    if (first_bullet_fix == true)
                    {
                        bullet_pos = new Vector2(player.front_rect.X - 10, player.front_rect.Y - 10);
                        first_bullet_fix = false;
                    }

                    if (oldState.IsKeyUp(Keys.F4) && newState.IsKeyDown(Keys.F4) && show_details == false)
                    {
                        show_details = true;
                    }

                    else if (show_details == true && oldState.IsKeyUp(Keys.F4) && newState.IsKeyDown(Keys.F4))
                    {
                        show_details = false;
                    }

                    //SHOOTING                             
                    if (oldState.IsKeyUp(Keys.Space) && newState.IsKeyDown(Keys.Space) && ammo > 0 && disable_shoot_input == false && show_score == true)
                    {
                        bullet_pos = new Vector2(player.front_rect.X - 10, player.front_rect.Y - 10);
                        laser_sound.Play();
                        if (!unlimited_ammo)
                        {
                            ammo--;
                        }
                        temp = 1;
                    }

                    if (temp == 1)
                    {
                        show_bullet = true;

                        bullet_rect = new Rectangle((int)bullet_pos.X, (int)bullet_pos.Y, bullet.Width, bullet.Height);
                        bullet_pos += bullet_vel;
                        bullet_vel.Y = -15;
                        range++;
                        disable_shoot_input = true;

                        foreach (Asteroid asteroid in asteroids)
                        {
                            if (bullet_rect.Intersects(asteroid.rectangle))
                            {
                                show_explosion = true;
                                asteroid_explosion_sound.Play();
                                explosion_pos = new Vector2(asteroid.position.X - 15, asteroid.position.Y - 15);

                                score -= 5;
                                asteroid.position.X = -50;

                                show_bullet = false;

                                range = 100;
                            }
                        }

                    }

                    if (range > 20)
                    {
                        temp = 0;
                        range = 0;
                        show_bullet = false;
                        disable_shoot_input = false;
                    }
                    //SHOOTING

                    //EXPLOSION
                    if (show_explosion == true)
                    {
                        explosion_timer++;
                        if (explosion_timer > 20)
                        {
                            explosion_timer = 0;
                            show_explosion = false;
                        }
                    }
                    //EXPLOSION
                    if (timer > 250)
                    {
                        show_finish_line = true;
                        finish_line_rect = new Rectangle((int)finish_line_pos.X, (int)finish_line_pos.Y - 200, finish_line.Width, finish_line.Height);
                        finish_line_vel.Y = 3f;
                    }

                    if (finish_line_rect.Intersects(player.rectangle))
                    {
                        for (int i = 0; i < asteroids.Count; i++)
                        {
                            asteroids.RemoveAt(i);
                        }
                        asteroid_add = false;
                        final_score = score;
                        show_score = false;
                        show_win_window = true;                        
                    }

                    if (timer > 260)
                    {
                        show_win_window = false;
                        show_score = true;
                        asteroid_add = true;
                        CurrentState = GameState.MainMenu;
                    }

                    if (show_score == true && !player_dead)
                    {
                        player.Update(gameTime);
                    }
                }
                else if (game_paused == true)
                {
                    if (oldState.IsKeyUp(Keys.Escape) && newState.IsKeyDown(Keys.Escape))
                    {
                        game_paused = false;
                    }

                    controls_button_rect = new Rectangle(315, 220, controls_button.Width, controls_button.Height);
                    exit_button_rect = new Rectangle(315, 270, exit_button.Width, exit_button.Height);

                    if (resume_button_rect.Contains(mouseState.X, mouseState.Y))
                    {
                        resume_button_hover_bool = true;
                        controls_button_hover_bool = false;
                        exit_button_hover_bool = false;
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            game_paused = false;
                        }
                    }
                    else if (controls_button_rect.Contains(mouseState.X, mouseState.Y))
                    {
                        resume_button_hover_bool = false;
                        controls_button_hover_bool = true;
                        exit_button_hover_bool = false;
                    }
                    else if (exit_button_rect.Contains(mouseState.X, mouseState.Y))
                    {
                        resume_button_hover_bool = false;
                        controls_button_hover_bool = false;
                        exit_button_hover_bool = true;
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            diff_buttons_visible = false;
                            game_paused = false;
                            CurrentState = GameState.MainMenu;
                        }
                    }
                    else
                    {
                        resume_button_hover_bool = false;
                        controls_button_hover_bool = false;
                        exit_button_hover_bool = false;
                    }
                }

                oldState = newState;
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (CurrentState == GameState.Splash)
            {
                spriteBatch.Draw(splash, new Vector2(0, 0), Color.White);                   
            }
            else if (CurrentState == GameState.MainMenu)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
                white_stars.Draw(spriteBatch);
                spriteBatch.Draw(title, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(play_button, new Vector2(50, 150), Color.White);
                spriteBatch.Draw(controls_button, new Vector2(50, 200), Color.White);
                spriteBatch.Draw(exit_button, new Vector2(50, 250), Color.White);

                if (control_background_visible == true)
                {
                    spriteBatch.Draw(controls_background, new Vector2(270, 85), Color.White);
                    spriteBatch.DrawString(S, "INSERT - infinite ammo", new Vector2(270, 370), Color.White);
                    spriteBatch.DrawString(S, "(must be done in main menu)", new Vector2(269, 390), Color.White);
                    if (infinite_ammo_num == 1)
                    {
                        spriteBatch.DrawString(S, "Infinite Ammo: On", new Vector2(269, 420), Color.White);
                    }
                    else if (infinite_ammo_num == 0)
                    {
                        spriteBatch.DrawString(S, "Infinite Ammo: Off", new Vector2(269, 420), Color.White);
                    }
                }

                if (play_button_hover_bool == true)
                {
                    spriteBatch.Draw(play_button_hover, new Vector2(50, 150), Color.White);
                }
                else if (controls_button_hover_bool == true)
                {
                    spriteBatch.Draw(controls_button_hover, new Vector2(50, 200), Color.White);
                }
                else if (exit_button_hover_bool == true)
                {
                    spriteBatch.Draw(exit_button_hover, new Vector2(50, 250), Color.White);
                }

                if (diff_buttons_visible == true)
                {
                    spriteBatch.Draw(play_background, new Vector2(270, 85), Color.White);
                    spriteBatch.Draw(easy_button, new Vector2(300, 150), Color.White);
                    spriteBatch.Draw(normal_button, new Vector2(300, 200), Color.White);
                    spriteBatch.Draw(hard_button, new Vector2(300, 250), Color.White);

                    if (easy_button_hover_bool == true)
                    {
                        spriteBatch.Draw(easy_button_hover, new Vector2(300, 150), Color.White);
                        spriteBatch.DrawString(ammo_font, "Starting Ammo: 20", new Vector2(480, 160), Color.White);
                    }
                    else if (normal_button_hover_bool == true)
                    {
                        spriteBatch.Draw(normal_button_hover, new Vector2(300, 200), Color.White);
                        spriteBatch.DrawString(ammo_font, "Starting Ammo: 10", new Vector2(480, 210), Color.White);
                    }
                    else if (hard_button_hover_bool == true)
                    {
                        spriteBatch.Draw(hard_button_hover, new Vector2(300, 250), Color.White);
                        spriteBatch.DrawString(ammo_font, "Weapon Disabled.", new Vector2(480, 260), Color.White);
                    }
                }
            }

            else if (CurrentState == GameState.Level)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
                white_stars.Draw(spriteBatch);
                yellow_stars.Draw(spriteBatch);
                planet.Draw(spriteBatch);

                if (show_finish_line)
                {
                    spriteBatch.Draw(finish_line, finish_line_pos, Color.White);
                }

                //SHOOTING
                if (show_bullet == true)
                {
                    spriteBatch.Draw(bullet, bullet_rect, Color.White);
                }
                //SHOOTING        

                player.Draw(spriteBatch);

                foreach (Asteroid asteroid in asteroids)
                {
                    asteroid.Draw(spriteBatch);
                }

                if (show_explosion == true)
                {
                    spriteBatch.Draw(explosion, explosion_pos, Color.White);
                }

                if (show_details == true)
                {
                    spriteBatch.DrawString(EGT, "EGT(Elapsed Game Time): " + timer + "s", new Vector2(3, GraphicsDevice.Viewport.Height - 100), Color.White);
                    spriteBatch.DrawString(PP, "Player_X: " + player.position.X, new Vector2(3, GraphicsDevice.Viewport.Height - 85), Color.White);
                    spriteBatch.DrawString(PP, "Player_Y: " + player.position.Y, new Vector2(3, GraphicsDevice.Viewport.Height - 70), Color.White);
                    spriteBatch.DrawString(PP, "Bullet_range: " + range, new Vector2(3, GraphicsDevice.Viewport.Height - 55), Color.White);
                    spriteBatch.DrawString(PP, "Screen_Width: " + GraphicsDevice.Viewport.Width, new Vector2(3, GraphicsDevice.Viewport.Height - 40), Color.White);
                    spriteBatch.DrawString(PP, "Screen_Height: " + GraphicsDevice.Viewport.Height, new Vector2(3, GraphicsDevice.Viewport.Height - 25), Color.White);
                }
                if (show_score)
                {
                    spriteBatch.DrawString(S, "Score: " + score, new Vector2(3, 2), Color.White);
                    spriteBatch.DrawString(S, "Ammo: " + ammo, new Vector2(3, 20), Color.White);
                }

                if (player_dead)
                {
                    spriteBatch.Draw(player_explode, new Rectangle((int)player.position.X - 10, (int)player.position.Y - 10, player.texture.Width + 20, player.texture.Height + 20), Color.White);
                }

                //TEMPORARY DRAW RECT COLLISION PLAYER    
                /*
                spriteBatch.Draw(fr, player.front_rect, Color.Green);
                spriteBatch.Draw(br, player.back_rect, Color.Red);*/
                //TMP                

                //PAUSED
                if (game_paused)
                {
                    spriteBatch.Draw(paused_background, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(resume_button, new Vector2(315, 170), Color.White);
                    spriteBatch.Draw(controls_button, new Vector2(315, 220), Color.White);
                    spriteBatch.Draw(exit_button, new Vector2(315, 270), Color.White);
                    if (resume_button_hover_bool == true)
                    {
                        spriteBatch.Draw(resume_button_hover, new Vector2(315, 170), Color.White);
                    }
                    else if (controls_button_hover_bool == true)
                    {
                        spriteBatch.Draw(controls_button_hover, new Vector2(315, 220), Color.White);
                        spriteBatch.Draw(controls_background, new Vector2(490, 100), Color.White);
                    }
                    else if (exit_button_hover_bool == true)
                    {
                        spriteBatch.Draw(exit_button_hover, new Vector2(315, 270), Color.White);
                    }
                }
                //PAUSED
            }

            if (show_win_window)
            {
                spriteBatch.DrawString(ammo_font, "Press ENTER to exit", new Vector2(0,0), Color.White);
                spriteBatch.Draw(win_window, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(FS, "" + final_score, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 50, 210), Color.White);
            }

            if (show_fade_texture == true)
            {
                spriteBatch.Draw(player_explode, new Rectangle((int)player.position.X - 10, (int)player.position.Y - 10, player.texture.Width + 20, player.texture.Height + 20), Color.White);
                spriteBatch.Draw(fade_texture, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(FS, "Score: " + final_score, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - 40, 270), Color.White);
            }

            //STORY
       /*     if (mission_info_show == true)
            {
                spriteBatch.Draw(mission_info, new Vector2(0, 0), Color.White);
            }
            else if (pilotcard_adi_show == true)
            {
                spriteBatch.Draw(pilotcard_adi, new Vector2(0, 0), Color.White);
            }
            else if (pilotcard_anes_show == true)
            {
                spriteBatch.Draw(pilotcard_anes, new Vector2(0, 0), Color.White);
            }
            else if (pilotcard_vedad_show == true)
            {
                spriteBatch.Draw(pilotcard_vedad, new Vector2(0, 0), Color.White);
            }      */      

            //dialogue.Draw(spriteBatch);
            //STORY

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
