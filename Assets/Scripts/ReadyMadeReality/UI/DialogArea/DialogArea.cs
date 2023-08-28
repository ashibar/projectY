using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyMadeReality
{
    public class DialogArea : MonoBehaviour
    {
        private static DialogArea instance;
        public static DialogArea Instance
        {
            get
            {
                if (instance == null) // instance가 비어있다
                {
                    var obj = FindObjectOfType<DialogArea>(true);
                    if (obj != null)
                    {
                        instance = obj;                                             // 전체 찾아봤는데? 있네? 그걸 넣자
                        Debug.Log("a");
                    }
                    else
                    {
                        var newObj = new GameObject().AddComponent<DialogArea>(); // 전체 찾아봤는데? 없네? 새로만들자
                        instance = newObj;
                        Debug.Log("b");
                    }
                }
                return instance; // 안비어있네? 그냥 그대로 가져와
            }
        }

        private DialogBox dialogBox;
        private PortraitBox_Left portraitBox_left;
        private PortraitBox_Right portraitBox_right;

        [SerializeField] private bool isActive = false;
        [SerializeField] private DialogInfo_so dialog_so;
        [SerializeField] private int cnt = 0;

        private void Awake()
        {
            var objs = FindObjectsOfType<DialogArea>(true);
            if (objs.Length != 1)
            {
                Destroy(gameObject);
                return;
            }
            dialogBox = GetComponentInChildren<DialogBox>(true);
            portraitBox_left = GetComponentInChildren<PortraitBox_Left>();
            portraitBox_right = GetComponentInChildren<PortraitBox_Right>();
        }

        private void Start()
        {
            //dialogBox.Init_dialog(dialog_so.DialogList);
            //SyncCount(cnt);
            //RenderDialog();
        }

        public void SetDialog(DialogInfo_so _dialog_so)
        {
            dialog_so = _dialog_so;
            cnt = 0;
            dialogBox.Init_dialog(dialog_so.DialogList);
            SyncCount(cnt);
        }

        public void SetActive(bool value)
        {
            instance.gameObject.SetActive(value);
            dialogBox.SetActive(value);
        }

        public void RenderDialog()
        {
            if (cnt >= dialog_so.DialogList.Count)
            {
                Debug.Log("DialogEnd");
                return;
            }
            dialogBox.RenderText(dialog_so.DialogList, cnt);
        }

        public void SyncCount(int value)
        {
            cnt = value;
            portraitBox_left.SetSprite(dialog_so.DialogList, cnt);
            portraitBox_right.SetSprite(dialog_so.DialogList, cnt);
            dialogBox.nameBox.SetName(dialog_so.DialogList, cnt);
        }
    }

}