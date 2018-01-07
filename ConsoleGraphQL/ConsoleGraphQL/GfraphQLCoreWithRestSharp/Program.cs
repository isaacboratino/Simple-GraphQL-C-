using System;
using System.Collections.Generic;

namespace GfraphQLCoreWithRestSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new GraphQLClientRestSharp("http://localhost:5000/graphql");

            var token = "";

            var mutationLoginEmail = @"
                mutation($input: LoginEmailInput!) { 
                    LoginEmail(input: $input) { 
                        clientMutationId 
                        token 
                        error 
                    }
                }
            ";

            try
            {
                var result = client.Execute(mutationLoginEmail, new
                {
                    input = new
                    {
                        clientMutationId = "abc",
                        email = "isaac@gmail.com",
                        password = "boratino"
                    }
                });

                if (result.errors == null)
                {
                    token = result.data["LoginEmail"].token;
                    Console.WriteLine("LoginEmail: Token = {0}", result.data["LoginEmail"].token);
                }
                else
                {
                    Console.WriteLine("LoginEmail Error: {0}", result.errors[0].message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("LoginEmail Exception: {0}", exception.Message);
            }

            var query = @"
                {
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

            try
            {
                var result = client.Execute(query, null, new Dictionary<string, string>()
                {
                    { "authorization", "Bearer " + token }
                });

                if (result.errors == null)
                {
                    var name = result.data["users"]["edges"][0]["node"]["name"].Value;
                    Console.WriteLine("users: {0}", result.data["users"].ToString());
                }
                else
                {
                    Console.WriteLine("users Error: {0}", result.errors[0].message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("users Exception: {0}", exception.Message);
            }
            Console.ReadLine();

        }
    }
}
