using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRepostion1 : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = Player.Instance.transform.position;
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
                    transform.Translate(Vector3.up * diry * 45);
                    transform.Translate(Vector3.right * dirx * 60);
                }
                else if (diffx > diffy)
                {
                    transform.Translate(Vector3.right * dirx * 60);
                }
                else if (diffx < diffy)
                {
                    transform.Translate(Vector3.up * diry * 45);

                }
                else
                {
                    transform.Translate(dirx * 60, diry * 45, 0);

                }
                break;
        }
    }
}
