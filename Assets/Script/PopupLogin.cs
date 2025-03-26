using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class PopupLogin : MonoBehaviour
{
    public TMP_InputField InputId;
    public TMP_InputField InputPassword;
    public GameObject signupPanel;
    public GameObject loginPanel;
    public GameObject Tirel;

    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath,"UserData.Json");
        
    }
    public void Login()
    {
        if (InputId == null || InputPassword == null)
        {
            Debug.LogError("����־�");
            return;
        }

        string id = InputId.text;
        string password = InputPassword.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            Debug.Log("����־�.");
            return;
        }

        string filePath = Path.Combine(Application.persistentDataPath, id + ".json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            UserData userData = JsonUtility.FromJson<UserData>(json);

            if (userData != null && userData.Password == password)
            {
                GameManager.Instance.userData = userData;
       
                    PopupBank.Instance.Refresh();  

                Tirel.SetActive(true);
                gameObject.SetActive(false); 
            }
            else
            {
                Debug.Log("���̵� ��й�ȣ�� Ʋ�Ƚ��ϴ�.");
            }
        }
        else
        {
            Debug.Log("�ش� ���̵�� ȸ�����Ե� �����Ͱ� �����ϴ�.");
        }
    }



    public void OpenSignUp()
    {
        signupPanel.SetActive(true); 
        loginPanel.SetActive(false); 
    }
}

