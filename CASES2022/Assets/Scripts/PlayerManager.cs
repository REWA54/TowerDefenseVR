using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float money;
   
    public bool Buy(float price)
    {
        if (price > money)
        {
            return false;
        }
        else
        {
            money -= price;
            return true;
        }
    }
}
