using System;
using UnityEngine;

public class UIPopupManager : MonoBehaviour
{
    [SerializeField] private GameObject _achievementPopup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PanelToggle(_achievementPopup);
        }
    }

    private static void PanelToggle(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    
}