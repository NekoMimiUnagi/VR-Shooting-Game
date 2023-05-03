using System.Collections;
using UnityEngine;

public class PanScript : DestroyableTarget
{
    public float initialSpeed = 15f;
    public Vector3 spawnArea;
    public float respawnDelay = 2f;
    private int initialHealth;

    private Vector3 velocity;
    public PanScript() : base(50) // Pass the score value to the base class constructor
    {
    }
    void Start()
    {
        initialHealth = GetTargetHealth();
        SetRandomPosition();
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
            StartCoroutine(RespawnPan());
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void SetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y),
            Random.Range(-spawnArea.z, spawnArea.z)
        );

        transform.position = randomPosition;
    }

    private IEnumerator RespawnPan()
    {
        // Disable the pan and wait for the respawn delay
        gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);

        // Set a new random position and re-enable the pan
        SetRandomPosition();
        SetTargetHealth(initialHealth);
        gameObject.SetActive(true);
    }
}
