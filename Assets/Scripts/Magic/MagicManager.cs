using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    private Unit unit;
    [SerializeField]
    private List<GameObject> spell_object;
    [SerializeField]
    private List<ShotManage> shotManager;
    [SerializeField] string target;

    // Start is called before the first frame update
    private void Start()
    {
        unit = GetComponentInParent<Unit>();
        //Mob = GetComponentInParent<Mob>();
        //���� GetComponent�� �����´�.
        //�� ���� ���̳� �÷��̾��ʿ��� ���ֿ� ������ Ư�� ID�� ) ex. UnitType�� �Ǵ��� Update������ ����
        shotManager.AddRange(GetComponentsInChildren<ShotManage>());
        foreach(ShotManage manager in shotManager)
        {
            manager.owner = unit;
            manager.target = target;
        }
    }

    // Update is called once per frame
    private void Update()
    {   
 
        foreach(ShotManage manager in shotManager)
        {
            manager.dir_toMove = unit.dir_toMove;
            manager.dir_toShoot = unit.dir_toShoot;
            manager.dir_toMove = unit.dir_toMove;
        }
    }
}
