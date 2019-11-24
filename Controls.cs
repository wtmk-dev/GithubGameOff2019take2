// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""PlayerKit"",
            ""id"": ""449678a2-c1e6-4770-bc71-2d88994b79c8"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""75c41684-6840-413c-a831-bd8a098dec56"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""caae353b-58a5-4eb8-b8a7-88df7d8fad4b"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerKit
        m_PlayerKit = asset.FindActionMap("PlayerKit", throwIfNotFound: true);
        m_PlayerKit_Fire = m_PlayerKit.FindAction("Fire", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerKit
    private readonly InputActionMap m_PlayerKit;
    private IPlayerKitActions m_PlayerKitActionsCallbackInterface;
    private readonly InputAction m_PlayerKit_Fire;
    public struct PlayerKitActions
    {
        private @Controls m_Wrapper;
        public PlayerKitActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_PlayerKit_Fire;
        public InputActionMap Get() { return m_Wrapper.m_PlayerKit; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerKitActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerKitActions instance)
        {
            if (m_Wrapper.m_PlayerKitActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_PlayerKitActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerKitActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerKitActionsCallbackInterface.OnFire;
            }
            m_Wrapper.m_PlayerKitActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
            }
        }
    }
    public PlayerKitActions @PlayerKit => new PlayerKitActions(this);
    public interface IPlayerKitActions
    {
        void OnFire(InputAction.CallbackContext context);
    }
}
