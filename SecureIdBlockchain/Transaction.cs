namespace SecureIdBlockchain.core
{
    /// <summary>
    /// This class represents a transaction on the SecureIdChain
    /// </summary>
    public class Transaction
    {
        public string FromAddress { get; set; }  
        public string ToAddress { get; set; }  
        public int Amount { get; set; } 
        public UserIdentity Data {get; set;}
  
        /// <summary>
        /// The transaction method is committed to the blockchain
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="amount"></param>
        /// <param name="data"></param>
        public Transaction(string fromAddress, string toAddress, int amount, UserIdentity data)  
        {  
            FromAddress = fromAddress;  
            ToAddress = toAddress;  
            Amount = amount;  
            Data = data;
        }  
    }
}