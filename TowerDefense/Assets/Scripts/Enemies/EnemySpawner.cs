using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;

    public void Spawn(float hpScale)
    {
        var go = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        var e = go.GetComponent<Enemy>();
        e.Init(waypoints, hpScale);
    }
}
