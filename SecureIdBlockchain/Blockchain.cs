using System;
using System.Collections.Generic;
using SecureIdBlockchain.Params;

namespace SecureIdBlockchain.core
{
    /// <summary>
    /// CORE: SecureID Blockchain
    /// </summary>
    public class Blockchain
    {
        // defines Mining Difficulty 
        public int MiningDifficulty { set; get; } = 1; 

        // a store for pending transactions
        public IList<Transaction> PendingTransactions = new List<Transaction>(); 

        // This list of block objects will become our blockchain
        public IList<Block> SecureIdChain { set;  get; } 

        /// <summary>
        /// This is the constructor for the SecureIdChain
        /// </summary>
        public Blockchain()  
        {  
            InitializeSecureIdChain();  
            AddGenesisBlock();  
        }

        /// <summary>
        /// This method adds the Genesis block to the SecureIdChain
        /// </summary>
        private void AddGenesisBlock()
        {
            SecureIdChain.Add(GenerateGenesisBlock());
        }

        /// <summary>
        /// This method generates a genesis block for the SecureIdchain
        /// </summary>
        /// <returns></returns>
        private Block GenerateGenesisBlock()
        {
            IList<Transaction> list = new List<Transaction>();
            return new Block(null,list ,DateTime.Now);
        }

        /// <summary>
        /// This method initializes the SecureIdChain
        /// </summary>
        public void InitializeSecureIdChain()
        {
            SecureIdChain = new List<Block>();  
        }

        /// <summary>
        /// This method will return the latest block
        /// </summary>
        /// <returns></returns>
        public Block GetLatestBlock()  
        {  
            return SecureIdChain[SecureIdChain.Count - 1];  
        }  
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        public void AddBlock(Block block)  
        {  
            //get the latest block
            Block latestBlock = GetLatestBlock();

            //set the block index
            block.Index = latestBlock.Index + 1;

            //set the previous block hash from the latest blocks hash value
            block.PreviousBlockHash = latestBlock.Hash;  

            //do some proof of work mining and compute a hash 
            block.Mine(this.MiningDifficulty);

            //add the block to the chain 
            SecureIdChain.Add(block);  
        }

        /// <summary>
        /// The method checks if chain is valid
        /// </summary>
        /// <returns></returns>
        public bool IsChainValid()  
        {  
            for (int i = 1; i < SecureIdChain.Count; i++)  
            {  
                Block currentBlock = SecureIdChain[i];  
                Block previousBlock = SecureIdChain[i - 1];  
        
                if (currentBlock.Hash != currentBlock.ComputeSha256Hash())  
                {  
                    return false;  
                }  
        
                if (currentBlock.PreviousBlockHash != previousBlock.Hash)  
                {  
                    return false;  
                }  
            }  
            return true;  
        }

        /// <summary>
        /// This method creates a transaction by adding a transaction to the pending transaction pool
        /// </summary>
        /// <param name="transaction"></param>
        public void CreateTransaction(Transaction transaction)  
        {  
            PendingTransactions.Add(transaction);  
        } 

        /// <summary>
        /// This method processes pending transactions on the SecureIdChain
        /// </summary>
        /// <param name="minerAddress"></param>
        public void ProcessPendingTransactions(string minerAddress)  
        {  
            Block block = new Block(GetLatestBlock().Hash, PendingTransactions, DateTime.Now);  
            AddBlock(block);  
        
            PendingTransactions = new List<Transaction>();  
            CreateTransaction(new Transaction(null, minerAddress, Reward.Amount, null));
        } 

        /// <summary>
        /// This function gets the balance of the users secureId account
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public decimal GetBalance(string address){

            // :TODO
            return 0;
        }
    }
}