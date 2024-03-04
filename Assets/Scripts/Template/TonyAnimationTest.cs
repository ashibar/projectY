using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TonyAnimationTest : MonoBehaviour
{
    [SerializeField] private Transform player_pos;
    [SerializeField] private SpriteRenderer tony_sr;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI tonyLog;
    [SerializeField] private Vector2 dir_toShoot;
    [SerializeField] private float range;
    [SerializeField] private bool ovalMode;
    [SerializeField] private string state;
    [SerializeField] private Vector3 angle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        dir_toShoot = GetDirectionToMouse();
        
        SetPos();
        SetState();
        SetAnimation();
        

        tonyLog.text = string.Format("{0}, {1}", state, ovalMode ? "Oval" : "Circle");
    }

    public Vector2 GetDirectionToMouse()
    {
        Vector2 len = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player_pos.position);
        Vector2 dir = new Vector2(len.x, len.y).normalized;

        return dir;
    }

    private void SetPos()
    {
        Vector2 targetPosition = Vector2.zero;
        if (!ovalMode)
            targetPosition = dir_toShoot * range;
        else
        {
            float x = range * Mathf.Cos(angle.z / 60 - 180);
            float y = (range / 4) * Mathf.Sin(angle.z / 60 - 180) - 0.2f;
            Vector2 new_dir = new Vector2(x, y);
            targetPosition = new_dir;
        }
            
        transform.localPosition = targetPosition;
    }

    private void SetState()
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(dir_toShoot.x, dir_toShoot.y, 0f));
        angle = rotation.eulerAngles;
        float z = angle.z;

        if ((315 <= z && z < 360) ||  (0 <= z  && z < 45))
        {
            state = "back";
            tony_sr.sortingOrder = -1;
        }            
        else if (45 <= z && z < 135)
        {
            state = "left";
            tony_sr.sortingOrder = -1;
        }
        else if (135 <= z && z < 225)
        {
            state = "front";
            tony_sr.sortingOrder = +1;
        }
        else if (225 <= z && z < 315)
        {
            state = "right";
            tony_sr.sortingOrder = -1;
        }
    }

    private void SetAnimation()
    {
        animator.SetTrigger(state);
    }

    private void SetRot()
    {
        Vector3 rot = angle;
        switch (state)
        {
            case "back":
                rot.z += 0; break;
            case "left":
                rot.z += 270f; break;
            case "front":
                rot.z += 180f; break;
            case "right":
                rot.z += 90f; break;
        }
        
        
        transform.rotation = Quaternion.Euler(rot);
    }
}
