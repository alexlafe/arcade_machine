using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_Controller : MonoBehaviour
{
    public float speed;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(0, 10f * Time.deltaTime, 0);
        Destroy(gameObject, 2);
    }
}
