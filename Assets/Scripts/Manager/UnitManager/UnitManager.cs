using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour, IEventListener
{
    private static UnitManager instance;    
    public static UnitManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<UnitManager>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<UnitManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private EmotionEffect emotionEffect;
    [SerializeField] private List<GameObject> clones = new List<GameObject>();
    [SerializeField] private Unit targetUnit;
    [SerializeField] private int targetDestroyed;
    [SerializeField] private int maxDestroyed;
    [SerializeField] private List<EventParams> events = new List<EventParams>();
    private Unit player;
    public List<GameObject> Clones { get => clones; }
    public Unit TargetUnit { get => targetUnit; set => targetUnit = value; }
    public int TargetDestroyed { get => targetDestroyed; set => targetDestroyed = value; }
    public int MaxDestroyed { get => maxDestroyed; set => maxDestroyed = value; }

    public static List<string> event_code = new List<string>
    {
        "Unit Force Move",
        "Unit Force Stop",
        "Player Move Input",
        "Player Animation",
        "Register Pos Search",
        "Emotion Effect",
        "Player Combat",
        "Set Unit Animation Bool",
        "Set Unit Animation Trigger",
        "FlipX Unit",
        "Unit Set Position",
    };

    private void Awake()
    {
        playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        emotionEffect= GetComponentInChildren<EmotionEffect>();
    }

    private void Start()
    {
        if (player == null)
        {
            player = Player.Instance;
        }
        //clones.Add(player.gameObject);
        Unit[] preset = FindObjectsOfType<Unit>();
        foreach (Unit u in preset)
        {
            clones.Add(u.gameObject);
        }
        SubscribeEvent();
    }
    private void Update()
    {
        EventReciever();
        EventListener();
    }

    public void SubscribeEvent()
    {
        foreach (string code in event_code)
            EventManager.Instance.AddListener(code, this);
    }

    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        //ExtraParams par = (ExtraParams)param[0];
        //Debug.Log(string.Format("{0}, {1}", event_type, par.VecList[0]));
        switch (event_type)
        {
            case "Unit Force Move":
                UnitForceMove((ExtraParams)param[0]); break;
            case "Unit Force Stop":
                UnitForceStop((ExtraParams)param[0]); break;
            case "Player Move Input":
                PlayerMoveInput((ExtraParams)param[0]); break;
            case "Player Animation":
                PlayerAnimation((ExtraParams)param[0]); break;
            case "Register Pos Search":
                RegisterPosSearch((EventParams)param[0]); break;
            case "Emotion Effect":
                EmotionEffect((ExtraParams)param[0]); break;
            case "Player Combat":
                PlayerCombat((ExtraParams)param[0]); break;
            case "Set Unit Animation Bool":
                SetUnitAnimationBool((ExtraParams)param[0]); break;
            case "Set Unit Animation Trigger":
                SetUnitAnimationTrigger((ExtraParams)param[0]); break;
            case "FlipX Unit":
                FlipXUnit((ExtraParams)param[0]); break;
            case "Unit Set Position":
                UnitSetPostion((ExtraParams)param[0]); break;
            default:
                break;
        }
    }

    private void UnitForceMove(ExtraParams par)
    {
        switch (par.Name)
        {
            case "Player":
                Debug.Log(string.Format("{0}, {1}", par.VecList[0], par.Floatvalue) );
                player.GetComponentInChildren<UnitForceMove>().SetForceMove(par.VecList[0], par.Floatvalue);
                break;
            default:
                foreach (GameObject go in clones)
                    if (string.Equals(go.tag, par.Name))
                        go.GetComponentInChildren<UnitForceMove>().SetForceMove(par.VecList[0], par.Floatvalue);
                break;
        }        
        //player.GetComponentInChildren<UnitForceMove>().IsForceMove = true;
    }

    private void UnitForceStop(ExtraParams par)
    {
        switch (par.Name)
        {
            case "Player":
                player.GetComponentInChildren<UnitForceMove>().ForceStop();
                break;
            default:
                break;
        }
    }

    private void PlayerMoveInput(ExtraParams par)
    {
        player.GetComponent<Player>().playerMovement.IsMove = par.Boolvalue;
        //Debug.Log(string.Equals(m.TargetSTR, "true"));
    }

    private void PlayerAnimation(ExtraParams par)
    {
        player.GetComponent<Player>().animationManager.AnimationControl(par.Name);
    }

    private void RegisterPosSearch(EventParams par)
    {

    }

    private void EmotionEffect(ExtraParams par)
    {
        foreach (GameObject c in clones)
        {
            if (string.Equals(c.tag, par.Name))
            {
                emotionEffect.MakeEffect(c.transform, par.Intvalue, par.Boolvalue);
            }
        }
    }

    private void PlayerCombat(ExtraParams par)
    {
        player.GetComponent<Player>().playerMovement.IsCombat = par.Boolvalue;
    }

    private void SetUnitAnimationBool(ExtraParams par)
    {
        EventManager.Instance.PostNotification("Set Module Animator Bool", this, null, par);
    }

    private void SetUnitAnimationTrigger(ExtraParams par)
    {
        EventManager.Instance.PostNotification("Set Module Animator trigger", this, null, par);
    }

    private void FlipXUnit(ExtraParams par)
    {
        foreach (GameObject c in clones)
        {
            if (string.Equals(c.tag, par.Name))
            {
                Vector3 scale = c.transform.localScale;
                c.transform.localScale = new Vector3(-scale.x, scale.y);
            }
        }
    }

    private void UnitSetPostion(ExtraParams par)
    {
        foreach (GameObject c in clones)
        {
            if (string.Equals(c.tag, par.Name))
            {
                c.transform.position = par.VecList[0];
            }
        }
    }

    public virtual void Delete_FromCloneList(GameObject clone)
    {
        int id = Clone_Comparer(clone);

        if (id < 0)
        {
            Debug.Log("out of range or not in list: " + id.ToString());
            return;
        }
        else
        {
            targetDestroyed += 1;
            clones.RemoveAt(id);
        }
    }

    private int Clone_Comparer(GameObject clone)
    {
        for (int i = 0; i < clones.Count; i++)
        {
            if (clone == clones[i]) return i;
        }
        return -1;
    }
    [SerializeField] private List<EventMessage> messageBuffer = new List<EventMessage>();

    private void EventReciever()
    {
        int error = StageManager.Instance.SearchMassage(3, messageBuffer);
        
        if (error == -1)
            return;
    }

    private void EventListener()
    {
        bool isError = false;
        EventMessage temp = new EventMessage();

        if (messageBuffer.Count == 0)
            return;
        else
            Debug.Log(messageBuffer.Count);

        foreach (EventMessage m in messageBuffer)
        {
            temp = m;
            switch (m.ActionSTR)
            {
                case "Player Move":
                    player.GetComponentInChildren<UnitForceMove>().SetForceMove(m.TargetPOS, m.TargetNUM);
                    player.GetComponentInChildren<UnitForceMove>().IsForceMove = true;
                    messageBuffer.Remove(m);
                    return;
                case "Player Stop":
                    player.GetComponentInChildren<UnitForceMove>().IsForceMove = false;
                    messageBuffer.Remove(m);
                    return;
                case "Player Move Input":
                    player.GetComponent<Player>().playerMovement.IsMove = string.Equals(m.TargetSTR, "true");
                    Debug.Log(string.Equals(m.TargetSTR, "true"));
                    messageBuffer.Remove(m);
                    return;
                case "Player Animation":
                    playerAnimationController.SetAnimation(m.TargetSTR);
                    messageBuffer.Remove(m);
                    return;
                default:
                    isError = true;
                    break;
            }
        }

        if (!isError)
        {
            messageBuffer.Remove(temp);
        }
    }

    private void UnitForceMove(Unit _unit, Vector2 targetPos, float speed, EventMessage m)
    {
        Vector2 pos = _unit.transform.position;
        Vector2 dir = (targetPos - pos).normalized;

        float distanceToTarget = (targetPos - pos).magnitude;

        // 등속 이동
        if (distanceToTarget > 0f)
        {
            if (speed == 0)
                _unit.transform.position = Vector2.MoveTowards(pos, targetPos, _unit.stat.Speed * Time.deltaTime);
            else
                _unit.transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
        }
        //else
            //messageBuffer.Remove(m);
    }

}
