using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fireball : MonoBehaviour
{
    public int speed = 10;
    public float DeadTime = 2.0f;
    public float Ye = 0.0f;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        Ye += Time.deltaTime;
        if (Ye >= DeadTime)
        {
            Ye = 0.0f;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}