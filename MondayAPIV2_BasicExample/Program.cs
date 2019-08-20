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
            
            // using statement used here to dispose of the client, but it's recommended to reuse HttpClient as much as possible in a real situation
            // see https://blogs.msdn.microsoft.com/shacorn/2016/10/21/best-practices-for-using-httpclient-on-services/
            using (var client = new MondayClient(apiToken, apiRoot)) 
            {
                var service = new MondayService(client);

                // get all boards
                List<Board> boards = service.GetBoards();
                boards.ForEach(x => Console.WriteLine(x.Name));

                if (boards.Any())
                {
                    // get items for the first board in the list
                    Board board = service.GetBoardWithItems(boards[0].Id);
                    foreach (var boardItem in board.Items)
                    {
                        Console.WriteLine(boardItem.Name);
                    }
                }
            }
                        
            Console.Read();
        }
    }
}
