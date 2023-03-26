using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour
{
    [SerializeField] float moveSpeed;

    [Header("Viewport Padding")]
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitializeBounds();
    }

    void Update()
    {
        if (!IsOwner) { return; }
        if (!IsLocalPlayer) { return; }
        if (!Application.isFocused) { return; }

        Move();
        Debug.Log($"Player {OwnerClientId} is Owner = {IsOwner}");
    }

    void InitializeBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Move()
    {
        Debug.Log($"Currently moving with {rawInput} input");
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();

        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        Debug.Log("New Position: " + newPos);

        MoveServerRpc(newPos);
    }

    [ServerRpc(RequireOwnership = false)]
    void MoveServerRpc(Vector2 newPos)
    {
        transform.position = newPos;
        DebugLogger(newPos);
    }

    void DebugLogger(Vector2 newPos)
    {
        Debug.Log($"Move RPC called: moving to {newPos}");
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log($"Raw Input: {rawInput}");
    }

    void OnFire(InputValue value)
    {
        if (shooter == null) { return; }

        shooter.isFiring = value.isPressed;
    }
}
