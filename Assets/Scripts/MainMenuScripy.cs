using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScripy : MonoBehaviour
{
    public void Jugar() {
        GameManager.Instance.ChangeScene(GameManager.PlayScene);
    }
}
