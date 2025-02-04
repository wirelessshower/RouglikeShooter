using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Joystick joystick;
    [SerializeField] private ControlType controlType;

    enum ControlType{
        PC,
        Android
    }
    

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (controlType == ControlType.PC)     
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else if(controlType == ControlType.Android)
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

}
