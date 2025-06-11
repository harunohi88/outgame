using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AccountNicknameSpecification : ISpecification<string>
{
    private static readonly Regex NicknameRegex = new Regex(@"^[a-zA-Z가-힣]{2,7}$", RegexOptions.Compiled);
    private static readonly HashSet<string> ForbiddenNicknames = new HashSet<string>
    {
        "바보", "멍청이", "운영자", "김홍일"
    };
    
    public bool IsSatisfiedBy(string value)
    {
        // 닉네임 유효성 검사
        if (string.IsNullOrWhiteSpace(value) || !NicknameRegex.IsMatch(value))
        {
            ErrorMessage = "닉네임은 2~7자의 한글 또는 영문만 사용할 수 있습니다.";
            return false;
        }

        if (ForbiddenNicknames.Contains(value))
        {
            ErrorMessage = "사용할 수 없는 닉네임입니다.";
            return false;
        } 
        
        return true;
    }

    public string ErrorMessage { get; private set; }
}