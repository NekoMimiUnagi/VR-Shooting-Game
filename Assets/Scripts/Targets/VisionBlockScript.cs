using UnityEngine;

public class VisionBlockScript : MonoBehaviour
{
    public float placementSpeed = 5f;
    public bool isPlaced = false;

    void Update()
    {
        if (!isPlaced)
        {
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * placementSpeed;
            targetPosition.y = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * placementSpeed);
        }
    }
}
