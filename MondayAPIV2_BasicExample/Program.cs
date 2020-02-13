using MondayV2API_BasicExample.MondayEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MondayAPIV2_BasicExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiToken = "YOUR TOKEN HERE";
            string apiRoot = "https://api.monday.com/v2/";
            
            // using statement used here to dispose of the client (but it's recommended to reuse HttpClient as much as possible in a real situation)
            // see https://blogs.msdn.microsoft.com/shacorn/2016/10/21/best-practices-for-using-httpclient-on-services/
            using (var client = new MondayClient(apiToken, apiRoot)) 
            {
                var service = new MondayService(client);

                // get all boards
                List<Board> boards = service.GetBoards();
                Console.WriteLine("-- Boards --");
                boards.ForEach(x => Console.WriteLine($"Board: {x.Name}"));

                if (boards.Any())
                {
                    // get items for the first board in the list
                    Board board = service.GetBoardWithItems(boards[0].Id);
                    Console.WriteLine($"\n-- Board {boards[0].Id} Items --");
                    foreach (var boardItem in board.Items)
                    {
                        Console.WriteLine($"-- {boardItem.Id} {boardItem.Name}");
                    }

                    // update a column value with mutation
                    Console.WriteLine($"\n-- Changing a column value --");
                    var columnChange = service.ChangeTextColumnValue(boards[0].Id, board.Items[0].Id, "text3", "hello world!");
                    Console.WriteLine($"The response body was: {columnChange}");
                }
            }
            Console.Read();
        }
    }
}
