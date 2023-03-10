using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // ▲윗쪽의 "MonoBehaviour"가 "Test"와 마찬가지로 청록색으로 바뀌어있는지 확인
    private void Start()
    // ▲윗쪽의 "Start"가 "void"와 마찬가지로 파란색으로 바뀌어있는지 확인, 노란색이면 왼쪽의 private를 지우고 다시 확인
    {
        Debug.Log("Hello world!!!");
    }
    // 색이 바뀌는데 어느정도 시간이 걸릴수 있으며, 5분정도 후에도 바뀌지 않는다면 문의바람
}
