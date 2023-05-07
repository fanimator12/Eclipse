using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscusOfLight : MonoBehaviour
{
    public int damage = 20;

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
        if(other.tag == "Knight")
        {
            transform.parent = other.transform;
            other.GetComponent<Knight>().TakeDamage(damage);
        }
    }
}
