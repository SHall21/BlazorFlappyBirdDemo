using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBirdDemo.Web.Models
{
    public class PipeModel
    {
        public int DistanceFromleft { get; private set; } = 500;
        public int DistanceFromBottom { get; private set; } = new Random().Next(0, 60);
        public int Speed { get; private set; } = 2;
        public int Gap { get; private set; } = 130;
        public int GapBottom => DistanceFromBottom + 300;
        public int GapTop => GapBottom + Gap;

        public bool IsOffScreen()
        {
            return DistanceFromleft <= -60;
        }

        public void Move()
        {
            DistanceFromleft -= Speed;
        }

        public bool IsCentered()
        {
            // Half of game container and half of bird object
            bool hasEnteredCenter = DistanceFromleft <= (500 / 2) + (60 / 2);
            // Above but width of pipe as well
            bool hasExitedcenter = DistanceFromleft <= (500 / 2) - (60 / 2) - 60;

            return hasEnteredCenter && !hasExitedcenter;
        }
    }
}
