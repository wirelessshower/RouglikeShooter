using UnityEngine;
using TMPro;

public class FloatingDamage : MonoBehaviour
{
   [HideInInspector] public float damage;

    [SerializeField] private TextMeshPro textMesh;

    private void Start() {        
        textMesh.text = "-" + damage;
    }

    public void OnAnimationOver() { 
        Destroy(gameObject);
    }
}
