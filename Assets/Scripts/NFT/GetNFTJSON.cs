using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;




public class GetNFTJSON : MonoBehaviour
{
    private UnityWebRequest request;

    //public IEnumerator ViewAccount(string ref)
    //{

    //    using (request = new UnityWebRequest(ref))
    //    {


    //        yield return request.SendWebRequest();

    //        //Returned result
    //        if (request.result == UnityWebRequest.Result.Success)
    //        {
    //            ViewAccount viewAccount = JsonConvert.DeserializeObject<ViewAccount>(request.downloadHandler.text);

    //        }
    //        else
    //        {
    //            Debug.LogError(string.Format("API post error: {0}", request.error));
    //        }
    //    }
    //}
}
