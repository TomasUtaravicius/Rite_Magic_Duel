using UnityEngine;

public class NetworkedPlayer : MonoBehaviour
{
    public ResourceManager healthManager;
    public AvatarStateController avatarStateController;
    public string name;
    public int score = 0;
    public int photonID;

}
