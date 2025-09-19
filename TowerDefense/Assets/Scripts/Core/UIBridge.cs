using UnityEngine;
using TMPro;

public class UIBridge : MonoBehaviour
{
    public static UIBridge I { get; private set; }

    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text waveText;

    private void Awake()
    {
        I = this;
        CurrencySystem.I.OnChanged.AddListener(UpdateCurrency);
        UpdateCurrency(CurrencySystem.I.currency);
        SetWave(0);
    }

    public static void SetWave(int w)
    {
        if (I == null) return;
        I.waveText.text = $"Wave: {w}";
    }

    private void UpdateCurrency(int c)
    {
        currencyText.text = $"$ {c}";
    }
}
