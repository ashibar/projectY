using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Player player;
    private Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationControl(string action)
    {
        // case 안에 있는 string 값을 입력하시면 Trigger가 실행됩니다.
        switch (action)
        {
            case "Attack": animator.SetTrigger("isAttack"); break;
            case "Move": animator.SetTrigger("isWalk"); break;
            case "None": animator.SetTrigger("isNone"); break;
            
        }
    }
}
