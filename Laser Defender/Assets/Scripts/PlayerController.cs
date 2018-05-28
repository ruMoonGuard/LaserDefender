using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public float Padding = 1f;
    public float Health = 200f;

    public GameObject LaserPrefab;
    public float SpeedLaser;
    public float RateLaser = 0.2f;

    public AudioClip AudioDestroy;
    public AudioClip AudioFire;

    [SerializeField]
    private float xMax, xMin;
    
	void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x + Padding;
        xMax = rightmost.x - Padding;
    }
	
    void Fire()
    {
        var projectile = Instantiate(LaserPrefab, transform.position, Quaternion.identity);
        var projectileRB2d = projectile.GetComponent<Rigidbody2D>();
        projectileRB2d.velocity = new Vector3(0, SpeedLaser, 0);

        AudioSource.PlayClipAtPoint(AudioFire, gameObject.transform.position);
    }

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, RateLaser);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-Speed * Time.deltaTime, 0f, 0f);
            transform.position += Vector3.left * Speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(Speed * Time.deltaTime, 0f, 0f);
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }

        float clampX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        if(projectile)
        {
            Health -= projectile.GetDamage();
            projectile.Hit();
            if(Health <= 0)
            {
                AudioSource.PlayClipAtPoint(AudioDestroy, gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }
}
