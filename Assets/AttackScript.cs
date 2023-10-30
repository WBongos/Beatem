using m17;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private PJSMB.golpes golpes;
    public PJSMB.golpes golpe { 
        get { return golpes; }
    }
    public float Damage {
        get { return damage; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHurtBox")) { 
            golpes = GetComponentInParent<PJSMB>().getTipoGolpe();  
        }
    }

}
