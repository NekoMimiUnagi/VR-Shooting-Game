using UnityEngine;

public class VisionBlockController : MonoBehaviour
{
    public GameObject frogPrefab;
    public Transform placementAnchor;
    public float placementDistance = 5f;
    public KeyCode placeKey = KeyCode.Space;
    public KeyCode inventoryKey = KeyCode.I;

    private GameObject currentFrog;
    private bool isPlacingFrog;
    private CharacterController playerController;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            isPlacingFrog = !isPlacingFrog;
            if (isPlacingFrog)
            {
                CreateFrog();
            }
            else
            {
                DestroyFrog();
            }
        }

        if (isPlacingFrog)
        {
            PlaceFrog();
            playerController.enabled = false;
        }
        else
        {
            playerController.enabled = true;
        }
    }

    private void CreateFrog()
    {
        currentFrog = Instantiate(frogPrefab, placementAnchor.position, Quaternion.identity);
        var frogRigidbody = currentFrog.AddComponent<Rigidbody>();
        frogRigidbody.useGravity = false;
        frogRigidbody.isKinematic = true;
        currentFrog.GetComponent<Collider>().enabled = false;
    }

    private void DestroyFrog()
    {
        if (currentFrog != null)
        {
            Destroy(currentFrog);
        }
    }

    private void PlaceFrog()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, placementDistance))
        {
            currentFrog.transform.position = hit.point;
        }
        else
        {
            Vector3 forwardOffset = ray.direction * placementDistance;
            currentFrog.transform.position = placementAnchor.position + forwardOffset;
        }

        if (Input.GetKeyDown(placeKey))
        {
            isPlacingFrog = false;
        }
    }
}
