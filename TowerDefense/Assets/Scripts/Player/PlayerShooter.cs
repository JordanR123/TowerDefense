using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Camera cam;
    public Transform muzzle;
    public GameObject bulletPrefab;
    public float fireRate = 8f;

    private float nextFire;

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Fire();
        }
    }

    private void Fire()
    {
        var go = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        var b = go.GetComponent<Bullet>();
        b.Init(gameObject, 25f, 45f, 2.5f);
    }
}
