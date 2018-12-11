namespace SecureID.p2p
{
    /// <summary>
    /// This class represents the client for the p2p comms
    /// </summary>
    public class Client
    {
        IDictionary<string, WebSocket> websocketDic = new Dictionary<string, WebSocket>();

        /// <summary>
        /// this is the Connect method
        /// </summary>
        /// <param name="url"></param>
        public void Connect(string url) 
        {  
            if (!websocketDic.ContainsKey(url))  
            {  
                WebSocket ws = new WebSocket(url);

                ws.OnMessage += (sender, e) =>   
                {  
                    if (e.Data == "You have joined the SecureIdChain")  
                    {  
                        Console.WriteLine(e.Data);  
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
                            Program.SecureIdAsset = p2pChain;  
                        }  
                    }
                }; 
                
                ws.Connect();  
                ws.Send("Request to join");  
                ws.Send(JsonConvert.SerializeObject(Program.SecureIdAsset));  
                websocketDic.Add(url, ws);  
            }  
        }  
  
        /// <summary>
        /// This is the Send method
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        public void Send(string url, string data)  
        {  
            foreach (var item in websocketDic)  
            {  
                if (item.Key == url)  
                {  
                    item.Value.Send(data);  
                }  
            }  
        }  

        /// <summary>
        /// this is the broadcast method
        /// </summary>
        /// <param name="data"></param>
        public void Broadcast(string data)  
        {  
            foreach (var item in websocketDic)  
            {  
                item.Value.Send(data);  
            }  
        }  
  
        /// <summary>
        /// This method gets a list of servers
        /// </summary>
        /// <returns></returns>
        public IList<string> GetServers()  
        {  
            IList<string> secureIdServers = new List<string>();  

            foreach (var item in websocketDic)  
            {  
                secureIdServers.Add(item.Key);  
            }  
            return servers;  
        }  
  
        /// <summary>
        /// This is the close method
        /// </summary>
        public void Close()  
        {  
            foreach (var item in websocketDic)  
            {  
                item.Value.Close();  
            }  
        }  
    }
}