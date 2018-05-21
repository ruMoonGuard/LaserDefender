using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public float Padding = 1f;

    [SerializeField]
    float xMax, xMin;
    
	void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x + Padding;
        xMax = rightmost.x - Padding;
    }
	
	void Update ()
    {
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
}
