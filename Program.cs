using System;
using Newtonsoft.Json;
using SecureID.p2p;
using SecureIdBlockchain.core;

namespace SecureID
{
    /// <summary>
    /// Entry point for SecureIDChain 
    /// </summary>
    class Program
    {
        public static int Port = 0;
        public static Server Server = null;
        public static Client Client = new Client();
        public static Blockchain SecureIdAsset = new Blockchain();
        public static string username = "darkmode";
        public static BlockchainCache ourCache = new BlockchainCache();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello SecureID Blockchain!");
            Console.WriteLine("");

            Console.WriteLine(ourCache.GetName());


            var startTime = DateTime.Now;  

            SecureIdAsset.InitializeSecureIdChain();

            if (args.Length >= 1){
                Port = int.Parse(args[0]);
            }

            if (args.Length >= 2){
                username = args[1];
            }

            if (Port > 0)
            {
                Server = new Server();
                Server.Start();
            }

            if (username != "darkmode")
            {
                Console.WriteLine($"Current username is the default {username}");
            }

            DisplayMenu();

  
            //Blockchain secureIdAsset = new Blockchain(); 

            // var user = new UserIdentity("Username", "Lismore", true, true);

            // secureIdAsset.CreateTransaction(new Transaction("Patrick", "Authenticator", 1, user)); 

            // secureIdAsset.ProcessPendingTransactions("LismoreNode");

            // System.Threading.Thread.Sleep(5000);
            
            //  secureIdAsset.ProcessPendingTransactions("LismoreNode2");

            // Console.WriteLine(JsonConvert.SerializeObject(secureIdAsset, Formatting.Indented)); 
            
            // var endTime = DateTime.Now;  
            
            // Console.WriteLine($"Duration: {endTime - startTime}");  
        }

        /// <summary>
        /// Display Menu method, prints the top level node menu
        /// </summary>
        /// <returns></returns>
        public static void DisplayMenu(){

            int selection = 0;

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Please enter the server URL");
                        string serverURL = Console.ReadLine();
                        Client.Connect($"{serverURL}/Blockchain");
                        break;
                    case 3:
                        Console.WriteLine("Please enter the receiver name");
                        string receiverName = Console.ReadLine();

                        Console.WriteLine("\nPlease enter the amount");
                        string amount = Console.ReadLine();

                        Console.WriteLine("\n Thanks we are ready to create your secureID on the blockchain\n");
                        
                        Console.WriteLine("Lets update secureID profile.\n Please enter your username for your public profile?\n");
                        string usernames = Console.ReadLine();

                        // set user id object
                        // TODO: expand UserIdentity object for full identity
                        // TODO: do a validation check with public/private keys before setting user object owner to true
                        UserIdentity user = new UserIdentity("username",usernames, true, true);
                        

                        SecureIdAsset.CreateTransaction(new Transaction(username, receiverName, int.Parse("1"), user));
                        SecureIdAsset.ProcessPendingTransactions(username);

                        Client.Broadcast(JsonConvert.SerializeObject(SecureIdAsset));

                        break;
                    case 4:
                        Console.WriteLine("Blockchain");
                        Console.WriteLine(JsonConvert.SerializeObject(SecureIdAsset, Formatting.Indented));
                        break;
                    case 5:

                        Console.WriteLine("Exit");
                       break;

                }
                Console.WriteLine("\n=========================");
                Console.WriteLine("1. Sync Node");
                Console.WriteLine("2. Create new account");
                Console.WriteLine("3. Create a transaction");
                Console.WriteLine("4. Display Blockchain");
                Console.WriteLine("5. Exit");
                Console.WriteLine("\n=========================\n");
                Console.WriteLine("Please select an action");
                string action = Console.ReadLine();
                selection = int.Parse(action);
               
            }
        }
    }
}
