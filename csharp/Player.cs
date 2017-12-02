using System;
using System.Collections.Generic;
using DarkGDK.Basic2D;
using DarkGDK.IO;

namespace SpaceInvasion
{
    public class Player : GameObject, IGameObject
    {
        private Image playerImage;
        private Image bulletImage;
        private Sprite player_s;
        private List<Sprite> bullets_s;
        private int lives_i;
        private int score_i;
        private float hits_f;
        private float misses_f;
        private bool fire_bl;

        public Sprite sprite { get { return this.player_s; } set { this.player_s = value; } }
        public List<Sprite> bullets { get { return this.bullets_s; } set { this.bullets_s = value; } }
        public int lives { get { return this.lives_i; } set { this.lives_i = value; } }
        public int score { get { return this.score_i; } set { this.score_i = value; } }
        public float hits { get { return this.hits_f; } }
        public float misses { get { return this.misses_f; } }
        public bool fire { get { return this.fire_bl; } set { this.fire_bl = value; } }

        public float hitPercent
        {
            get
            {
                if ((this.hits_f + this.misses_f) != 0)
                {
                    return (100.0f * (this.hits_f / (this.hits_f + this.misses_f)));
                }
                else
                {
                    return 0.0f;
                }
            }
        }

        public Player(
            int screenWidth,
            int screenHeight,
            int bulletCount,
            int bulletSpeed,
            int lives)
            : base(
                screenWidth,
                screenHeight,
                bulletCount,
                bulletSpeed)
        {
            this.lives_i = lives;
            this.score_i = 0;
            this.hits_f = 0.0f;
            this.misses_f = 0.0f;
        }

        public override void Setup()
        {
            this.playerImage = new Image("resources/graphics/player.bmp");
            this.bulletImage = new Image("resources/graphics/playerbullet.bmp");
            
            this.bullets_s = new List<Sprite>(this.count_i);

            this.player_s = new Sprite(
                ((this.screenWidth_i / 2) - 20),
                (this.screenHeight_i - 70),
                this.playerImage);

            this.player_s.SetPriority(1);

            for (int i = 0; i < this.count_i; i++)
            {
                this.bullets_s.Add(
                    new Sprite(
                        -(this.screenWidth_i),
                        -(this.screenHeight_i),
                        this.bulletImage));

                this.bullets_s[i].SetPriority(0);
                this.bullets_s[i].Visible = false;
            }
        }

        public override void Reset()
        {
            try
            {
                this.player_s.Position(
                    ((this.screenWidth_i / 2) - 20),
                    (this.screenHeight_i - 70));
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            } 
        }

        public override void Update()
        {
            throw new NotSupportedException();
        }

        public void Update(
            ref Backdrop backdrop,
            ref GameSound sound)
        {
            try
            {
                this.PlayerUpdate();
                this.BulletUpdates(ref backdrop, ref sound);
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void PlayerUpdate()
        {
            try
            {
                if (!this.player_s.Visible)
                {
                    this.lives_i--;
                    this.Reset();
                    this.player_s.Visible = true;
                }

                if ((Mouse.X > (this.player_s.Width / 4))
                    && (Mouse.X <= (this.screenWidth_i - (this.player_s.Width + (this.player_s.Width / 4)))))
                {
                    this.player_s.Position(
                        Mouse.X,
                        this.player_s.Y);
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void BulletUpdates(
            ref Backdrop backdrop, 
            ref GameSound sound)
        {
            Sprite tempCollision_s = null;

            try
            {
                if (this.fire_bl)
                {
                    for (int i = 0; i < this.count_i; i++)
                    {
                        if (!this.bullets_s[i].Visible)
                        {
                            this.bullets_s[i].Visible = true;
                            sound.PlayFire();
                            break;
                        }
                    }

                    this.fire_bl = false;
                }

                for (int i = 0; i < this.count_i; i++)
                {
                    if (this.bullets_s[i].Visible)
                    {
                        if (this.bullets_s[i].Y > -(this.bullets_s[i].Height))
                        {
                            this.bullets_s[i].Position(
                                this.bullets_s[i].X,
                                (this.bullets_s[i].Y - this.speed_i));
                        }
                        else
                        {
                            this.misses_f++;
                            this.bullets_s[i].Visible = false;
                        }

                        tempCollision_s = this.bullets_s[i].Collision();

                        if (tempCollision_s != null
                            //&& tempCollision_s != this.player_s
                            && !backdrop.sprites.Contains(tempCollision_s))
                        {
                            this.hits_f++;
                            this.score_i += 100;
                            this.bullets_s[i].Visible = false;

                            sound.PlayExplosion();
                            tempCollision_s.Visible = false;
                            tempCollision_s = null;
                        }
                    }
                    else
                    {
                        this.bullets_s[i].Position(
                            (this.player_s.X + (this.player_s.Width / 2)),
                            (this.player_s.Y + (this.player_s.Height / 2)));
                    }
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }
    }
}
