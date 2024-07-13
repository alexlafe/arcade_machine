using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; //для объекта TextMeshPro

public class raketa_Controller : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 115200);
    public string received_message;
    public int val;

    float score;
    int score2;
    int health;
    public TextMeshPro text_score;
    public TextMeshProUGUI text_score_UI;

    public Transform shotpos;
    public GameObject Bullet;

    Rigidbody2D rigidbody2d;

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 5000;
        rigidbody2d = GetComponent<Rigidbody2D>();

        score = 0;
        health = 3;
    }

    void Update()
    {
        score = score + Time.deltaTime * 1.5f;
        score2 = (int)score;
        text_score_UI.text = score2.ToString();

        if (sp.IsOpen)
        {
            try
            {
                received_message = sp.ReadLine();

                if (received_message == "exit")
                {
                    loadscene();
                }
                else if (received_message == "click")
                {
                    Instantiate(Bullet, shotpos.transform.position, transform.rotation);
                }
                else
                {
                    val = Convert.ToInt32(received_message);

                    Vector3 rotate = transform.eulerAngles;
                    rotate.z = val;
                    transform.rotation = Quaternion.Euler(rotate);

                    transform.Translate(0, 5f * Time.deltaTime, 0);
                }
            }
            catch (System.Exception)
            {
                Debug.Log("Unable to open port monitor");
            }
        }
    }

void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet_enemy")
        {
            health--;

            //Debug.Log(health);

            if (health == 0)
            {
                SceneManager.LoadScene("gameOver");
            }
        }
    }

    public void loadscene()
    {
        SceneManager.LoadScene("start");
    }

    public void Change_score_ametyst()
    {
        score = score + 20f;
    }

    public void Change_score_saphire()
    {
        score = score + 50f;
    }

    public void Change_score_enemy_spider()
    {
        score = score + 75f;
    }

    public void Change_score_enemy_butterfly()
    {
        score = score + 120f;
    }

    public void Change_score_enemy_red()
    {
        score = score + 200f;
    }
}