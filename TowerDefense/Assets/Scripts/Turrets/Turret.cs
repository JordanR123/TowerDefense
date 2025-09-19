using UnityEngine;
using System.Linq;

public class Turret : MonoBehaviour
{
    [Header("Firing")]
    public float range = 12f;
    public float fireRate = 1.5f;

    [Header("Refs")]
    public Transform muzzle;
    public GameObject bulletPrefab;
    public LayerMask enemyMask; // should include "Enemies" layer

    float nextShot;

    void OnValidate()
    {
        // If you forgot to set the mask, try to auto-fill it
        if (enemyMask.value == 0)
        {
            int enemiesLayer = LayerMask.NameToLayer("Enemies");
            if (enemiesLayer != -1)
                enemyMask = 1 << enemiesLayer;
        }

        if (muzzle == null) Debug.LogWarning("[Turret] Muzzle not assigned.", this);
        if (bulletPrefab == null) Debug.LogWarning("[Turret] Bullet prefab not assigned.", this);
    }

    void Update()
    {
        if (Time.time < nextShot) return;

        var target = FindTarget();
        if (target == null) return;

        nextShot = Time.time + 1f / fireRate;

        // aim & fire
        Vector3 aim = target.position;
        transform.rotation = Quaternion.LookRotation((aim - transform.position).normalized, Vector3.up);

        if (muzzle == null || bulletPrefab == null)
        {
            Debug.LogError("[Turret] Missing Muzzle or Bullet Prefab.", this);
            return;
        }

        var go = Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation((aim - muzzle.position).normalized));
        var b = go.GetComponent<Bullet>();
        if (b == null)
        {
            Debug.LogError("[Turret] Bullet prefab is missing Bullet component.", bulletPrefab);
            Destroy(go);
            return;
        }
        b.Init(gameObject, 15f, 40f, 2.5f);
    }

    Transform FindTarget()
    {
        if (enemyMask.value == 0)
        {
            Debug.LogWarning("[Turret] Enemy mask is 0. Set it to include the 'Enemies' layer.", this);
            return null;
        }

        var hits = Physics.OverlapSphere(transform.position, range, enemyMask);
        if (hits.Length == 0) return null;

        // nearest enemy
        return hits.OrderBy(h => (h.transform.position - transform.position).sqrMagnitude).First().transform;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
