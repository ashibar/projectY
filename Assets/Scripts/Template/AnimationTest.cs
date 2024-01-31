using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationTest : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI log;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Time.timeScale = 4f * slider.value;
        log.text = string.Format("x{0}", 4f * slider.value);

        if (dir.sqrMagnitude > 0)
        {
            if (x > 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);

            animator.SetBool("isWalk", true);
            Vector3 pos = transform.position;
            transform.position = Vector3.MoveTowards(pos, pos + (Vector3)dir, Time.deltaTime * 3);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }
}
