using System;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText;  // 결과 텍스트
    public TMP_InputField EmailInputField;
    public TMP_InputField NicknameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordConfirmInputField;
    public Button ConfirmButton;   // 로그인 or 회원가입 버튼
}

public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject ResisterPanel;

    [Header("로그인")] 
    public UI_InputFields LoginInputFields;
    
    [Header("회원가입")] 
    public UI_InputFields RegisterInputFields;

    private const string PREFIX = "ID_";
    private const string SALT = "10043420";
    
    

    // 게임 시작하면 로그인 켜주고 회원가입은 꺼주고..
    private void Start()
    {
        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);
        
        LoginInputFields.ResultText.text    = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;

        LoginCheck();
    }

    // 회원가입하기 버튼 클릭
    public void OnClickGoToResisterButton()
    {
        LoginPanel.SetActive(false);
        ResisterPanel.SetActive(true);
    }
    
    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);
    }


    // 회원가입
    public void Resister()
    {
        // 1. 아이디 입력을 확인한다.
        string email = RegisterInputFields.EmailInputField.text;
        if (string.IsNullOrEmpty(email))
        {
            RegisterInputFields.ResultText.text = "아이디를 입력해주세요.";
            return;
        }

        // 2. 1차 비밀번호 입력을 확인한다.
        string password = RegisterInputFields.PasswordInputField.text;
        if (string.IsNullOrEmpty(password))
        {
            RegisterInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        // 3. 2차 비밀번호 입력을 확인하고, 1차 비밀번호 입력과 같은지 확인한다.
        string password2 = RegisterInputFields.PasswordConfirmInputField.text;
        if (string.IsNullOrEmpty(password2))
        {
            RegisterInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        if (password != password2)
        {
            RegisterInputFields.ResultText.text = "비밀번혹가 다릅니다.";
            return;
        }

        string nickname = RegisterInputFields.NicknameInputField.text;
        if (AccountManager.Instance.TryRegister(email, nickname, password))
        {
            
        }

        OnClickGoToLoginButton();
    }

    public void Login()
    {
        // 1. 아이디 입력을 확인한다.
        string id = LoginInputFields.EmailInputField.text;
        if (string.IsNullOrEmpty(id))
        {
            LoginInputFields.ResultText.text = "아이디를 입력해주세요.";
            return;
        }
        
        // 2. 비밀번호 입력을 확인한다.
        string password = LoginInputFields.PasswordInputField.text;
        if (string.IsNullOrEmpty(password))
        {
            LoginInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }
        
        // 3. PlayerPrefs.Get을 이용해서 아이디와 비밀번호가 맞는지 확인한다.
        if (!PlayerPrefs.HasKey(PREFIX + id))
        {
            LoginInputFields.ResultText.text = "아이디와 비밀번호를 확인해주세요.";
            return;
        }
        
        string hashedPassword = PlayerPrefs.GetString(PREFIX + id);
        if (hashedPassword != CryptoUtil.Encryption(password + SALT))
        {
            LoginInputFields.ResultText.text = "아이디와 비밀번호를 확인해주세요.";
            return;
        }
        
        // 4. 맞다면 로그인
        Debug.Log("로그인 성공!");
        SceneManager.LoadScene(1);
    }
    
    
    // 아이디와 비밀번호 InputField 값이 바뀌었을 경우에만
    public void LoginCheck()
    {
        string id = LoginInputFields.EmailInputField.text;
        string password = LoginInputFields.PasswordInputField.text;
        
        LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password);
    }
    
}