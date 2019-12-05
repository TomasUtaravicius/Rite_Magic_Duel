using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlotAnimator : MonoBehaviour
{
    Animator animator;

    public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("GestureAnimation") && this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.97f)
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
  
}
