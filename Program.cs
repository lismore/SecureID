using System;
using Newtonsoft.Json;
using SecureID.core;

namespace SecureID
{
    /// <summary>
    /// Entry point for SecureIDChain 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello SecureID Blockchain!");

            var startTime = DateTime.Now;  
  
            Blockchain secureIdAsset = new Blockchain(); 

            var user = new UserIdentity("Username", "Lismore", true, true);

            secureIdAsset.CreateTransaction(new Transaction("Patrick", "Authenticator", 1, user)); 

            secureIdAsset.ProcessPendingTransactions("LismoreNode");

            System.Threading.Thread.Sleep(5000);
            
             secureIdAsset.ProcessPendingTransactions("LismoreNode2");

            Console.WriteLine(JsonConvert.SerializeObject(secureIdAsset, Formatting.Indented)); 
            
            var endTime = DateTime.Now;  
            
            Console.WriteLine($"Duration: {endTime - startTime}");  
        }
    }
}
