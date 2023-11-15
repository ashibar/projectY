using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyMadeReality
{
    public class DialogArea : MonoBehaviour
    {
        private DialogBox dialogBox;
        private PortraitBox_Left portraitBox_left;
        private PortraitBox_Right portraitBox_right;

        //[SerializeField] private bool isActive = false;
        [SerializeField] private DialogInfo_so dialog_so;
        [SerializeField] private int cnt = 0;
        [SerializeField] private DialogMode mode;

        private void Awake()
        {
            dialogBox = GetComponentInChildren<DialogBox>(true);
            portraitBox_left = GetComponentInChildren<PortraitBox_Left>();
            portraitBox_right = GetComponentInChildren<PortraitBox_Right>();
        }

        private void Start()
        {
            //dialogBox.Init_dialog(dialog_so.DialogList);
            //SyncCount(cnt);
            //RenderDialog();
            //SetDialog(dialog_so);
        }

        public void SetDialog(DialogInfo_so _dialog_so)
        {
            dialog_so = _dialog_so;
            cnt = 0;
            dialogBox.Init_dialog(dialog_so.DialogList);
            dialogBox.FormByMode(_dialog_so.Mode);
            portraitBox_left.FormByMode(dialog_so.Mode);
            portraitBox_right.FormByMode(dialog_so.Mode);
            SetMode(_dialog_so.Mode);
            SetAuto(_dialog_so.IsAuto);
            SyncCount(cnt);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
            dialogBox.SetActive(value);

            ExtraParams para = new ExtraParams();
            para.Boolvalue = !value;
            if (transform.parent.parent.GetComponentInChildren<Map_RMR_Controller>() != null)
            {
                Debug.Log("!!");
                EventManager.Instance.PostNotification("Set Actvie Map Player", this, null, para);
            }
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
            portraitBox_left.SetSprite(dialog_so, cnt);
            portraitBox_right.SetSprite(dialog_so, cnt);
            dialogBox.nameBox.SetName(dialog_so, cnt);
        }

        public void SetMode(DialogMode mode)
        {
            this.mode = mode;
        }

        public void SetAuto(bool isAuto)
        {
            dialogBox.SetAuto(isAuto);
        }
    }

    public enum DialogMode
    {
        Normal,
        Battle,
    }
}