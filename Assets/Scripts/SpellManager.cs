using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SpellManager : MonoBehaviour {
    public GameObject spellCastingPoint;
    public GameObject BlueBlast;
    public GameObject StupefyPrefab;
    public GameObject ProtegoPrefab;
    public GameObject RotationOfProtego;
    public PhotonView photonView;
    public Transform headTransform;
    public SteamVR_Action_Boolean gripPressed;
    public SteamVR_Input_Sources handType;
    public delegate void SpellValueChanged();
    public SpellValueChanged OnSpellValueChanged;
    public bool canCastSpells;
    public bool isLobbyMode;
    [HideInInspector]
    public enum Spells {BLUELIGHTNING,REDLIGHTNING,SHIELD,NULL };
    public Spells bufferedSpell = Spells.NULL;
    [HideInInspector]
    public Information Expelliarmus;
    [HideInInspector]
    public Information Stupefy;
    [HideInInspector]
    public Information Lethal;
    [HideInInspector]
    public bool castExpelliarmus;
    [HideInInspector]
    public bool castStupefy;
    [HideInInspector]
    public bool castLethal;
    [HideInInspector]
    public bool castProtego;
    private ResourceManager resourceManager;
    private void Start()
    {
        Expelliarmus = BlueBlast.GetComponent<Information>();
        Stupefy = StupefyPrefab.GetComponent<Information>();
        castExpelliarmus = false;
        castStupefy = false;
        castLethal = false;
        castProtego = false;
        resourceManager = GetComponent<ResourceManager>();
    }
    
    private void Update()
    {
        if(canCastSpells)
        {
            if(photonView.IsMine || isLobbyMode)
            {
                if(bufferedSpell!=Spells.NULL && gripPressed.GetStateUp(handType))
                {

                    if (bufferedSpell==Spells.BLUELIGHTNING)
                    {
                        CastBlueLightining();
                    }
                    if (bufferedSpell == Spells.SHIELD)
                    {
                        CastShield();
                    } 
                    if (castStupefy == true)
                    {
                        CastStupefy();
                    }
                    
                }
           }    
        }
        
    }
    public void SetBufferedSpell(Spells value)
    {
        bufferedSpell = value;
        OnSpellValueChanged?.Invoke();
    }
    
    [PunRPC]
    public void CastBlueLightining()
    {
        //Instantiate a spell over the network.
        if (resourceManager.mana>= BlueBlast.GetComponent<SB_Spell>().manaCost)
        {
            PhotonNetwork.Instantiate(BlueBlast.name, spellCastingPoint.transform.position, spellCastingPoint.transform.rotation, 0);
            SetBufferedSpell(Spells.NULL);
            resourceManager.ReduceMana(BlueBlast.GetComponent<SB_Spell>().manaCost);
        }
        else
        {
            NotEnoughMana();
        }
       
       
        
    }
    public void CastSpell(Spell spell)
    {

    }
    [PunRPC]
    public void CastStupefy()
    {
        castStupefy = false;
        //Instantiate the spell
        PhotonNetwork.Instantiate("Stupefy", spellCastingPoint.transform.position,
              spellCastingPoint.transform.rotation, 0);
    }
    
    [PunRPC]
    public void CastShield()
    {
        if(resourceManager.mana>=ProtegoPrefab.GetComponent<ShieldManager>().manaCost)
        {
            PhotonNetwork.Instantiate(ProtegoPrefab.name, headTransform.position, RotationOfProtego.transform.rotation, 0);
            SetBufferedSpell(Spells.NULL);
            resourceManager.mana -= ProtegoPrefab.GetComponent<ShieldManager>().manaCost;
        }
        else
        {
            NotEnoughMana();
        }
        

    }
    private void NotEnoughMana()
    {

    }
}
