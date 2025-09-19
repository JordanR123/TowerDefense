using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int InstanceCount = 0;

    [Header("Stats")]
    public float baseHP = 60f;
    public float moveSpeed = 3.5f;
    public int bounty = 5;

    private Transform[] path;
    private int index = 0;
    private float hp;

    public void Init(Transform[] waypoints, float hpScale)
    {
        path = waypoints;
        hp = baseHP * hpScale;
        InstanceCount++;
    }

    private void Update()
    {
        if (path == null || path.Length == 0) return;
        Transform target = path[index];
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.2f);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            index++;
            if (index >= path.Length) ReachBase();
        }
    }

    private void ReachBase()
    {
        GameManager.I.LoseLife(1);
        Die(false);
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0f) Die(true);
    }

    private void Die(bool reward)
    {
        if (reward) CurrencySystem.I.Add(bounty);
        InstanceCount = Mathf.Max(0, InstanceCount - 1);
        Destroy(gameObject);
    }

    private void OnDestroy() { InstanceCount = Mathf.Max(0, InstanceCount); }
}
