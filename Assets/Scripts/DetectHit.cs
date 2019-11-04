using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHit : MonoBehaviour {

    public ResourceManager hManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OffensiveSpell")
        {
            Debug.Log("Debug detect hit script");
            hManager.TakeDamage(other.gameObject.GetComponent<Information>().damage);
        }
    }
}
