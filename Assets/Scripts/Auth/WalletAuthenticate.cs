using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Near;

public class WalletAuthenticate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtHeading;
    [SerializeField] private TextMeshProUGUI btnLoginText;
    [SerializeField] private TMP_Dropdown ddNetwork;

    /// <summary>
    /// Once you authenticate with the Near wallet you will be redirected back here.
    /// Near passes 2 perameters in the URL needed for the session (account_id and allKeys)
    /// The user is forwarded to a new scene before the login scene can load.
    /// </summary>


    private void Start()
    {
        //Set the network drop down
        CurrentNetwork();
        ddNetwork.onValueChanged.AddListener(delegate { UpdateNetwork(); });
        LoginStatus();
       
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

    public void Login()
    {
        if (!Near_API.isLoggedIn)
        {
            Near_API.Login("", PlayerPrefs.GetString("networkId"));
        }
        else
        {
            Near_API.Logout(PlayerPrefs.GetString("networkId"));
        }
        LoginStatus();
    }


    public void LoginStatus()
    {
        Near_API.LoginStatus(PlayerPrefs.GetString("networkId"));
    }

    public void AccountId()
    {
        Near_API.AccountId(PlayerPrefs.GetString("networkId"));
    }

    public void ChangeText(string message)
    {
        if (message == "")
        {
            message = "No Account";
        }
        txtHeading.text = message;
    }

    public void ChangeLoginStatus(string status)
    {
        if (status == "true")
        {
            Near_API.isLoggedIn = true;
            btnLoginText.text = "Logout";
        }
        else
        {
            Near_API.isLoggedIn = false;
            btnLoginText.text = "Login";
        }
        ChangeText("Login Status: " + status);
    }
}