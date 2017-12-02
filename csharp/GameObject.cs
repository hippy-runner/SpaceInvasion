using System;

namespace SpaceInvasion
{
    public interface IGameObject
    {
        void Setup();
        void Update();
        void Reset();
    }

    public abstract class GameObject : IGameObject
    {
        protected Exception exception_ex;

        protected int speed_i;
        protected int count_i;
        protected int screenWidth_i;
        protected int screenHeight_i;

        public GameObject(
            int screenWidth,
            int screenHeight,
            int count,
            int speed)
        {
            this.screenWidth_i = screenWidth;
            this.screenHeight_i = screenHeight;
            this.count_i = count;
            this.speed_i = speed;
        }

        public abstract void Setup();
        public abstract void Update();
        public abstract void Reset();
    }
}
