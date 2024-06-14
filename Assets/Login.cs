using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleJSON;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField accountNameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    AccountManager accountManager;
    [SerializeField] Transform updateUI;
    [SerializeField] Transform loginUI;

    private void Start() {
        accountManager = AccountManager.Instance;    
    }

    public void _Login(){
        if(this.IsLoginAble()){
            Debug.Log("Login success");
            accountManager.currentAccount = accountManager.GetAccountByAccountName(accountNameInputField.text);
            updateUI.gameObject.SetActive(true);
            loginUI.gameObject.SetActive(false);
        }else{
            Debug.Log("Login fail");
        }
    }

    private bool IsLoginAble(){
        Account account = AccountManager.Instance.GetAccountByAccountName(accountNameInputField.text);
        if(account== null){
            return false;
        }
        if(account.User.Password != passwordInputField.text){
            return false;
        }
        return true;
    }
}
