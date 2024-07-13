using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_butterfly_Controller : MonoBehaviour
{
    public int health_butterfly; //здоровье
    public GameObject raketa; //объект, от которого мы получаем урон

    public GameObject bullet_butterfly; //объект, которым стреляет враг
    public Transform shotpos_butterfly; //точка, из которой он стреляет

    float firing_interval; //интервал для стрельбы

    void Update()
    {
        //стреляем раз в интервал
        firing_interval = firing_interval + Time.deltaTime;

        if (firing_interval > 2f)
        {
            Instantiate(bullet_butterfly, shotpos_butterfly.transform.position, transform.rotation);
            firing_interval = 0f;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        raketa_Controller controller = raketa.GetComponent<raketa_Controller>();

        if (other.tag == "Bullet")
        {
            health_butterfly--;
            Destroy(other.gameObject);

            if (health_butterfly == 0)
            {
                if (controller != null)
                {
                    //Debug.Log(controller.val);
                    controller.Change_score_enemy_butterfly();
                }
                Destroy(gameObject);
            }
        }
    }
}
