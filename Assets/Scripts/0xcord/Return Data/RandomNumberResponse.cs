using System;

[Serializable]
public class RandomNumberResponse 
{   
     public Data data;

    [Serializable]
    public class Data
    {
        public bool success;
        public string requestId;
        public string transactionHash;
        public string url;
        public string[] randomNumber;
    }
}
