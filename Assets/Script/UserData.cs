using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string name;
    public int cash;
    public int balance;
    public string ID;
    public string Password;

    private string filePath;

    public UserData(string id, string password)
    {
        this.ID = id;
        this.Password = password;
        this.name = id;
        filePath = Path.Combine(Application.persistentDataPath, id + ".json");

        if (File.Exists(filePath))
        {
            LoadUserData();
        }
        else
        {
            this.cash = 50000;
            this.balance = 100000;
            SaveUserData();
        }
    }

    public void SaveUserData()
    {
        if (string.IsNullOrEmpty(ID))
        {
            Debug.LogError(" SaveUserData() 실패: ID가 설정되지 않았습니다.");
            return;
        }

        string path = Application.persistentDataPath + "/" + ID + ".json";
        string json = JsonUtility.ToJson(this, true);

            File.WriteAllText(path, json);
     
    }


    public void LoadUserData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}


