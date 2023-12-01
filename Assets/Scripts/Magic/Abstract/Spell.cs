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

    // ���� ��� �븮��
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

    // �븮�� ���� ����
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

    // Spell_Core [���] ���� �Լ�
    // ��� �� �������̵� �Ͽ� ���� ��� ����

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

    // ����ü�� ������ �븮�� �Լ�

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
    /// Ÿ�� �ھ� ���翡 �� ��ũ��Ʈ�� delegate�� �Ҵ�
    /// </summary>
    /// <param name="target">Ÿ�� spell  ��ũ��Ʈ, �밳 Spell_Core</param>
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
    /// ����� ���ΰ� ������ Ÿ�� �±� ����
    /// </summary>
    /// <param name="owner">����� ���� ���� ������Ʈ</param>
    /// <param name="target">������ Ÿ�� �±�</param>
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
