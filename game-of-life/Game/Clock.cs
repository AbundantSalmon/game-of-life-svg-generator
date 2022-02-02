using System;
using System.Diagnostics;

namespace GameOfLife.Game
{
    public sealed class Clock
    {
        // Singleton
        public static Clock Instance { get; } = new Clock();
        private Clock()
        {
            // Prevent outside instantiation
        }

        private bool _hasSetup = false;
        private int _totalTime; //seconds
        private int _timeBetweenTicks; //milliseconds
        private int _currentTime = 0; //milliseconds

        public void SetUp(int totalTime, int timeBetweenTicks)
        {
            _totalTime = totalTime;
            _timeBetweenTicks = timeBetweenTicks;
            _hasSetup = true;
        }

        public int getTotalTime()
        {
            Debug.Assert(_hasSetup);
            return _totalTime;
        }

        public bool Tick()
        {
            Debug.Assert(_hasSetup);
            _currentTime += _timeBetweenTicks;
            return (_currentTime < _totalTime * 1000);
        }

        public int getCurrentTime()
        {
            Debug.Assert(_hasSetup);
            return _currentTime;
        }

        public double getCurrentTimeFraction()
        {
            Debug.Assert(_hasSetup);
            return (double)_currentTime / ((double)_totalTime * 1000.0);
        }
    }
}