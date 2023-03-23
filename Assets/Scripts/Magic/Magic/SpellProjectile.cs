using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    [SerializeField]
    private float duration;
    
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
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        
    }
}
