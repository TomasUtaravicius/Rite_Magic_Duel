using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayer : MonoBehaviour {

    public HealthManager healthManager;
    public AvatarStateController avatarStateController;
    public SpawnInfo spawnInfo;
    public string name;
    public int score = 0;
    public int photonID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
