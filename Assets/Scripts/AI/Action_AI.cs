using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Action_AI : MonoBehaviour
{
    protected Unit unit;
    [SerializeField] private bool isActive = true;

    [SerializeField] protected List<bool> isCooltime = new List<bool>();
    [SerializeField] protected List<bool> isActive_Spell = new List<bool>();

    private CancellationTokenSource cts = new CancellationTokenSource();

    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        unit = GetComponent<Unit>();
    }
    protected virtual void Update()
    {
        if (isActive)
        {
            ai_process();
            ai_Attack_base();
        }
    }
    public virtual void ai_process()
    {
        // ai가 할 행동
    }
    protected virtual void ai_movement(Vector3 targetpos, Vector2 dir)
    {
        
    }

    /// <summary>
    /// 일정시간마다 스펠코어를 활성화 하는 함수.
    /// 
    /// - 사용법 -
    /// 1. 이 함수를 오버라이드 하고 오버라이드 된 Update함수(base함수 아래)에 넣는다.
    /// 2. 인덱스와 매개변수를 넣은 ai_Attack_cooltime 함수를 코어에 등록
    /// 
    /// </summary>
    protected virtual void ai_Attack_base()
    {
        //ai_Attack_cooltime(0, Vector2.zero, Vector2.zero);
    }
    protected async void ai_Attack_cooltime(int id, Vector3 targetpos, Vector2 dir, float duration)
    {
        if (isCooltime.Count <= id)
            while (isCooltime.Count <= id)
                isCooltime.Add(false);
        if (isActive_Spell.Count <= id)
            while (isActive_Spell.Count <= id)
                isActive_Spell.Add(true);

        if (!isActive_Spell[id])
            return;

        if (!isCooltime[id])
        {
            isCooltime[id] = true;
            await ai_Attack_routine(id, targetpos, dir, duration);
            isCooltime[id] = false;
        }
    }
    protected async Task ai_Attack_routine(int id, Vector3 targetpos, Vector2 dir, float duration)
    {
        float end = Time.time + duration * (1 - unit.stat_processed.Cooldown);

        while (Time.time < end && !cts.Token.IsCancellationRequested)
        {
            await Task.Yield();
        }

        ai_Attack_function(id, targetpos, dir);
    }
    protected virtual void ai_Attack_function(int id, Vector3 targetpos, Vector2 dir)
    {
        if (unit.spellManager != null)
            if (unit.spellManager.Get_Core(id) != null)
                unit.spellManager.Get_Core(id).trigger = true;
    }
    protected void OnDestroy()
    {
        cts?.Cancel();
    }
}
