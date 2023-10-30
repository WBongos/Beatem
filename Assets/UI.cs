using m17;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_Vida;
    [SerializeField]
    private TextMeshProUGUI m_Ronda;
    [SerializeField]
    private Spawner m_Spawner;
    [SerializeField]
    private PJSMB m_Player;
    void Start()
    {
        m_Spawner.m_CambiarGUI += CambiarRonda;
        m_Player.m_CambiarGUI += CambiarVida;
        m_Ronda.text = "Ronda: " + m_Spawner.m_Ronda;
        m_Vida.text = "Vidas: " + m_Player.vida;
    }

    private void CambiarRonda()
    {
        m_Ronda.text = "Ronda: "+ m_Spawner.m_Ronda;
        GameManager.Instance.TopScore++;
    }
    private void CambiarVida()
    {
        m_Vida.text = "Vidas: " + m_Player.vida;
    }
}
