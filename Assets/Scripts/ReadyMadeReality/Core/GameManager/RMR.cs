using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyMadeReality
{
	public class RMR : MonoBehaviour, IEventListener
	{
        private static RMR instance;
        public static RMR Instance
        {
            get
            {
                if (instance == null) // instance�� ����ִ�
                {
                    var obj = FindObjectOfType<RMR>(true);
                    if (obj != null)
                    {
                        instance = obj;                                             // ��ü ã�ƺôµ�? �ֳ�? �װ� ����
                        Debug.Log("a");
                    }
                    else
                    {
                        var newObj = new GameObject().AddComponent<RMR>(); // ��ü ã�ƺôµ�? ����? ���θ�����
                        instance = newObj;
                        Debug.Log("b");
                    }
                }
                return instance; // �Ⱥ���ֳ�? �׳� �״�� ������
            }
        }

        public DialogArea dialogArea;

        public static List<string> event_code = new List<string>
        {
            "Set Dialog",
        };

        private void Awake()
        {
            var objs = FindObjectsOfType<RMR>(true);
            if (objs.Length != 1)
            {
                Destroy(gameObject);
                return;
            }

            dialogArea = GetComponentInChildren<DialogArea>(true);
            SubscribeEvent();
        }

        public void SubscribeEvent()
        {
            foreach (string code in event_code)
                EventManager.Instance.AddListener(code, this);
        }

        public void OnEvent(string event_type, Component sender, Condition condition, params object[] param)
        {
            ExtraParams par = (ExtraParams)param[0];

            switch (event_type)
            {
                case "Set Dialog":
                    SetDialog(par); break;
            }
        }

        private void SetDialog(ExtraParams param)
        {
            dialogArea.SetActive(true);
            dialogArea.SetDialog(param.Dialog_so);            
        }
    } 
}
