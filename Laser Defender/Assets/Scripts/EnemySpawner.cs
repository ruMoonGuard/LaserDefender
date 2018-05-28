using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public float Widht = 5f;
    public float Height = 5f;
    public float Speed = 3f;
    public float SpawnSpeed = 1f;

    [SerializeField]
    float xMax, xMin;
    short direction = -1; //-1 - left, 1 - right

    void Start ()
    {
        SpawnEnemies();

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

        if(IsEmptyFormation())
        {
            SpawnEnemiesOneAtTime();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Widht, Height, 0f));
    }

    // custom private function

    private bool IsEmptyFormation()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount > 0) return false;
        }

        return true;
    }

    private void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            var newEnemyObject = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity);
            newEnemyObject.transform.parent = child;
        }
    }

    private void SpawnEnemiesOneAtTime()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            var newEnemyObject = Instantiate(EnemyPrefab, freePosition.position, Quaternion.identity);
            newEnemyObject.transform.parent = freePosition;
        }

        if (NextFreePosition())
        {
            Invoke("SpawnEnemiesOneAtTime", SpawnSpeed);
        }
    }

    private Transform NextFreePosition()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount == 0) return child;
        }

        return null;
    }
}
