using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spirit_energy_project
{
    public static class Time
    {
        static internal GameTime gameTime;

        static internal long TotalFrames { get; private set; }
        static internal float TotalSeconds { get; private set; }
        static internal float AverageFramesPerSecond { get; private set; }
        static internal float CurrentFramesPerSecond { get; private set; }

        internal const int MaximumSamples = 100;

        static internal Queue<float> _sampleBuffer = new();

        static internal void Update(GameTime _gameTime)
        {
            gameTime = _gameTime;
            CurrentFramesPerSecond = 1.0f / DeltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MaximumSamples)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += DeltaTime;
        }

        public static float FPS => AverageFramesPerSecond;
        public static float DeltaTime => (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}
