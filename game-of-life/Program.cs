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

            RootCommand rootCommand = new RootCommand{
                rowsOption,
                colsOption,
                totalTimeOption,
                timeBetweenTicksOption
            };

            rootCommand.Description = "Generates Conway's game of life";

            rootCommand.SetHandler(
                (int rows, int cols, int totalTime, int timeBetweenTicks) =>
                {
                    Run(rows, cols, totalTime, timeBetweenTicks);
                },
                rowsOption, colsOption, totalTimeOption, timeBetweenTicksOption
                );

            return rootCommand.Invoke(args);
        }

        static void Run(int rows, int cols, int totalTime, int timeBetweenTicks)
        {
            Game.Board board = new Game.Board(rows, cols);
            board.SetRandomBoardState();

            for (int i = 0; i < totalTime * 1000 / timeBetweenTicks; ++i)
            {
                Console.Clear();
                board.PrintBoardState();
                board.Tick();
                System.Threading.Thread.Sleep(timeBetweenTicks);
            }
        }
    }
}
