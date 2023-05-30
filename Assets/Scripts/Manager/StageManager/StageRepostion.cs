using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StageRepostion : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = Player.instance.transform.position;
        Vector3 myPos = transform.position;

        switch (transform.tag)
        {
            case "Ground":
                float diffx = playerPos.x - myPos.x;
                float diffy = playerPos.y - myPos.y;
               
                float dirx = diffx < 0 ? -1 : 1;
                float diry = diffy < 0 ? -1 : 1;

                diffx = Mathf.Abs(diffx);
                diffy = Mathf.Abs(diffy);
                if (Mathf.Abs(diffx - diffy) <= 0.1f)
                {
                    transform.Translate(Vector3.up * diry * 40);
                    transform.Translate(Vector3.right * dirx * 44);
                }
                else if (diffx > diffy)
                {
                    transform.Translate(Vector3.right * dirx * 44);
                }
                else if(diffx < diffy)
                {
                    transform.Translate(Vector3.up * diry * 40);

                }
                else
                {
                    transform.Translate(dirx * 44, diry * 40, 0);

                }
                break;
        }
    }
}
