using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_Controller : MonoBehaviour
{
    public GameObject raketa;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(raketa.transform.position.x, raketa.transform.position.y, -10f);
    }
}
