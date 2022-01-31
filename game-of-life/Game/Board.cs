using System;
using System.Collections.Generic;

namespace GameOfLife.Game
{
    public class Board
    {
        private readonly Svg.Canvas _canvas;
        private readonly Cell[,] _cells;

        public Svg.Canvas Canvas => _canvas;

        public Board(
            int rows,
            int cols,
            int cellSpacing,
            int cellRadius,
            String cellColour,
            int width,
            int height)
        {
            this._canvas = new Svg.Canvas(width, height);
            this._cells = new Cell[rows, cols];

            for (int i = 0; i < this.GetHeight(); ++i)
            {
                for (int j = 0; j < this.GetWidth(); ++j)
                {
                    Cell newCell = new Cell(State.ALIVE);
                    this._cells[i, j] = newCell;
                    this._canvas.AddCell(newCell.SvgCell);

                    int cellHeight = (_canvas.Height / rows) - cellSpacing;
                    int cellWidth = (_canvas.Width / cols) - cellSpacing;
                    newCell.Height = cellHeight;
                    newCell.Width = cellWidth;
                    newCell.X = (cellWidth + cellSpacing) * j;
                    newCell.Y = (cellWidth + cellSpacing) * i;
                    newCell.Rx = cellRadius;
                    newCell.DurationSeconds = Clock.Instance.getTotalTime();
                    newCell.Fill = cellColour;
                }
            }

            // Set neighbours
            for (int i = 0; i < this.GetHeight(); ++i)
            {
                for (int j = 0; j < this.GetWidth(); ++j)
                {
                    // Adjacents
                    Cell cellOfConcern = this._cells[i, j];
                    if (i > 0)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i - 1, j]);
                    }
                    if (i < this.GetHeight() - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i + 1, j]);
                    }
                    if (j > 0)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i, j - 1]);
                    }
                    if (j < this.GetWidth() - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i, j + 1]);
                    }

                    // Diagonals
                    if (i > 0 && j > 0)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i - 1, j - 1]);
                    }
                    if (i > 0 && j < this.GetWidth() - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i - 1, j + 1]);
                    }
                    if (i < this.GetHeight() - 1 && j > 0)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i + 1, j - 1]);
                    }
                    if (i < this.GetHeight() - 1 && j < this.GetWidth() - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i + 1, j + 1]);
                    }

                    // Wrap around
                    if (i == 0 && j != 0 && j != this.GetWidth() - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[this.GetHeight() - 1, j]);
                        cellOfConcern.AddNeighbour(this._cells[this.GetHeight() - 1, j - 1]);
                        cellOfConcern.AddNeighbour(this._cells[this.GetHeight() - 1, j + 1]);
                    }
                    if (i == this.GetHeight() - 1 && j != 0 && j != this.GetWidth() - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[0, j]);
                        cellOfConcern.AddNeighbour(this._cells[0, j - 1]);
                        cellOfConcern.AddNeighbour(this._cells[0, j + 1]);
                    }
                    if (j == 0 && i != 0 && i != this.GetHeight() - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i, this.GetWidth() - 1]);
                        cellOfConcern.AddNeighbour(this._cells[i - 1, this.GetWidth() - 1]);
                        cellOfConcern.AddNeighbour(this._cells[i + 1, this.GetWidth() - 1]);
                    }
                    if (j == this.GetWidth() - 1 && i != 0 && i != this.GetHeight() - 1)
                    {
                        cellOfConcern.AddNeighbour(this._cells[i, 0]);
                        cellOfConcern.AddNeighbour(this._cells[i - 1, 0]);
                        cellOfConcern.AddNeighbour(this._cells[i + 1, 0]);
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

        public void SetBoardState(List<List<String>> list)
        {
            for (int i = 0; i < this.GetHeight(); ++i)
            {
                for (int j = 0; j < this.GetWidth(); ++j)
                {
                    this._cells[i, j].SetState(list[i][j].Equals("1") ? State.ALIVE : State.DEAD);
                }
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

        public void SetFinalState()
        {
            for (int i = 0; i < this.GetHeight(); ++i)
            {
                for (int j = 0; j < this.GetWidth(); ++j)
                {
                    this._cells[i, j].SetNextState(1.0, true);
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
