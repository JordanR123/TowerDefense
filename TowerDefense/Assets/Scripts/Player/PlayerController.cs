using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float gravity = -20f;
    public Camera cam;

    private CharacterController cc;
    private Vector3 velocity;

    private void Awake() { cc = GetComponent<CharacterController>(); }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(h, 0, v).normalized;

        // camera-relative movement
        Vector3 fwd = cam.transform.forward; fwd.y = 0; fwd.Normalize();
        Vector3 right = cam.transform.right; right.y = 0; right.Normalize();
        Vector3 move = (fwd * input.z + right * input.x) * moveSpeed;

        if (cc.isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;

        cc.Move((move + velocity) * Time.deltaTime);
    }
}
