using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    [SerializeField]
    private float duration;
    private bool isDeleted = false;
    private void Start()
    {
        AutoDelete(duration);
    }

    private async void AutoDelete(float duration)
    {
        float end = Time.time + duration;

        while(Time.time < end)
        {
            await Task.Yield();
        }
        if(!isDeleted) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Delete_FromCloneList();
            Destroy(collision.gameObject);
            isDeleted = true;
            Destroy(gameObject);

        }

    }
}
