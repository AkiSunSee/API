using System.Collections;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SignupAccount : MonoBehaviour
{
    [SerializeField] private TMP_InputField accountName;
    [SerializeField] private TMP_InputField password;
    public void SignUp(){
        if(this.IsSignUpAble()){
            StartCoroutine(SignUp_Coroutine());
        }
    }

    IEnumerator SignUp_Coroutine(){
        User newUser = new User(){
            AccountName = accountName.text,
            Password = password.text
        };
        Debug.Log(newUser.AccountName + "   "+newUser.Password);
        string url = "http://localhost:3000/accounts";
        JSONNode jsonNode = new JSONObject();
        jsonNode["User"]["AccountName"] = newUser.AccountName;
        jsonNode["User"]["Password"] = newUser.Password;
        jsonNode["User"]["UserName"] = newUser.UserName;
        jsonNode["User"]["Age"] = newUser.Age;
        string jsonData = jsonNode.ToString();

        Debug.Log(jsonData);
        using (UnityWebRequest request = UnityWebRequest.Post(url, jsonData,"application/json"))
        {

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
            }
            else
            {
                Debug.Log("Signup successful!");
            }
        }
    } 

    private bool IsSignUpAble(){
        if(AccountManager.Instance.IsAccountNameTaken(accountName.text)){
            Debug.LogWarning("The username has been taken, try another name");
            return false;
        }
        if(password.text == "" || password.text == null){
            Debug.LogWarning("The password can't be empty");
            return false;
        }
        return true;
    }
}
