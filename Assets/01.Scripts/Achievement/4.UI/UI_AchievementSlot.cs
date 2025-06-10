using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    //public TextMeshProUGUI RewardCountTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI RewardClaimDate;
    public Button RewardClaimButton;

    private AchievementDTO _achievementDto;
    
    public void Refresh(AchievementDTO achievementDto)
    {
        _achievementDto = achievementDto;
        NameTextUI.text = achievementDto.Name;
        DescriptionTextUI.text = achievementDto.Description;
        //RewardCountTextUI.text = achievement.RewardAmount.ToString();
        ProgressSlider.value = (float)achievementDto.CurrentValue / (float)achievementDto.GoalValue;
        RewardClaimButton.interactable = !achievementDto.IsRewardClaimed && achievementDto.CurrentValue >= achievementDto.GoalValue;
    }
    
    public void ClaimReward()
    {
        AchievementManager.Instance.TryClaimReward(_achievementDto);
    }
}
