using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private MovementManager movementmanager;

    public float moveSpeed = 10;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = ControlByKeyboard();
        MoveAnimation(CheckMove(dir));
        player.dir_toMove = dir;
        Movement(dir);
    }

    private bool CheckMove(Vector2 dir)
    {
        return dir == Vector2.zero ? false : true;
    }

    private void MoveAnimation(bool isMove)
    {
        if (isMove) player.animationManager.AnimationControl("Move");
        else player.animationManager.AnimationControl("Stop");
    }

    private Vector2 ControlByKeyboard()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        return new Vector2(inputX, inputY);
    }

    private void Movement(Vector2 dir)
    {
        player.transform.Translate(dir * Time.deltaTime * player.stat_processed.Speed);
    }
}
