using System.Collections;
using UnityEngine;

public class AnimalScript : DestroyableTarget
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float stayDuration = 10f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 5f;
    public float changeDirectionTime = 2f;

    private Vector3 moveDirection;
    private float currentSpeed;
    private float currentTime;
    private float waitTime;

    public AnimalScript() : base(30) { }

    void Start()
    {
        SetTargetHealth(100);
        StartCoroutine(AnimalBehaviour());
    }


    IEnumerator AnimalBehaviour()
    {
        while (true)
        {
            // Appear randomly in the scene
            SetRandomPosition();
            gameObject.SetActive(true);

            // Walk randomly
            StartCoroutine(WalkRandomly());

            // Stay for a certain duration
            yield return new WaitForSeconds(stayDuration);

            // Disappear
            StopCoroutine(WalkRandomly());
            gameObject.SetActive(false);

            // Wait before reappearing
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

    IEnumerator WalkRandomly()
    {
        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime < waitTime)
            {
                yield return null;
                continue;
            }

            transform.position += moveDirection * currentSpeed * Time.deltaTime;

            if (currentTime > changeDirectionTime + waitTime)
            {
                SetRandomDirection();
                SetRandomSpeed();
                SetRandomWaitTime();
                currentTime = 0;
            }

            yield return null;
        }
    }

    private void SetRandomPosition()
    {
        float randomX = Random.Range(-50f, 50f);
        float randomZ = Random.Range(-50f, 50f);
        transform.position = new Vector3(randomX, 0, randomZ);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
