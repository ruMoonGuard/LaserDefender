using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 150f;

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
}