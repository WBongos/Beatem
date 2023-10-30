using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyTrack : MonoBehaviour
{
    [SerializeField]
    Enemy m_Melee;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            m_Melee.OnEnemyTrack(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_Melee.OnEnemyTrackExit(collision.transform);
        }
    }
}
