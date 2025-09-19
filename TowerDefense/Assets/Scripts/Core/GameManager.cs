using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager I { get; private set; }

    public enum Phase { Build, Wave, GameOver }
    public Phase CurrentPhase { get; private set; } = Phase.Build;

    [Header("Team State")]
    public int teamLives = 20;
    public UnityEvent<int> OnLivesChanged;
    public UnityEvent<Phase> OnPhaseChanged;

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
    }

    public void SetPhase(Phase next)
    {
        CurrentPhase = next;
        OnPhaseChanged?.Invoke(CurrentPhase);
    }

    public void LoseLife(int amount = 1)
    {
        teamLives = Mathf.Max(0, teamLives - amount);
        OnLivesChanged?.Invoke(teamLives);
        if (teamLives <= 0) SetPhase(Phase.GameOver);
    }
}
