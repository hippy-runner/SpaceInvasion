using System;
using System.IO;
using System.Text;
using System.Xml;
using DarkGDK;
using DarkGDK.Audio;
using DarkGDK.Basic2D;
using DarkGDK.Basic3D;
using DarkGDK.Camera;
using DarkGDK.IO;
using DarkGDK.Lighting;

namespace SpaceInvasion
{
    public class SpaceInvasion
    {
        // game variables
        private bool fire_bl;
        private int width_i;
        private int height_i;
        private GameState state_e;
        private GameLevel level_e;

        // backdrop 
        private Backdrop backdrop;

        // enemy variables
        private Aliens aliens;

        // player variables
        private Player player;

        // game sounds
        private GameSound sound;

        // data persistence variables
        private DataAccess dataAccess;
        private long highScore_l;

        // other : error trapping
        private Exception exception_ex;

        // public properties used by windows form
        // handles the keyboard input
        public bool fire { get { return this.fire_bl; } set { this.fire_bl = value; } }

        public SpaceInvasion(int w, int h)
        {
            try
            {
                this.state_e = GameState.LOADING;
                this.level_e = GameLevel.LEVEL_01;
                this.fire_bl = false;
                this.width_i = w;
                this.height_i = h;

                this.dataAccess = new DataAccess();
                this.highScore_l = this.dataAccess.GetHighScore();

                ///////////////////////////////////////////////////////

                this.backdrop = new Backdrop(
                    this.width_i,
                    this.height_i,
                    2, 2); // backdropCount, scrollSpeed

                this.aliens = new Aliens(
                    this.width_i, 
                    this.height_i,
                    3, 10, // bulletCount, bulletSpeed
                    5, 8); // rowCount, colCount

                this.player = new Player(
                    this.width_i,
                    this.height_i,
                    4, 10, 5); // bulletCount, bulletSpeed, lives

                this.sound = new GameSound();
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        public void Setup()
        {
            try
            {
                this.state_e = GameState.SETUP;
                
                Core.DrawSpritesFirst();
                DefaultCamera.SetAspect((float)this.height_i / (float)this.width_i);
                Mouse.Hide();

                this.backdrop.Setup();
                this.player.Setup();
                this.aliens.Setup();
                this.sound.Setup();

                this.state_e = GameState.TITLE;

                Core.Sync();
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        public void Run()
        {
            try
            {
                while (DarkGDK.Engine.LoopGDK)
                {
                    this.player.fire = this.fire_bl;

                    this.Update();

                    Core.Sync();
                }

                if (this.state_e != GameState.LOADING
                    && this.state_e != GameState.SETUP
                    && this.state_e != GameState.TITLE)
                {
                    this.SaveStats();
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }            
        }

        private void Update()
        {
            try
            {
                this.backdrop.Update();

                if (this.player.score > this.highScore_l)
                {
                    this.highScore_l = this.player.score;
                }

                switch (this.state_e)
                {
                    case GameState.TITLE:
                        this.Title();
                        break;

                    case GameState.LEVEL_START:
                        this.LevelStart();
                        break;

                    case GameState.LEVEL_PLAY:
                        this.LevelPlay();
                        break;

                    case GameState.LEVEL_END:
                        this.LevelEnd();
                        break;

                    case GameState.GAME_OVER:
                        this.GameOver();
                        break;

                    default:
                        break;
                }

                this.fire_bl = false;
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void Title()
        {
            try
            {
                Text.CenterText(this.width_i / 2, this.height_i / 2, "SPACE INVASION");

                this.player.lives = 5;
                this.player.score = 0;
                this.level_e = GameLevel.LEVEL_01;

                this.player.Reset();
                this.CleanUp();

                this.sound.LoopAtmos();

                if (this.fire_bl)
                {
                    this.state_e = GameState.LEVEL_START;
                    this.fire_bl = false;
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void LevelStart()
        {
            try
            {
                this.DisplayStats();

                Text.CenterText(this.width_i / 2, this.height_i / 2, string.Format("LEVEL: {0}", ((int)this.level_e + 1)));

                this.player.Reset();
                this.CleanUp();

                this.sound.LoopAtmos();

                if (this.fire_bl)
                {
                    this.sound.StopAtmos();
                    this.aliens.Reset();
                    this.state_e = GameState.LEVEL_PLAY;
                    this.fire_bl = false;
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void LevelPlay()
        {
            try
            {
                this.DisplayStats();

                this.player.Update(ref this.backdrop, ref this.sound);
                this.aliens.Update(ref this.backdrop, ref this.player, ref this.sound);

                this.sound.LoopMusic();

                if (this.fire_bl)
                {
                    this.fire_bl = false;
                }

                // if aliens are all gone, then advance to the next level
                if (this.aliens.alienCount == 0)
                {
                    this.sound.StopMusic();
                    this.state_e = GameState.LEVEL_END;
                }

                // if player is out of life, then end the game
                if (this.player.lives == 0)
                {
                    this.sound.StopMusic();
                    this.state_e = GameState.GAME_OVER;
                }

                // this makes the game impossible to beat :)
                if (this.aliens.alienCount == 1 && this.level_e == GameLevel.LEVEL_15)
                {
                    this.aliens.Reset();
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void LevelEnd()
        {
            try
            {
                if (this.level_e == GameLevel.LEVEL_15)
                {
                    this.state_e = GameState.GAME_OVER;
                }
                else
                {
                    this.level_e = (GameLevel)((int)this.level_e + 1);
                    this.state_e = GameState.LEVEL_START;
                }

                this.CleanUp();
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void GameOver()
        {
            try
            {
                this.DisplayStats();

                Text.CenterText(this.width_i / 2, this.height_i / 2, "GAME OVER");

                this.player.Reset();
                this.CleanUp();

                if (this.fire_bl)
                {
                    this.SaveStats();
                    this.state_e = GameState.TITLE;
                    this.fire_bl = false;
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void CleanUp()
        {
            try
            {
                // clean-up left-over player bullet
                for (int i = 0; i < this.player.bullets.Count; i++)
                {
                    if (this.player.bullets[i].Visible)
                    {
                        this.player.bullets[i].Visible = false;
                    }
                }

                // clean-up left-over alien bullets
                for (int i = 0; i < this.aliens.bullets.Count; i++)
                {
                    if (this.aliens.bullets[i].Visible)
                    {
                        this.aliens.bullets[i].Visible = false;
                    }
                }

                // clean-up left-over aliens
                for (int i = 0; i < (this.aliens.rowCount * this.aliens.colCount); i++)
                {
                    if (this.aliens.sprites[i].Visible)
                    {
                        this.aliens.sprites[i].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void DisplayStats()
        {
            try
            {
                string score_str = string.Format(" <SCORE: {0}> ", this.player.score);
                string level_str = string.Format(" <LEVEL: {0}> ", ((int)this.level_e + 1));
                string lives_str = string.Format(" <LIVES: {0}> ", this.player.lives);
                string highScore_str = string.Format(" <HIGH SCORE: {0}>", this.highScore_l);
                string hitPercent_str = string.Format(" <HIT PERCENT: {0}> ", Math.Round((decimal)this.player.hitPercent, 0));

                if (hitPercent_str.Length == 20)
                {
                    highScore_str += " ";
                }

                Text.CenterText(
                    (this.width_i / 2), 10,
                    string.Format(
                        "{0} | {1} | {2} | {3} | {4}",
                        level_str,
                        lives_str,
                        hitPercent_str,
                        highScore_str,
                        score_str));
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void SaveStats()
        {
            try
            {
                GameData gameData = new GameData();

                gameData.time = DateTime.Now;
                gameData.score = this.player.score;
                gameData.lives = this.player.lives;
                gameData.level = ((int)this.level_e + 1);
                gameData.hits = this.player.hits;
                gameData.misses = this.player.misses;

                this.dataAccess.SaveData(gameData);
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }
    }
}
