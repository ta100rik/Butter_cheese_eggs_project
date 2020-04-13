using System;
using System.Linq;
using System.Numerics;
using TicTacToe_engine;

namespace tictoetoe_console
{
    class Program
    {
        static void Main(string[] args)
        {
            TicTacToe_engine.TicTacToeEngine engine = new TicTacToeEngine();
            string[] numberbox = {"1", "2", "3", "4", "5", "6", "7", "8", "9" };
            while (true) {
                Console.WriteLine("Type a number from 1-9, new or quit");
                Console.WriteLine(engine.getGameStatusStringified());

                Console.WriteLine(engine.Board());
                string command = Console.ReadLine();
                if (command == "quit") {
                    break;
                } else if (command == "new") {
                    engine.Reset();
                } else if (numberbox.Contains(command)) { 
                    int convertedCommand = Int32.Parse(command) - 1;
                   Boolean validCell =  engine.ChooseCell(convertedCommand);
                    if (!validCell) 
                    {
                        Console.WriteLine("Invalid choice");
                    }
                }
                if (engine.getGameover()) {
                    Console.WriteLine(engine.getGameStatusStringified());
                    engine.Reset();
                }

            }
        }
    }
}

