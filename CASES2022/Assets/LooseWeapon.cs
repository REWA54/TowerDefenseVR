using UnityEngine;

public class LooseWeapon : MonoBehaviour
{
    [SerializeField] Transform respawnWeaponPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && !other.GetComponent<Pistol>().isGrabbed)
        {
            Debug.Log("I replace the Weapon to the spawn point");
            other.gameObject.transform.position = respawnWeaponPoint.transform.position;
            other.gameObject.transform.rotation = respawnWeaponPoint.transform.rotation;
        }
    }

}
