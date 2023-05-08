using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 20;
    private Rigidbody rb;
    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit)
            return;
        else
            targetHit = true;

        if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        rb.isKinematic = true;

        transform.SetParent(collision.transform);
    }
}
