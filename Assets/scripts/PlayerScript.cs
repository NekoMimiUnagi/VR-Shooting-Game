using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject visionBlockPrefab;
    public Transform placementPoint;
    public float movementSpeed = 5f;

    private CharacterController characterController;
    private GameObject currentVisionBlock;
    private bool isPlacingVisionBlock = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Replace "Fire1" with the desired input for placing the Vision Block
        {
            if (!isPlacingVisionBlock)
            {
                PlaceVisionBlock();
            }
            else
            {
                currentVisionBlock.GetComponent<VisionBlockScript>().isPlaced = true;
                currentVisionBlock = null;
                isPlacingVisionBlock = false;
            }
        }

        if (!isPlacingVisionBlock)
        {
            // Replace with your existing player movement logic
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
            characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
        }
    }

    private void PlaceVisionBlock()
    {
        isPlacingVisionBlock = true;
        currentVisionBlock = Instantiate(visionBlockPrefab, placementPoint.position, Quaternion.identity);
    }
}
