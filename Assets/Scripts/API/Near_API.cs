using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class Near_API : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    
    public static string nodeUrl;
    public static string walletUrl;

    private UnityWebRequest request;
    private Post_ViewAccount account;

    //Base URLs
    public static Dictionary<string, string> baseNodeUrl = new Dictionary<string, string>()
    {
        {"mainnet", "https://rpc.mainnet.near.org"},
        {"testnet", "https://rpc.testnet.near.org"},
        {"betanet", "https://rpc.betanet.near.org"},
        {"localnet", "http://localhost:3030"},
    };

    public static Dictionary<string, string> baseWalletUrl = new Dictionary<string, string>()
    {
        {"mainnet", "https://wallet.mainnet.near.org"},
        {"testnet", "https://wallet.testnet.near.org"},
        {"betanet", "https://wallet.betanet.near.org"},
    };

    public static Dictionary<string, string> baseNetworkId = new Dictionary<string, string>()
    {
        {"mainnet", "mainnet"},
        {"testnet", "testnet"},
        {"betanet", "betanet"},
    };

    private void Start()
    {
        StartCoroutine(ViewAccount());
    }

    private IEnumerator ViewAccount()
    {
        //Init Near RPC post request
        account = new Post_ViewAccount();
        account.@params.account_id = PlayerPrefs.GetString("nearAccountId");
        byte[] rawData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(account));

        using (request = new UnityWebRequest(baseNodeUrl[PlayerPrefs.GetString("networkId")], "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(rawData);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            //Returned result
            if (request.result == UnityWebRequest.Result.Success)
            {
                ViewAccount viewAccount = JsonConvert.DeserializeObject<ViewAccount>(request.downloadHandler.text);
                resultText.text = "<< NEAR RPC API >>" + "\n"
                    + "Amount: " + viewAccount.result.amount + "\n"
                     + "Block Hash: " + viewAccount.result.block_hash + "\n"
                      + "Block Height: " + viewAccount.result.block_height + "\n"
                       + "Code Hash: " + viewAccount.result.code_hash + "\n"
                        + "Locked: " + viewAccount.result.locked + "\n"
                         + "Storage Paid At: " + viewAccount.result.storage_paid_at + "\n"
                          + "Storage Usage: " + viewAccount.result.storage_usage + "\n"
                           + "\n"
                            + "<< NEAR JAVASCRIPT API >>" + "\n"
                             + "Account ID: " + PlayerPrefs.GetString("nearAccountId") + "\n"
                              + "All Keys: " + PlayerPrefs.GetString("nearAllKeys") + "\n";
            }
            else
            {
                Debug.LogError(string.Format("API post error: {0}", request.error));
            }
        }
    }
}

