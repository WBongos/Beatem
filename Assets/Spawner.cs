using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_SpawnPoints;
    [SerializeField]
    private EnemySO[] m_info;
    [SerializeField]
    private GameObject[] m_Enemies;
    [SerializeField]
    private float m_Probability = 0.4f;
    

    public Action m_CambiarGUI;

    private int m_EnemiesDeadCount;
    private int m_EnemiesTobeDead;
    private int m_EnemiesToSpawn;
    private int m_EnemiesSpawned;
    public int m_Ronda;
    void Start()
    {
        InitValues();
        InitRonda();
    }

    private IEnumerator Spawn() {
        while (m_EnemiesSpawned != m_EnemiesToSpawn) {
            if (m_Ronda == 1)
            {
                float random = Random.Range(0.0f, 1.0f);
                GameObject enemy = Instantiate(m_Enemies[0]);
                if (random <= 0.5f)
                {
                    enemy.GetComponent<EnemyMelee>().Info = m_info[0];
                }
                else {
                    enemy.GetComponent<EnemyMelee>().Info = m_info[1];
                }
                enemy.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].transform.position;
                m_EnemiesSpawned++;
            }
            else if (m_Ronda == 2)
            {
                float random = Random.Range(0.0f, 1.0f);
                GameObject enemy = Instantiate(m_Enemies[1]);
                if (random <= 0.5f)
                {
                    enemy.GetComponent<EnemyRango>().Info = m_info[2];
                }
                else
                {
                    enemy.GetComponent<EnemyRango>().Info = m_info[3];
                }
                enemy.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].transform.position;
                m_EnemiesSpawned++;
            }
            else {
                float random = Random.Range(0.0f, 1.0f);
                if (random <= m_Probability) {
                    float rando = Random.Range(0.0f, 1.0f);
                    GameObject enemy = Instantiate(m_Enemies[1]);
                    if (random <= 0.5f)
                    {
                        enemy.GetComponent<EnemyRango>().Info = m_info[2];
                    }
                    else
                    {
                        enemy.GetComponent<EnemyRango>().Info = m_info[3];
                    }
                    enemy.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].transform.position;
                    m_EnemiesSpawned++;
                }
                else {
                    float rando = Random.Range(0.0f, 1.0f);
                    GameObject enemy = Instantiate(m_Enemies[0]);
                    if (random <= 0.5f)
                    {
                        enemy.GetComponent<EnemyMelee>().Info = m_info[0];
                    }
                    else
                    {
                        enemy.GetComponent<EnemyMelee>().Info = m_info[1];
                    }
                    enemy.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].transform.position;
                    m_EnemiesSpawned++;
                }
       
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void InitValues() { 
        m_EnemiesDeadCount = 0;
        m_Ronda = 1;
        m_EnemiesSpawned = 0;
        m_EnemiesToSpawn = 5;
        m_EnemiesTobeDead = m_EnemiesToSpawn;


    }

    private void InitRonda() {
        StartCoroutine(Spawn());
    }

    public void EndRonda() {
        m_EnemiesDeadCount++;
        if (m_EnemiesDeadCount == m_EnemiesTobeDead)
        {
            StartCoroutine(EndRondaCorutine());
        }
        }

    IEnumerator  EndRondaCorutine() {
            StopCoroutine(Spawn());
            m_EnemiesDeadCount = 0;
            m_EnemiesSpawned = 0;
            m_EnemiesToSpawn += 5;
            m_EnemiesTobeDead = m_EnemiesToSpawn;
            m_Ronda++;
            m_CambiarGUI?.Invoke();        
            yield return new WaitForSeconds(5f);
            InitRonda();
        
    }

   

   
    
}
