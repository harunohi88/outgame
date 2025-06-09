using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        Currency gold = new Currency(ECurrencyType.Gold, 100);
        Currency diamond = new Currency(ECurrencyType.Diamond, 34);
        
        gold.Add(-799);
    }
}
