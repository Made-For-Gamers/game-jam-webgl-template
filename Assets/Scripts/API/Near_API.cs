using UnityEngine;
using System.Runtime.InteropServices;

namespace Near
{

    public class Near_API
    {
        //JSLIB plugin functions to interact the Near JavaScript API
#if UNITY_WEBGL

        [DllImport("__Internal")]
        public static extern void Login(string contractId, string networkId);

        [DllImport("__Internal")]
        public static extern void Logout(string networkId);

        [DllImport("__Internal")]
        public static extern void LoginStatus(string networkId);

        [DllImport("__Internal")]
        public static extern void AccountId(string networkId);

#endif
        public static bool isLoggedIn;
    }
}
