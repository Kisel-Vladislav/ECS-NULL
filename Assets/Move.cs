using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController _characterController;

    void Update()
    {
        _characterController.Move(new Vector3(Input.GetAxisRaw("Vertical"), 0f, Input.GetAxisRaw("Vertical")));
    }
}
