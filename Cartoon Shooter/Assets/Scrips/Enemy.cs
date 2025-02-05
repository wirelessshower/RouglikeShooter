using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float speed;
    [SerializeField] GameObject daethEffect;

    private void Update() {

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        if (health <= 0) {
            Instantiate(daethEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
    }
}
