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

        // �ʼ� �Է� �ʵ� Ȯ��
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
        {
            ShowError("��� �׸��� �Է����ּ���.");
            return;
        }

        // ��й�ȣ Ȯ��
        if (password != passwordConfirm)
        {
            Debug.Log("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        // �ߺ� ID Ȯ��
        string filePath = GetUserFilePath(id);
        if (File.Exists(filePath))
        {
            Debug.Log("�̹� �����ϴ� ID�Դϴ�.");
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

        Debug.Log("ȸ������ ����: " + id);
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
