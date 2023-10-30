using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class PlayerSO : ScriptableObject
{
    public int vida;
    public int ataque1;
    public int ataque2;
    public float velocidad;
}
