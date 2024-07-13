using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spider_Controller : MonoBehaviour
{
    public int health_spider; //��������
    public GameObject raketa; //������, �� �������� �� �������� ����

    public GameObject bullet_spider; //������, ������� �������� ����
    public Transform shotpos_spider; //�����, �� ������� �� ��������

    float firing_interval; //�������� ��� ��������

    void Update()
    {
        //�������� ��� � ��������
        firing_interval = firing_interval + Time.deltaTime;

        if (firing_interval > 1.5f)
        {
            Instantiate(bullet_spider, shotpos_spider.transform.position, transform.rotation);
            firing_interval = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        raketa_Controller controller = raketa.GetComponent<raketa_Controller>();

        if (other.tag == "Bullet")
        {
            health_spider--;
            Destroy(other.gameObject);

            if (health_spider == 0)
            {
                if (controller != null)
                {
                    //Debug.Log(controller.val);
                    controller.Change_score_enemy_spider();
                }
                Destroy(gameObject);
            }
        }
    }
}
