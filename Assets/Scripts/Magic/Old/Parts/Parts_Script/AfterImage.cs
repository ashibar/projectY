using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    [SerializeField] private GameObject origin;
    [SerializeField] private GameObject husk;
    [SerializeField] private float duration = 0.02f;
    [SerializeField] private bool isActive;
    [SerializeField] private bool flipX;

    public bool IsActive { get => isActive; set => isActive = value; }


    public void SetImage(GameObject _origin, bool flipX = false)
    {
        origin = _origin;
        this.flipX = flipX;
    }

    private bool isCooltime;
    private void Update()
    {
        if (isActive && !isCooltime)
        {
            StartCoroutine(Loop(duration));
        }
    }

    private IEnumerator Loop(float duration)
    {
        isCooltime = true;
        GameObject clone = Instantiate(husk, origin.transform.position, Quaternion.identity, Holder.projectile_holder);
        clone.GetComponent<SpriteRenderer>().sprite = origin.GetComponent<SpriteRenderer>().sprite;
        clone.GetComponent<SpriteRenderer>().flipX = flipX;
        clone.transform.localScale = origin.transform.localScale;
        yield return new WaitForSeconds(duration);
        isCooltime = false;
    }
}
