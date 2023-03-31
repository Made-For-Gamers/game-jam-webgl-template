
public class MFGNFTMeta
{
    public string token_id { get; set; }
    public string owner_id { get; set; }
    public Metadata metadata { get; set; }

    public class Metadata
    {
        public string title { get; set; }
        public string description { get; set; }
        public string media { get; set; }
        public string custom_fields { get; set; }
    }

}
