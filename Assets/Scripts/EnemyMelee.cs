using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class EnemyMelee :  Enemy
{

    private enum States { IDLE, RUN, TRACK, ATTACK };
    private States m_CurrentState;

    private float m_StateDeltaTime;
    private Coroutine m_DisparoCoroutine;

    private Transform m_Target;

    private Animator m_Animator;
    private bool Attacking = false;
    private bool Tracking = false;

    private Rigidbody2D m_rb;
    [SerializeField]
    private EnemigoMuertoGameEvent m_Event;
    [SerializeField]
    private GameObject m_TargetGameObject;
    [SerializeField]
    private GameObject m_TargetGameObject2;
    private EnemySO m_info;
    public EnemySO Info
    {
        get { return m_info; } 
        set { m_info = value; }
    }
    public int vida;
    public int daño;
    public float velocidad;
    [SerializeField]
    private GameObject hitbox;
   
    private void Start()
    {
      
       vida = m_info.vida;
       daño = m_info.daño;
        hitbox.GetComponent<EnemyAttack>().daño = daño;
        velocidad = m_info.velocidad;
       m_rb = GetComponent<Rigidbody2D>();
       m_Animator = GetComponent<Animator>();
        ChangeState(States.RUN);
    }

    private void Update()
    {
        UpdateState(m_CurrentState);
    }

    private void ChangeState(States newState)
    {
        if (newState == m_CurrentState)
            return;
        ExitState(m_CurrentState);
        InitState(newState);
    }

    private void InitState(States initState)
    {
        m_CurrentState = initState;
        m_StateDeltaTime = 0;

        switch (m_CurrentState)
        {
            case States.IDLE:
                m_Animator.Play("meleeIdle");
                m_rb.velocity = Vector3.zero;
                break;
            case States.RUN:
                m_Animator.Play("meleeRun");
                break;

            case States.TRACK:
                m_Animator.Play("meleeRun");
                break;
            case States.ATTACK:
                break;
            default:
                break;
        }
    }


    private void UpdateState(States updateState)
    {
        m_StateDeltaTime += Time.deltaTime;
        switch (updateState)
        {
            case States.IDLE:
                break;
            case States.RUN:
                m_rb.velocity = transform.right * velocidad;
                if (transform.position.x > 10)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if(transform.position.x < -10)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            
                if (Tracking)
                    ChangeState(States.TRACK);
                break;
            case States.TRACK:
                 transform.position = Vector2.MoveTowards(transform.position, m_Target.position, velocidad * Time.deltaTime);
                 if (m_Target.position.x > transform.position.x)
                 {
                   transform.rotation = Quaternion.Euler(0, 0, 0);
                 }
                 else
                 {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                 }
                if (!Tracking && Attacking)
                {
                    ChangeState(States.ATTACK);
                }
                else if (!Tracking && !Attacking) {
                    ChangeState(States.RUN);
                }
                break;
            case States.ATTACK:
                StartCoroutine(Attack());
                break;
            default:
                break;
        }
    }

    private IEnumerator Attack() {
        
            m_Animator.Play("meleeAttack");
           
            yield return new WaitForSeconds(1f);
            ChangeState(States.IDLE);

            yield return new WaitForSeconds(3f);
            ChangeState(States.TRACK);
        
 
    }

    private void ExitState(States exitState)
    {
        switch (exitState)
        {
            case States.IDLE:
                break;
            case States.RUN:
                m_rb.velocity = Vector3.zero;
                break;
            case States.TRACK:
             
                break;
            case States.ATTACK:
                StopCoroutine(Attack());
                break;
            default:
                break;
        }
    }


    public override void OnEnemyAttack()
    {
        Attacking = true;
        Tracking = false;
 
    }

    public override void OnEnemyTrack(Transform transform)
    {
        m_Target = transform;
        Tracking = true;
        
    }

    public override void OnEnemyTrackExit(Transform transform)
    {
        m_Target = transform;
        Tracking = false;
    }

    public override void OnEnemyAttackExit()
    {
        Attacking = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerHitbox"))  {
         
            vida -= 3;
            if (vida <= 0) {
                m_Event.Raise();
                Destroy(gameObject);
            }
        }
    }

}
