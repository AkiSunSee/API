using System;
[System.Serializable]
public class User
{
    public string AccountName { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public int Age { get; set; }

    public User(){
        this.AccountName = "";
        this.Password ="";
        this.UserName = "";
        this.Age = -1;
    }
}

