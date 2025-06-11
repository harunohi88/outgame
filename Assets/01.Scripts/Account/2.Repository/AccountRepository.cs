using UnityEngine;

public class AccountRepository
{
    public const string SAVE_PRIFIX = "ACCOUNT_";
    
    public void Save(AccountDTO accountDto)
    {
        AccountSaveData data = new AccountSaveData(accountDto);
        
        string json = JsonUtility.ToJson(data);
        
        PlayerPrefs.SetString(SAVE_PRIFIX + data.Email, json);
    }

    public AccountSaveData Find(string email)
    {
        if (!PlayerPrefs.HasKey(SAVE_PRIFIX + email))
        {
            return null;
        }
     
        return JsonUtility.FromJson<AccountSaveData>(PlayerPrefs.GetString(SAVE_PRIFIX + email));
    }
}

public class AccountSaveData
{
    public string Email;
    public string Nickname;
    public string Password;

    public AccountSaveData(AccountDTO accountDto)
    {
        Email = accountDto.Email;
        Nickname = accountDto.Nickname;
        Password = accountDto.Password;
    }
}