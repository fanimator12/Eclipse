using UnityEngine;
using UnityEngine.UI;

public class Knight : MonoBehaviour
{
    private int HP = 100;
    public Slider healthBar;
    public Animator animator;

    private void Update()
    {
        healthBar.value = HP;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }
}
