﻿namespace SFMLReady.System
{
    public class GameTime
    {
        private float _deltaTime = 0f;
        private float _timeScale = 10f;

        public float TimeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }

        public float DeltaTime
        {
            get { return TimeScale * _deltaTime; }
            set { _deltaTime = value; }
        }

        public float NonScaledDeltaTime
        {
            get { return _deltaTime; }
        }

        public float TotalTimeElapsed
        {
            get;
            private set;
        }

        public void Update (float deltaTime, float totalTimeElapsed)
        {
            DeltaTime = deltaTime;
            TotalTimeElapsed = totalTimeElapsed;
        }
    }
}
