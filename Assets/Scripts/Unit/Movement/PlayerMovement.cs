using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private MovementManager movementmanager;
    private Rigidbody2D rb;
    [SerializeField] private bool isMove = true;
    [SerializeField] private bool isCombat = true;

    public bool IsMove { get=>isMove; set => isMove = value; }
    public bool IsCombat { get => isCombat; set => isCombat = value; }

    public float moveSpeed = 10;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(player.stat.Hp_current == 0)
           {
               player.stat.Hp_current -= 1;
                Debug.Log("Game Over");
           }
        
        Vector2 dir = ControlByKeyboard();
        if(IsMove)
        {
            MoveAnimation(CheckMove(dir));
            if (!isCombat)
                Flip(dir);
            player.dir_toMove = dir;
            Movement(dir);
        }
        rb.velocity = Vector2.zero;
    }

    private bool CheckMove(Vector2 dir)
    {
        return dir == Vector2.zero ? false : true;
    }

    private void MoveAnimation(bool isMove)
    {
        if (isMove)
        {
            if (IsCombat)
            {
                player.animationManager.AnimationControl("Move");
            }
            else
                player.animationManager.AnimationControl("Walk");
        }
        else player.animationManager.AnimationControl("None");
    }

    private void Flip(Vector2 dir)
    {
        if (dir.x > 0)
        {
            player.transform.localScale = new Vector3(1, player.transform.localScale.y, player.transform.localScale.z);
        }
        if (dir.x < 0)
        {
            player.transform.localScale = new Vector3(-1, player.transform.localScale.y, player.transform.localScale.z);
        }
    }

    private Vector2 ControlByKeyboard()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
               

        return new Vector2(inputX, inputY).normalized;
    }

    private void Movement(Vector2 dir)
    {
        
        player.transform.Translate(dir * Time.deltaTime * player.stat_processed.Speed);
    }
}
