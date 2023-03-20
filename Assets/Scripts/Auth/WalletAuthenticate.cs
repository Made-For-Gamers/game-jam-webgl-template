using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using System;
using System.Web;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;

public class WalletAuthenticate : MonoBehaviour
{

#if UNITY_WEBGL && !UNITY_EDITOR

    [DllImport("__Internal")]
    private static extern void AuthenticateWithNearWallet(string appKey, string contractName, string networkId, string nodeUrl, string walletUrl);
 
    public void Authenticate()
    {
        AuthenticateWithNearWallet("Made-For-Gamers", "", PlayerPrefs.GetString("networkId"), Near_API.nodeUrl, Near_API.walletUrl);
    }

#endif

    [SerializeField] private TextMeshProUGUI txtAccountId;
    [SerializeField] private TMP_Dropdown ddNetwork;

    private void Start()
    {
        UpdateNetwork();
        ddNetwork.onValueChanged.AddListener(delegate { UpdateNetwork(); });
    }   

    private void UpdateNetwork()
    {
        PlayerPrefs.SetString("networkId", ddNetwork.options[ddNetwork.value].text);
        Near_API.nodeUrl = Near_API.baseNodeUrl[PlayerPrefs.GetString("networkId")];
        Near_API.walletUrl = Near_API.baseWalletUrl[PlayerPrefs.GetString("networkId")];
    }

    public void OnAuthenticationSuccess(string accountId, string allKeys)
    {
        PlayerPrefs.SetString("nearAccountId", accountId);
        PlayerPrefs.SetString("nearAllKeys", allKeys);
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
