using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //usable outputs:
    public Vector2 playerDirection;
    bool isJumpPressed;
    bool isCoverPressed;
    bool isInteractPressed;
    bool isSprintPressed;
    bool isShootPressed;
    bool isGamePaused = false;
    bool pauseIsPressable = true; //this is necessary to help the start button act like there is a OnKeyDown function

    //Intakes
    float jumpPressed;
    float coverPressed;
    float interactPressed;
    float sprintPressed;
    float shootPressed;
    float pausePressed;

    //inputaction declarations
    public InputAction playerControls;
    public InputAction playerJump;
    public InputAction playerCover;
    public InputAction playerInteract;
    public InputAction playerSprint;
    public InputAction playerShoot;
    public InputAction pauseStartButton;

    //must enable each control
    private void OnEnable()
    {
        playerControls.Enable();
        playerJump.Enable();
        playerCover.Enable();
        playerInteract.Enable();
        playerSprint.Enable();
        playerShoot.Enable();
        pauseStartButton.Enable();
    }

    //must disable each control
    private void OnDisable()
    {
        playerControls.Disable();
        playerJump.Disable();
        playerCover.Disable();
        playerInteract.Disable();
        playerSprint.Disable();
        playerShoot.Disable();
        pauseStartButton.Disable();
    }

    void Update()
    {
        PlayerDirectionInput();
        JumpInput();
        PlayerInteract();
        PlayerCover();
        PlayerSprint();
        PlayerShoot();
        PauseGame();
    }

    void PlayerDirectionInput()
    {
        playerDirection = playerControls.ReadValue<Vector2>();

    }

    public Vector2 GetPlayerDirectionInput()
    {
        return playerDirection;
    }

    void PlayerShoot()
    {
        shootPressed = playerShoot.ReadValue<float>();

        if (shootPressed > 0.1f)
        {
            isShootPressed = true;
        }
        else
        {
            isShootPressed = false;
        }
    }

    public bool GetShootPressed()
    {
        return isShootPressed;
    }

    void JumpInput()
    {
        jumpPressed = playerJump.ReadValue<float>();

        if (jumpPressed > 0.1)
        {
            isJumpPressed = true;
        }
        else
        {
            isJumpPressed = false;
        }
    }

    public bool GetJumpPressed()
    {
        return isJumpPressed;
    }

    void PlayerInteract()
    {
        interactPressed = playerInteract.ReadValue<float>();
        if (interactPressed > 0.1)
        {
            isInteractPressed = true;
        }
        else
        {
            isInteractPressed = false;
        }
    }

    public bool GetInteractPressed()
    {
        return isInteractPressed;
    }

    void PlayerCover()
    {
        coverPressed = playerCover.ReadValue<float>();
        if (coverPressed > 0.1)
        {
            isCoverPressed = true;
        }
        else
        {
            isCoverPressed = false;
        }
    }

    public bool GetCoverPressed()
    {
        return isCoverPressed;
    }

    void PlayerSprint()
    {
        sprintPressed = playerSprint.ReadValue<float>();

        if (sprintPressed > 0.1f)
        {
            isSprintPressed = true;
        }
        else
        {
            isSprintPressed = false;
        }
    }

    public bool GetSprintPressed()
    {
        return isSprintPressed;
    }

    void PauseGame()
    {
        pausePressed = pauseStartButton.ReadValue<float>();

        if (pauseStartButton.ReadValue<float>() == 0f)
        {
            pauseIsPressable = true;
        }

        if (pausePressed > 0.1f && pauseIsPressable)
        {
            if (!isGamePaused)
            {
                isGamePaused = true;
                pauseIsPressable = false;

            }
            else if (isGamePaused)
            {
                isGamePaused = false;
                pauseIsPressable = false;
            }
        }

    }

    public bool GetPauseGame()
    {
        return isGamePaused;
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        pauseIsPressable = true;
    }

}


