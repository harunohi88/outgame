using System;
using UnityEngine;

public enum EAchievementCondition
{
    GoldCollected,
    DroneKillCount,
    BossKillCount,
    PlayTime,
    Trigger,
}

public class Achievement
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;
    
    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _isRewardClaimed;
    public bool IsRewardClaimed => _isRewardClaimed;

    public Achievement(
        string id,
        string name,
        string description,
        EAchievementCondition condition,
        int goalValue,
        ECurrencyType rewardCurrencyType,
        int rewardAmount,
        int currentValue = 0,
        bool isRewardClaimed = false)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("id cannot be null or empty");
        }
        
        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("Achievement name cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(description))
        {
            throw new Exception("Achievement description cannot be null or empty.");
        }
        
        if (goalValue <= 0)
        {
            throw new Exception("Goal value must be greater than zero.");
        }
        
        if (rewardAmount <= 0)
        {
            throw new Exception("Reward amount must be greater than zero.");
        }

        if (currentValue < 0)
        {
            throw new Exception("Current value must be greater than or equal to zero.");
        }

        ID = id;
        Name = name;
        Description = description;
        Condition = condition;
        GoalValue = goalValue;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
        _currentValue = currentValue;
        _isRewardClaimed = isRewardClaimed;
    }
    
    public void Increase(int value)
    {
        if (value <= 0)
        {
            throw new Exception("Value to increase cannot be negative.");
        }

        _currentValue += value;
        
        // 업적 완료 로직
    }
    
    private bool CanClaimReward()
    {
        return !_isRewardClaimed && _currentValue >= GoalValue;
    }

    public bool ClaimReward()
    {
        if (CanClaimReward())
        {   
            Debug.Log("Reward claimed for achievement: " + Name);
            _isRewardClaimed = true;
            return true;
        }

        return false;
    }
}
