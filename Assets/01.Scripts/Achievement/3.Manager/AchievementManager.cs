using UnityEngine;
using System.Collections.Generic;
using System;

public class AchievementManager : BehaviourSingleton<AchievementManager>
{
    [SerializeField] private List<AchievementSO> _metaDatas;
    
    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll((x) => new AchievementDTO(x));

    public Action OnDataChanged;

    private AchievementRepository _repository;

    private void Awake()
    {
        Init();
    }
    
    public void Init()
    {
        _achievements = new List<Achievement>();
        
        foreach (AchievementSO metaData in _metaDatas)
        {
            Achievement achievement = new Achievement(
                metaData.Id,
                metaData.Name,
                metaData.Description,
                metaData.Condition,
                metaData.GoalValue,
                metaData.RewardCurrencyType,
                metaData.RewardAmount
                );
            _achievements.Add(achievement);
        }
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach (Achievement achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                achievement.Increase(value);
            }
        }
        
        OnDataChanged?.Invoke();
    }

    public void TryClaimReward(AchievementDTO achievementDto)
    {
        Achievement achievement = _achievements.Find((x) => x.Condition == achievementDto.Condition);

        if (achievement == null) return;

        if (achievement.ClaimReward())
        {
            CurrencyManager.Instance.Add(achievementDto.RewardCurrencyType, achievementDto.RewardAmount);
        }
        OnDataChanged?.Invoke();
    }
    
}