using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelay : MonoBehaviour
{
    Animator animator;
    public Animation gestureAnimation;
    public Animation noAnimation;
    float nextTimeToChangeMode;
    float intervalBetweenModes=2f;
    public ParticleSystem ps;
    bool isAnimationPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nextTimeToChangeMode = Time.time + intervalBetweenModes;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("GestureAnimation") && this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.97f)
        {
           
                var em = ps.emission;
                em.enabled = false;






        }
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("GestureAnimation") && this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.03f)
        {

            var em = ps.emission;
            em.enabled = true;



        }
        

    }
    private void ChangeMode()
    {
        nextTimeToChangeMode = Time.time + intervalBetweenModes;
        if(isAnimationPlaying)
        {
            isAnimationPlaying = false;
           // particles.SetActive(false);
        }
        else
        {
            isAnimationPlaying = true;
           // particles.SetActive(true);
        }
    }
}
