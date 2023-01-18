using UnityEngine;

public class RespawnTanksIfMissed : MonoBehaviour
{
    [SerializeField] Transform RespawnTanksPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.position = RespawnTanksPoint.position;
        }
    }
}
