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
        // case �ȿ� �ִ� string ���� �Է��Ͻø� Trigger�� ����˴ϴ�.
        switch (action)
        {
            case "attack": animator.SetTrigger("isAttack"); break;
            case "move": animator.SetTrigger("isMove"); break;
            case "stop": animator.SetTrigger("isAttack"); break;
        }
    }
}
