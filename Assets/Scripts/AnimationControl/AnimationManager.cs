using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Player player;
    private Animator animator;

    private List<string> states = new List<string>()
    {
        "isIdle",
        "isNone",
        "isWalk",
        "isMove",
        "isHit",
        "isGaze",
        "isDead",
    };

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
            case "Attack": SetState("isAttack"); break;
            case "Idle": SetState("isIdle"); break;
            case "None": SetState("isNone"); break;
            case "Walk": SetState("isWalk"); break;
            case "Move": SetState("isMove"); break;
            case "Gaze": SetState("isGaze"); break;
            case "Hit": SetState("isHit"); break;
            case "Dead": SetState("isDead"); break;
        }
    }

    private void SetState(string state)
    {
        foreach (string s in states)
        {
            if (string.Equals(state, s))
                animator.SetBool(s, true);
            else
                animator.SetBool(s, false);
        }
    }
}
