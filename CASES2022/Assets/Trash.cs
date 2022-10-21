using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public LevelManager levelManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Tower")
        {
            return;
        }
        levelManager.Refund(collision.gameObject.GetComponent<Tower>().value);
    }
}
