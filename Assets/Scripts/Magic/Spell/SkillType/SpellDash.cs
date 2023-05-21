using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/**
 *  
    * �ۼ��� : ������
    * ���� �ð� : 23/5/11 15:14   
    * �ۼ� ���� : �뽬���
    * ���� ���� : Unit�� stat�� speed�� ������(�ð��� ��������) ������� �뽬��� ����
        ������� ���� ���߿� ������ ����.
    * ���� ������ : Movement�� Speed�� �������� ������� ���⿡ ������ ����� Ȯ�� �Ұ�.
 */
public class SpellDash : MonoBehaviour
{
    [SerializeField] Unit unit;
    [SerializeField] BoxCollider2D razer;
    [SerializeField] float delay,duration;
    [SerializeField] float razerSize_x, razerSize_y;

    private void Start()
    {
        unit = GetComponentInParent<Unit>();
        razer = GetComponent<BoxCollider2D>();
        
    }
    private IEnumerator RazerStart()
    {
        yield return new WaitForSeconds(delay);
        razer.size = new Vector2(razerSize_x, razerSize_y);
        yield return new WaitForSeconds(duration);
    }



}
