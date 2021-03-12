using System;
using System.Linq;
using CoreTweet;
using static CoreTweet.OAuth;

namespace TwOAuthGetter
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] commandArgs = Environment.GetCommandLineArgs();
            if(!(commandArgs.Contains("--consumer-key") && commandArgs.Contains("--consumer-select")) && commandArgs.Length <= 5 )
            {
                Console.WriteLine("Usage: " + commandArgs[0] + " --consumer-key <consumer(API) key> --consumer-select <consumer(API) select key>");
                Environment.Exit(1);
            } else
            {
                string consumerKey = commandArgs[Array.IndexOf(commandArgs, "--consumer-key") + 1];
                string consumerSelect = commandArgs[Array.IndexOf(commandArgs, "--consumer-select") + 1];
                Uri oauthUri = null;
                OAuthSession session = null;
                try
                {
                    session = OAuth.Authorize(consumerKey, consumerSelect);
                    oauthUri = session.AuthorizeUri;
                } catch(Exception ex)
                {
                    Console.WriteLine("An error has occurred. Check the value and internet connection.");
                    Console.WriteLine("Error:" + ex);
                    Environment.Exit(1);
                }

                Console.WriteLine("Authentication URL:" + oauthUri);
                Console.WriteLine("Please acsess. after getting PIN, Please input PIN and Enter.");
                string pin = Console.ReadLine();
                Tokens tokens = null;

                try
                {
                    tokens = session.GetTokens(pin);
                } catch(Exception ex)
                {
                    Console.WriteLine("An error has occurred. Check the value and internet connection.");
                    Console.WriteLine("Error:" + ex);
                    Environment.Exit(1);
                }
                Console.WriteLine("Acess Token(oauth token): " + tokens.AccessToken);
                Console.WriteLine("Acess Token Select(oauth token select): " + tokens.AccessTokenSecret);
                Environment.Exit(0);
            }
        }
    }
}
