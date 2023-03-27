using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    PlayerInputActions playerInputActions;

    public event Action OnFirePerformed;
    public event Action OnFireCanceled;
    public event Action OnFireSecondaryPerformed;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();

        playerInputActions.Player.Fire.performed += Fire_performed;
        playerInputActions.Player.Fire.canceled += Fire_canceled;
        playerInputActions.Player.FireSecondary.performed += FireSecondary_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Fire.performed -= Fire_performed;
        playerInputActions.Player.Fire.canceled -= Fire_canceled;
        playerInputActions.Player.FireSecondary.performed -= FireSecondary_performed;

        playerInputActions.Dispose();
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnFirePerformed?.Invoke();
    }

    private void Fire_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnFireCanceled?.Invoke();
    }

    private void FireSecondary_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnFireSecondaryPerformed?.Invoke();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
