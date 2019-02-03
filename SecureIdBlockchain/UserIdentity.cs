namespace SecureIdBlockchain.core
{
    /// <summary>
    /// This class represents the users infor object for Identity 
    /// </summary>
    public class UserIdentity
    {
        /// The identifier for the user
        public string Label {get; set;}
        
        /// The value for the particular user
        public string Value {get; set;}
        
        /// Whether field is valid on the blockchain
        public bool IsValid {get; set;}
        
        /// Whether the field is owned by the user.
        public bool IsOwner {get; set;}

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <param name="valid"></param>
        /// <param name="owner"></param>
        public UserIdentity(string label, string value, bool valid, bool owner){

            Label = label;
            Value = value;
            IsValid = valid;
            IsOwner = owner;
        }
    }
}