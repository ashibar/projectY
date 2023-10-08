using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <para/>��ũ��Ʈ �̸� : EventManager
/// <para/>����� : �̿��
/// <para/>��� : Observer ������ Ȱ���� �����ڵ鿡�� ���ôٹ������� �̺�Ʈ�� �����ϴ� �̱��� ������Ʈ
/// <para/>��� : 
/// <para/>������Ʈ ���� : 
/// <para/>    - (23.08.22) : ��ũ��Ʈ ����
/// <para/>
/// </summary>

public class EventManager : MonoBehaviour
{
    // �̱��� ����
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null) // instance�� ����ִ�
            {
                var obj = FindObjectOfType<EventManager>();
                if (obj != null)
                {
                    instance = obj;                                             // ��ü ã�ƺôµ�? �ֳ�? �װ� ����
                }
                else
                {
                    var newObj = new GameObject().AddComponent<EventManager>(); // ��ü ã�ƺôµ�? ����? ���θ�����
                    instance = newObj;
                }
            }
            return instance; // �Ⱥ���ֳ�? �׳� �״�� ������
        }
    }

    // ������ ����Ʈ
    //      Key : EventCode�� �̺�Ʈ�� �ĺ��ϴ� �ڵ�. EventCode ���� ���� link:...\EventCode.cs
    //      Value : �����ڵ��� ����� �������̽�. ���⼱ ������ ������Ʈ�� �ǹ� link:...\IEventListener.cs
    private Dictionary<string, List<IEventListener>> listeners = new Dictionary<string, List<IEventListener>>();

    // ��ũ��Ʈ �ʱ�ȭ �� �żҵ�
    // �Ѱ��� ������Ʈ�� �����ϵ��� �ϴ� �̱��� ����
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
    /// <para><b>������ �ʱ�ȭ �Լ�.</b></para>
    /// <para>���� �ε�ɶ� ����Ǿ� �������� �����ڵ��� ������</para>
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

    // ** �ܺ� ���� ��ũ��Ʈ **

    /// <summary>
    /// <para><b>�ڽ��� � �̺�Ʈ�� �����ڷ� �����ϴ� �Լ�</b></para>
    /// <para>����Ϸ��� �ݵ�� IEventListener�� ����Ͽ� OnEvent�Լ��� �޾ƾ��Ѵ�.</para>
    /// ���� :  link:...\IEventListener.cs
    /// </summary>
    /// <param name="event_type">������ �̺�Ʈ enum��</param>
    /// <param name="listener">�����ϴ� Component. �⺻������ this�� ����ϴ� ���� ������.</param>
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
    /// <para><b>�۽��ڰ� �����ڵ��� Ȱ��ȭ�ϴ� �Լ�.</b></para>
    /// <para>�̺�Ʈ enum���� ������ Component���� OnEvent�� Ȱ��ȭ �ϸ�, param���� ������</para>
    /// </summary>
    /// <param name="event_type">Ȱ��ȭ�� �̺�Ʈ enum��</param>
    /// <param name="sender">�۽��� Component. �⺻������ this�� ����ϴ� ���� ������.</param>
    /// <param name="condition">�̺�Ʈ�� Ȱ��ȭ �� ������ ���� Ŭ����. ConditionChecker ��ũ��Ʈ ����.</param>
    /// <param name="param">�� �ܿ� ������ ���. condition�ڷ� ���� ��� �������� ��Ī��</param>
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
