using UnityEngine;
using TMPro;
using System.Collections;

public class UIBridge : MonoBehaviour
{
    public static UIBridge I { get; private set; }

    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text waveText;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        // Wait until CurrencySystem singleton is ready
        if (CurrencySystem.I == null)
        {
            StartCoroutine(WaitForCurrencySystem());
            return;
        }
        HookCurrency();
    }

    private IEnumerator WaitForCurrencySystem()
    {
        while (CurrencySystem.I == null) yield return null;
        HookCurrency();
    }

    private void HookCurrency()
    {
        CurrencySystem.I.OnChanged.AddListener(UpdateCurrency);
        UpdateCurrency(CurrencySystem.I.currency);
        SetWave(0);
    }

    public static void SetWave(int w)
    {
        if (I == null || I.waveText == null) return;
        I.waveText.text = $"Wave: {w}";
    }

    private void UpdateCurrency(int c)
    {
        if (currencyText != null)
            currencyText.text = $"$ {c}";
    }
}
