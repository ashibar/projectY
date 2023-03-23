using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPoint : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        player.dir_toMouse = GetDirectionToMouse();
    }

    public Vector2 GetDirectionToMouse()
    {
        Vector2 len = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Vector2 dir = new Vector2(len.x, len.y).normalized;

        return dir;
    }
}
