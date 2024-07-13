using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_red_Controller : MonoBehaviour
{
    public int health_red; //��������
    public GameObject raketa; //������, �� �������� �� �������� ����

    public GameObject bullet_red; //������, ������� �������� ����
    public Transform shotpos_red; //�����, �� ������� �� ��������

    float firing_interval; //�������� ��� ��������

    void Update()
    {
        //�������� ��� � ��������
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
