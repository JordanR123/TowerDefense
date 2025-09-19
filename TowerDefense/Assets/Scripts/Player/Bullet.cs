using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private float damage;
    private float lifetime;
    private GameObject owner;

    public void Init(GameObject owner, float dmg, float spd, float life)
    {
        this.owner = owner; damage = dmg; speed = spd; lifetime = life;
        Destroy(gameObject, lifetime);
    }

    private void Update() { transform.position += transform.forward * speed * Time.deltaTime; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) return;
        if (other.TryGetComponent<Enemy>(out var e))
        {
            e.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
