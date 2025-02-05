using UnityEngine;

public class Player : MonoBehaviour
{
    enum ControlType{
        PC,
        Android
    }

    [SerializeField] private float speed;
    [SerializeField] private Joystick joystick;
    [SerializeField] private ControlType controlType;
    [SerializeField] private int health; 

    
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool facingRight = true;

    const string IS_RUNNING = "IsRunning";

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (controlType == ControlType.PC)
            joystick.gameObject.SetActive(false);
    }

    private void Update() {
        if (controlType == ControlType.PC)     
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else if(controlType == ControlType.Android)
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        moveVelocity = moveInput.normalized * speed;

        if (moveInput.x == 0)
            animator.SetBool(IS_RUNNING, false);
        else 
            animator.SetBool(IS_RUNNING, true);

        HandleFlipping();
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void HandleFlipping() {
        if (moveInput.x > 0 && !facingRight) {
            Flip();
        } else if (moveInput.x < 0 && facingRight) {
            Flip();
        }
    }


    private void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void ChengeHelth(int helthValue) {
        health += helthValue;
    }
}
