using UnityEngine;

public class UFOController : DestroyableTarget
{
    public float minHeight = 10f;
    public float maxHeight = 25f;
    public float minSpeed = 3f;
    public float maxSpeed = 8f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    public float changeDirectionTime = 2f;

    private Vector3 moveDirection;
    private float currentSpeed;
    private float currentTime;
    private float waitTime;

    private ScoreSystem scoreSystem;

    void Start()
    {
        SetRandomDirection();
        SetRandomSpeed();
        SetRandomWaitTime();

        // Disable gravity and ensure the object has a collider
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();

        float livingTime = Random.Range(livingTimeMin, livingTimeMax);
        Destroy(this.gameObject, livingTime);
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
        moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        moveDirection.y = Mathf.Clamp(moveDirection.y, minHeight / maxHeight, 1f);
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
            hitBullet = collision.gameObject;
            bulletInfo = hitBullet.GetComponent<BulletInfo>().GetBulletInfo();
            Destroy(collision.gameObject);
            Debug.Log("AnimalScript: Bullet hit");
            Destroy(gameObject);
            scoreSystem.UpdatePlayerData(gameObject);
        }
    }
}
