using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour {

    #region Singleton

    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    #endregion

    #region Input Values

    public Vector2 mouse;
    public bool mouseLeftHold;
    public bool mouseLeftClick;
    public bool mouseRight;
    public Vector2 movement;
    public bool shift;

    #endregion

    #region Input Functions

    public void OnMouse(InputAction.CallbackContext context) { mouse = context.ReadValue<Vector2>(); }
    public void OnMouseLeftHold(InputAction.CallbackContext context) { mouseLeftHold = context.ReadValueAsButton(); }
    public void OnMouseLeftClick(InputAction.CallbackContext context) { mouseLeftClick = context.ReadValueAsButton(); }
    public void OnMouseRight(InputAction.CallbackContext context) { mouseRight = context.ReadValueAsButton(); }
    public void OnMove(InputAction.CallbackContext context) { movement = context.ReadValue<Vector2>(); }
    public void OnShift(InputAction.CallbackContext context) { shift = context.ReadValueAsButton(); }

    #endregion

}