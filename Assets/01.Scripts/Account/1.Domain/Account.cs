using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;


    public Account(string email, string nickname, string password)
    {
        AccountEmailSpecification accountEmailSpecification = new AccountEmailSpecification();
        AccountNicknameSpecification accountNicknameSpecification = new AccountNicknameSpecification();
        
        // 이메일 유효성 검사
        if (!accountEmailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(accountEmailSpecification.ErrorMessage);
        }

        // 닉네임 유효성 검사
        if (accountNicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(accountNicknameSpecification.ErrorMessage);
        }

        // 비밀번호 유효성 검사
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6 || password.Length > 12)
        {
            throw new Exception("비밀번호는 6~12자 사이여야 합니다.");
        }

        // 모든 조건 통과 시 등록
        Email = email;
        Nickname = nickname;
        Password = password;
    }
}