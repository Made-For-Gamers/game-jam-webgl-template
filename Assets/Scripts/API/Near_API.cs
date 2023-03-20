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

    private UnityWebRequest request;
    private Post_ViewAccount account;

    //Base URLs
    static Dictionary<string, string> baseUrl = new Dictionary<string, string>()
    {
        {"mainnet", "https://rpc.mainnet.near.org"},
        {"testnet", "https://rpc.testnet.near.org"},
        {"betanet", "https://rpc.betanet.near.org"},
        {"localnet", "http://localhost:3030"},
    };

    private void Start()
    {
        StartCoroutine(ViewAccount(baseUrl["testnet"]));
    }

    private IEnumerator ViewAccount(string url)
    {
        //Init post request
        account = new Post_ViewAccount();
        byte[] rawData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(account));

        using (request = new UnityWebRequest(url, "POST"))
        {

            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(rawData);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            //Result
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
                             + "Account ID: " + WalletAuthenticate.nearAccountID + "\n"
                              + "All Keys: " + WalletAuthenticate.nearAllKeys + "\n";
            }
            else
            {
                Debug.LogError(string.Format("API post error: {0}", request.error));
            }
        }
    }
}

