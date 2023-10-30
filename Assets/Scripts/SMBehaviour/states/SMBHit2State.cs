using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace m17
{
    public class SMBHit2State : SMBComboState
    {
        public override void Init()
        {
            base.Init();
            m_Animator.Play("playerAttack2");
        }

        //Podriem fer alguna cosa, per� a aquest exemple nom�s fem un combo
        protected override void OnComboFailedAction()
        {
            
        }

        protected override void OnComboSuccessAction()
        {
            m_StateMachine.ChangeState<SMBHit1State>();
        }

        protected override void OnEndAction()
        {
            m_StateMachine.ChangeState<SMBIdleState>();
        }
    }
}
