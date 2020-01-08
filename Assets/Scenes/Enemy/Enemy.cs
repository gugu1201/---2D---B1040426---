﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("傷害"), Range(0, 100)]
    public float damage = 100;
    [Header("檢查地板")]
    public Transform checkPoint;      
        

    private Rigidbody2D r2d;

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;                                
        Gizmos.DrawRay(checkPoint.position, -checkPoint.up * 1.5f);    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Play")
        {
            Track(collision.transform.position);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Playr" && collision.transform.position.y < transform.position.y + 1)
        {
            collision.gameObject.GetComponent<Play>().Damage(damage);
        }
    }

    private void Move()
    {
        
        r2d.AddForce(-transform.right * speed); 
                
        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, -checkPoint.up, 1.5f, 1 << 8);

        if (hit == false)
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }

    private void Track(Vector3 target)
    {
        
        if (target.x < transform.position.x)
        {
            transform.eulerAngles = Vector3.zero; 
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
