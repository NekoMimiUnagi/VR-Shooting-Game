using UnityEngine;

public class SupplyBoxScript : MonoBehaviour
{
    public float speed = 1f;
    public float attractSpeed = 5f;
    public bool isFalling = true;
    public string attractWeaponTag = "AttractWeapon";

    private Rigidbody rb;
    private GameObject attractedPlayer;
    private bool isAttracted;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (isFalling)
        {
            rb.useGravity = true;
            rb.velocity = new Vector3(0, -speed, 0);
        }
        else
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (isAttracted && attractedPlayer != null)
        {
            rb.useGravity = false;
            transform.position = Vector3.MoveTowards(transform.position, attractedPlayer.transform.position, attractSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isFalling = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }

        if (isAttracted && collision.gameObject == attractedPlayer)
        {
            Destroy(this.gameObject);
        }
    }

    public void Attract(GameObject player)
    {
        if (!isAttracted)
        {
            isAttracted = true;
            attractedPlayer = player;
        }
    }
}
