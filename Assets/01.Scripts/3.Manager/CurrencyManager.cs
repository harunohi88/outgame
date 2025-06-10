using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrencyManager : BehaviourSingleton<CurrencyManager>
{
    public Action OnCurrencyChanged;
    
    private Dictionary<ECurrencyType, Currency> _currencies;
    private CurrencyRepository _repository;
    
    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        // 생성
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);

        // 레포지토리(깃허브)
        _repository = new CurrencyRepository();

        List<CurrencyDTO> loadedCurrencies = _repository.Load();
        if (loadedCurrencies == null)
        {
            for (int i = 0; i < (int)ECurrencyType.Count; ++i)
            {
                ECurrencyType type = (ECurrencyType)i;
            
                // 골드, 다이아몬드 등을 0 값으로 생성후 딕셔너리에 삽입
                Currency currency = new Currency(type, 0);
                _currencies.Add(type, currency);
            }
        }
        else
        {
            foreach (CurrencyDTO data in loadedCurrencies)
            {
                Currency currency = new Currency(data.Type, data.Value);
                _currencies.Add(currency.Type, currency);
            }
            for (int i = 0; i < (int)ECurrencyType.Count; ++i)
            {
                ECurrencyType type = (ECurrencyType)i;
                if (!_currencies.ContainsKey(type))
                {
                    _currencies[type] = new Currency(type, 0);
                }
            }
        }
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    private List<CurrencyDTO> ToDtoList()
    {
        return _currencies.Values.Select(x => new CurrencyDTO(x)).ToList();
    }

    // 유효성 검사는 관리자가 아닌 도메인에서 처리한다.
    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);

        _repository.Save(ToDtoList());
        OnCurrencyChanged?.Invoke();
    }

    public bool TryBuy(ECurrencyType type, int value)
    {
        bool result = _currencies[type].TryBuy(value);

        _repository.Save(ToDtoList());
        OnCurrencyChanged?.Invoke();

        return result;
    }
}
