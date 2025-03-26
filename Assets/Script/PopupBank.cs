using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    public static PopupBank Instance;

    public TMP_InputField targetIdInput; 
    public TMP_InputField amountInput;   

    public TMP_InputField InputField;
    public GameObject popupPanel;
    public GameObject popupPanel1;
    public GameObject popupPanel2;
    public GameObject popupPanel3; 
    public GameObject popupPanel4;
    public GameObject popupPanel5;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    void Start()
    {
        Refresh();
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        if (nameText == null)
            nameText = GameObject.Find("NameText").GetComponent<TextMeshProUGUI>(); 
        if (cashText == null)
            cashText = GameObject.Find("CashText").GetComponent<TextMeshProUGUI>(); 
        if (balanceText == null)
            balanceText = GameObject.Find("BalanceText").GetComponent<TextMeshProUGUI>();
    }
   
        public void Refresh()
    {
        if (GameManager.Instance != null && GameManager.Instance.userData != null)
        {
            nameText.text = GameManager.Instance.userData.name;
            cashText.text = string.Format("{0:N0}원", GameManager.Instance.userData.cash);
            balanceText.text = string.Format("{0:N0}원", GameManager.Instance.userData.balance);
        }
    }

    public void Deposit(int amount)
    {
        var userData = GameManager.Instance.userData;
        if (userData.cash >= amount)
        {
            userData.cash -= amount;
            userData.balance += amount;

            Refresh();
            GameManager.Instance.SaveUserData(); 
        }
    }

    public void Withdraw(int amount)
    {
        var userData = GameManager.Instance.userData;
        if (userData.balance >= amount)
        {
            userData.balance -= amount;
            userData.cash += amount;

            Refresh(); 
            GameManager.Instance.SaveUserData();
        }
    }



public void DepositCustomAmount()
    {
        int amount;
        if (int.TryParse(InputField.text, out amount))
        {
            Deposit(amount);
            InputField.text = "";
        }
    }

    public void WithdrawCustomAmount()
    {
        int amount;
        if (int.TryParse(InputField.text, out amount))
        {
            Withdraw(amount);
            InputField.text = "";
        }
    }
    public void TransferMoney()
    {
        GameManager.Instance.LoadAllUsersData(); 
        string targetId = targetIdInput.text;
        string amountText = amountInput.text;

        if (string.IsNullOrEmpty(targetId) || string.IsNullOrEmpty(amountText))
        {
            Debug.Log("송금 대상과 금액을 입력");
            return;
        }

        if (!int.TryParse(amountText, out int amount) || amount <= 0)
        {
            Debug.Log("유효한 금액을 입력");
            return;
        }

        // 송금 대상 ID 확인
        var targetUser = GameManager.Instance.GetUserDataById(targetId);
        if (targetUser == null)
        {
            Debug.Log("Id 없음");
            return;
        }

        var userData = GameManager.Instance.userData;
        if (userData.balance < amount)
        {
            Debug.Log("잔액 부족");
            return;
        }

        // 송금 실행
        userData.balance -= amount;
        targetUser.balance += amount;

        GameManager.Instance.SaveUserData();
        GameManager.Instance.SaveUserData(targetUser);
        Debug.Log("송금 완료");

        targetIdInput.text = "";
        amountInput.text = "";
    }

    public void truePopup()
    {
        popupPanel.SetActive(false);
        popupPanel1.SetActive(true);
        popupPanel3.SetActive(true);
    }
    public void truePopup1()
    {
        popupPanel.SetActive(false);
        popupPanel2.SetActive(true);
        popupPanel3.SetActive(true);
    }
    public void truePopup2()
    {
        popupPanel.SetActive(false);
        popupPanel4.SetActive(true);
    }


    public void falsePopup()
    {
        popupPanel.SetActive(true);
        popupPanel2.SetActive(false);
        popupPanel1.SetActive(false);
        popupPanel3.SetActive(false);
        popupPanel5.SetActive(false);
    }
}
