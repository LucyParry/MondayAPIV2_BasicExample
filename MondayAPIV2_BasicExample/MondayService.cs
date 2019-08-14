using MondayV2API_BasicExample.MondayEntities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MondayAPIV2_BasicExample
{
    public class MondayService
    {
        private MondayClient _mondayClient;

        public MondayService(MondayClient client)
        {
            _mondayClient = client;
        }

        /// <summary>
        /// Send query as a HTTP POST and return the response
        /// </summary>
        private string GetResponseData(string query)
        {
            StringContent content = new StringContent(this.GetQueryContentBody(query), Encoding.UTF8, "application/json");
            using (var response = _mondayClient.PostAsync("", content))
            {
                if (!response.Result.IsSuccessStatusCode)
                {
                    throw new JsonException($"The response from {_mondayClient.BaseAddress} was {response.Result.StatusCode}");
                }
                return response.Result.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// Get the 'data' node from a GraphQL response if it exists and no errors occurred, else throw a HttpException.
        /// </summary>
        private dynamic ParseGraphQLResponse(string responseString, string collectionKey)
        {
            JObject responseObject = JObject.Parse(responseString);

            // The 'errors' and 'data' keys are part of the standard response format set in the GraphQL spec (see https://graphql.github.io/graphql-spec/draft/#sec-Response)
            if (responseObject["errors"] != null)
            {
                throw new JsonException($"The request was successful but contained errors: " +
                                       $"{JsonConvert.DeserializeObject<dynamic>(responseObject["errors"].ToString())}");
            }
            if (responseObject["data"] == null)
            {
                throw new JsonException("The request was successful but contained no data");
            }
            dynamic data = JsonConvert.DeserializeObject<dynamic>(responseObject["data"][collectionKey].ToString());
            return data;
        }

        /// <summary>
        /// Add a GraphQL query statement into JSON with 'query' key ready to be sent in the body of a a HTTP POST
        /// </summary>
        private string GetQueryContentBody(string statement)
        {
            return "{ \"query\": \"" + RemoveNewlinesTabsReturns(statement) + "\" }";
        }

        /// <summary>
        /// Get all boards for the Monday account with ID and name
        /// </summary>
        public List<Board> GetBoards()
        {
            string query = @"{ 
                                boards { 
                                    id 
                                    name
                                }
                             }";

            string response = GetResponseData(query);
            dynamic data = ParseGraphQLResponse(response, "boards");
            List<Board> boards = JsonConvert.DeserializeObject<List<Board>>(data.ToString());
            return boards;
        }

        /// <summary>
        /// Get a board by ID, with item details
        /// </summary>
        public Board GetBoardWithItems(int boardId)
        {
            string query = @"{ boards(ids: " + boardId + ") { " +
                                 @"name
                                   items {
                                       id
                                       name
                                       column_values {
                                           id
                                           value
                                           title
                                       }
                                   }
                               }
                           }";

            string response = GetResponseData(query);
            dynamic data = ParseGraphQLResponse(response, "boards");
            List<Board> boards = JsonConvert.DeserializeObject<List<Board>>(data.ToString());

            if (boards == null || boards.Count < 1)
                return null;

            return boards[0];
        }

        /// <summary>
        /// Helper to remove the newlines, tabs and returns added when setting out the query nicely
        /// </summary>
        public static string RemoveNewlinesTabsReturns(string inputString)
        {
            return inputString.Replace("\n", "").Replace("\r", "").Replace("\t", "");
        }
    }
}
