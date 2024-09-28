using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    public int ammoAmount = 30;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TankController tankController = other.GetComponent<TankController>();

            if (tankController != null)
            {
                tankController.AddAmmoFromItemDrop(ammoAmount);
            }
            Destroy(gameObject);
        }
    }
}
    