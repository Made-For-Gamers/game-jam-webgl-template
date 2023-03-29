using System.Runtime.InteropServices;

namespace Near
{

    public class Near_API
    {
        //JSLIB plugin - functions to interact the Near JavaScript API
#if UNITY_WEBGL

        [DllImport("__Internal")]
        public static extern void Login(string contractId, string networkId);

        [DllImport("__Internal")]
        public static extern void Logout(string networkId);

        [DllImport("__Internal")]
        public static extern void LoginStatus(string networkId);

        [DllImport("__Internal")]
        public static extern void AccountId(string networkId);

        [DllImport("__Internal")]
        public static extern void AccountBalance(string networkId, string accountId);

        [DllImport("__Internal")]
        public static extern void ContractMethod(string accountId, string contractId, string methodName, string network);


#endif

        public static bool isLoggedIn;
        public static string accountId;

    }
}