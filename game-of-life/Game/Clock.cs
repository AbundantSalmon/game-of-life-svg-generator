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

        private bool _hasBeenSetup = false;
        private int _totalTime; //seconds
        private int _timeBetweenTicks; //milliseconds
        private int _currentTime = 0; //milliseconds

        public void SetUp(int totalTime, int timeBetweenTicks)
        {
            Debug.Assert(!_hasBeenSetup);
            _totalTime = totalTime;
            _timeBetweenTicks = timeBetweenTicks;
            _hasBeenSetup = true;
        }

        public int getTotalTime()
        {
            Debug.Assert(_hasBeenSetup);
            return _totalTime;
        }

        public bool Tick()
        {
            Debug.Assert(_hasBeenSetup);
            _currentTime += _timeBetweenTicks;
            return (_currentTime < _totalTime * 1000);
        }

        public int getCurrentTime()
        {
            Debug.Assert(_hasBeenSetup);
            return _currentTime;
        }

        public double getCurrentTimeFraction()
        {
            Debug.Assert(_hasBeenSetup);
            return (double)_currentTime / ((double)_totalTime * 1000.0);
        }
    }
}