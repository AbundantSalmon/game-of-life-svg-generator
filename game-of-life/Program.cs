using System;
using System.CommandLine;

namespace GameOfLife
{
    class Program
    {
        static int Main(string[] args)
        {
            Option<int> rowsOption = new Option<int>(
                "--rows",
                getDefaultValue: () => 10,
                description: "Number of rows");
            Option<int> colsOption = new Option<int>(
                "--cols",
                getDefaultValue: () => 10,
                description: "Number of columns");
            Option<int> totalTimeOption = new Option<int>(
                "--time",
                getDefaultValue: () => 5,
                description: "Time in seconds");
            Option<int> timeBetweenTicksOption = new Option<int>(
                "--tick",
                getDefaultValue: () => 500,
                description: "Time between ticks in milliseconds");
            Option<int> cellSpacingOption = new Option<int>(
                "--spacing",
                getDefaultValue: () => 10,
                description: "Spacing in pixels between the cells"
                );
            Option<int> cellRadiusOption = new Option<int>(
                "--radius",
                getDefaultValue: () => 4,
                description: "Radius in pixels between the cells"
                );
            Option<String> cellColourOption = new Option<String>(
                "--colour",
                getDefaultValue: () => "black",
                description: "Colour of the cells"
                );
            Option<int> widthOption = new Option<int>(
                "--width",
                getDefaultValue: () => 600,
                description: "Width in pixels"
                );
            Option<int> heightOption = new Option<int>(
                "--height",
                getDefaultValue: () => 600,
                description: "Height in pixels"
                );

            RootCommand rootCommand = new RootCommand{
                rowsOption,
                colsOption,
                totalTimeOption,
                timeBetweenTicksOption,
                cellSpacingOption,
                cellRadiusOption,
                cellColourOption,
                widthOption,
                heightOption
            };

            rootCommand.Description = "Generates Conway's game of life";

            rootCommand.SetHandler(
                (int rows, int cols, int totalTime, int timeBetweenTicks, int cellSpacing, int cellRadius, String cellColour, int width, int height) =>
                {
                    Run(
                        rows,
                        cols,
                        totalTime,
                        timeBetweenTicks,
                        cellSpacing,
                        cellRadius,
                        cellColour,
                        width,
                        height);
                },
                rowsOption,
                colsOption,
                totalTimeOption,
                timeBetweenTicksOption,
                cellSpacingOption,
                cellRadiusOption,
                cellColourOption,
                widthOption,
                heightOption
                );

            return rootCommand.Invoke(args);
        }

        static void Run(
            int rows,
            int cols,
            int totalTime,
            int timeBetweenTicks,
            int cellSpacing,
            int cellRadius,
            String cellColour,
            int width,
            int height)
        {
            Game.Clock clock = Game.Clock.Instance;
            clock.SetUp(totalTime, timeBetweenTicks);

            Game.Board board = new Game.Board(
                rows,
                cols,
                cellSpacing,
                cellRadius,
                cellColour,
                width,
                height);
            board.SetRandomBoardState();

            while (clock.Tick())
            {
                Console.Clear();
                board.PrintBoardState();
                System.Threading.Thread.Sleep(timeBetweenTicks);
                board.Tick();
            }

            board.SetFinalState();

            board.Canvas.WriteSvg();
        }
    }
}
