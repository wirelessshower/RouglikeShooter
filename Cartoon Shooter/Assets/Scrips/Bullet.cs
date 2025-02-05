using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float distance;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask whatIsSolid;

    [SerializeField] private GameObject bulletEffect;

    private void Start() {
        Invoke("DestroyBullet", lifetime);
    }

    private void Update() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy")) {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Instantiate(bulletEffect, transform.position, Quaternion.identity);
            DestroyBullet();
        }
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void DestroyBullet() {
        Destroy(gameObject);   
    }
}
