using UnityEngine;
using UnityEngine.UI;

public class Knight : MonoBehaviour
{
    private int HP = 100;
    public Slider healthBar;
    public Animator animator;

    void Update()
    {
        healthBar.value = HP;
        Debug.Log("Enemy HP updated");
        Debug.Log(HP);
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            Debug.Log("Enemy died");
        }
        else
        {
            animator.SetTrigger("damage");
            Debug.Log("Damage given");
        }
    }
}
