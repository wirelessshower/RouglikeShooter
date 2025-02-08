using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public enum ControlType{
        PC,
        Android
    }
    [Header("Controls")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;
    public ControlType controlType;

    [Header("Helth")]
    [SerializeField] private int health;
    [SerializeField] private TextMeshProUGUI helthDisplay;
    [SerializeField] private GameObject potionEffect;

    [Header("Shield")]
    public GameObject shield;
    public Shield shieldTimer;
    [SerializeField] private GameObject shieldEffect;
 
    
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool facingRight = true;

    const string IS_RUNNING = "IsRunning";

    private void Start() {
        shield.SetActive(false);

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

        if (health <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Potion")) {
            ChengeHelth(5);
            Instantiate(potionEffect, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }else if (collision.CompareTag("Shield")) {
            if (!shield.activeInHierarchy) {
                shield.SetActive(true);
                Instantiate(shieldEffect, collision.transform.position, Quaternion.identity);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isCoolDown = true;
                Destroy(collision.gameObject);
            } else { shieldTimer.ResetTimer();  Destroy(collision.gameObject); }

        }
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
        if (!shield.activeInHierarchy || shield.activeInHierarchy && helthValue > 0) {
            health += helthValue;
            helthDisplay.text = $"HP:{health}";
        } else if (shield.activeInHierarchy && helthValue < 0) {
            shieldTimer.ReduceTime(helthValue);
        }
    }
}
