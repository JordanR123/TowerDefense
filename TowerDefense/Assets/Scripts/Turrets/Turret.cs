using UnityEngine;
using System.Linq;

public class Turret : MonoBehaviour
{
    public float range = 8f;
    public float fireRate = 2.5f;
    public Transform muzzle;
    public GameObject bulletPrefab;
    public LayerMask enemyMask;

    private float nextShot;

    private void Update()
    {
        if (GameManager.I.CurrentPhase == GameManager.Phase.GameOver) return;

        if (Time.time >= nextShot)
        {
            var target = FindTarget();
            if (target != null)
            {
                nextShot = Time.time + 1f / fireRate;
                transform.LookAt(target.position);
                Fire(target.position);
            }
        }
    }

    private Transform FindTarget()
    {
        var hits = Physics.OverlapSphere(transform.position, range, enemyMask);
        if (hits.Length == 0) return null;
        return hits.OrderBy(h => (h.transform.position - transform.position).sqrMagnitude).First().transform;
    }

    private void Fire(Vector3 aim)
    {
        var go = Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation((aim - muzzle.position).normalized));
        go.GetComponent<Bullet>().Init(gameObject, 15f, 40f, 2.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
