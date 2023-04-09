using UnityEngine;

public class PanScript : MonoBehaviour
{
    public float initialSpeed = 15f;
    private Vector3 velocity;

    void Start()
    {
        velocity = new Vector3(0, initialSpeed, 0);
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}