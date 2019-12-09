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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CastSpell((Gesture)0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            CastSpell((Gesture)1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            CastSpell((Gesture)2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            CastSpell((Gesture)3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            CastSpell((Gesture)4);


        if (heldCasting)
        {
            if (vRInputModule.rightController.GetHairTriggerUp() || Input.GetKeyUp(KeyCode.Alpha5))
            {
                SetBufferedGesture(Gesture.NONE);
                heldCasting = false;

                //TODO Give back to pool
                heldSpell?.GetComponent<Spell>().photonView.RPC("OnSpellReleased", RpcTarget.AllBufferedViaServer);
                heldSpell = null;
            }
        }

        if (canCastSpells && bufferedGesture != Gesture.NONE)
        {
            if ((photonView.IsMine || isLobbyMode) && vRInputModule.rightController.GetHairTriggerDown())
            {
                CastSpell(bufferedGesture);
            }
        }

    }

    [PunRPC]
    public void CastSpell(Gesture gesture)
    { CastSpell((int)gesture); }

    [PunRPC]
    public void CastSpell(int gestureIdx)
    {
        if ((Gesture)gestureIdx == Gesture.NONE)
            return;

        SpellData spellData = spellBook.GetSpellData(gestureIdx);

        if (spellData)
        {
            if (spellData.manaCost < resourceManager.mana)
            {
                //TODO add support for canChargeOnCast spells

                GameObject spellInstance = spellBook.CastSpell(gestureIdx, spellCastingPoint.transform.position, spellCastingPoint.transform.rotation);
                resourceManager.ReduceMana(spellBook.GetSpellData(gestureIdx).manaCost);
                bufferedGesture = Gesture.NONE;
                Spell spell = spellInstance.GetComponent<Spell>();

                if (spell && spell.RequiresHeldCast)
                {
                    //TODO disable casting and look for trigger up event to disable spell
                    heldSpell = spellInstance;
                    heldCasting = true;
                }
                else
                {
                    SetBufferedGesture(Gesture.NONE);
                    return;
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


    private void NotEnoughMana()
    {

    }
}