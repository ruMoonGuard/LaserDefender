using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float damage = 100f;

    public float GetDamage() => damage;

    public void Hit() => Destroy(gameObject);
}
