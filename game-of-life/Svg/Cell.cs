using System;
namespace GameOfLife.Svg
{
    public class Cell
    {
        private int _x = 0;
        private int _y = 0;
        private int _width = 50;
        private int _height = 50;
        private int _rx = 4;
        private String _fill = "black";
        private int _durationSeconds = 5;
        private String _animationValues = "";
        private String _keyTimes = "";

        public Cell()
        {
        }

        public Cell(int x,
                    int y,
                    int width,
                    int height,
                    int rx)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Rx = rx;
        }

        public int DurationSeconds { get => _durationSeconds; set => _durationSeconds = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public int Rx { get => _rx; set => _rx = value; }
        public string Fill { get => _fill; set => _fill = value; }

        public void AddKeyFrame(int opacityValue, double timeFraction)
        {
            if(_animationValues.Equals(""))
            {
                _animationValues += $"{opacityValue}";
            } else
            {
                _animationValues += $";{opacityValue}";
            }

            if (_keyTimes.Equals(""))
            {
                _keyTimes += $"{timeFraction}";
            } else
            {
                _keyTimes += $";{timeFraction}";
            }
        }

        public String GenerateSvgElement()
        {
            String calcMode = Game.Config.Instance.CalcMode;
            String element =
                $"<rect x=\"{X}\" y=\"{Y}\" width=\"{Width}\" height=\"{Height}\" rx=\"{Rx}\" fill=\"{Fill}\">\n" +
                $"\t<animate calcMode=\"{calcMode}\" attributeName=\"fill-opacity\" values=\"{_animationValues}\" keyTimes=\"{_keyTimes}\" dur=\"{_durationSeconds}s\" repeatCount =\"indefinite\"/>" +
                $"</rect>\n";
            return element;
        }
    }
}
