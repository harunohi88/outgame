using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : BehaviourSingleton<CurrencyManager>
{
    private Dictionary<ECurrencyType, Currency> _currencies = new Dictionary<ECurrencyType, Currency>();

    public Action OnCurrencyChanged;
    
    private void Awake()
    {
        for (int i = 0; i < (int)ECurrencyType.Count; ++i)
        {
            ECurrencyType type = (ECurrencyType)i;
            Currency currency = new Currency(type, 0);
            _currencies.Add(type, currency);
        }
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    // 유효성 검사는 관리자가 아닌 도메인에서 처리한다.
    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);
        
        OnCurrencyChanged?.Invoke();
    }

    public bool TryBuy(ECurrencyType type, int value)
    {
        bool result = _currencies[type].TryBuy(value);
        OnCurrencyChanged?.Invoke();

        return result;
    }
}
