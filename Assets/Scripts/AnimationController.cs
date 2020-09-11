using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Randomly blink
/// </summary>
public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public float threshold = 0.75f;
    private float time;

    // Private; edit if adding more animation params.
    readonly int CLOSE_EYES_PARAM = Animator.StringToHash("closeEyes");

    private void Update()
    {
        // if passed time since we last set animator is more than threshold
        if (time > threshold)
        {
            if (Random.value > 0.95)
            {
                animator.SetTrigger(CLOSE_EYES_PARAM);
                time = 0;
            }
        }
        time += Time.deltaTime;
    }
}
