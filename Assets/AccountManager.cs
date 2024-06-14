using UnityEngine;
using UnityEngine.Networking;
using System.Collections; 
using SimpleJSON;

public class AccountManager : MonoBehaviour
{
    private static AccountManager instance;
    public static AccountManager Instance => instance;
    public Account[] accounts;

    public Account currentAccount;

    private void Awake() {
        if(AccountManager.instance != null) Debug.LogError("Only 1 AccountManager allowed to exist");  
        AccountManager.instance = this;  
    }

    void Start()
    {
        StartCoroutine(GetAccounts());
    }

    IEnumerator GetAccounts()
    {
        string url = "http://localhost:3000/accounts";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success){
                Debug.LogError($"Failed to retrieve data: {request.error}");
            } else {
                string json = request.downloadHandler.text;
                try{
                    JSONArray jsonArray = JSON.Parse(json).AsArray;
                    accounts = new Account[jsonArray.Count];
                    for (int i = 0; i < jsonArray.Count; i++){
                        JSONNode jsonNode = jsonArray[i];
                        this.accounts[i] = new Account
                        {
                            id = jsonNode["id"],
                            User = new User
                            {
                                AccountName = jsonNode["User"]["AccountName"],
                                Password = jsonNode["User"]["Password"],
                                UserName = jsonNode["User"]["UserName"],
                                Age = jsonNode["User"]["Age"]
                            }
                        };
                    }
                } catch (System.Exception e){
                    Debug.LogError($"Failed to deserialize JSON: {e.Message}");
                }
            }
        }
    }

    public bool IsAccountNameTaken(string accountName){
        foreach(Account account in this.accounts){
            if(account.User.AccountName == accountName){
                return true;
            }
        }
        return false;
    }

    public Account GetAccountByAccountName(string accountName){
         foreach(Account account in this.accounts){
            if(account.User.AccountName == accountName){
                return account;
            }
        }
        return null;
    }
}
