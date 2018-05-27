using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Projectile;

    public float SpeedProjectile = 2.5f;
    public float ShootPerSeconds = 0.5f;
    public float Health = 150f;

    private void Update()
    {
        float probability = Time.deltaTime * ShootPerSeconds;

        if (Random.value < probability)
        {
            Fire();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if(projectile)
        {
            Health -= projectile.GetDamage();
            projectile.Hit();
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Fire()
    {
        var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        var projectileRB2d = projectile.GetComponent<Rigidbody2D>();
        projectileRB2d.velocity = Vector3.down * SpeedProjectile;
    }
}