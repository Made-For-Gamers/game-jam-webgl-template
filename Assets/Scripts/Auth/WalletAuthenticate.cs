using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using System;
using System.Web;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WalletAuthenticate : MonoBehaviour
{
    //JSLIB plugin functions to interact the Near JavaScript API
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


    /// <summary>
    /// When you authenticate with the Near wallet it will redirect back to your application url and the WebGL application will restart.
    /// We will get the accountId and keys from the url perameters that are returned from Near and store in PlayerPrefs.
    /// We call a JavaScript function to remove the perameters from the URL.
    /// The user is forwarded to a new scene before the login scene can load.
    /// </summary>
    void Awake()
    {
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
        LoginButtonText();
    }

    private void Start()
    {
        //Set the network drop down
        CurrentNetwork();
        ddNetwork.onValueChanged.AddListener(delegate { UpdateNetwork(); });
    }

    //Login to Near wallet
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

    //Logout of Near wallet
    private void Logout()
    {
        PlayerPrefs.SetString("nearAccountId", null);
        PlayerPrefs.SetString("nearAllKeys", null);
        Near_API.isLoggedin = false;
        WalletLogout(PlayerPrefs.GetString("networkId"));
        LoginButtonText();
        IsLoginButtonText("Is Logged In");
    }

    //Check the login status
    public void LoggedInStatus()
    {
        IsLoggedIn(PlayerPrefs.GetString("networkId"));
    }

    //Change the login button text with login status
    private void LoginButtonText()
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

    //Change the IsLoggedIn button text with login boolean status
    public void IsLoginButtonText(string status)
    {
        btnIsLogin.GetComponentInChildren<TextMeshProUGUI>().text = status;
    }

    //Change dropdown selection on start
    private void CurrentNetwork()
    {
        if (PlayerPrefs.GetString("networkId") == "")
        {
            PlayerPrefs.SetString("networkId", ddNetwork.options[ddNetwork.value].text);
        }
        else
        {
            switch (PlayerPrefs.GetString("networkId"))
            {
                case "mainnet":
                    ddNetwork.SetValueWithoutNotify(1);
                    break;
                case "testnet":
                    ddNetwork.SetValueWithoutNotify(0);
                    break;
                case "betanet":
                    ddNetwork.SetValueWithoutNotify(2);
                    break;
            }
        }
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

    //Throw any authentication errors to the header text label
    public void OnAuthenticationFailure(string error)
    {
        txtHeading.text = error;
    }
}