using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using System;
using System.Web;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;
using UnityEngine.UI;

public class WalletAuthenticate : MonoBehaviour
{
    //JSLIB plugin to authenticate with a near wallet using their JavaScript API
#if UNITY_WEBGL 

    [DllImport("__Internal")]
    private static extern void WalletLogin(string contractId, string networkId);

    [DllImport("__Internal")]
    private static extern void RemoveUrlParams();

    [DllImport("__Internal")]
    private static extern void WalletLogout(string networkId);

    [DllImport("__Internal")]
    private static extern void IsLoggedIn(string networkId);

#endif

    [SerializeField] private TextMeshProUGUI txtHeading;
    [SerializeField] private Button btnLogin;
    [SerializeField] private Button btnIsLogin;
    [SerializeField] private TMP_Dropdown ddNetwork;

    private void Start()
    {
        UpdateNetwork();
        ddNetwork.onValueChanged.AddListener(delegate { UpdateNetwork(); });
    }

    public void Login()
    {
        if (!Near_API.isLoggedin)
        {
            btnLogin.GetComponentInChildren<TextMeshProUGUI>().text = "Wait...";
            btnLogin.enabled = false;
            WalletLogin("", PlayerPrefs.GetString("networkId"));
        }
        else
        {
            Logout();
        }
    }

    public void LoggedIn()
    {
        IsLoggedIn(PlayerPrefs.GetString("networkId"));
    }

    public void DisplayLoginStatus(string status)
    {
        btnIsLogin.GetComponentInChildren<TextMeshProUGUI>().text = status;
    }

    private void Logout()
    {
        PlayerPrefs.SetString("nearAccountId", null);
        PlayerPrefs.SetString("nearAllKeys", null);
        Near_API.isLoggedin = false;
        WalletLogout(PlayerPrefs.GetString("networkId"));
        LoginButton();
    }


    //Update the network from the network dropdown
    private void UpdateNetwork()
    {
        PlayerPrefs.SetString("networkId", ddNetwork.options[ddNetwork.value].text);
    }

    //Returning from the near wallet store the accountId and keys, load next scene
    public void OnAuthenticationSuccess(string accountId, string allKeys)
    {
        PlayerPrefs.SetString("nearAccountId", accountId);
        PlayerPrefs.SetString("nearAllKeys", allKeys);
        Near_API.isLoggedin = true;
        SceneManager.LoadScene("NearAccount");
    }

    public void OnAuthenticationFailure(string error)
    {
        txtHeading.text = error;
    }

    void Awake()
    {
        //When returning from the wallet, get the accountId and keys from the url perams
        if (!Near_API.isLoggedin)
        {
            string currentUrl = Application.absoluteURL;
            var uri = new Uri(currentUrl);
            var queryParams = HttpUtility.ParseQueryString(uri.Query);

            if (queryParams["account_id"] != null && queryParams["all_keys"] != null)
            {
                var accountId = queryParams["account_id"];
                var allKeys = queryParams["all_keys"];
                RemoveUrlParams();
                OnAuthenticationSuccess(accountId, allKeys);
            }
        }

        LoginButton();
    }

    private void LoginButton()
    {
        btnLogin.enabled = true;

        if (Near_API.isLoggedin)
        {
            btnLogin.GetComponentInChildren<TextMeshProUGUI>().text = "LOGOUT";
        }
        else
        {
            btnLogin.GetComponentInChildren<TextMeshProUGUI>().text = "LOGIN";
        }
    }
}
