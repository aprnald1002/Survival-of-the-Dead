using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IItem
{
    public float powerHealth;
    
    public void Use(GameObject target) {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

        playerHealth.healthSlider.maxValue += powerHealth;
    
        // 사용되었으므로, 자신을 파괴
        Destroy(gameObject);
    }
}
