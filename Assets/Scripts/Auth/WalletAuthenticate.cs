using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Near;
using UnityEngine.UI;

public class WalletAuthenticate : MonoBehaviour
{
    //UI objects
    [SerializeField] private TextMeshProUGUI txtHeading;
    [SerializeField] private TextMeshProUGUI btnLoginText;
    [SerializeField] private TMP_Dropdown ddNetwork;
    [SerializeField] private TMP_InputField inputContract;
    [SerializeField] private TMP_InputField inputMethod;
    [SerializeField] private TMP_InputField inputArgs;
    [SerializeField] private Toggle toggleChange;

    /// <summary>
    /// Once authenticated with the Near wallet, the user is redirected back here.
    /// Near passes 2 perameters in the URL needed for the session (account_id and allKeys)
    /// </summary>

    #region Scene Methods

    private void Start()
    {
        //Set the network drop down
        CurrentNetwork();
        LoginStatus();
    }

    private void OnEnable()
    {
        ddNetwork.onValueChanged.AddListener(delegate { UpdateNetwork(); });
    }

    private void OnDisable()
    {
        ddNetwork.onValueChanged.RemoveListener(delegate { UpdateNetwork(); });
    }

    //Update dropdown selection at start
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

    //Update the network from any network dropdown change
    private void UpdateNetwork()
    {
        PlayerPrefs.SetString("networkId", ddNetwork.options[ddNetwork.value].text);
    }

    //Log messages to the heading label
    public void ChangeText(string message)
    {
        if (message == "")
        {
            message = "No Account";
        }
        txtHeading.text = message;
    }

    //Change the login button text and stored isLogged variable with each login/logout action
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

    //Update the stored accountId vriable
    public void UpdateAccountId(string accountId)
    {
        if (accountId == "")
        {
            accountId = "Zero";
        }
        txtHeading.text = accountId;
        Near_API.accountId = accountId;
    }

    //Load the RPC example scene
    public void RPCScene()
    {
        SceneManager.LoadScene("RPC");
    }

    #endregion

    #region API Calls

    //Login to Near Wallet
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

    //Ask Near for the login status
    public void LoginStatus()
    {
        Near_API.LoginStatus(PlayerPrefs.GetString("networkId"));
    }

    //Get the account ID
    public void AccountId()
    {
        Near_API.AccountId(PlayerPrefs.GetString("networkId"));
    }

    //Get the total account balance
    public void AccountBalance()
    {
        Near_API.AccountBalance(PlayerPrefs.GetString("networkId"), Near_API.accountId);
    }

    //Call contract
    public void CallContract()
    {
        Near_API.CallContract(inputContract.text, inputMethod.text, inputArgs.text, Near_API.accountId, PlayerPrefs.GetString("networkId"), toggleChange.isOn);
    }
  
    #endregion

}