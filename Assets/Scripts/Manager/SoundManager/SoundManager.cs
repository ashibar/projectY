using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para/><b>��� SoundManager ���</b>
/// <para/>����� : �̿��
/// <para/>��� : �Ҹ� ������ ���� �� ���
/// <para/>��� : 
/// <para/>������Ʈ ���� : 
/// <para/> - (23.09.07) : ���� ����
/// <para/>
/// </summary>

public class SoundManager : MonoBehaviour, IEventListener
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SoundManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<SoundManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    [SerializeField] private GameObject sound_origin;
    [SerializeField] private List<GameObject> soundClone = new List<GameObject>();

    public static List<string> event_code = new List<string>
    {
        "Play Sound",
        "Become Smaller",
        "Become Louder",
    };

    private void Awake()
    {
        var objs = FindObjectsOfType<SoundManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        sound_origin = GetComponentInChildren<SoundModule>(true).gameObject;

        SubscribeEvent();
    }

    public void SubscribeEvent()
    {
        foreach (string code in event_code)
            EventManager.Instance.AddListener(code, this);
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        
        switch (event_type)
        {
            case "Play Sound": // audioclip(����), boolvalue(��������), floatvalue(���� ���, 0~1), name(���� �̸�)
                PlaySound((ExtraParams)param[0]); break;
            case "Become Smaller":
                BecomeSmaller((ExtraParams)param[0]); break;
            case "Become Louder":
                BecomeLounder((ExtraParams)param[0]); break;
        }
    }

    private void PlaySound(ExtraParams par)
    {
        GameObject clone = Instantiate(sound_origin, transform);
        clone.GetComponent<AudioSource>().clip = par.Audioclip;
        clone.GetComponent<AudioSource>().Play();
        clone.GetComponent<AudioSource>().loop = par.Boolvalue;
        clone.GetComponent<SoundModule>().SetSound(par.Floatvalue, par.Name == "" ? soundClone.Count.ToString() : par.Name);
            
        soundClone.Add(clone);
    }

    private void BecomeSmaller(ExtraParams par)
    {
        GameObject target = null;
        foreach (GameObject g in soundClone)
            if (g.name == par.Name)
                target = g;
        if (target == null)
            return;
        else
            target.GetComponent<SoundModule>().BecomeSmaller(par.Floatvalue);
    }

    private void BecomeLounder(ExtraParams par)
    {
        GameObject target = null;
        foreach (GameObject g in soundClone)
            if (g.name == par.Name)
                target = g;
        if (target == null)
            return;
        else
            target.GetComponent<SoundModule>().BecomeLouder(par.Floatvalue);
    }
}
