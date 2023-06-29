using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostMethod : MonoBehaviour
{
    InputField OutputArea;

    private void Start()
    {
        OutputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("POSTButton").GetComponent<Button>().onClick.AddListener(PostData);
    }

    void PostData() => StartCoroutine(PostData_Coroutine());

    IEnumerator PostData_Coroutine()
    {
        OutputArea.text = "Loading...";
        //string uri = "https://my-json-server.typicode.com/typicode/demo/posts";
        string uri = "https://my-json-server.typicode.com/david-wei0628/RepositoryAPI/UserApi";
        //var uri = File.ReadAllText("Assets/DB/db.json");
        WWWForm form = new WWWForm();
        form.AddField("id", 6);

        //form.AddField("Weaponry", "nife");
        //form.AddField("ATK", 15);
        //form.AddField("DEF", 5);
        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
                OutputArea.text = request.error;
            else
                OutputArea.text = request.downloadHandler.text;
        }
    }
}

//class WeaponryData 
//{ 
//    public string Weaponry;
//    public int ATK;
//    public int DEF;
//}
