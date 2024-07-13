using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubin_Controller : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        raketa_Controller controller = other.GetComponent<raketa_Controller>();

        if (controller != null)
        {
            controller.Change_score_ametyst();
            Destroy(gameObject);
        }
    }
}
