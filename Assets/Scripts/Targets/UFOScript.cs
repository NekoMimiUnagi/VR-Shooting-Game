using System.Collections;
using UnityEngine;

public class UFOScript : DestroyableTarget
{
    public Vector3 spawnArea;
    public float minSpeed = 10f;
    public float maxSpeed = 30f;
    public float stayDuration = 10f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 5f;
    public float changeDirectionTime = 2f;
    public float respawnDelay = 2f;

    private Vector3 moveDirection;
    private float currentSpeed;
    private float currentTime;
    private float waitTime;
    private int initialHealth;
        public UFOScript() : base(40) // Pass the score value to the base class constructor
    {
    }
    void Start()
    {
        initialHealth = GetTargetHealth();
        SetRandomPosition();
        SetRandomDirection();
        SetRandomSpeed();
        SetRandomWaitTime();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime < waitTime)
        {
            return;
        }

        transform.position += moveDirection * currentSpeed * Time.deltaTime;

        if (currentTime > changeDirectionTime + waitTime)
        {
            SetRandomDirection();
            SetRandomSpeed();
            SetRandomWaitTime();
            currentTime = 0;
        }

        if (GetTargetHealth() <= 0 || currentTime >= stayDuration)
        {
            StartCoroutine(RespawnUFO());
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

    private void SetRandomDirection()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.rotation = Quaternion.LookRotation(moveDirection);
    }

    private void SetRandomSpeed()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void SetRandomWaitTime()
    {
        waitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    private IEnumerator RespawnUFO()
    {
        // Disable the UFO and wait for the respawn delay
        gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);

        // Set a new random position and re-enable the UFO
        SetRandomPosition();
        SetTargetHealth(initialHealth);
        currentTime = 0;
        gameObject.SetActive(true);
    }
}
