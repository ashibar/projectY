using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class alphacolor : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    [SerializeField]
    private float alphaspeed;
    Color alpha;
    // Start is called before the first frame update
    void Start()
    {
        
        textMeshPro = GetComponent<TextMeshProUGUI>();
        alpha = textMeshPro.color;
    }
    //속도는 한번정하면 머 상관업승ㄹ거가틍ㄴ데
    //예

    // Update is called once per frame
    void Update()
    {

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaspeed);

        textMeshPro.color = alpha;
    }
}
