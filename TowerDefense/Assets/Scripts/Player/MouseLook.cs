using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Refs")]
    public Transform playerRoot;   // the object you move (your Player capsule)
    public Transform cam;          // the camera (PlayerCamera)

    [Header("Settings")]
    public float sensitivity = 120f;   // try 120–250
    public float minPitch = -75f;
    public float maxPitch = 75f;
    public bool lockCursor = true;     // lock/hide cursor in play mode
    public KeyCode toggleCursorKey = KeyCode.Escape;

    float pitch; // up/down angle

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // initialize pitch from current camera rotation
        if (cam != null)
        {
            Vector3 e = cam.localEulerAngles;
            // Convert 0..360 to -180..180 for clamping
            pitch = (e.x > 180f) ? e.x - 360f : e.x;
        }
    }

    void Update()
    {
        // allow toggling cursor for UI/debug
        if (Input.GetKeyDown(toggleCursorKey))
        {
            bool currentlyLocked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = currentlyLocked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !currentlyLocked;
        }

        if (Cursor.lockState != CursorLockMode.Locked) return;

        // mouse delta (old input system; works if Active Input Handling = Both)
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        // yaw: rotate the player horizontally
        if (playerRoot != null && Mathf.Abs(mouseX) > 0.0001f)
            playerRoot.Rotate(Vector3.up, mouseX, Space.Self);

        // pitch: rotate the camera up/down (local)
        if (cam != null && Mathf.Abs(mouseY) > 0.0001f)
        {
            pitch -= mouseY;                         // invert so moving mouse up looks up
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            Vector3 e = cam.localEulerAngles;
            cam.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }
}
