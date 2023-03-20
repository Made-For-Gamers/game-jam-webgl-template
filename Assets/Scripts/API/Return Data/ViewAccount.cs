using System;

[Serializable]
public class ViewAccount 
{
    public string jsonrpc;
    public Result result = new Result();
    public string id;

    public class Result
    {
        public string amount;
        public string block_hash;
        public int block_height;
        public string code_hash;
        public string locked;
        public int storage_paid_at;
        public int storage_usage;
    }
}
