using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public InputActionReference triggerLeft;
    public InputActionReference triggerRight;

    void Update() {

        if (triggerLeft.action.triggered || triggerRight.action.triggered) {
            SceneManager.LoadScene("Scenes/MainScene");
        }
    }
}
