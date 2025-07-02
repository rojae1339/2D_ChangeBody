using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _anim;


    private void Start() { _anim = GetComponentInChildren<Animator>(); }

    public void UpdateMove(float speed) { _anim.SetFloat("Speed", speed); }

    public void TriggerAttack() { _anim.SetTrigger("AttackTrigger"); }

    public void TriggerJump() { _anim.SetTrigger("JumpTrigger"); }

    public void TriggerDead() { _anim.SetTrigger("DeadTrigger"); }

    // 애니메이션 이벤트에서 호출할 수 있는 메서드들
    public void OnAttackComplete()
    {
        // 공격 완료 시 Player에게 알림 (필요시)
    }

    public void OnJumpComplete()
    {
        // 점프 완료 시 Player에게 알림 (필요시)
    }
}