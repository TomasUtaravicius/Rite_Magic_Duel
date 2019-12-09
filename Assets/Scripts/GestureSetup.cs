using UnityEngine;

public class GestureSetup : MonoBehaviour
{
    [SerializeField] public string LoadGesturesFile;
    [SerializeField] public string SaveGesturesFile;
    [SerializeField] public SpellManager spellManager;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetUp", 1f);
    }
    void SetUp()
    {
        GestureController gc = this.gameObject.AddComponent<GestureController>();
        gc.LoadGesturesFile = LoadGesturesFile;
        gc.SaveGesturesFile = SaveGesturesFile;
        gc.spellManager = spellManager;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
