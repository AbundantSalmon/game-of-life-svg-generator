using System;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife.Svg
{
    public class Canvas
    {
        private int _width = 600;
        private int _height = 600;
        private readonly List<Cell> _elements;

        public Canvas(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this._elements = new List<Cell>();
        }

        public Canvas()
        {
            this._elements = new List<Cell>();
        }

        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }

        public void AddCell(Cell cell)
        {
            _elements.Add(cell);
        }

        public String GenerateSvg()
        {
            String text = $"<svg style=\"background-color:{Game.Config.Instance.BackgroundColour}\" width=\"{_width}\" height=\"{_height}\">\n";
            foreach (Cell cell in _elements)
            {
                if (!cell.IsEmpty())
                {
                    text += cell.GenerateSvgElement();
                }
            }
            text += $"</svg>\n";
            return text;
        }

        public void WriteSvg(String filename)
        {
            String text = GenerateSvg();
            File.WriteAllText($"{filename}.svg", text);
        }

        public void WriteHtml(String filename)
        {
            String text = "<html>\n";
            text += "<body>\n";
            text += GenerateSvg();
            text += "</body>\n";
            text += "</html>\n";
            File.WriteAllText($"{filename}.html", text);
        }
    }
}
