using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    // Use this for initialization
	void Start ()
    {
        foreach (Transform child in transform)
        {
            var newEnemyObject = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity);
            newEnemyObject.transform.parent = child;
        }
	}
}
