using UnityEngine;

public class AttractWeaponScript : MonoBehaviour
{
    public float attractRange = 5f;

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attractRange);
        foreach (Collider hitCollider in hitColliders)
        {
            SupplyBoxScript supplyBox = hitCollider.GetComponent<SupplyBoxScript>();
            if (supplyBox != null)
            {
                supplyBox.Attract(this.gameObject);
            }
        }
    }
}
