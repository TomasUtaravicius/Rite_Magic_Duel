using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SpellManager : MonoBehaviour {
    public GameObject spellCastingPoint;
    public GameObject ExpelliarmusPrefab;
    public GameObject StupefyPrefab;
    public GameObject LethalPrefab;
    public GameObject ProtegoPrefab;
    public GameObject RotationOfProtego;
    public PhotonView photonView;
    public Transform headTransform;
    public SteamVR_Action_Boolean gripPressed;
    public SteamVR_Input_Sources handType;
    public delegate void SpellValueChanged();
    public SpellValueChanged OnSpellValueChanged;
    public bool canCastSpells;
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
        Expelliarmus = ExpelliarmusPrefab.GetComponent<Information>();
        Stupefy = StupefyPrefab.GetComponent<Information>();
        Lethal = LethalPrefab.GetComponent<Information>();
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
            if(photonView.IsMine)
            {
                if(bufferedSpell!=Spells.NULL && gripPressed.GetStateDown(handType))
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
                    if (castLethal == true)
                    {
                        CastLethal();
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
        //Instatiate a spell over the network.
        if(resourceManager.mana>=Expelliarmus.manaCost)
        {
            PhotonNetwork.Instantiate(ExpelliarmusPrefab.name, spellCastingPoint.transform.position, spellCastingPoint.transform.rotation, 0);
            SetBufferedSpell(Spells.NULL);
            resourceManager.mana -= Expelliarmus.manaCost;
        }
        else
        {
            NotEnoughMana();
        }
       
       
        
    }
    [PunRPC]
    public void CastStupefy()
    {
        castStupefy = false;
        //Instantiate the spell
        PhotonNetwork.Instantiate("Stupefy", spellCastingPoint.transform.position,
              spellCastingPoint.transform.rotation, 0);
    }
    public void CastLethal()
    {
        castLethal = false;
        //Instantiate the spell
        var spell = (GameObject)Instantiate(
            LethalPrefab,
            spellCastingPoint.transform.position,
            spellCastingPoint.transform.rotation);
        //Fire it
        spell.GetComponent<Rigidbody>().velocity = spell.transform.forward * Lethal.speed;
        // Destroy
        Destroy(spell, 4.0f);
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
