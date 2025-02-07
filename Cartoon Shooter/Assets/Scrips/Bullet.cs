using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float distance;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask whatIsSolid;

    [SerializeField] private GameObject bulletEffect;
    [SerializeField] private bool EnemyBulett;

    private void Start() {
        Invoke("DestroyBullet", lifetime);
    }

    private void Update() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy")) {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("Player") && EnemyBulett) {
                hitInfo.collider.GetComponent<Player>().ChengeHelth(-damage);
            }
            DestroyBullet();
        }
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void DestroyBullet() {
        Instantiate(bulletEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);   
    }
}
