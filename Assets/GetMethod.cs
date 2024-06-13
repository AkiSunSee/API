using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class GetMethod : MonoBehaviour
{
    TMP_InputField outputArea;

    private void Start() {
        outputArea = GameObject.Find("OutputArea").GetComponent<TMP_InputField>();
        GameObject.Find("GetBtn").GetComponent<Button>().onClick.AddListener(GetData);
    }

    public void GetData(){
        StartCoroutine(GetData_Coroutine());
    }

    IEnumerator GetData_Coroutine(){
        outputArea.text = "Loading...";
        string url = "https://jsonplaceholder.typicode.com/posts/2";
        url += "?random=" + Random.Range(0, 1000); // Thêm tham số ngẫu nhiên để tránh cache
        using(UnityWebRequest request = UnityWebRequest.Get(url)){
            yield return request.SendWebRequest();
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError ){
                outputArea.text = request.error;
            }
            else{
                outputArea.text = request.downloadHandler.text;
            }
        }
    }
}