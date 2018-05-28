using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Projectile;
    public AudioClip AudioDestroy;
    public AudioClip AudioFire;

    public float SpeedProjectile = 2.5f;
    public float ShootPerSeconds = 0.5f;
    public float Health = 150f;
    public int ScoreValue = 150;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>(); // very bad practic ;)
    }

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
                scoreKeeper.ScoreAdd(ScoreValue);
                AudioSource.PlayClipAtPoint(AudioDestroy, gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }

    void Fire()
    {
        var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        var projectileRB2d = projectile.GetComponent<Rigidbody2D>();
        projectileRB2d.velocity = Vector3.down * SpeedProjectile;

        AudioSource.PlayClipAtPoint(AudioFire, gameObject.transform.position);
    }
}