using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

//Get an image from a URL and return as a texture
public class GetNFTImage : MonoBehaviour
{
    async static public Task<Texture> GetImage(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            try
            {
                await webRequest.SendWebRequest();
                Texture texture = DownloadHandlerTexture.GetContent(webRequest);
                return texture;
            }
            catch (System.Exception)
            {
                Debug.Log("Error: " + url);
                return null;
            }
        }
    }
}

