using System;
using System.Collections.Generic;
using DarkGDK.Basic2D;

namespace SpaceInvasion
{
    public class Aliens : GameObject, IGameObject
    {
        private Image alienImage;
        private Image bulletImage;
        private List<Sprite> aliens_s;
        private List<Sprite> bullets_s;
        private Dictionary<Sprite, Sprite> shooting_s;
        private int rowCount_i;
        private int colCount_i;
        private int alienCount_i;
        private int alienSpeed_i;
        private int direction_i;

        public List<Sprite> sprites { get { return this.aliens_s; } set { this.aliens_s = value; } }
        public List<Sprite> bullets { get { return this.bullets_s; } set { this.bullets_s = value; } }
        public int rowCount { get { return this.rowCount_i; } }
        public int colCount { get { return this.colCount_i; } }
        public int alienCount { get { return this.alienCount_i; } }
        public int speed { get { return this.alienSpeed_i; } set { this.alienSpeed_i = value; } }

        public Aliens(
            int screenWidth, 
            int screenHeight, 
            int bulletCount, 
            int bulletSpeed,
            int rowCount,
            int colCount)
            : base(
                screenWidth,
                screenHeight,
                bulletCount,
                bulletSpeed)
        {
            this.shooting_s = new Dictionary<Sprite, Sprite>(this.count_i);
            this.rowCount_i = rowCount;
            this.colCount_i = colCount;
            this.alienCount_i = this.rowCount_i * this.colCount_i;
            this.alienSpeed_i = 1;
            this.direction_i = 1;
        }

        public override void Setup()
        {
            try
            {
                this.alienImage = new Image("resources/graphics/enemy.bmp");
                this.bulletImage = new Image("resources/graphics/enemybullet.bmp");

                this.aliens_s = new List<Sprite>(this.alienCount_i);
                this.bullets_s = new List<Sprite>(this.count_i);

                for (int i = 0; i < this.rowCount_i; i++)
                {
                    for (int j = 0; j < this.colCount_i; j++)
                    {
                        int index_i = (this.colCount_i * i) + j;

                        this.aliens_s.Add(
                            new Sprite(
                            -(this.screenWidth_i),
                            -(this.screenHeight_i),
                            this.alienImage));

                        this.aliens_s[index_i].SetPriority(1);
                        this.aliens_s[index_i].Visible = false;
                    }
                }

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
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        public override void Reset()
        {
            try
            {
                this.alienCount_i = (this.rowCount_i * this.colCount_i);
                this.alienSpeed_i++;

                for (int i = 0; i < this.rowCount_i; i++)
                {
                    for (int j = 0; j < this.colCount_i; j++)
                    {
                        int index_i = (this.colCount_i * i) + j; 

                        this.aliens_s[index_i].Position(
                            ((j * (this.alienImage.Width + 10)) + 10),
                            ((i * (this.alienImage.Height + 10)) + 50));

                        this.aliens_s[index_i].Flipped = true;
                        this.aliens_s[index_i].Visible = true;
                    }
                }
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
            ref Player player, 
            ref GameSound sound)
        {
            try
            {
                this.MoveAliens(ref backdrop, ref sound);
                this.AliensShoot(ref player, ref sound);
                this.MoveBullets(ref backdrop, ref sound);
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void MoveAliens(
            ref Backdrop backdrop,
            ref GameSound sound)
        {
            try
            {
                for (int i = 0; i < (this.rowCount_i * this.colCount); i++)
                {
                    if (this.aliens_s[i].Visible)
                    {
                        if (direction_i == 1)
                        {
                            if (this.aliens_s[i].X > (this.aliens_s[i].Width / 5))
                            {
                                this.aliens_s[i].Position(
                                    (this.aliens_s[i].X - this.alienSpeed_i),
                                    this.aliens_s[i].Y);
                            }
                            else
                            {
                                this.direction_i = -1;
                            }
                        }
                        else
                        {
                            if (this.aliens_s[i].X
                                < (this.screenWidth_i - (this.aliens_s[i].Width + (this.aliens_s[i].Width / 4))))
                            {
                                this.aliens_s[i].Position(
                                    (this.aliens_s[i].X + this.alienSpeed_i),
                                    this.aliens_s[i].Y);
                            }
                            else
                            {
                                this.direction_i = 1;
                            }
                        }
                    }
                    else
                    {
                        this.aliens_s[i].Position(
                            -(this.screenWidth_i),
                            -(this.screenHeight_i));
                    }
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void AliensShoot(            
            ref Player player, 
            ref GameSound sound)
        {
            try
            {
                for (int i = 0; i < (this.rowCount_i * this.colCount_i); i++)
                {
                    if ((this.aliens_s[i].X - (this.alienImage.Width / 2)) > (player.sprite.X)
                        && (this.aliens_s[i].X - (this.alienImage.Width / 2)) < (player.sprite.X + player.sprite.Width))
                    {
                        if (!this.shooting_s.ContainsValue(this.aliens_s[i])
                            && this.shooting_s.Count < this.count_i)
                        {
                            for (int a = 0; a < this.count_i; a++)
                            {
                                if (!this.bullets_s[a].Visible)
                                {
                                    this.bullets_s[a].Visible = true;
                                    this.bullets_s[a].Position(
                                        (this.aliens_s[i].X + (this.aliens_s[i].Width / 2)),
                                        (this.aliens_s[i].Y + (this.aliens_s[i].Height / 2)));
                                    sound.PlayFire();
                                    this.shooting_s.Add(this.bullets_s[a], this.aliens_s[i]);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        private void MoveBullets(
            ref Backdrop backdrop,
            ref GameSound sound)
        {
            Sprite tempCollision_s = null;

            try
            {
                for (int i = 0; i < this.count_i; i++)
                {
                    if (this.bullets_s[i].Visible)
                    {
                        if (this.bullets_s[i].Y < (this.screenHeight_i + this.bullets_s[i].Height))
                        {
                            this.bullets_s[i].Position(
                                this.bullets_s[i].X,
                                (this.bullets_s[i].Y + this.speed_i));
                        }
                        else
                        {
                            this.bullets_s[i].Visible = false;
                            this.shooting_s.Remove(this.bullets_s[i]);
                        }

                        tempCollision_s = this.bullets_s[i].Collision();

                        if (tempCollision_s != null
                            && !this.aliens_s.Contains(tempCollision_s)
                            && !backdrop.sprites.Contains(tempCollision_s))
                        {
                            this.bullets_s[i].Visible = false;
                            this.shooting_s.Remove(this.bullets_s[i]);

                            sound.PlayExplosion();
                            tempCollision_s.Visible = false;
                            tempCollision_s = null;
                        }

                        tempCollision_s = null;
                    }
                    else
                    {
                        this.bullets_s[i].Position(
                            -(this.screenWidth_i),
                            -(this.screenHeight_i));
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
