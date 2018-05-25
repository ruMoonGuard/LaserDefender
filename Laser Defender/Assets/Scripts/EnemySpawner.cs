using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public float Widht = 5f;
    public float Height = 5f;
    public float Speed = 3f;

    [SerializeField]
    float xMax, xMin;
    short direction = -1; //-1 - left, 1 - right

    void Start ()
    {
        foreach (Transform child in transform)
        {
            var newEnemyObject = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity);
            newEnemyObject.transform.parent = child;
        }

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xMin = leftmost.x + Widht * 0.5f;
        xMax = rightmost.x - Widht * 0.5f;
    }

    private void Update()
    {
        Vector3 newPosition = new Vector3();
        if (direction == -1)
        {
            newPosition = transform.position + Vector3.left * Speed * Time.deltaTime;
        }
        else if(direction == 1)
        {
            newPosition = transform.position + Vector3.right * Speed * Time.deltaTime;
        }

        float clampX = Mathf.Clamp(newPosition.x, xMin, xMax);
        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);

        if(newPosition.x <= xMin)
        {
            direction = 1;
        }
        else if(newPosition.x >= xMax)
        {
            direction = -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Widht, Height, 0f));
    }
}
