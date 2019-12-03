using Photon.Pun;
using UnityEngine;
using static GestureController;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private SpellBook spellBook;
    public GameObject spellCastingPoint;

    public PhotonView photonView;
    public Transform headTransform;
    //public SteamVR_Action_Boolean gripPressed;
    //public SteamVR_Input_Sources handType;
    public delegate void SpellValueChanged();
    public SpellValueChanged OnSpellValueChanged;
    public bool canCastSpells;
    public bool isLobbyMode;
    public VRInputModule vRInputModule;
    public Gesture bufferedGesture = Gesture.NONE;
    private GameObject heldSpell;
    private bool heldCasting;
    [SerializeField]
    private ResourceManager resourceManager;
    private void Start()
    {

        
    }

    private void Update()
    {
        if (heldCasting)
        {
            if (vRInputModule.rightController.GetHairTriggerUp())
            {
                SetBufferedGesture(Gesture.NONE);
                heldCasting = false;

                //TODO Give back to pool
                heldSpell?.GetComponent<ShieldManager>().photonView.RPC("TurnOffShield", RpcTarget.AllBufferedViaServer);
                heldSpell = null;
            }
        }

        if (canCastSpells)
        {
            if ((photonView.IsMine || isLobbyMode) && vRInputModule.rightController.GetHairTriggerDown())
            {
                CastSpell(bufferedGesture);
            }
            /*
            if (spellInitiated)
            {
                CastSpell(bufferedGesture);
            }
            */
        }

    }

    [PunRPC]
    public void CastSpell(Gesture gesture)
    { CastSpell((int)gesture); }

    [PunRPC]
    public void CastSpell(int gestureIdx)
    {
        SpellData spellData = spellBook.GetSpellData(gestureIdx);

        if (spellData)
        {
            if (spellData.manaCost < resourceManager.mana)
            {
                GameObject spellInstance = spellBook.CastSpell(gestureIdx, spellCastingPoint.transform.position, spellCastingPoint.transform.rotation);
                resourceManager.ReduceMana(spellBook.GetSpellData(gestureIdx).manaCost);

                Spell spell = spellInstance.GetComponent<Spell>();

                if (spell == null)
                {
                    SetBufferedGesture(Gesture.NONE);
                    return;
                }
                else if (spell.RequiresHeldCast)
                {
                    //TODO disable casting and look for trigger up event to disable spell
                    heldSpell = spellInstance;
                    heldCasting = true;
                }
            }
            else
                NotEnoughMana();
        }
        else
            Debug.LogWarning("No spell data found for " + ((Gesture)gestureIdx).ToString() + " gesture!");
    }

    public void SetBufferedGesture(Gesture value)
    {
        bufferedGesture = value;
        OnSpellValueChanged?.Invoke();
    }

    [PunRPC]
    public void CastShield()
    {
        /*
        if (resourceManager.mana >= ProtegoPrefab.GetComponent<ShieldManager>().manaCost && !spellInitiated)
        {
            spellInitiated = true;
            currentShield = PhotonNetwork.Instantiate(ProtegoPrefab.name, spellCastingPoint.transform.position, RotationOfProtego.transform.rotation, 0);
            resourceManager.mana -= ProtegoPrefab.GetComponent<ShieldManager>().manaCost;
        }
        if (vRInputModule.rightController.GetHairTriggerUp())
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
        */

    }
    private void NotEnoughMana()
    {

    }
}
