using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellManager : MonoBehaviour {
    public GameObject spellCastingPoint;
    public GameObject BlueBlast;
    public GameObject ChargedPlasma;
    public GameObject FireBolt;
    public GameObject DarknessBlast;
    public GameObject ProtegoPrefab;
    public GameObject RotationOfProtego;
    public PhotonView photonView;
    public Transform headTransform;
    //public SteamVR_Action_Boolean gripPressed;
    //public SteamVR_Input_Sources handType;
    public delegate void SpellValueChanged();
    public SpellValueChanged OnSpellValueChanged;
    public bool canCastSpells;
    public bool isLobbyMode;
    public VRInputModule vRInputModule;
    [HideInInspector]
    public enum Spells {BLUELIGHTNING,CHARGEDPLASMA,SHIELD,FIREBOLT,DARKNESSBLAST,NULL };
    public Spells bufferedSpell = Spells.NULL;
    private GameObject currentShield;
    private bool spellInitiated;
    private ResourceManager resourceManager;
    private void Start()
    {
        vRInputModule = GameObject.Find("PR_VRInputModule").GetComponent<VRInputModule>();
        resourceManager = GetComponent<ResourceManager>();
    }
    
    private void Update()
    {
        if(canCastSpells)
        {
            if((photonView.IsMine ||isLobbyMode) && vRInputModule.rightController.GetHairTriggerDown())
            {
               
                    if (bufferedSpell == Spells.BLUELIGHTNING)
                    {
                        CastBlueBlast();
                    }
                    if (bufferedSpell == Spells.SHIELD)
                    {
                        CastShield();
                    }


           }    
            if(spellInitiated)
            {
                if (bufferedSpell == Spells.BLUELIGHTNING)
                {
                    CastBlueBlast();
                }
                if (bufferedSpell == Spells.SHIELD)
                {
                    CastShield();
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
    public void CastBlueBlast()
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
    public void CastChargedPlasma()
    {
        //Instantiate a spell over the network.
        if (resourceManager.mana >= BlueBlast.GetComponent<SB_Spell>().manaCost)
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
    public void CastFireBolt()
    {
        //Instantiate a spell over the network.
        if (resourceManager.mana >= BlueBlast.GetComponent<SB_Spell>().manaCost)
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
    public void CastDarknessBlast()
    {
        //Instantiate a spell over the network.
        if (resourceManager.mana >= BlueBlast.GetComponent<SB_Spell>().manaCost)
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
    public void CastShield()
    {
        
        if(resourceManager.mana>=ProtegoPrefab.GetComponent<ShieldManager>().manaCost && !spellInitiated)
        {
            spellInitiated = true;
            currentShield=PhotonNetwork.Instantiate(ProtegoPrefab.name, spellCastingPoint.transform.position, RotationOfProtego.transform.rotation, 0);
            resourceManager.mana -= ProtegoPrefab.GetComponent<ShieldManager>().manaCost;
        }
        if(vRInputModule.rightController.GetHairTriggerUp())
        {
            if (!currentShield)
            {
                SetBufferedSpell(Spells.NULL);
                spellInitiated = false;
                currentShield = null;
            }
            else
            {
                SetBufferedSpell(Spells.NULL);
                //currentShield.GetComponent<ShieldManager>().TurnOffShield();

                currentShield.GetComponent<ShieldManager>().photonView.RPC("TurnOffShield", RpcTarget.AllBufferedViaServer);
                spellInitiated = false;
                currentShield = null;
            }
                
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
