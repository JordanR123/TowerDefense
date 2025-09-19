using UnityEngine;
using UnityEngine.Events;

public class CurrencySystem : MonoBehaviour
{
    public static CurrencySystem I { get; private set; }
    public int currency = 100;
    public UnityEvent<int> OnChanged;

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        OnChanged?.Invoke(currency);
    }

    public bool CanAfford(int cost) => currency >= cost;

    public bool Spend(int cost)
    {
        if (!CanAfford(cost)) return false;
        currency -= cost;
        OnChanged?.Invoke(currency);
        return true;
    }

    public void Add(int amount)
    {
        currency += amount;
        OnChanged?.Invoke(currency);
    }
}
