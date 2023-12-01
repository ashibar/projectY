using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected Stat_Spell_so stat_spell_so;
    [SerializeField] protected Stat_Spell stat_spell;

    [SerializeField] protected Unit owner;
    [SerializeField] protected string target;

    [SerializeField] public string parent_code;

    [SerializeField] public Sprite sprite_back;
    [SerializeField] public Sprite sprite_front;
    [SerializeField] public Sprite sprite_spell;

    // 공통 배당 대리자
    public delegate void InstantiateOneProjectileFunction_delegate();
    public delegate void FunctionWhileCooltime_delegate();
    public delegate void FunctionWhileProjectileDelay_delegate();
    public delegate Quaternion SetAngle_delegate();
    public delegate GameObject InstantiateProjectile_delegate(DelegateParameter para);
    public delegate void TriggerEnterTickFunction_delegate(DelegateParameter para);
    public delegate void TriggerEnterEndFunction_delegate(DelegateParameter para);
    public delegate void TriggerEnterStackProcess_delegate(DelegateParameter para);
    public delegate void ShootingFunction_delegate(DelegateParameter para);
    public delegate void DestroyFunction_delegate(DelegateParameter para);

    // 대리자 저장 변수
    public InstantiateOneProjectileFunction_delegate instantiateOneProjectileFunction;
    public FunctionWhileCooltime_delegate functionWhileCooltime;
    public FunctionWhileProjectileDelay_delegate functionWhileProjectileDelay;
    public SetAngle_delegate setAngle;
    public InstantiateProjectile_delegate instantiateProjectile;
    public TriggerEnterTickFunction_delegate triggerEnterTickFunction;
    public TriggerEnterEndFunction_delegate triggerEnterEndFunction;
    public TriggerEnterStackProcess_delegate triggerEnterStackProcess;
    public ShootingFunction_delegate shootingFunction;
    public DestroyFunction_delegate destroyFunction;

    public virtual void Awake()
    {
        if (stat_spell_so != null)
            stat_spell = new Stat_Spell(stat_spell_so);
    }

    protected virtual void Update()
    {

    }

    // Spell_Core [상속] 내부 함수
    // 상속 시 오버라이딩 하여 고유 기능 개발

    protected virtual void InstantiateOneProjectileFunction()
    {
        
    }    

    protected virtual void FunctionWhileCooltime()
    {

    }

    protected virtual void FunctionWhileProjectileDelay()
    {

    }

    protected virtual Quaternion SetAngle()
    {
        return Quaternion.identity;
    }

    protected virtual GameObject InstantiateProjectile(DelegateParameter para)
    {
        return null;
    }

    // 투사체에 전달할 대리자 함수

    public virtual void TriggerEnterTickFunction(DelegateParameter para)
    {

    }

    public virtual void TriggerEnterEndFunction(DelegateParameter para)
    {
        
    }

    public virtual void TriggerEnterStackProcess(DelegateParameter para)
    {
    }

    public virtual void ShootingFunction(DelegateParameter para)
    {
        
    }

    public virtual void DestroyFunction(DelegateParameter para)
    {

    }

    /// <summary>
    /// 타겟 코어 스펠에 이 스크립트의 delegate를 할당
    /// </summary>
    /// <param name="target">타겟 spell  스크립트, 대개 Spell_Core</param>
    public virtual void AddDelegate(Spell target)
    {
        target.instantiateOneProjectileFunction += InstantiateOneProjectileFunction;
        target.functionWhileCooltime += FunctionWhileCooltime;
        target.functionWhileProjectileDelay += FunctionWhileProjectileDelay;
        target.setAngle += SetAngle;
        target.instantiateProjectile += InstantiateProjectile;
        target.triggerEnterTickFunction += TriggerEnterTickFunction;
        target.triggerEnterEndFunction += TriggerEnterEndFunction;
        target.triggerEnterStackProcess += TriggerEnterStackProcess;
        target.shootingFunction += ShootingFunction;
        target.destroyFunction += DestroyFunction;
    }

    public virtual void ResetDelegate(Spell target)
    {
        target.instantiateOneProjectileFunction = null;
        target.functionWhileCooltime = null;
        target.functionWhileProjectileDelay = null;
        target.setAngle = null;
        target.instantiateProjectile = null;
        target.triggerEnterTickFunction = null;
        target.triggerEnterEndFunction = null;
        target.triggerEnterStackProcess = null;
        target.shootingFunction = null;
        target.destroyFunction = null;
    }

    /// <summary>
    /// 모듈의 주인과 공격할 타겟 태그 설정
    /// </summary>
    /// <param name="owner">모듈의 주인 유닛 컴포넌트</param>
    /// <param name="target">공격할 타겟 태그</param>
    public void SetUnits(Unit owner, string target)
    {
        this.owner = owner;
        this.target = target;
    }

    public string GetCode()
    {
        return stat_spell_so.Spell_Code;
    }

    public string GetName()
    {
        return stat_spell_so.Spell_Name;
    }

    public Stat_Spell_so GetStatSpell()
    {
        return stat_spell_so;
    }
}
