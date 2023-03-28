using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private List<GameObject> spell_object;
    [SerializeField]
    private List<ShotManage> shotManager;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponentInParent<Player>();
        shotManager.AddRange(GetComponentsInChildren<ShotManage>());
    }

    // Update is called once per frame
    private void Update()
    {
        foreach(ShotManage manager in shotManager)
        {
            manager.dir_toMouse = player.dir_toShoot;
        }
    }
}
