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
    List<WeaponryData> weapon = new List<WeaponryData>();

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
        weapon.Clear();
        outputArea.text = "Loading...";
        //string uri = "https://my-json-server.typicode.com/typicode/demo/posts";
        string uri = "https://my-json-server.typicode.com/david-wei0628/RepositoryAPI";
        //uri = uri + "/comments?Strength=normal";
        uri = uri + "/UserApi";

        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
                outputArea.text = request.error;
            else
            {
                var WeaponryData_DBtoVar_Data = SequenceData(request.downloadHandler.text);

                outputArea.text = WeaponryData_DBtoVar_Data;
                print(weapon.Count);
                //var weap = JsonConvert.DeserializeObject<WeaponryData>(b);
                //weapon.Add( JsonUtility.FromJson<WeaponryData>(WeaponryData_DBtoVar_Data));
            }
        }

    }

    string SequenceData(string data)
    {
        //data = data.TrimStart('[');
        //data = data.TrimEnd(']');
        //return data;

        var newdata = data.Split(new string[] { "[\n","]"},System.StringSplitOptions.RemoveEmptyEntries);

        var Listdata = newdata[0].Split(new string[] { "    {", "},\n", "}\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        if (Listdata.Length > 2)
        {
            int i = 0;
            var ListJsondata = new List<string>();
            foreach (var item in Listdata)
            {
                //if (item.Length > 10)
                //{
                ListJsondata.Add(item + "}");
                JsondataToList( JsonUtility.FromJson<WeaponryData>(ListJsondata[i]));
                //weapon.Add(new WeaponryData() { Strength = lastVardata.Strength, Weaponry = lastVardata.Weaponry, ATK = lastVardata.ATK, DEF = lastVardata.DEF});
                i++;
                //}
            }
        }
        else
        {
            JsondataToList(JsonUtility.FromJson<WeaponryData>(newdata[0]));
            //weapon.Add(new WeaponryData() { Strength = lastVardata.Strength, Weaponry = lastVardata.Weaponry, ATK = lastVardata.ATK, DEF = lastVardata.DEF });
        }

        return newdata[0];
    }

    void JsondataToList(WeaponryData lastVardata)
    {
        weapon.Add(new WeaponryData() { Strength = lastVardata.Strength, Weaponry = lastVardata.Weaponry, ATK = lastVardata.ATK, DEF = lastVardata.DEF });
    }
}
