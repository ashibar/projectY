using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellExplainText : MonoBehaviour
{
    private static SpellExplainText instance;
    public static SpellExplainText Instance
    {
        get
        {
            if (instance == null) // instance가 비어있다
            {
                var obj = FindObjectOfType<SpellExplainText>(true);
                if (obj != null)
                {
                    instance = obj;                                             // 전체 찾아봤는데? 있네? 그걸 넣자
                }
                else
                {
                    var newObj = new GameObject().AddComponent<SpellExplainText>(); // 전체 찾아봤는데? 없네? 새로만들자
                    instance = newObj;
                }
            }
            return instance; // 안비어있네? 그냥 그대로 가져와
        }
    }
    private void Awake()
    {
        var objs = FindObjectsOfType<SpellExplainText>(true);
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }
}
