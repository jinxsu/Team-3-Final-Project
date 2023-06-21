using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor;
using System.IO;

public class InputManager : MonoBehaviour
{
    public static PlayerControls inputActions;
    public static bool editingKeyboardControls;

    public static bool yInvert = false;
    public static bool xInvert = false;

    public static event Action rebindComplete;
    public static event Action rebindCanceled;
    public static event Action<InputAction, int> rebindStarted;


    public static void ToggleYInvert(bool toggleState)
    {
        yInvert = toggleState;
        PlayerPrefs.SetInt("YInvert", (toggleState ? 1 : 0));
    }

    public static void ToggleXInvert(bool toggleState)
    {
        xInvert = toggleState;
        PlayerPrefs.SetInt("XInvert", (toggleState ? 1 : 0));
    }


    private void Awake()
    {

        if (!PlayerPrefs.HasKey("knmSens"))
        {
            PlayerPrefs.SetInt("knmSens", 100);
        }
        
        if (!PlayerPrefs.HasKey("ctrSens"))
        {
            PlayerPrefs.SetInt("ctrSens", 150);
        }

        if (inputActions== null)
        {
            inputActions = new PlayerControls();
        }
        DontDestroyOnLoad(this.gameObject);

        yInvert = (PlayerPrefs.GetInt("YInvert") != 0);
        xInvert = (PlayerPrefs.GetInt("XInvert") != 0);
    }


    private void Update()
    {
        if (inputActions != null)
        {
            Debug.Log(inputActions.ToString());
        }
        else
        {
            Debug.Log("I stopped existing");
        }
    }

    public static void StartRebind(string actionName, int bindingIndex, TextMeshProUGUI statusText, bool excludeMouse)
    {
        InputAction action = inputActions.asset.FindAction(actionName);
        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Couldn't find action or binding!");
            return;
        }

        DoRebind(action, bindingIndex,statusText, false, excludeMouse);
    }

    private static void DoRebind(InputAction actionToRebind, int bindingIndex, TextMeshProUGUI statusText, bool allCompositeParts, bool excludeMouse)
    {
        if (actionToRebind == null || bindingIndex < 0)
        {
            return;
        }

        statusText.text = $"Press a {actionToRebind.expectedControlType}";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            SaveBindingOverride(actionToRebind);
            rebindComplete?.Invoke();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();
            rebindCanceled?.Invoke();
        });

        if(editingKeyboardControls)
        {
            rebind.WithCancelingThrough("<Keyboard>/escape");
        }
        else
        {
            rebind.WithCancelingThrough("<Gamepad>/start");
        }

        if(excludeMouse)
        {
            rebind.WithControlsExcluding("Mouse");
        }

        rebindStarted?.Invoke(actionToRebind, bindingIndex);
        rebind.Start();
    }

    public static string GetBindingName(string ActionName, int bindingIndex)
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
        }
        InputAction action = inputActions.asset.FindAction(ActionName);
        return action.GetBindingDisplayString(bindingIndex);
    }

    private static void SaveBindingOverride(InputAction action)
    {
        
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }

    public static void LoadBindingOverride(string actionName)
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
        }

        InputAction action = inputActions.asset.FindAction(actionName);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
            {
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
            }
        }
    }


    public static void ResetBinding(string actionName, int bindingIndex)
    {
        InputAction action = inputActions.asset.FindAction(actionName);

        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Could not find action or binding");
            return;
        }

        action.RemoveBindingOverride(bindingIndex);

        SaveBindingOverride(action);
    }
}
