using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace SecureID.p2p
{
    /// <summary>
    /// WebSocket Server to enable p2p across a network
    /// </summary>
    public class Server: WebSocketBehavior
    {
        
        bool isSynched = false;  
        WebSocketServer server = null;  
  
        /// <summary>
        /// This is the server start method
        /// </summary>
        public void Start()  
        {  
            server = new WebSocketServer($"ws://127.0.0.1:{Program.Port}");  
            server.AddWebSocketService<Server>("/Blockchain");  
            server.Start();  
            Console.WriteLine($"Started server at ws://127.0.0.1:{Program.Port}");  
        }  
  
        /// <summary>
        ///  the onMessage method 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMessage(MessageEventArgs e)  
        {  
            if (e.Data == "Request to join")  
            {  
                Console.WriteLine(e.Data);  
                Send("You have joined the SecureIdChain");  
            }  
            else  
            {  
                Blockchain p2pChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);  
    
                if (p2pChain.IsChainValid() && p2pChain.SecureIdChain.Count > Program.SecureIdAsset.SecureIdChain.Count)  
                {  
                    List<Transaction> newTransactions = new List<Transaction>();  
                    newTransactions.AddRange(p2pChain.PendingTransactions);  
                    newTransactions.AddRange(Program.SecureIdAsset.PendingTransactions);
    
                    p2pChain.PendingTransactions = newTransactions;  
                    Program.SecureIdAsset = newChain;  
                }  
    
                if (!isSynched)  
                {  
                    Send(JsonConvert.SerializeObject(Program.SecureIdAsset));  
                    isSynched = true;  
                }  
            }  
        } 
    }
}