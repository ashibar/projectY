using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Spell_Test : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform a;
    [SerializeField] private Transform b;

    [SerializeField] private List<Sprite> spritelist;

    private int currentSpriteIndex = 0;  // 현재 사용 중인 스프라이트의 인덱스
    public float changeInterval = 0.3f;  // 스프라이트 변경 간격 (초 단위)
    private float timer = 0.0f;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        
        if (a != null && b != null)
        {
            lr.SetPosition(0, a.position);
            lr.SetPosition(1, b.position);
            //lr.material.SetTexture("_MainTex", spritelist[0].texture);

            timer += Time.deltaTime;

            if (timer >= changeInterval)
            {
                // 스프라이트 변경 간격에 도달했을 때
                timer = 0.0f;  // 타이머 초기화

                // 현재 사용 중인 스프라이트를 변경
                currentSpriteIndex++;

                // 스프라이트 리스트의 끝에 도달하면 처음 스프라이트로 돌아감
                if (currentSpriteIndex >= spritelist.Count)
                {
                    currentSpriteIndex = 0;
                }

                // 스프라이트를 변경된 스프라이트로 설정
                ChangeSprite(currentSpriteIndex);
            }
        }

        void ChangeSprite(int index)
        {
            if (spritelist.Count > 0 && index >= 0 && index < spritelist.Count)
            {
                lr.material.SetTexture("_MainTex", spritelist[index].texture);
            }
        }
    }
}
