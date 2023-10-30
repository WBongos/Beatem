using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextMeshProUGUI;
  

    private void Start()
    {
        m_TextMeshProUGUI.text = "Rondas: " + GameManager.Instance.TopScore;
    }

    public void Retry() {
        GameManager.Instance.ChangeScene(GameManager.MenuScene);
    }
}
