using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI HealthBuyText;

    private void Start()
    {
        CurrencyManager.Instance.OnCurrencyChanged += Refresh;
        Refresh();
    }
    
    private void Refresh()
    {
        GoldCountText.text = $"Gold: {CurrencyManager.Instance.Get(ECurrencyType.Gold).Value}";
        DiamondCountText.text = $"Diamond: {CurrencyManager.Instance.Get(ECurrencyType.Diamond).Value}";

        HealthBuyText.color =
            CurrencyManager.Instance.Get(ECurrencyType.Gold).HaveEnough(300) ? Color.green : Color.red;
    }

    public void BuyHealth()
    {
        if (!CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, 300)) return;
        
        PlayerCharacterController player = GameObject.FindFirstObjectByType<PlayerCharacterController>();
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.Heal(100);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BuyHealth();
        }
    }
}
