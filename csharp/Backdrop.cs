using System;
using System.Collections.Generic;
using DarkGDK.Basic2D;

namespace SpaceInvasion
{
    public class Backdrop : GameObject, IGameObject
    {
        // variables
        private Image backdropImage;    // bmp image used for the scrolling bdrop
        private List<Sprite> backdrops;     // a sprite array used to animate the bdrop

        public List<Sprite> sprites { get { return this.backdrops; } set { this.backdrops = value; } }

        public Backdrop(
            int screenWidth,
            int screenHeight,
            int backdropCount,
            int scrollSpeed)
            : base(
                screenWidth,
                screenHeight,
                backdropCount,
                scrollSpeed)
        {

        }

        public override void Setup()
        {
            try
            {
                this.backdropImage = new Image("resources/graphics/backdrop2.bmp");
                this.backdrops = new List<Sprite>(this.count_i);

                for (int i = 0; i < this.count_i; i++)
                {
                    this.backdrops.Add(
                            new Sprite(
                            0, (-i * this.backdropImage.Height),
                            this.backdropImage));

                    this.backdrops[i].SetPriority(0);
                }
            }
            catch (Exception ex)
            {
                this.exception_ex = ex;
            }
        }

        public override void Reset()
        {
            throw new NotSupportedException();
        }

        public override void Update()
        {
            try
            {
                for (int i = 0; i < this.count_i; i++)
                {
                    if (this.backdrops[i].Y >= this.screenHeight_i)
                    {
                        this.backdrops[i].Position(
                            0, (this.speed_i - this.backdropImage.Height));
                    }
                    else
                    {
                        this.backdrops[i].Position(
                            0, (this.backdrops[i].Y + this.speed_i));
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
