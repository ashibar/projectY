using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPoint : MonoBehaviour
{
    private Player player;
    private bool isActiveMove = true;
    public bool IsActiveMove { get => isActiveMove; set => isActiveMove=value; }
    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        //여기에 조건문으로 입력막기
        if (isActiveMove)
        {
            player.dir_toShoot = GetDirectionToMouse();
            player.pos_toShoot = GetPositionToMouse();
        }
    }

    public Vector2 GetDirectionToMouse()
    {
        Vector2 len = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Vector2 dir = new Vector2(len.x, len.y).normalized;
        
        return dir;
    }
    public Vector2 GetPositionToMouse()
    {
        Vector2 pos = (Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y)));
        return pos;
            
    }
}
