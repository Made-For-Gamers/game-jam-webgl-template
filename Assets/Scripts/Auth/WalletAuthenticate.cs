using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using System;
using System.Web;
using UnityEngine.SceneManagement;

public class WalletAuthenticate : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR

    [DllImport("__Internal")]
    private static extern void AuthenticateWithNearWallet(string appKey, string contractName);
 
    public void AuthenticateTest()
    {
         AuthenticateWithNearWallet("Made-For-Gamers", "");
    }

#endif

    [SerializeField] private TextMeshProUGUI txtAccountId;
    public static string nearAccountID;
    public static string nearAllKeys;

    public void OnAuthenticationSuccess(string accountId, string allKeys)
    {
        nearAccountID = accountId;
        nearAllKeys = allKeys;
        SceneManager.LoadScene("NearAccount");
    }

    public void OnAuthenticationFailure(string error)
    {
        txtAccountId.text = error;
    }

    void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR

        string currentUrl = Application.absoluteURL;
        var uri = new Uri(currentUrl);
        var queryParams = HttpUtility.ParseQueryString(uri.Query);

        if (queryParams["account_id"] != null && queryParams["all_keys"] != null)
        {
            var accountId = queryParams["account_id"];
            var allKeys = queryParams["all_keys"];
            OnAuthenticationSuccess(accountId, allKeys);
        }        

#endif
    }
}
