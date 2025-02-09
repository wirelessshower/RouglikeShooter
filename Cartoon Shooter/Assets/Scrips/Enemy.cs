using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject floatingDamage;


    private float stopTime;
    private Player player;
    private Animator anim;

    private float TimeBtwAttack;
    [SerializeField]private float startTimeBtwAttack;

    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private GameObject daethEffect;
    [SerializeField] private int damage;
    [SerializeField] private float StartStopTime;
    [SerializeField] private float normalSpeed;

   
    private void Start() {
        anim = GetComponent<Animator>();
        player = FindFirstObjectByType<Player>();     
    }

    private void Update() {

        if (stopTime <= 0) {
            speed = normalSpeed;
        } else {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (player.transform.position.x > transform.position.x) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
        if (health <= 0) {
            Instantiate(daethEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) {
        stopTime = StartStopTime;        
        health -= damage;
        Vector2 damagePose = new Vector2(transform.position.x, transform.position.y + 2.75f);
        Instantiate(floatingDamage, damagePose, Quaternion.identity);
        floatingDamage.GetComponentInChildren<FloatingDamage>().damage = damage;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (TimeBtwAttack <= 0) {
                anim.SetTrigger("attack");
            } else {
                    TimeBtwAttack -= Time.deltaTime;
            }              
        }
    }

    public void OnEnemyAttack() {        
        player.ChengeHelth(-damage);
        TimeBtwAttack = startTimeBtwAttack;        
    }
}
