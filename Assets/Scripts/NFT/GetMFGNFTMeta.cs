using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

//Get the the MFG NFT metadata from an NFT stored on IPFS
public class GetMFGNFTMeta : MonoBehaviour
{
    private const string ipfsUrl = "";

    async static public Task<MFGNFTMeta> GetNftData(string urlRef)
    {
        urlRef = ipfsUrl + urlRef;
        using (UnityWebRequest request = UnityWebRequest.Get(urlRef))
        {
            try
            {
                await request.SendWebRequest();
                MFGNFTMeta nftData = JsonConvert.DeserializeObject<MFGNFTMeta>(request.downloadHandler.text);
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
