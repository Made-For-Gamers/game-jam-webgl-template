using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

//Get the the Minbase NFT metadata from an NFT stored on arweave.net
public class GetArweaveNFTMeta : MonoBehaviour
{
    private const string ipfsUrl = "https://arweave.net/";

    async static public Task<ArweaveNFTMeta> GetNftData(string urlRef)
    {
        urlRef = ipfsUrl + urlRef;
        using (UnityWebRequest request = UnityWebRequest.Get(urlRef))
        {
            try
            {
                await request.SendWebRequest();
                ArweaveNFTMeta nftData = JsonConvert.DeserializeObject<ArweaveNFTMeta>(request.downloadHandler.text);
                return nftData;
            }
            catch
            {
                Debug.Log("Error: " + urlRef);
                return null;
            }
        }
    }
}
