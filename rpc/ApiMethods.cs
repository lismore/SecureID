namespace SecureID.rpc
{
    /// <summary>
    /// Enum for ApiMethods
    /// </summary>
    public enum ApiMethods
    {
        #region account

        account_list,
        account_new,
        account_unlock,
        account_lock,
        account_sendTransaction,
        
        #endregion 

        #region SecureID

        secureid_version,
        secureid_
        #endregion

    }
}