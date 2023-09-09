using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour, IEventListener
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<string> states = new List<string>
    {
        "isIdle",
    };

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        SubscribeEvent();
    }

    public void SubscribeEvent()
    {
        EventManager.Instance.AddListener("Set Module Animator Bool", this);
        EventManager.Instance.AddListener("Set Module Animator trigger", this);
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        ExtraParams extraParams = (ExtraParams)param[0];

        string objname = extraParams.Name;
        string value = extraParams.Name2;
        Debug.Log(GetComponentInParent<Unit>().gameObject.name);
        if (GetComponentInParent<Unit>().gameObject.name != objname)
            return;

        switch (event_type)
        {
            case "Set Module Animator Bool":
                SetModuleAnimatorBool(value); break;
            case "Set Module Animator trigger":
                SetModuleAnimatorTrigger(value); break;
        }
    }

    private void SetModuleAnimatorBool(string value)
    {
        SetState_bool(value);
    }

    private void SetModuleAnimatorTrigger(string value)
    {
        SetState_trigger(value);
    }

    private void SetState_bool(string state)
    {
        foreach (string s in states)
        {
            if (string.Equals(state, s))
                animator.SetBool(s, true);
            else
                animator.SetBool(s, false);
        }
    }

    public void SetState_trigger(string state)
    {
        Debug.Log(string.Format("{0}, {1}", GetComponentInParent<Unit>().gameObject.name, state));
        animator.SetTrigger(state);
    }
}
