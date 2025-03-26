using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UserData userData;
    private List<UserData> allUsersData = new List<UserData>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllUsersData();  
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Login(string id, string password)
    {
        userData = new UserData(id, password);

        userData.LoadUserData();
    }

    public void SaveUserData()
    {
        userData.SaveUserData();

    }
    public void SaveUserData(UserData data)
    {
        data.SaveUserData();
    }

    public void LoadUserData()
    {
        userData.LoadUserData();
        if (FindObjectOfType<PopupBank>() != null)
        {
            FindObjectOfType<PopupBank>().Refresh();
        }
    }
    public UserData GetUserDataById(string id)
    {
        foreach (var user in allUsersData)
        {
            if (user.ID.Trim().Equals(id.Trim(), StringComparison.OrdinalIgnoreCase)) 
            {
                return user;
            }
        }
        return null; 

    }
    public void LoadAllUsersData()
    {
        allUsersData.Clear(); // 기존 데이터 초기화

        string path = Application.persistentDataPath;
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] files = dir.GetFiles("*.json");

        foreach (FileInfo file in files)
        {
            string json = File.ReadAllText(file.FullName);
            UserData userData = JsonUtility.FromJson<UserData>(json);
            allUsersData.Add(userData);
        }
    }

}



