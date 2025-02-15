using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;

    public enum Direction { 
        Top,
        bottom,
        Left, 
        Right,
        None
    }

    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 1f;

    private void Start() {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();  
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.5f);
    }

    public void Spawn() {
        if (spawned == true) return;

        if (direction == Direction.Top) {
            rand = Random.Range(0, variants.topRooms.Length);
            Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);
        } else if (direction == Direction.bottom) {
            rand = Random.Range(0, variants.bottomRooms.Length);
            Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);
        } else if (direction == Direction.Right) {
            rand = Random.Range(0, variants.rightRooms.Length);
            Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);
        } else if (direction == Direction.Left) {
            rand = Random.Range(0, variants.leftRooms.Length);
            Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);
        }
        spawned = true;
        
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().spawned) {
            Destroy(gameObject);
        }
    }
}
