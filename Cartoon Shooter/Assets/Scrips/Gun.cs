using UnityEngine;

public class Gun : MonoBehaviour
{
    enum GunType { Default, Enemy}

    [SerializeField] GunType gunType;
    [SerializeField] private float offset;
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float startTimeBtwShots;

    private float TimeBtwShots;
    private Player player;
    private Vector3 difference;
    private float rotZ;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.controlType == Player.ControlType.PC && gunType == GunType.Default) {
            joystick.gameObject.SetActive(false);
        }
    }


    private void Update() {

        if (gunType == GunType.Default) {
            if (player.controlType == Player.ControlType.PC) {
                difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            } else if (player.controlType == Player.ControlType.Android && Mathf.Abs(joystick.Horizontal) > .3f || Mathf.Abs(joystick.Vertical) > .3f) {

                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            }
        } else if (gunType == GunType.Enemy) {
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotZ + offset);

        if (TimeBtwShots <= 0) {
            if (Input.GetMouseButtonDown(0) && player.controlType == Player.ControlType.PC || gunType == GunType.Enemy) {
                Shoot();
            } else if (player.controlType == Player.ControlType.Android) {
                if (joystick.Horizontal != 0 || joystick.Vertical != 0) {
                    Shoot();
                }
            }
        } else {
            TimeBtwShots -= Time.deltaTime;
        }
    }

    public void Shoot() {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        TimeBtwShots = startTimeBtwShots;
    }
}
