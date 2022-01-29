using System;
namespace game_of_life
{
    public class Board
    {
        private readonly Cell[,] _cells;

        public Board(int rows, int cols)
        {
            this._cells = new Cell[rows, cols];
            for (int i = 0; i < this.GetHeight(); ++i)
            {
                for (int j = 0; j < this.GetWidth(); ++j)
                {
                    this._cells[i, j] = new Cell(State.ALIVE);
                }
            }

            // Set neighbours
            for (int i = 0; i < this.GetHeight(); ++i)
            {
                for (int j = 0; j < this.GetWidth(); ++j)
                {
                    Cell cellOfConcern = this._cells[i, j];
                    if (i > 0)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i - 1, j]);
                    }
                    if (i < this.GetHeight() - 1 - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i + 1, j]);
                    }
                    if (j > 0)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i, j - 1]);
                    }
                    if (j < this.GetWidth() - 1 - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i, j + 1]);
                    }
                }
            }
        }

        public int GetWidth()
        {
            return this._cells.GetLength(1);
        }

        public int GetHeight()
        {
            return this._cells.GetLength(0);
        }

        public Cell GetCell(int row, int col)
        {
            if (row < 0 || row > this.GetHeight() || col < 0 || col > this.GetWidth())
            {
                throw new ArgumentException("Specified cell outside board parameters");
            }
            return this._cells[row, col];
        }

        public void SetRandomBoardState()
        {
            Array values = Enum.GetValues(typeof(State));
            Random random = new Random();
            foreach (Cell cell in this._cells)
            {
                State randomState = (State)values.GetValue(random.Next(values.Length));
                cell.SetState(randomState);
            }
        }

        public void Tick()
        {
            for (int i = 0; i < this.GetHeight(); ++i)
            {
                for (int j = 0; j < this.GetWidth(); ++j)
                {
                    this._cells[i, j].PrepareNextState();
                }
            }
            for (int i = 0; i < this.GetHeight(); ++i)
            {
                for (int j = 0; j < this.GetWidth(); ++j)
                {
                    this._cells[i, j].SetNextState();
                }
            }
        }

        public void PrintBoardState()
        {
            for (int i = 0; i < this.GetHeight(); ++i)
            {
                for (int j = 0; j < this.GetWidth(); ++j)
                {
                    Cell cell = this.GetCell(i, j);
                    if (cell.GetState() == State.ALIVE)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }
        }
    }
}
