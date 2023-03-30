using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class GetNFTTexture : MonoBehaviour
{
    public const string ipfsUrl = "https://arweave.net/";

    async static public Task<Texture> GetImage(string ipfsImage = "xY0ydTNzdmeghJMRAp63tgjS08Cs7AhgGy0bNn06iUE")
    {

        ipfsImage = ipfsUrl + ipfsImage;
        Debug.Log("GetImage: " + ipfsImage);
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(ipfsImage))
        {
            try
            {
                await webRequest.SendWebRequest();
                Texture texture = DownloadHandlerTexture.GetContent(webRequest);
                return texture;
            }
            catch (System.Exception)
            {
                Debug.Log("Error: " + ipfsImage);
                return null;
            }
        }
    }

    private async void Start()
    {
        await GetNFTTexture.GetImage();
    }
}

