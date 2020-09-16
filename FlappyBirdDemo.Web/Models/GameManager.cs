using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBirdDemo.Web.Models
{
    public class GameManager
    {
        private readonly int _gravity = 2;

        public event EventHandler MainLoopCompleted;

        public BirdModel Bird { get; set; }
        public List<PipeModel> Pipes { get; private set; }
        public bool IsRunning { get; set; } = false;

        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                MoveObjects();
                CheckForCollisions();
                ManagePipes();

                // Render any changes and wait
                MainLoopCompleted.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }
        }

        public void Jump()
        {
            if (IsRunning)
            {
                Bird.Jump();
            }
        }

        void CheckForCollisions()
        {
            if (Bird.IsOnGround())
            {
                GameOver();
            }

            var centeredPipe = Pipes.FirstOrDefault(p => p.IsCentered());
            if (centeredPipe != null)
            {
                bool hasColidedWithBottom = Bird.DistanceFromGround < centeredPipe.GapBottom - 150;
                bool hasColidedWithTop = Bird.DistanceFromGround + 45 > centeredPipe.GapTop - 150;

                if (hasColidedWithBottom || hasColidedWithTop)
                {
                    GameOver();
                }
            }
        }

        void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().DistanceFromleft <= 250)
            {
                Pipes.Add(new PipeModel());
            }

            if (Pipes.First().IsOffScreen())
            {
                Pipes.Remove(Pipes.First());
            }
        }

        void MoveObjects()
        {
            Bird.Fall(_gravity);
            foreach (var pipe in Pipes)
            {
                pipe.Move();
            }
        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}
