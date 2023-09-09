using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EmotionEffect : MonoBehaviour
{
    [SerializeField] private List<GameObject> origins = new List<GameObject>();
    [SerializeField] private List<GameObject> clones = new List<GameObject>();

    public void MakeEffect(Transform target, int no, bool isLeft = false)
    {
        if (origins.Count <= no) return;

        Vector2 offset = new Vector2(isLeft ? -0.4f : 0.4f, 0.5f);
        
        GameObject clone = Instantiate(origins[no], target.position + (Vector3)offset, Quaternion.identity, target);
        clone.GetComponent<SpriteRenderer>().flipX = isLeft;
        clones.Add(clone);        
        StartCoroutine(Blink(clone));
    }

    private IEnumerator Blink(GameObject clone)
    {
        for (int i = 0; i < 3; i++)
        {
            clone.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            clone.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        clone.SetActive(false);
        if (clone != null)
            Destroy(clone);
    }
}
