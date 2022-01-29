using System;
using System.Collections.Generic;

namespace game_of_life
{
    public enum State
    {
        ALIVE,
        DEAD,
    }

    public class Cell
    {
        private State _state = State.ALIVE;
        private readonly List<Cell> _neighbours = new List<Cell>();
        private State _nextState = State.ALIVE;

        public Cell(State state)
        {
            this.SetState(state);
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
            foreach (Cell cell in this._neighbours)
            {
                if (cell.GetState() == State.ALIVE)
                {
                    numOfAliveNeighbours++;
                }
            }

            if (this.GetState() == State.ALIVE)
            {
                if (numOfAliveNeighbours == 2 || numOfAliveNeighbours == 3)
                {
                    this._nextState = State.ALIVE;
                }
                else
                {
                    this._nextState = State.DEAD;
                }
            }
            else
            {
                if (numOfAliveNeighbours == 3)
                {
                    this._nextState = State.ALIVE;
                }
                else
                {
                    this._nextState = State.DEAD;
                }

            }
        }

        public void SetNextState()
        {
            SetState(this._nextState);
        }
    }
}
