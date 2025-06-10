using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class CurrencySaveData
{
    public List<CurrencyDTOAdapter> DataList;
}

[Serializable]
public class CurrencyDTOAdapter
{
    public ECurrencyType Type;
    public int Value;

    public CurrencyDTOAdapter() { }

    public CurrencyDTOAdapter(CurrencyDTO dto)
    {
        Type = dto.Type;
        Value = dto.Value;
    }

    public CurrencyDTO ToDTO()
    {
        return new CurrencyDTO(Type, Value);
    }

    public static CurrencyDTOAdapter FromDTO(CurrencyDTO dto)
    {
        return new CurrencyDTOAdapter(dto);
    }
}

public class CurrencyRepository
{
    private const string SAVE_KEY = nameof(CurrencyRepository);

    public void Save(List<CurrencyDTO> datas)
    {
        List<CurrencyDTOAdapter> adapterList = new List<CurrencyDTOAdapter>();
        foreach (var dto in datas)
        {
            adapterList.Add(CurrencyDTOAdapter.FromDTO(dto));
        }

        CurrencySaveData saveData = new CurrencySaveData { DataList = adapterList };
        string json = JsonUtility.ToJson(saveData, true);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<CurrencyDTO> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        CurrencySaveData saveData = JsonUtility.FromJson<CurrencySaveData>(json);

        List<CurrencyDTO> result = new List<CurrencyDTO>();
        foreach (var adapter in saveData.DataList)
        {
            result.Add(adapter.ToDTO());
        }

        return result;
    }
}
