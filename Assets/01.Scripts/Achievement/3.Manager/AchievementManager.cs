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
        _repository = new AchievementRepository();

        // 저장된 업적 데이터 로드
        AchievementRepository repository = new AchievementRepository();
        List<AchievementSaveData> savedDataList = repository.Load();

        // 빠른 조회를 위한 딕셔너리 구성
        Dictionary<string, AchievementSaveData> savedMap = new Dictionary<string, AchievementSaveData>();
        if (savedDataList != null)
        {
            foreach (var data in savedDataList)
            {
                savedMap[data.ID] = data;
            }
        }

        // 메타데이터를 기반으로 업적 초기화
        foreach (AchievementSO metaData in _metaDatas)
        {
            Achievement achievement;

            if (savedMap.TryGetValue(metaData.Id, out AchievementSaveData saved))
            {
                // 저장된 데이터를 기반으로 업적 복원
                int parsedValue = 0;
                int.TryParse(saved.CurrentValue, out parsedValue); // 안정성 보장

                achievement = new Achievement(
                    metaData.Id,
                    metaData.Name,
                    metaData.Description,
                    metaData.Condition,
                    metaData.GoalValue,
                    metaData.RewardCurrencyType,
                    metaData.RewardAmount,
                    parsedValue,
                    saved.RewardClaimed
                );
            }
            else
            {
                // 저장 데이터가 없는 경우 초기값으로 생성
                achievement = new Achievement(
                    metaData.Id,
                    metaData.Name,
                    metaData.Description,
                    metaData.Condition,
                    metaData.GoalValue,
                    metaData.RewardCurrencyType,
                    metaData.RewardAmount
                );
            }

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
     
        _repository.Save(Achievements);
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
        
        _repository.Save(Achievements);
        OnDataChanged?.Invoke();
    }
    
}