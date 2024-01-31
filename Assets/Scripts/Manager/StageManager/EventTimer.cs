using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// <para/><b>■■ EventTimer ■■</b>
/// <para/>담당자 : 이용욱
/// <para/>요약 : 이벤트에 주어진 대기 시간 체크 후 실행
/// <para/>비고 : 
/// <para/>업데이트 내역 : 
/// <para/> - (23.08.22) : 요약문 생성
/// <para/> - (23.09.02) : Time단일 조건 시 작동 가능
/// <para/> - (23.09.15) : Move To Pos 조건, Time조건 병행 시 문제되는 버그 수정
/// <para/> - (23.09.15) : Trigger, Number 조건 추가
/// </summary>

public class EventTimer : MonoBehaviour, IEventListener
{
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private List<EventPhase_so> phases = new List<EventPhase_so>();
    [SerializeField] private List<StringNTrigger> trigger = new List<StringNTrigger>();
    [SerializeField] private List<StringNNumber> number = new List<StringNNumber>();
    [SerializeField] private int eventIndex = 0;

    [SerializeField] private bool verbose;

    private List<CancellationTokenSource> cts = new List<CancellationTokenSource>();

    public static List<string> event_code = new List<string>
    {
        "Add New Trigger",
        "Remove Trigger",
        "Add New Number",
        "Remove Number",
        "Set Number",
        "Set Trigger",
    };

    /// <summary>
    /// <b>스크립트 첫 활성화 시 가장 먼저 실행되는 함수.</b><br></br>
    /// <br></br>
    /// - 이벤트 구독 <br></br>
    /// </summary>
    private void Awake()
    {
        SubscribeEvent();
    }

    /// <summary>
    /// <b>스크립트 첫 활성화 시 두번째로 실행되는 함수.</b><br></br>
    /// - UnitManager 모듈 로드 <br></br>
    /// 
    /// </summary>
    private void Start()
    {
        unitManager = UnitManager.Instance;
        //Debug.Log("?");
        //CTS(0);
    }

    /// <summary>
    /// 이벤트 구독
    /// </summary>
    public void SubscribeEvent()
    {
        foreach (string code in event_code)
            EventManager.Instance.AddListener(code, this);
    }

    /// <summary>
    /// 이벤트 매니저가 방송하는 이벤트와 비교후 실행하는 함수
    /// </summary>
    /// <param name="event_type">이벤트 코드</param>
    /// <param name="sender">송신자</param>
    /// <param name="condition">조건</param>
    /// <param name="param">매개변수 묶음</param>
    public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
    {
        ExtraParams para = (ExtraParams)param[0];

        switch (event_type)
        {
            case "Add New Trigger":
                AddNewTrigger(para); break;
            case "Remove Trigger":
                RemoveTrigger(para); break;
            case "Add New Number":
                AddNewNumber(para); break;
            case "Remove Number":
                RemoveNumber(para); break;
            case "Set Number":
                SetNumber(para); break;
            case "Set Trigger":
                SetTrigger(para); break;
        }
    }

    /// <summary>
    /// <b>[OnEvent] 새로운 트리거를 등록</b> <br></br>
    /// - Name : 등록할 트리거 이름
    /// - Bool : 트리거의 true/false 여부
    /// </summary>
    /// <param name="para"></param>
    private void AddNewTrigger(ExtraParams para)
    {
        if (verbose)
            Debug.Log(string.Format("{0}, {1}", para.Name, para.Boolvalue));
        
        // 중복이 아니라면 등록
        if (!TriggerExists(para.Name))
            trigger.Add(new StringNTrigger(para.Name, para.Boolvalue));
        // 중복이면 bool값만 변경
        else
            SetTrigger(para);
    }

    /// <summary>
    /// <b>트리거 리스트에서 트리거가 존재하는지 검사</b>
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private bool TriggerExists(string name)
    {
        foreach (StringNTrigger t in trigger)
            if (string.Equals(t.triggerName, name))
                return true;
        return false;
    }

    /// <summary>
    /// <b>[OnEvent] 트리거 제거</b>
    /// - Name : 제거할 트리거 이름
    /// </summary>
    /// <param name="para"></param>
    private void RemoveTrigger(ExtraParams para)
    {
        for (int i = trigger.Count - 1; i >= 0; i--)
        {
            if (string.Equals(trigger[i].triggerName, para.Name))
            {
                trigger.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// <b>[OnEvent] 새로운 Number변수 등록</b> <br></br>
    /// - Name : 등록할 변수 이름
    /// - Float : 등록될 변수의 수치
    /// </summary>
    /// <param name="para"></param>
    private void AddNewNumber(ExtraParams para)
    {
        if (verbose)
            Debug.Log(string.Format("{0}, {1}", para.Name, para.Floatvalue));
        number.Add(new StringNNumber(para.Name, para.Floatvalue));
    }

    /// <summary>
    /// <b>[OnEvent] 수치 변수 제거</b> <br></br>
    /// - Name : 제거할 변수 이름
    /// </summary>
    /// <param name="para"></param>
    private void RemoveNumber(ExtraParams para)
    {
        for (int i = number.Count - 1; i >= 0; i--)
        {
            if (string.Equals(number[i].numberName, para.Name))
            {
                number.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// <b>[OnEvent] 수치 변수의 수치 변경</b> <br></br>
    /// - Name : 변경할 수치 변수 이름
    /// - Float : 변경할 수치 변수의 수치
    /// </summary>
    /// <param name="para"></param>
    private void SetNumber(ExtraParams para)
    {
        Debug.Log(string.Format("{0}, {1}", para.Name, para.Floatvalue));
        for (int i = number.Count - 1; i >= 0; i--)
        {
            if (string.Equals(number[i].numberName, para.Name))
            {
                number[i].numberValue = para.Floatvalue;
                return;
            }
        }
    }

    /// <summary>
    /// <b>[OnEvent] 트리거 변수의 값 변경</b> <br></br>
    /// - Name : 값을 변경할 트리거 변수 이름
    /// - Bool : 그 변수의 변경될 값
    /// </summary>
    /// <param name="para"></param>
    private void SetTrigger(ExtraParams para)
    {
        Debug.Log(string.Format("{0}, {1}", para.Name, para.Boolvalue));
        for (int i = trigger.Count - 1; i >= 0; i--)
        {
            if (string.Equals(trigger[i].triggerName, para.Name))
            {
                trigger[i].triggerValue = para.Boolvalue;
                return;
            }
        }
    }

    /// <summary>
    /// <b>EventPhase를 사용하여 서브루틴을 생성</b>
    /// </summary>
    /// <param name="id"></param>
    private async void CreatePhaseSubroutine(int id)
    {
        cts.Add(new CancellationTokenSource());
        await PhaseSubroutine(id, cts[id].Token);
    }

    /// <summary>
    /// <b>phase를 읽고 이벤트를 실행시키는 서브루틴</b>
    /// </summary>
    /// <param name="no"></param>
    /// <param name="_cts"></param>
    /// <returns></returns>
    private async Task<int> PhaseSubroutine(int no, CancellationToken _cts)
    {
        // 페이즈 무결성
        while (no >= phases.Count)
        {
            if (_cts.IsCancellationRequested)
                break;
            await Task.Yield();
        }

        if (verbose)
        {
            Debug.Log(string.Format("Phase {0} subroutine Activated", no));
        }        
        List<EventParams> eventList = phases[no].Events;

        float start = Time.time;    // 시작 시간
        float accumulated;          // 누적 시간
        while (!_cts.IsCancellationRequested)
        {
            // 이벤트 무결성
            if (eventList == null)
            {
                if (verbose)
                {
                    Debug.Log(string.Format("No event list found, Phase {0} inActivated", no));
                }
                break;
            }

            // 누적 시간 초기화
            accumulated = 0;


            // 조건 검사 및 이벤트 실행
            for (int i = 0; i < eventList.Count; i++)
            {
                EventParams p = eventList[i];
                
                //if (p.condition.IsContinued)            // 이전 이벤트가 만족시만 실행되는 이벤트
                //{
                //    // 이전 이벤트 만족 여부 검사
                //    if (i > 0 && !eventList[i - 1].condition.IsSatisfied)
                //        continue;

                //    // 조건에 따른 연속 이벤트 처리, 만족되지 않을 경우 다시 반복
                //    bool isPass = ContinuousEventProcess(p, ref start, ref accumulated);
                //    if (!isPass) continue;
                //}
                if (true)            // 이전 이벤트가 만족시만 실행되는 이벤트
                {
                    // 이전 이벤트 만족 여부 검사
                    if (i > 0 && !eventList[i - 1].isSatisfied)
                        continue;

                    // 조건에 따른 연속 이벤트 처리, 만족되지 않을 경우 다시 반복
                    bool isPass = ContinuousEventProcess_(p, ref start, ref accumulated);
                    if (!isPass) continue;
                }
                //else                                    // 다른 이벤트와 독립적으로 작동하는 이벤트
                //{
                //    bool isPass = IndependentEventProcess(p, ref start);
                //    if (!isPass) continue;
                //}
            }


            // 이벤트들의 모든 조건이 완료되면 Phase를 종료
            //if (no < phases.Count)
            //{
            //    bool allCheck = true;
            //    foreach (EventParams p in phases[no].Events)
            //    {
            //        if (p.condition.IsSatisfied == false)
            //            allCheck = false;
            //    }
            //    if (allCheck)
            //    {
            //        if (verbose)
            //        {
            //            Debug.Log(string.Format("All Events of Phase {0} satisfied. Phase {0} inactivated", no));
            //        }
            //        break;
            //    }                    
            //}
            if (no < phases.Count)
            {
                bool allCheck = true;
                foreach (EventParams p in phases[no].Events)
                {
                    if (p.isSatisfied == false)
                        allCheck = false;
                }
                if (allCheck)
                {
                    if (verbose)
                    {
                        Debug.Log(string.Format("All Events of Phase {0} satisfied. Phase {0} inactivated", no));
                    }
                    break;
                }
            }

            await Task.Yield();
            
        }

        return -1;
    }

    /// <summary>
    /// <b>연속적으로 실행되는 이벤트들에 대한 처리</b>
    /// </summary>
    /// <param name="p">이벤트</param>
    /// <param name="start">시작 시간</param>
    /// <param name="accumulated">누적 시간</param>
    /// <returns></returns>
    private bool ContinuousEventProcess(EventParams p, ref float start, ref float accumulated)
    {
        bool check;
        // 누적 시간
        switch (p.condition.Sort)
        {
            // 시간
            case ConditionSort.Time:                                
                accumulated += p.condition.TargetNum;
                if (p.condition.IsSatisfied)
                    return false;
                if (Time.time - start >= accumulated)
                    p.condition.IsSatisfied = PostNotification(p);
                return true;
            
            // 조건없음(0초)
            case ConditionSort.None:
                accumulated += 0;
                if (p.condition.IsSatisfied)
                    return false;
                if (Time.time - start >= accumulated)
                    p.condition.IsSatisfied = PostNotification(p);
                return true;
            
            // 특정 위치 이동
            case ConditionSort.MoveToPos:
                if (p.condition.IsSatisfied)
                    return false;
                bool value = CheckPosition(p);
                p.condition.IsSatisfied = value;
                if (value)
                    start = Time.time;
                return true;
            
            // Trigger 확인
            case ConditionSort.Trigger:
                if (p.condition.IsSatisfied)
                    return false;
                check = CheckTrigger(p);
                p.condition.IsSatisfied = check;
                if (check)
                    start = Time.time;
                return true;
            
            // 정수 확인
            case ConditionSort.Number:
                if (p.condition.IsSatisfied)
                    return false;
                check = CheckNumber(p);
                p.condition.IsSatisfied = check;
                if (check)
                    start = Time.time;
                return true;
            default:
                return false;
        }        
    }

    private bool ContinuousEventProcess_(EventParams p, ref float start, ref float accumulated)
    {
        // 이미 만족된 
        
        List<Condition> conditions = p.conditions;
        // 조건 한개라도 만족하지 못할 시 false
        bool result = true;
        bool isTime = false;
        for (int i = 0; i < conditions.Count; i++)
        {
            // 누적 시간
            switch (conditions[i].Sort)
            {
                // 시간
                case ConditionSort.Time:
                    accumulated += conditions[i].TargetNum;
                    if (p.isSatisfied)
                        return false;
                    if (Time.time - start < accumulated)
                        result = false;
                    isTime = true;
                    break;
                // 조건없음(0초)
                case ConditionSort.None:
                    accumulated += 0;
                    if (p.isSatisfied)
                        return false;
                    if (Time.time - start < accumulated)
                        result = false;
                    isTime = true;
                    break;
                // 특정 위치 이동
                case ConditionSort.MoveToPos:
                    if (p.isSatisfied)
                        return false;
                    if (!CheckPosition(conditions[i]))
                        result = false;
                    break;
                // Trigger 확인
                case ConditionSort.Trigger:
                    if (p.isSatisfied)
                        return false;
                    if (!CheckTrigger(conditions[i]))
                        result = false;
                    break;
                // 정수 확인
                case ConditionSort.Number:
                    if (p.isSatisfied)
                        return false;
                    if (!CheckNumber(conditions[i]))
                        result = false;                   
                    break;
                default:
                    break;
            }
        }

        if (!isTime)
            start = Time.time;

        if (result)
        {
            p.isSatisfied = PostNotification(p);
            return true;
        }            
        else
            return false;
    }

    /// <summary>
    /// <b>독립적으로 실행되는 이벤트들에 대한 처리</b>
    /// </summary>
    /// <param name="p"></param>
    /// <param name="start"></param>
    /// <returns></returns>
    private bool IndependentEventProcess(EventParams p, ref float start)
    {
        switch (p.condition.Sort)
        {
            case ConditionSort.Time:
                if (p.condition.IsSatisfied)
                    return false;
                if (Time.time - start >= p.condition.TargetNum)
                    p.condition.IsSatisfied = PostNotification(p);
                return true;
            case ConditionSort.None:
                p.condition.IsSatisfied = PostNotification(p);
                return true;
            case ConditionSort.MoveToPos:
                p.condition.IsSatisfied = CheckPosition(p);
                return true;
            case ConditionSort.Trigger:
                p.condition.IsSatisfied = CheckTrigger(p);
                return true;
            case ConditionSort.Number:
                p.condition.IsSatisfied = CheckNumber(p);
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// <b>조건이 만족되었을 EventManager에 Event를 방송함</b>
    /// </summary>
    /// <param name="parm"></param>
    /// <returns></returns>
    private bool PostNotification(EventParams parm)
    {
        if (verbose)
        {
            Debug.Log(string.Format("Event ID {0} : code = {1}, condiiton sort = {2}, extra params = {3}",
                parm.no, parm.eventcode, parm.condition.Sort, (parm.extraParams != null) ? parm.extraParams : null));
        }        
        EventManager.Instance.PostNotification(parm.eventcode, this, parm.condition, parm.extraParams);
        return true;
    }
        
    /// <summary>
    /// <b>특정 범위 내에 특정 타겟의 유닛이 몇개 존재하는지 검사</b>
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private bool CheckPosition(EventParams p)
    {
        // 유닛 체크를 위한 UnitManager 모듈 무결성 검사
        if (unitManager == null) unitManager = UnitManager.Instance;

        // UnitManager 내에 Clones 리스트 무결성 검사
        if (unitManager.Clones == null) return false;        
        List<GameObject> clones = unitManager.Clones;
        if (clones.Count == 0) return false;

        // 이벤트가 이미 조건이 만족된 상태라면 true 반환
        if (p.condition.IsSatisfied) return true;


        // clone 리스트에서 태그와 비교해 특정 위치의 특정 범위 내에 일치하는 유닛의 갯수를 구함
        // p.condition.TargetTag는 찾아야하는 타겟의 태그
        int num = 0;
        foreach (GameObject go in clones)
        {
            if (go == null) continue;
            if (!string.Equals(p.condition.TargetTag, go.tag)) continue;
            
            // 범위내 존재하는지 검사
            if (IsInsideRectangle(p.condition.TargetPos, p.condition.TargetRange, go.transform))
            {
                num++;
            }
        }

        // 유닛의 갯수가 목표치 이상인지 검사
        // p.condition.TargetNum은 목표치
        if (num >= p.condition.TargetNum)
        {
            p.condition.IsSatisfied = true;
            return PostNotification(p);
        }
        else
            return false;
    }

    private bool CheckPosition(Condition c)
    {
        // 유닛 체크를 위한 UnitManager 모듈 무결성 검사
        if (unitManager == null) unitManager = UnitManager.Instance;

        // UnitManager 내에 Clones 리스트 무결성 검사
        if (unitManager.Clones == null) return false;
        List<GameObject> clones = unitManager.Clones;
        if (clones.Count == 0) return false;


        // clone 리스트에서 태그와 비교해 특정 위치의 특정 범위 내에 일치하는 유닛의 갯수를 구함
        // p.condition.TargetTag는 찾아야하는 타겟의 태그
        int num = 0;
        foreach (GameObject go in clones)
        {
            if (go == null) continue;
            if (!string.Equals(c.TargetTag, go.tag)) continue;

            // 범위내 존재하는지 검사
            if (IsInsideRectangle(c.TargetPos, c.TargetRange, go.transform))
            {
                num++;
            }
        }

        // 유닛의 갯수가 목표치 이상인지 검사
        // p.condition.TargetNum은 목표치
        if (num >= c.TargetNum)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// <b>매개변수로 받아온 transform이 직사각형 안에 있는지 여부를 확인</b>
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <param name="objectTransform"></param>
    /// <returns></returns>
    private bool IsInsideRectangle(Vector2 pos, Vector2 size, Transform objectTransform)
    {
        // 무결성 검사
        if (pos == null)
        {
            Debug.Log("pos is null");
            return false;
        }
        if (size == null)
        {
            Debug.Log("range is null");
            return false;
        }
        
        // 최소 범위 설정 (x,y 0.1 반지름 정사각형)
        size = Vector2.Distance(size, Vector2.zero) <= 0 ? new Vector2(0.1f, 0.1f) : size;
        
        // 경계
        float leftBoundary = pos.x - (size.x / 2);
        float rightBoundary = pos.x + (size.x / 2);
        float bottomBoundary = pos.y - (size.y / 2);
        float topBoundary = pos.y + (size.y / 2);

        // 확인할 유닛의 위치
        Vector3 objectPosition = objectTransform.position;

        // 유닛의 위치가 경계 내에 위치하는지 검사
        if (objectPosition.x >= leftBoundary && objectPosition.x <= rightBoundary &&
            objectPosition.y >= bottomBoundary && objectPosition.y <= topBoundary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// <b>트리거 리스트를 검사하여 이벤트의 조건에 적힌 트리거가 true인지 검사</b>
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool CheckTrigger(EventParams p)
    {
        foreach (StringNTrigger t in trigger)
        {
            if (string.Equals(t.triggerName, p.condition.TargetFlag))
            {
                if (t.triggerValue == p.condition.FlagValue)
                {
                    return PostNotification(p);
                }
            }                
        }
        return false;
    }

    public bool CheckTrigger(Condition c)
    {
        foreach (StringNTrigger t in trigger)
        {
            if (string.Equals(t.triggerName, c.TargetFlag))
            {
                if (t.triggerValue == c.FlagValue)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// <b>트리거 리스트를 검사하여 일치하는 트리거가 true인지 검사</b>
    /// </summary>
    /// <param name="triggerName"></param>
    /// <param name="flagValue"></param>
    /// <returns></returns>
    public bool CheckTrigger(string triggerName, bool flagValue)
    {
        foreach (StringNTrigger t in trigger)
        {
            if (string.Equals(t.triggerName, triggerName))
            {
                if (t.triggerValue == flagValue)
                {
                    Debug.Log(string.Format("Custom CheckTrigger : {0}, {1}", triggerName, flagValue));
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// <b>확인하려는 정수 변수의 이름과 비교해 일치하는 변수의 숫자가, 원하는 수치 이상/이하인지 확인</b>
    /// p.condition.TargetFlag : 대상 변수
    /// p.condition.FlagValue : true = 이상, false = 이하
    /// p.condition.TargetNum : 수치
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool CheckNumber(EventParams p)
    {
        foreach (StringNNumber n in number)
        {
            if (string.Equals(n.numberName, p.condition.TargetFlag))
            {
                if (p.condition.FlagValue)
                {
                    if (n.numberValue >= p.condition.TargetNum)
                    {
                        Debug.Log(string.Format("{0} : {1}, {2}, {3}, {4}", eventIndex, p.eventcode, p.condition.Sort, p.condition.TargetFlag, p.condition.TargetNum));
                        EventManager.Instance.PostNotification(p.eventcode, this, p.condition, p.extraParams);
                        return true;
                    }
                }
                else
                {
                    if (n.numberValue <= p.condition.TargetNum)
                    {
                        Debug.Log(string.Format("{0} : {1}, {2}, {3}, {4}", eventIndex, p.eventcode, p.condition.Sort, p.condition.TargetFlag, p.condition.TargetNum));
                        EventManager.Instance.PostNotification(p.eventcode, this, p.condition, p.extraParams);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool CheckNumber(Condition c)
    {
        foreach (StringNNumber n in number)
        {
            if (string.Equals(n.numberName, c.TargetFlag))
            {
                if (c.FlagValue)
                {
                    if (n.numberValue >= c.TargetNum)
                    {
                        return true;
                    }
                }
                else
                {
                    if (n.numberValue <= c.TargetNum)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// <b>오브젝트 파괴 시 실행될 함수</b> <br></br>
    /// - 파괴시 모든 이벤트 중단
    /// </summary>
    private void OnDestroy()
    {
        foreach(CancellationTokenSource c in cts)
            c?.Cancel();
        //isInterrupted = true;
    }

    // ** 외부 참조 스크립트 **

    /// <summary>
    /// <b>Phase리스트에 Phase를 추가하고 추가한 Phase를 이용한 서브루틴을 생성</b> <br></br>
    /// - 추가된 phase는 condition들이 모두 불만족 처리 됨 <br></br>
    /// </summary>
    /// <param name="phase"></param>
    public void AddPhaseToPhaseList_And_CreatePhaseSubroutine(EventPhase_so phase)
    {
        // phase의 condition들의 isSatisfied변수를 false로 전환(조건들이 모두 불만족 된 상태로 전환)
        phase.UnSatisfyAll();
        
        // phase리스트에 매개변수로 가져온 phase를 추가
        phases.Add(phase);

        // 추가된 phase는 마지막 인덱스에 저장되므로 그 인덱스에 해당하는 phase를 이용한 서브루틴을 생성
        CreatePhaseSubroutine(phases.Count - 1);
    }

    /// <summary>
    /// <b>phase 리스트를 비움</b>
    /// </summary>
    public void ClearPhase()
    {
        phases.Clear();
    }


    // 더미 코드
    //private async void Async_Function()
    //{
    //    // 무결성
    //    if (events == null)
    //        return;

    //    // 업데이트
    //    if (!isCooltime && eventIndex < events.Count)
    //    {
    //        isCooltime = true;
    //        interruptedIndex = await Task_Function();
    //        isCooltime = false;
    //    }

    //    if (interruptedIndex != -1)
    //        Debug.Log("interruptedIndex = " + interruptedIndex.ToString());
    //}
    //// 이벤트의 메시지 입력 함수
    //private async Task<int> Task_Function()
    //{
    //    float end = Time.time + events[eventIndex].DurationToStart;
    //    while (Time.time < end)
    //    {
    //        if (events[eventIndex].Isinterrupted)
    //            await Task.FromResult(0);
    //        await Task.Yield();
    //    }
    //    Debug.Log(eventIndex + ", " + events[eventIndex].Id + ", " + events[eventIndex].DurationToStart);
    //    StageManager.Instance.messageBuffer.Add(events[eventIndex].Message);
    //    eventIndex++;

    //    return -1;
    //}

}
