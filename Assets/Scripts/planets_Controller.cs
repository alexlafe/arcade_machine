using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planets_Controller : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Bullet" || other.tag == "Bullet_enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
