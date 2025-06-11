using System.Text.RegularExpressions;
using UnityEngine;

public class AccountEmailSpecification : ISpecification<string>
{
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    
    public string ErrorMessage { get; private set; }
    
    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "Email is empty";
            return false;
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(value, EmailRegex.ToString()))
        {
            ErrorMessage = "Invalid email format";
            return false;
        }

        return true;
    }
}