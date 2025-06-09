using System;
using UnityEngine;

public enum ECurrencyType
{
    Gold,
    Diamond,
    
    Count
}

public class Currency
{
    // 도메인 클래스
    // 1. 표현력이 증가한다 -> 화폐의 종류와 값 모두 표현할 수 있다.
    // 2. 무결성이 유지된다. (데이터의 정확성 / 일관성 / 유효성) -> 불가능한 변화를 미리 차단할 수 있다.
    // 3. 데이터와 데이터를 다루는 로직이 뭉쳐있다. -> 응집도가 높다.
    
    // 자기 서술적인 코드가 된다. (기획서에 의거한 코드가 된다.)
    // 도메인(기획서) 변경이 일어날 때 코드에 반영하기 쉽다.
    
    // 재화 도메인
    private ECurrencyType _type;
    public ECurrencyType Type => _type;

    private int _value;
    public int Value => _value;

    public Currency(ECurrencyType type, int value)
    {
        if (value < 0)
        {
            throw new Exception("Currency value cannot be negative.");
        }

        _type = type;
        _value = value;
    }

    public void Add(int value)
    {
        if (value < 0)
        {
            throw new Exception("Added currency value cannot be negative.");
        }

        _value += value;
    }
    
    public bool TryBuy(int value)
    {
        if (value < 0)
        {
            throw new Exception("Subtracted currency value cannot be negative.");
        }

        if (_value < value)
        {
            return false;
        }

        _value -= value;
        return true;
    }
}
