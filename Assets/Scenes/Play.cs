using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Play : MonoBehaviour
{
    [Header("移動速度"), Range(0, 1000)]
    public int speed = 50;
    [Header("跳躍高度"), Range(0, 8000)]
    public float jump = 2.5f;
    [Header("血量"), Range(0, 200)]
    public float hp = 100;
    [Header("結束畫面")]
    public GameObject final;
    public string playNeme = "玩家";
    public bool pass = false;
    public bool isGround;
    public GameObject fireball;
    public GameObject shoot;

    public float ShootTime = 0;
    public float Delay = 1;

    public UnityEvent onEat;
    private float hpMax;
    private Rigidbody2D r2D;
    private Animator ani;
    // Start is called before the first frame update
    private void Start()
    {

        r2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Turn(0);
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);

        if (Time.time > ShootTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Instantiate(fireball, shoot.transform.position, transform.rotation);
                ShootTime = Time.time + Delay;
            }
        }
            Jump();
        
    }
    private void FixedUpdate()
    {
        Walk();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        ani.SetBool("jump", false);
        Debug.Log("碰到東西:" + collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "祕寶")
        {
            Destroy(collision.gameObject);
            onEat.Invoke();
        }
        if (collision.tag == "死亡") Dead();

    }

    private void Walk()
    {
        if (r2D.velocity.magnitude < 10)
            r2D.AddForce(new Vector2(speed * Input.GetAxisRaw("Horizontal"), 0));
        ani.SetBool("RUN", Input.GetAxisRaw("Horizontal") != 0);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2D.AddForce(new Vector2(0,jump));
            ani.SetBool("jump", true);
        }
           
    }

    private void Turn(int direction = 0)
    {
        transform.eulerAngles = new Vector3(0, direction, 0);
    }

    public void Damage(float damage)
    {
        hp -= damage;        

        if (hp <= 0) Dead();
    }

    public void Dead()
    {
        final.SetActive(true);
    }
}
