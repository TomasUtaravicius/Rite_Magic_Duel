using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarLocomotionController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        if(x!=0f||y!=0f)
        {
            animator.SetBool("VRIK_IsMoving", true);
        }
        else
        {
            animator.SetBool("VRIK_IsMoving", false);
        }
        Move(x, y);
    }
    private void Move(float x, float y)
    {
        animator.SetFloat("VRIK_Horizontal", x);
        animator.SetFloat("VRIK_Vertical", y);
       
    }
}
