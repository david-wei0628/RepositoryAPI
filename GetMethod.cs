using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GetMethod : MonoBehaviour
{
    InputField outputArea;
    WeaponryData weapon;

    public class WeaponryData
    {
        public string Strength { get; set; }
        public string Weaponry { get; set; }
        public double ATK { get; set; }
        public double DEF { get; set; }
    }

    private void Start()
    {
        outputArea =GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("GETButton").GetComponent<Button>().onClick.AddListener(GetData);
    }

    void GetData() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine()
    {
        outputArea.text = "Loading...";
        //string uri = "https://my-json-server.typicode.com/typicode/demo/posts";
        string uri = "https://my-json-server.typicode.com/david-wei0628/RepositoryAPI";
        //uri = uri + "/comments";
        uri = uri + "/UserApi?Strength=easy";

        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
                outputArea.text = request.error;
            else
            {
                var WeaponryData_DBtoVar_Data = SequenceData(request.downloadHandler.text);

                outputArea.text = WeaponryData_DBtoVar_Data;
                print(WeaponryData_DBtoVar_Data);
                //var weap = JsonConvert.DeserializeObject<WeaponryData>(b);
                weapon = JsonUtility.FromJson<WeaponryData>(WeaponryData_DBtoVar_Data);
            }
        }

    }

    string SequenceData(string data)
    {
        data = data.TrimStart('[');
        data = data.TrimEnd(']');

        return data;
    }
}
