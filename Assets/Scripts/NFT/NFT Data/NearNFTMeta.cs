using Newtonsoft.Json;

public class NearNFTMeta
{

    public class ApprovedAccountIds
    {
       
    }

    public class ComposeableStats
    {
        public int local_depth { get; set; }
        public int cross_contract_children { get; set; }
    }

    public class Metadata
    {
        public object title { get; set; }
        public object description { get; set; }
        public object media { get; set; }
        public object media_hash { get; set; }
        public int copies { get; set; }
        public object issued_at { get; set; }
        public object expires_at { get; set; }
        public object starts_at { get; set; }
        public object updated_at { get; set; }
        public string extra { get; set; }
        public string reference { get; set; }
        public object reference_hash { get; set; }
    }

    public class Nearcon2Near
    {
        public int numerator { get; set; }
    }

    public class Percentage
    {
        public int numerator { get; set; }
    }

    public class Root
    {
        public string token_id { get; set; }
        public string owner_id { get; set; }
        public ApprovedAccountIds approved_account_ids { get; set; }
        public Metadata metadata { get; set; }
        public Royalty royalty { get; set; }
        public object split_owners { get; set; }
        public string minter { get; set; }
        public object loan { get; set; }
        public ComposeableStats composeable_stats { get; set; }
        public object origin_key { get; set; }
    }

    public class Royalty
    {
        public SplitBetween split_between { get; set; }
        public Percentage percentage { get; set; }
    }

    public class SplitBetween
    {
      
    }

}
