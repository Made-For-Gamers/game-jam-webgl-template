using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//0xford - Chainlink VRF
// https://docs.chain.link/vrf/v2/introduction/
// See below link on how to obtain your own API key
// https://www.notion.so/How-to-use-0xCord-s-Chainlink-VRF-API-in-your-unity-game-c5b508a63a094b90b46f5843e45df000

public class Chainlink_API : MonoBehaviour
{
    //UI objects
    [SerializeField] private TextMeshProUGUI txtHeading;
    [SerializeField] private Button BtnCallApi;
    [SerializeField] private string apiKey;

    public void CallVRF()
    {
        StartCoroutine(RequestRandomNumber());
    }


    private IEnumerator RequestRandomNumber()
    {
        BtnCallApi.enabled = false;
        txtHeading.text = "Requesting random number from VRF.  Please wait...";

        string url = "https://0xcord.com/api/vrfv2/requestRandomNumber?network=fuji&numWords=1";
        string authToken = apiKey;

        UnityWebRequest request = UnityWebRequest.Post(url, "");

        request.SetRequestHeader("Authorization", authToken);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            RandomNumberResponse response = JsonConvert.DeserializeObject<RandomNumberResponse>(json);

            if (response != null && response != null && response.data.randomNumber != null && response.data.randomNumber.Length > 0)
            {
                string randomNumber = response.data.randomNumber[0];
                txtHeading.text = "Random number: " + randomNumber;

                Debug.Log("requestId: " + response.data.requestId);
                Debug.Log("transactionHash: " + response.data.transactionHash);
                Debug.Log("url: " + response.data.url);
                Debug.Log("randomNumber: " + randomNumber);
            }
            else
            {
                Debug.Log("Failed to parse response");
            }
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
        BtnCallApi.enabled = true;
    }

    public void NearScene()
    {
        SceneManager.LoadScene("WalletLogin");
    }
}
