using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions {
    private GameInput _gameInput;

    private void OnEnable() {
        if (_gameInput == null) {
            _gameInput = new GameInput();

            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);

            SetGamePlay();
        }
    }

    public void SetGamePlay() {
        _gameInput.Gameplay.Enable();
        _gameInput.UI.Disable();
    }

    public void setUI() {
        _gameInput.UI.Enable();
        _gameInput.Gameplay.Disable();
    }

    public event Action<Vector2> MoveEvent;

    public event Action JumpEvent;
    public event Action JumpCancelledEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;


    public void OnMove(InputAction.CallbackContext context) {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context) {
        switch (context.phase) {
            case InputActionPhase.Performed:
                JumpEvent?.Invoke();
                break;
            case InputActionPhase.Canceled:
                JumpCancelledEvent?.Invoke();
                break;
        }
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            PauseEvent?.Invoke();
            setUI();
        }
    }

    public void OnResume(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            ResumeEvent?.Invoke();
            SetGamePlay();
        }
    }
}