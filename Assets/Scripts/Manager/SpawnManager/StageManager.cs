using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageInfo_so stageInfo_so;

    public List<int> massageBuffer = new List<int>();

    private void Awake()
    {
        //Async_Function();
    }

    private void Update()
    {
        
    }

    public int SearchMassage(int moduleID)
    {
        if (massageBuffer.Count == 0)
            return -1;

        for (int i = 0; i < massageBuffer.Count; i++)
        {
            if (massageBuffer[i] == moduleID)
            {
                int tmp = massageBuffer[i];
                massageBuffer.RemoveAt(i);
                return tmp;
            }
        }

        return -1;
    }
}
