using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] MoveScript playerMove;
    [SerializeField] Transform aimArm;
    [SerializeField] PauseMenu pauseMenu;
    float flipAim; //handles flipping the player's arm

    private void Update()
    {
        if (pauseMenu.GetGamePaused() == false)
        {
            Flip();
            Aim();
        }
    }

    void Aim()
    {

        if (Mathf.Abs(playerInput.GetPlayerDirectionInput().y) < 0.75f)
        {
            Quaternion aimProcessor = Quaternion.Euler(0f, flipAim, playerInput.GetPlayerDirectionInput().y * 45f);
            aimArm.rotation = aimProcessor;
        }
        else if (Mathf.Abs(playerInput.GetPlayerDirectionInput().y) > 0.75f)
        {
            Quaternion aimProcessor = Quaternion.Euler(0f, flipAim, playerInput.GetPlayerDirectionInput().y * 90f);
            aimArm.rotation = aimProcessor;
        }
    }

    void Flip()
    {
        if (playerMove.GetFlipped())
        {
            flipAim = 180f;
        }
        else if (!playerMove.GetFlipped())
        {
            flipAim = 0f;
        }
    }


}
