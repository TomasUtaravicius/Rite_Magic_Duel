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
    private bool isCastingHeld;
    [SerializeField]
    private ResourceManager resourceManager;

    private void Update()
    {
        if (isCastingHeld)
        {
            if (vRInputModule.rightController.GetHairTriggerUp())
            {
                isCastingHeld = false;
                SetBufferedGesture(Gesture.NONE);
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
                GameObject spellInstance = spellBook.ConstructSpell(gestureIdx, 
                                                                    spellCastingPoint.transform.position, 
                                                                    spellCastingPoint.transform.rotation);

                resourceManager.ReduceMana(spellBook.GetSpellData(gestureIdx).manaCost);
                Spell spell = spellInstance.GetComponent<Spell>();

                if (spell && spell.RequiresHeldCast)
                {
                    heldSpell = spellInstance;
                    isCastingHeld = true;
                }
                else
                    SetBufferedGesture(Gesture.NONE);
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
        //TODO no implementation!
    }
}