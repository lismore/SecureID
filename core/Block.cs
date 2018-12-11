using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SecureID.core
{
    /// <summary>
    /// This class represents a Block on the Blockchain
    /// </summary>
    public class Block
    {
        public int Nonce { get; set; } = 0;  
        public int Index {get; set;}
        public DateTime TimeStamp { get; set; }
        public string PreviousBlockHash { get; set; }
        public string Hash {get; set;}

        public IList<Transaction> Transactions { get; set; } 
        
        /// <summary>
        /// This is the objects constrictor 
        /// </summary>
        /// <param name="PreviousBlockHash"></param>
        /// <param name="data"></param>
        /// <param name="timeStamp"></param>
        public Block(string _previousBlockHash, IList<Transaction>  _data, DateTime _timeStamp){

            Index = 0;  
            TimeStamp = _timeStamp;  
            PreviousBlockHash = _previousBlockHash;  
            Transactions = _data;  
            Hash = ComputeSha256Hash();
        }

        /// <summary>
        /// This method computes the hash for the block
        /// </summary>
        /// <returns></returns>
        public string ComputeSha256Hash()
        {
            SHA256 sha256 = SHA256.Create();  
  
            byte[] inputString = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousBlockHash ?? ""}-{JsonConvert.SerializeObject(Transactions)}-{Nonce}");
            byte[] outputString = sha256.ComputeHash(inputString);  

            return Convert.ToBase64String(outputString);  
        }

        /// <summary>
        /// This method will handle proof of work 
        /// </summary>
        /// <param name="difficulty"></param>
        public void Mine(int difficulty)  
        {  
            var prefixZeros = new string('0', difficulty);  
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != prefixZeros)  
            {  
                this.Nonce++;  
                this.Hash = this.ComputeSha256Hash();  
            }  
        }  
    }
}