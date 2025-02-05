using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float startTimeBtwShots;

    private float TimeBtwShots;


    private void Update() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + offset);

        if (TimeBtwShots <= 0) {
            if (Input.GetMouseButtonDown(0)) {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                TimeBtwShots = startTimeBtwShots;
            }
        } else {
            TimeBtwShots -= Time.deltaTime;
        }
    }
}
