using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdleConquest1._5
{
    class Timer
    {
        public float timer;         //Initialize a 10 second timer
        float TIMER;
        public int count = 0;



        public void startTimer(GameTime gameTime, float myTimer)
        {
            TIMER = myTimer;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                timer = TIMER;   //Reset Timer
                count += 1;

            }

            // TODO: Add your update logic here
        }
    }
}
