using UnityEngine;
using System.Collections;

public class WaveDirector : MonoBehaviour
{
    public EnemySpawner spawner;
    public float buildTime = 10f;
    public float timeBetweenSpawns = 0.6f;
    public int baseEnemyCount = 10;
    public float waveHpMultiplier = 1.15f;

    private int currentWave = 0;

    private void Start() { StartCoroutine(Run()); }

    private IEnumerator Run()
    {
        while (GameManager.I.CurrentPhase != GameManager.Phase.GameOver)
        {
            // Build phase
            GameManager.I.SetPhase(GameManager.Phase.Build);
            yield return new WaitForSeconds(buildTime);

            // Wave phase
            currentWave++;
            UIBridge.SetWave(currentWave);
            GameManager.I.SetPhase(GameManager.Phase.Wave);

            int count = baseEnemyCount + (currentWave - 1) * 2;
            float hpScale = Mathf.Pow(waveHpMultiplier, currentWave - 1);

            for (int i = 0; i < count; i++)
            {
                spawner.Spawn(hpScale);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            // wait until all enemies are cleared or base died
            while (Enemy.InstanceCount > 0 && GameManager.I.CurrentPhase != GameManager.Phase.GameOver)
                yield return null;
        }
    }
}
