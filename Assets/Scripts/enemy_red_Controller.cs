using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_red_Controller : MonoBehaviour
{
    public int health_red; //здоровье
    public GameObject raketa; //объект, от которого мы получаем урон

    public GameObject bullet_red; //объект, которым стреляет враг
    public Transform shotpos_red; //точка, из которой он стреляет

    float firing_interval; //интервал для стрельбы

    void Update()
    {
        //стреляем раз в интервал
        firing_interval = firing_interval + Time.deltaTime;

        if (firing_interval > 1f)
        {
            Instantiate(bullet_red, shotpos_red.transform.position, transform.rotation);
            firing_interval = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        raketa_Controller controller = raketa.GetComponent<raketa_Controller>();

        if (other.tag == "Bullet")
        {
            health_red--;
            Destroy(other.gameObject);

            if (health_red == 0)
            {
                if (controller != null)
                {
                    //Debug.Log(controller.val);
                    controller.Change_score_enemy_red();
                }
                Destroy(gameObject);
            }
        }
    }
}
