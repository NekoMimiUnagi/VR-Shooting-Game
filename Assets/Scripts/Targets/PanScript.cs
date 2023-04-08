using UnityEngine;

public class PanScript : MonoBehaviour
{
    public float initialSpeed = 5f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    void Start()
    {
        velocity = new Vector3(0, initialSpeed, 0);
    }

    void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Destroy(this.gameObject);
        }
    }
}
