using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationBehaviour : StateMachineBehaviour
{
    private List<MultiLayerCharacterRenderer> _multiLayerRenderers = null;
    private SpriteRenderer _characterSR;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _characterSR = animator.transform.Find("Base").GetComponent<SpriteRenderer>();
        _multiLayerRenderers = new List<MultiLayerCharacterRenderer>();
        for (int i = 0; i < animator.transform.childCount; i++)
        {
           if( animator.transform.GetChild(i).TryGetComponent<MultiLayerCharacterRenderer>(out var renderer))
            {
                _multiLayerRenderers.Add(renderer);
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        char secondLast = _characterSR.sprite.name[^2];
        char last = _characterSR.sprite.name[^1];
        int animIndex = -1;
        if (secondLast == '_')
        {
            animIndex = (int)char.GetNumericValue( last);
            UpdateSprites(animIndex);
            return;
        }
        else
        {
            var concat = string.Concat(secondLast ,last);
            animIndex = int.Parse(concat);
        }
        
            UpdateSprites(animIndex);
    }

    private void UpdateSprites(int animIndex)
    {
        if (_multiLayerRenderers == null) return;
        foreach (var render in _multiLayerRenderers)
        {
            render.UpdateAnimation(_characterSR.flipX,animIndex);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
