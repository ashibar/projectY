using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ũ��Ʈ �̸� : Unit
/// ����� : �̿��
/// ��� : ���� �ֻ��� ��ü
/// ��� : ����, ����, �������� ���� ����
/// ������Ʈ ���� :
///     - (23.03.24) : ��ũ��Ʈ ����
///     - (24.02.01) : ��� �� �ּ� �߰�
/// </summary>

public class Unit : MonoBehaviour
{
    [SerializeField] public Stat_so stat_so;                // ������ ���� �⺻�� (����X)
    [SerializeField] public Stat stat;                      // ������ ����
    [SerializeField] public Stat stat_processed;            // ���� ����� ��ó���� ����

    public SpellManager spellManager;                       // ������ ������ �����ϴ� �ֻ��� ���
    public BuffManager buffManager;                         // ������ ����/�����/�н��긦 �����ϴ� �ֻ��� ���

    public Vector2 dir_toMove = new Vector2();              // ������ �̵� ���⿡ ���� ������ǥ
    public Vector2 dir_toShoot = new Vector2();             // ������ ���� �߻� ���⿡ ���� ������ǥ
    public Vector2 pos_toShoot = new Vector2();             // ������ ���� �߻� ��ġ�� ���� ������ǥ

    /// <summary>
    /// ���Ἲ �� ���� ��� �ε�
    /// </summary>
    protected virtual void Awake()
    {
        if (stat_so != null)
            stat = new Stat(stat_so);

        spellManager = GetComponentInChildren<SpellManager>(true);
        buffManager = GetComponentInChildren<BuffManager>(true);
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }

    // ���� �ǰ� �� ������ �����ϴ� ȿ��
    // ���Ŀ� ���� �и� ����
    [SerializeField] protected bool isBlinkCooltime;
    [SerializeField] protected float blinkCooltime = 0.1f;
    [SerializeField] protected Color color_origin;

    /// <summary>
    /// <b>�ǰ� �� ������ ���� �����ƾ ���� �Լ�</b>
    /// - ���� ��⿡�� TriggerEnterStackProcess()�� �Ʒ��� �ڵ带 �Է��ϸ� ������.
    /// - collision.gameObject.GetComponent<Unit>().ActiveBlink();
    /// </summary>
    public void ActiveBlink()
    {
        if (!isBlinkCooltime)
            StartCoroutine(Blink_Routine());
    }

    /// <summary>
    /// <b>���� �����ƾ</b>
    /// </summary>
    /// <returns></returns>
    private IEnumerator Blink_Routine()
    {
        // ���������� ������ �ð�
        float end = Time.time + blinkCooltime / 2;
        
        // ���Ἲ �˻�
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            yield break;

        isBlinkCooltime = true;
        
        // ���������� ����
        color_origin = sr.color;
        sr.color = Color.red;

        // ���
        while (Time.time < end)
        {
            yield return null;
        }

        // ���� ������ ����
        sr.color = color_origin;

        // ���� ������ ������ �ð�
        end = Time.time + blinkCooltime / 2;

        //���
        while (Time.time < end)
        {
            yield return null;
        }

        isBlinkCooltime = false;
    }

}
