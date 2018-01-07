using Newtonsoft.Json;
using GraphQL;
using System;

namespace ConsoleGraphQLCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new GraphQLClient("http://localhost:5000/graphql");

            Console.WriteLine("QUERY ---------------- ");

            var query = @"
                query {
                  users {
                    edges {
                      node {
                        name 
                        email
                      }
                    }
                  }
                }
            ";

            var obj = client.Query(query, null).Get("users");
            if (obj != null)
            {

                Console.WriteLine("success ");
            }
            else
            {
                Console.WriteLine("Null :(");
            }

            Console.WriteLine("MUTATION ---------------- ");

            var mutation = "mutation { LoginEmail(input: { clientMutationId: \"abc\" email: \"isaac@gmail.com\" password: \"boratino\" }) { clientMutationId token error }}";
            var result = client.Query(mutation, null).Get("LoginEmail");
           

            Console.WriteLine("MUTATION PARAM ---------------- ");

            mutation = @"mutation($input: LoginEmailInput!) { 
                            LoginEmail(input: $input) { 
                                clientMutationId 
                                token 
                                error 
                            }
                        }";
            var result2 = client.Query(mutation, new { input = new { clientMutationId = "abc", email = "isaac@gmail.com", password = "boratino" } } ).Get("LoginEmail");

            Console.ReadLine();
        }
    }
}
