using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int HP = 100;
    public Slider healthBar;
    public Animator animator;

    void Update()
    {
        healthBar.value = HP;
        Debug.Log("Enemy HP updated");
        Debug.Log(HP);
        Debug.Log(healthBar.value);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log(HP);
        if (HP <= 0)
        {   
            animator.SetTrigger("die");
            Debug.Log("Enemy died");
            GetComponent<Collider>().enabled = false;
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {   
            Debug.Log(HP);
            animator.SetTrigger("damage");
            Debug.Log("Damage given");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
           Attack attack = collision.GetComponent<Attack>();
            if (attack != null)
            {
                TakeDamage(attack.damage);
                Destroy(collision.gameObject);
            }
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
