using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract  void OnEnemyAttack();
    public abstract void OnEnemyAttackExit();
    public abstract void OnEnemyTrack(Transform transform);
    public abstract void OnEnemyTrackExit(Transform transform);


}
