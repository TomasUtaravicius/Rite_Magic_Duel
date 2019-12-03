using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TeaCupScript : MonoBehaviour
{
    [Range(0, 5)] private int teaness = 0;
    [SerializeField] Color currentColor;

    private Color originalColor;

    private void Start()
    {
        originalColor = GetComponent<MeshRenderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("TeaBag"))
        {
            Debug.Log("It's a bag");
        }
    }

}
