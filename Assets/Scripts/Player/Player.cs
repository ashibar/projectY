using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Player
/// ����� : �̿��
/// ��� : �÷��̾� ������Ʈ �ֻ��� ��ũ��Ʈ, stat���� ����
/// ��� :
/// </summary>

public class Player : MonoBehaviour
{
    public Stat_so stat;
    // stat ���� ������ Stat��ũ��Ʈ �������ּ���
    // �� �ʿ��� ������ ����� �ٷ� �ٷ� �ǵ�� ��Ź�帳�ϴ�.
    public MovementManager movementManger;
    public MagicManager magicManager;
    public AnimationManager animationManager;

    public Vector2 dir_move = new Vector2();
    public Vector2 dir_toMouse = new Vector2();

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    
}
