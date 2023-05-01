using UnityEngine;

public class AnimalScript : DestroyableTarget
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float stayDuration = 2f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 5f;
    public float changeDirectionTime = 2f;

    private Vector3 moveDirection;
    private float currentSpeed;
    private float currentTime;
    private float waitTime;
    private int targetScore = 10;
    void Start()
    {
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
