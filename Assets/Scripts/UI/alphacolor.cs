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
    //�ӵ��� �ѹ����ϸ� �� ������¤��Ű��v����
    //��

    // Update is called once per frame
    void Update()
    {

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaspeed);

        textMeshPro.color = alpha;
    }
}
