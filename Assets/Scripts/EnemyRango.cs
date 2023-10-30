using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRango : Enemy
{
    [SerializeField]
    private GameObject flecha;
    private enum States { IDLE,RUN, TRACK, ATTACK };
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
    private GameObject rango;
    [SerializeField]
    private GameObject hitbox;
    private void Start()
    {
     
        m_rb = GetComponent<Rigidbody2D>(); 
        vida = m_info.vida;
        daño = m_info.daño;
        velocidad = m_info.velocidad;
        hitbox.GetComponent<EnemyAttack>().daño = daño;
        rango.GetComponent<CircleCollider2D>().radius = m_info.rango;
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
                m_Animator.Play("rangoIdle");
                m_rb.velocity = Vector3.zero;
                break;
            case States.RUN:
                m_Animator.Play("rangoRun");
                break;

            case States.TRACK:
                m_Animator.Play("rangoRun");
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
                else if (transform.position.x < -10)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (Tracking)
                    ChangeState(States.TRACK);
                break;
            case States.TRACK:
                transform.position = Vector2.MoveTowards(transform.position, m_Target.position, 2f * Time.deltaTime);
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
                else if (!Tracking && !Attacking)
                {
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

    private IEnumerator Attack()
    {
      
            m_Animator.Play("rangoAttack");
        yield return new WaitForSeconds(1f);
        ChangeState(States.IDLE);

        yield return new WaitForSeconds(2f);
        ChangeState(States.TRACK);
        Debug.Log(Attacking);
        Debug.Log(Tracking);

    }

    private void ExitState(States exitState)
    {
        switch (exitState)
        {
            case States.IDLE:
                break;
            case States.RUN:
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
    

    public void Shoot() {
        GameObject projectil = Instantiate(flecha);
        projectil.transform.position = transform.position;
        Vector2 direccion = (m_Target.position - transform.position).normalized;
        projectil.transform.up = direccion;
        projectil.GetComponent<Rigidbody2D>().velocity = projectil.transform.up * 3f;


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
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerHitbox"))
        {
            m_Event.Raise();
            Destroy(gameObject);
        }
    }
}
