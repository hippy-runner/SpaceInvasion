using System;

namespace SpaceInvasion
{
    public class GameData
    {
        private DateTime time_dt;
        private long score_l;
        private int lives_i;
        private int level_i;
        private float hits_f;
        private float misses_f;

        public DateTime time { get { return this.time_dt; } set { this.time_dt = value; } }
        public long score { get { return this.score_l; } set { this.score_l = value; } }
        public int lives { get { return this.lives_i; } set { this.lives_i = value; } }
        public int level { get { return this.level_i; } set { this.level_i = value; } }
        public float hits { get { return this.hits_f; } set { this.hits_f = value; } }
        public float misses { get { return this.misses_f; } set { this.misses_f = value; } }
        public float hitPercent
        {
            get
            {
                if ((this.misses_f + this.hits_f) != 0)
                {
                    return (100.0f * (this.hits_f / (this.misses_f + this.hits_f)));
                }
                else
                {
                    return 0.0f;
                }
            }
        }

        public GameData()
        {
            this.time_dt = new DateTime();
            this.score_l = 0;
            this.lives_i = 0;
            this.level_i = 0;
            this.hits_f = 0.0f;
            this.misses_f = 0.0f;
        }

        public GameData(
            DateTime time,
            long score,
            int lives,
            int level,
            float hits,
            float misses)
        {
            this.time_dt = time;
            this.score_l = score;
            this.lives_i = lives;
            this.level_i = level;
            this.hits_f = hits;
            this.misses_f = misses;
        }
    }
}
