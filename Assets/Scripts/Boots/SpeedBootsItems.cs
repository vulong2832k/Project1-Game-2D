using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBootsItems : MonoBehaviour
{
    [SerializeField] protected float speedIncreaseAmount = 2f;
    [SerializeField] protected float duration = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TankMovement tankMovement = other.GetComponent<TankMovement>();

            if (tankMovement != null)
            {
               tankMovement.ApplyTemporarySpeedBoost(speedIncreaseAmount, duration);
            }
            Destroy(gameObject);
        }
    }
}   
