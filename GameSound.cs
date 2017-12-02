using DarkGDK.Audio;

namespace SpaceInvasion
{
    public class GameSound
    {
        private Sound atmos;
        private Sound music;
        private Sound bulletFire;
        private Sound explosion;
        private int volume_i;

        public GameSound()
        {
            this.volume_i = 100;
        }

        public void Setup()
        {
            this.atmos = new Sound("resources/audio/space.wav");
            this.music = new Sound("resources/audio/triumph.wav");
            this.bulletFire = new Sound("resources/audio/laser1.wav");
            this.explosion = new Sound("resources/audio/explode.wav");
        }

        public void LoopAtmos()
        {
            if (!this.atmos.Playing)
            {
                this.atmos.Play();
                this.atmos.Volume = this.volume_i;
            }
        }

        public void LoopMusic()
        {
            if (!this.music.Playing)
            {
                this.music.Play();
                this.music.Volume = this.volume_i;
            }
        }

        public void StopAtmos()
        {
            this.atmos.Stop();
        }

        public void StopMusic()
        {
            this.music.Stop();
        }

        public void PlayFire()
        {
            this.bulletFire.Play();
            this.bulletFire.Volume = this.volume_i;
        }

        public void PlayExplosion()
        {
            this.explosion.Play();
            this.explosion.Volume = this.volume_i;
        }
    }
}
