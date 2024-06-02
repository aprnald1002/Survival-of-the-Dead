using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour, IItem
{
    public float powerSpeed;
    
    public void Use(GameObject target) {
        PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();

        playerMovement.moveSpeed += powerSpeed;
        
        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}
