using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private float Textspeed = 2.0f;
    [SerializeField]
    private float alphaspeed = 2.0f;
    [SerializeField]
    private float destroyTime = 2.0f;

    TextMeshPro text;
    Color alpha;
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, Textspeed * Time.deltaTime));
        alpha.a = Mathf.Lerp(alpha.a,0, Time.deltaTime * alphaspeed);
        text.color = alpha;
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
