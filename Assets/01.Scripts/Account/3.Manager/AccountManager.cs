using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AccountManager : BehaviourSingleton<AccountManager>
{
    private Account _myAccount;
    private AccountRepository _accountRepository;
    private const string SALT = "10043420";

    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        _accountRepository = new AccountRepository();
    }
    
    public bool TryLogin(string email, string password)
    {
        AccountSaveData saveData = _accountRepository.Find(email);
        if (saveData == null)
        {
            return false;
        }

        if (CryptoUtil.Verify(password, saveData.Password, SALT))
        {
            _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
            return true;
        }
        return false;
    }

    public bool TryRegister(string email, string nickname, string password)
    {
        if (_accountRepository.Find(email) != null)
        {
            throw new Exception("이미 존재하는 이메일입니다.");
        }
        
        // 비밀번호 규약 확인
        
        string encryptedPassword = CryptoUtil.Encryption(password);
        Account account = new Account(email, nickname, encryptedPassword);
        _accountRepository.Save(new AccountDTO(account));
        
        return true;
    }
}
