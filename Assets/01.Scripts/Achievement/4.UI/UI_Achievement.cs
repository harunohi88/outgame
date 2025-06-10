using UnityEngine;
using System.Collections.Generic;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField]
    private List<UI_AchievementSlot> _achievementSlots;

    private void Start()
    {
        AchievementManager.Instance.OnDataChanged += Refresh;
        Refresh();
    }
    
    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for (int i = 0; i < _achievementSlots.Count; i++)
        {
            _achievementSlots[i].Refresh(achievements[i]);
        }
    }
}
