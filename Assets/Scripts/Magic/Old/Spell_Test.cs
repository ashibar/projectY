using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Test : MonoBehaviour
{
    public delegate int TestFunction_delegate(int value);
    public TestFunction_delegate testFunction;

    public int TestFunction(int value)
    {
        Debug.Log("1," + value);
        return value;
    }

    public int TestFunction2(int value)
    {
        Debug.Log("2," + value + 1);
        return value + 1;
    }

    private void Awake()
    {
        testFunction += TestFunction;
        testFunction += TestFunction2;

        int result = testFunction(2);

        Debug.Log("result = " + result);
    }
}
