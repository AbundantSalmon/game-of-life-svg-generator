using System;
using System.Runtime.Serialization;

namespace GameOfLife.Game
{
    public static class SvgCalcMode
    {
        public const String DISCRETE = "discrete";
        public const String LINEAR = "linear";
    }

    public sealed class Config
    {
        // Singleton
        public static Config Instance { get; } = new Config();
        public string CalcMode { get => _calcMode; set => _calcMode = value; }
        public string BackgroundColour { get => _backgroundColour; set => _backgroundColour = value; }

        private Config()
        {
            // Prevent outside instantiation
        }

        private String _calcMode = SvgCalcMode.DISCRETE;
        private String _backgroundColour = "rgba(0, 0, 0, 0)";
    }
}
