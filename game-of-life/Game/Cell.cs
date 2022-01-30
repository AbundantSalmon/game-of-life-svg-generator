using System;
using System.Collections.Generic;

namespace GameOfLife.Game
{
    public enum State
    {
        ALIVE,
        DEAD,
    }

    public class Cell
    {
        private readonly Svg.Cell _svgCell;
        private State _state = State.ALIVE;
        private readonly List<Cell> _neighbours = new List<Cell>();
        private State _nextState = State.ALIVE;

        public int DurationSeconds { get => SvgCell.DurationSeconds; set => SvgCell.DurationSeconds = value; }
        public int X { get => SvgCell.X; set => SvgCell.X = value; }
        public int Y { get => SvgCell.Y; set => SvgCell.Y = value; }
        public int Width { get => SvgCell.Width; set => SvgCell.Width = value; }
        public int Height { get => SvgCell.Height; set => SvgCell.Height = value; }
        public int Rx { get => SvgCell.Rx; set => SvgCell.Rx = value; }
        public string Fill { get => SvgCell.Fill; set => SvgCell.Fill = value; }

        public Svg.Cell SvgCell => _svgCell;

        public Cell(State state)
        {
            SetState(state);
            _svgCell = new Svg.Cell();
            _svgCell.AddKeyFrame(
                0,
                0.0);
        }

        public State GetState()
        {
            return _state;
        }

        public void SetState(State value)
        {
            _state = value;
        }

        public void AddNeighbour(Cell cell)
        {
            _neighbours.Add(cell);
        }

        public void PrepareNextState()
        {
            int numOfAliveNeighbours = 0;
            foreach (Cell cell in _neighbours)
            {
                if (cell.GetState() == State.ALIVE)
                {
                    numOfAliveNeighbours++;
                }
            }

            if (GetState() == State.ALIVE)
            {
                if (numOfAliveNeighbours == 2 || numOfAliveNeighbours == 3)
                {
                    _nextState = State.ALIVE;
                }
                else
                {
                    _nextState = State.DEAD;
                }
            }
            else
            {
                if (numOfAliveNeighbours == 3)
                {
                    _nextState = State.ALIVE;
                }
                else
                {
                    _nextState = State.DEAD;
                }

            }
        }

        public void SetNextState()
        {
            Clock clock = Clock.Instance;
            State newState = _nextState;

            SetState(newState);
            int opacityValue = newState == State.ALIVE ? 1 : 0;
            SvgCell.AddKeyFrame(opacityValue, clock.getCurrentTimeFraction());
        }

        public void SetNextState(double timeFraction, bool force = false)
        {
            State newState = _nextState;
            SetState(newState);
            int opacityValue = newState == State.ALIVE ? 1 : 0;
            SvgCell.AddKeyFrame(opacityValue, timeFraction, force);
        }
    }
}
