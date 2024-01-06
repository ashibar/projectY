using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideShot_Part : Spell_Part
{
    private float sense_range = 10;
    private GameObject closest_obj;

    //public override Quaternion SetAngle(DelegateParameter para)
    //{
    //    Debug.Log("get Angle");
    //    Vector3 dir = SetDir(para);
    //    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //    Quaternion rotation = Quaternion.Euler(0, 0, angle);
    //    return rotation;
    //}
    //protected override void Update()
    //{
    //    targetAngle
    //}

    public override void ShootingFunction(DelegateParameter para)
    {
        base.ShootingFunction(para);
        FindClosest(para);
        Vector3 dir = SetDir(para);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        para.projectile.transform.rotation = rotation;
        para.projectile.GetComponent<Rigidbody2D>().velocity = dir * para.stat_spell.Spell_Speed;
    }

    private void FindClosest(DelegateParameter para)
    {
        Vector3 projpos = para.projectile.transform.position;
        if (UnitManager.Instance != null)
        {
            List<GameObject> clones = UnitManager.Instance.Clones;
            GameObject closest_obj = null;
            float closest_distance = sense_range;
            foreach (GameObject go in clones)
            {
                if (go != null && go.tag == "Enemy")
                {
                    float distance = Vector3.Distance(go.transform.position, projpos);
                    if (distance <= closest_distance && distance <= sense_range)
                    {
                        closest_obj = go;
                        closest_distance = distance;
                    }
                }
            }
            //if (closest_obj != null)
            //    targetAngle = Mathf.Atan2((closest_obj.transform.position - projpos).normalized.y, (closest_obj.transform.position - projpos).normalized.x) * Mathf.Rad2Deg;
            if (closest_obj == null)
            {
                this.closest_obj = null;
            }
            else if (this.closest_obj != closest_obj)
                this.closest_obj = closest_obj;
        }
    }

    private Vector3 SetDir(DelegateParameter para)
    {
        if (closest_obj == null)
            return para.projectile.GetComponent<Rigidbody2D>().velocity.normalized;

        Vector3 current_dir = para.projectile.GetComponent<Rigidbody2D>().velocity.normalized;
        Vector3 dir = (closest_obj.transform.position - para.projectile.transform.position).normalized;
        return Vector3.RotateTowards(current_dir, current_dir + dir, 100 * Time.deltaTime, 0f);        
    }
}
