using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruirse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerHurtBox")) { 
            Destroy(gameObject);
        }
    }
}
