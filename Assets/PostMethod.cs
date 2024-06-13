using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostMethod : MonoBehaviour
{
    public TMP_InputField outputArea; // Kéo và thả Text vào Inspector của Unity để gán

    void Start()
    {
        // Gán hàm PostData_Coroutine() cho sự kiện onClick của Button
        GameObject.Find("PostBtn").GetComponent<Button>().onClick.AddListener(PostData);
    }

    public void PostData(){
        StartCoroutine(PostData_Coroutine());
    }

    IEnumerator PostData_Coroutine(){
        outputArea.text = "Loading...";

        string url = "https://my-json-server.typicode.com/typicode/demo/posts";
        string jsonData = "{\"user1\": {\"account\": \"abc1\", \"pass\": \"123456\"}}";

        using (UnityWebRequest request = UnityWebRequest.Post(url, jsonData,"application/json"))
        {

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                outputArea.text = request.error;
            }
            else
            {
                outputArea.text = "Data posted successfully!";
                Debug.Log("POST request successful!");
            }
        }
    } 
}
