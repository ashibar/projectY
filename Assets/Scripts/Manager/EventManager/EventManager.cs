using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <para/>스크립트 이름 : EventManager
/// <para/>담당자 : 이용욱
/// <para/>요약 : Observer 패턴을 활용해 구독자들에게 동시다발적으로 이벤트를 전송하는 싱글톤 컴포넌트
/// <para/>비고 : 
/// <para/>업데이트 내역 : 
/// <para/>    - (23.08.22) : 스크립트 생성
/// <para/>
/// </summary>

public class EventManager : MonoBehaviour
{
    // 싱글톤 패턴
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null) // instance가 비어있다
            {
                var obj = FindObjectOfType<EventManager>();
                if (obj != null)
                {
                    instance = obj;                                             // 전체 찾아봤는데? 있네? 그걸 넣자
                }
                else
                {
                    var newObj = new GameObject().AddComponent<EventManager>(); // 전체 찾아봤는데? 없네? 새로만들자
                    instance = newObj;
                }
            }
            return instance; // 안비어있네? 그냥 그대로 가져와
        }
    }

    // 구독자 리스트
    //      Key : EventCode로 이벤트를 식별하는 코드. EventCode 문서 참조 link:...\EventCode.cs
    //      Value : 구독자들이 상속할 인터페이스. 여기선 구독자 컴포넌트를 의미 link:...\IEventListener.cs
    private Dictionary<string, List<IEventListener>> listeners = new Dictionary<string, List<IEventListener>>();

    // 스크립트 초기화 시 매소드
    // 한개의 오브젝트만 존재하도록 하는 싱글톤 패턴
    private void Awake()
    {
        var objs = FindObjectsOfType<EventManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
    private void OnDestroy()
    {
        instance = null;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        RefreshListeners();
    }

    /// <summary>
    /// <para><b>구독자 초기화 함수.</b></para>
    /// <para>씬이 로드될때 실행되어 이전씬의 구독자들을 제거함</para>
    /// </summary>
    private void RefreshListeners()
    {
        Dictionary<string, List<IEventListener>> tmpListeners = new Dictionary<string, List<IEventListener>>();

        foreach (KeyValuePair<string, List<IEventListener>> item in listeners)
        {
            for (int i = item.Value.Count - 1; i >= 0; i--)
            {
                if (item.Value[i].Equals(null))
                    item.Value.RemoveAt(i);
            }

            if (item.Value.Count > 0)
                tmpListeners.Add(item.Key, item.Value);
        }

        listeners = tmpListeners;
    }

    // ** 외부 참조 스크립트 **

    /// <summary>
    /// <para><b>자신을 어떤 이벤트의 구독자로 설정하는 함수</b></para>
    /// <para>사용하려면 반드시 IEventListener를 상속하여 OnEvent함수로 받아야한다.</para>
    /// 참조 :  link:...\IEventListener.cs
    /// </summary>
    /// <param name="event_type">구독할 이벤트 enum값</param>
    /// <param name="listener">구독하는 Component. 기본적으로 this를 사용하는 것을 권장함.</param>
    public void AddListener(string event_type, IEventListener listener)
    {
        List<IEventListener> listenList = null;

        if (listeners.TryGetValue(event_type, out listenList))
        {
            listenList.Add(listener);
            return;
        }

        listenList = new List<IEventListener>();
        listenList.Add(listener);
        listeners.Add(event_type, listenList);
    }

    /// <summary>
    /// <para><b>송신자가 구독자들을 활성화하는 함수.</b></para>
    /// <para>이벤트 enum값을 구독한 Component들의 OnEvent를 활성화 하며, param값을 전달함</para>
    /// </summary>
    /// <param name="event_type">활성화할 이벤트 enum값</param>
    /// <param name="sender">송신자 Component. 기본적으로 this를 사용하는 것을 권장함.</param>
    /// <param name="condition">이벤트가 활성화 될 조건을 위한 클래스. ConditionChecker 스크립트 참조.</param>
    /// <param name="param">그 외에 전달할 요소. condition뒤로 오는 모든 변수들을 총칭함</param>
    public void PostNotification(string event_type, Component sender, Condition condition, params object[] param)
    {
        List<IEventListener> listenList = null;

        if (!listeners.TryGetValue(event_type, out listenList))
            return;

        for (int i = 0; i < listenList.Count; i++)
        {
            if (!listenList[i].Equals(null))
                listenList[i].OnEvent(event_type, sender, condition, param);
        }
    }

    public void RemoveEvent(string event_type)
    {
        listeners.Remove(event_type);
    }
    
    
    
}
