using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelay : MonoBehaviour
{
    Animator animator;

    public ParticleSystem ps;
    bool isAnimationPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
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
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            animator.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.enabled = false;
        }
    }


}
