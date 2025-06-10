using UnityEngine;

public class CurrencyDTO
{
    // 계층간 데이터 전송을 위해 도메인 모델 대신 사용되는 오브젝트
    // 도메인 객체를 직접 조작하는것을 막아 데이터 오염을 예방하고 매니저를 통해 데이터를 조작하도록 한다.
    public readonly ECurrencyType Type;
    public readonly int Value;

    public CurrencyDTO(Currency currency)
    {
        Type = currency.Type;
        Value = currency.Value;
    }

    public CurrencyDTO(ECurrencyType type, int value)
    {
        Type = type;
        Value = value;
    }

    // 이정도는 DTO에서 제공해도 됨
    public bool HaveEnough(int value)
    {
        return Value >= value;
    }
}
