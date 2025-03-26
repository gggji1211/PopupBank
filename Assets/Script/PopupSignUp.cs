using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSignUp : MonoBehaviour
{
    public TMP_InputField inputID;
    public TMP_InputField inputName;
    public TMP_InputField inputPassword;
    public TMP_InputField inputPasswordConfirm;
    public TextMeshProUGUI errorText;
    public Button signUpButton;
    public Button cancelButton;
    public GameObject loginPanel;

    private string GetUserFilePath(string id) => Path.Combine(Application.persistentDataPath, id + ".json");

    public void OnSignUp()
    {
        string id = inputID.text.Trim();
        string name = inputName.text.Trim();
        string password = inputPassword.text;
        string passwordConfirm = inputPasswordConfirm.text;

        // 필수 입력 필드 확인
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
        {
            ShowError("모든 항목을 입력해주세요.");
            return;
        }

        // 비밀번호 확인
        if (password != passwordConfirm)
        {
            Debug.Log("비밀번호가 일치하지 않습니다.");
            return;
        }

        // 중복 ID 확인
        string filePath = GetUserFilePath(id);
        if (File.Exists(filePath))
        {
            Debug.Log("이미 존재하는 ID입니다.");
            return;
        }

        UserData newUser = new UserData(id,password)
        {
            ID = id,
            name = name,
            Password = password,
            cash = 1000000,
            balance = 100000
        };

        string json = JsonUtility.ToJson(newUser, true);
        File.WriteAllText(filePath, json);

        Debug.Log("회원가입 성공: " + id);
        gameObject.SetActive(false);
        loginPanel.SetActive(true);
    }

    private void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }

    public void OnCancel()
    {
        gameObject.SetActive(false);
    }
}
