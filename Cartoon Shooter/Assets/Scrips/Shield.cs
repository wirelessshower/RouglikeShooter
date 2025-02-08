using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] private  float cooldown;

    public bool isCoolDown;

    private Image shiedlImage;
    private Player player;

    private void Start() {
        shiedlImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isCoolDown = true;
    }

    private void Update() {
        if (isCoolDown) { 
            shiedlImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (shiedlImage.fillAmount <= 0) {
                shiedlImage.fillAmount = 1;
                isCoolDown=false;
                player.shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTimer() {
        shiedlImage.fillAmount = 1;
    }

    public void ReduceTime(int damage) { 
        shiedlImage.fillAmount += damage / 5f;
    }
}
