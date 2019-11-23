using Photon.Pun;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    
    public GameObject cameraObject;
    public PhotonView photonView;
    public Transform spellCastingPoint;
    public GameObject spell;
    public SpellManager sManager;
    private Vector2 rotation = new Vector2(0, 0);
    public float speed = 3;
    public bool enableMouseControl;
    public bool enableShootSpells;
    public bool lobbyMode;
    public bool testMode;

    private void Update()
    {
        if (testMode)
        {
            if (enableShootSpells)
            {
                if (photonView.IsMine)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        sManager.CastBlueBlast();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        sManager.CastShield();
                    }
                }
            }

            if (photonView.IsMine || lobbyMode)
            {
                var x = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
                var z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    transform.Translate(0, 2f, 0);
                }
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cameraObject.transform.localEulerAngles.y, transform.localEulerAngles.z);
                transform.Translate(new Vector3(x, 0f, z));
                if (enableMouseControl)
                {
                    rotation.y += Input.GetAxis("Mouse X");
                    rotation.x += -Input.GetAxis("Mouse Y");
                    transform.eulerAngles = (Vector2)rotation * speed;
                }
            }
        }
    }

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}