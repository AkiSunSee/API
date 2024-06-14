using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;

public class UpdateAccount : MonoBehaviour
{
    [SerializeField] private TMP_InputField accountNameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField userNameInputField;
    [SerializeField] private TMP_InputField ageInputField;

    private Account currentAccount;

    private void Start() {
        currentAccount = AccountManager.Instance.currentAccount;
        this.FilingInputFields();
    }
    public void _UpdateAccount(){
        if(this.IsUpdateAble()){
            StartCoroutine(Update_Coroutine());
        }
    }

    IEnumerator Update_Coroutine(){
        User newUser = new User(){
            AccountName = accountNameInputField.text,
            Password = passwordInputField.text,
            UserName = userNameInputField.text,
            Age = int.Parse(ageInputField.text)
        };
        string url = "http://localhost:3000/accounts/"+currentAccount.id;
        JSONNode jsonNode = new JSONObject();
        jsonNode["User"]["AccountName"] = newUser.AccountName;
        jsonNode["User"]["Password"] = newUser.Password;
        jsonNode["User"]["UserName"] = newUser.UserName;
        jsonNode["User"]["Age"] = newUser.Age;
        string jsonData = jsonNode.ToString();
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = UnityWebRequest.Put(url,jsonBytes))
        {

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
            }
            else
            {
                Debug.Log("Update successful!");
            }
        }
    } 

    private void FilingInputFields(){
        this.accountNameInputField.text = currentAccount.User.AccountName;
        this.passwordInputField.text = currentAccount.User.Password;
        this.userNameInputField.text = currentAccount.User.UserName;
        this.ageInputField.text = currentAccount.User.Age.ToString();
    }

    private bool IsUpdateAble(){
        if(passwordInputField.text == null || passwordInputField.text == ""){
            Debug.LogWarning("The password can't be empty");
            return false;
        }
        if(int.Parse(ageInputField.text)<0){
            Debug.LogWarning("The age can't be negative ");
            return false;
        }
        return true;
    }
}
