// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using Firebase.Database;

// [Serializable]
// public class DataSaver : MonoBehaviour 
// {
//     public User user;
//     public string userID;
//     DatabaseReference dbRef;

//     private void Awake() {
//     dbRef = FirebaseDatabase.DefaultInstance.RootReference();
//     }

//     public void SaveData(){
//         string json =JsonUtility.ToJson(user);
//         dbRef.Child("users").Child(userID).SetRawJsonValueAsync(json);
//     }
// }
