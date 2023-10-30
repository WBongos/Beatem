using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static m17.PJSMB;

namespace m17
{
    [RequireComponent(typeof(MBStateMachine))]
    [RequireComponent(typeof(SMBIdleState))]
    [RequireComponent(typeof(SMBWalkState))]    
    [RequireComponent(typeof(SMBHit1State))]
    [RequireComponent(typeof(SMBHit2State))]
    public class PJSMB : MonoBehaviour
    {
        private MBStateMachine m_StateMachine;

        public enum golpes { Golpe1, Golpe2, opcionculera}


        [SerializeField]
        private InputActionAsset m_InputAsset;
        private InputActionAsset m_Input;
        public InputActionAsset Input => m_Input;
        private InputAction m_MovementAction;
        public InputAction MovementAction => m_MovementAction;

        public int vida;
        public Action m_CambiarGUI;
        
        private void Awake()
        {
            vida = 10000;
            Assert.IsNotNull(m_InputAsset);
            m_StateMachine = GetComponent<MBStateMachine>();

            m_Input = Instantiate(m_InputAsset);
            m_MovementAction = m_Input.FindActionMap("Movement").FindAction("Mover");
            m_Input.FindActionMap("Movement").Enable();
            //m_Input.FindActionMap("Attack").Enable();
        }

        private void Start()
        {
            m_StateMachine.ChangeState<SMBIdleState>();
           
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "EnemyAttack") {
                vida -= collision.gameObject.GetComponent<EnemyAttack>().daño;
                m_CambiarGUI.Invoke();
                if (vida <= 0) { 
                    Destroy(gameObject);
                    GameManager.Instance.ChangeScene(GameManager.GameOverScene);
                }
            }
        }

        public golpes getTipoGolpe()
        {
            if (typeof(SMBHit1State).IsInstanceOfType(m_StateMachine.CurrentState))
                return golpes.Golpe1;
            else if (typeof(SMBHit2State).IsInstanceOfType(m_StateMachine.CurrentState))
                return golpes.Golpe2;
            else
                return golpes.opcionculera;
        }
    }
}
