//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Script/Player/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Move"",
            ""id"": ""68631a7a-819e-4c2d-8ca0-a70db35e7869"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ca9c6587-9b28-4c44-8642-c822d67c739c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c790421b-3a64-412f-abe2-0080fec06f4d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0a46cd8c-966e-4bbb-bd98-7e5f06de89e0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e5e8222c-7ae0-4d9b-a49e-807f5dca6de8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""91697db9-8f87-45ea-9459-3f5f28fa39d4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a302cc6a-1825-4e67-b80a-dbefe85437cd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Attack"",
            ""id"": ""5ae109f7-a66e-43b0-9c43-df5217ff5efc"",
            ""actions"": [
                {
                    ""name"": ""Perform"",
                    ""type"": ""Button"",
                    ""id"": ""75a842a6-bbfa-4d30-a117-08950d4098a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c07580ab-7f53-4c48-820e-b5dafd2ab54b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Perform"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Action"",
            ""id"": ""8e7dcdc7-14d7-4761-b98e-8b7b51f9b9f2"",
            ""actions"": [
                {
                    ""name"": ""Pnp"",
                    ""type"": ""Button"",
                    ""id"": ""15b4cd94-ee28-4021-9023-fb1b6968d07a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""62b98fdf-f7ac-46c5-81ba-5191b0838d62"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pnp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Move
        m_Move = asset.FindActionMap("Move", throwIfNotFound: true);
        m_Move_Movement = m_Move.FindAction("Movement", throwIfNotFound: true);
        // Attack
        m_Attack = asset.FindActionMap("Attack", throwIfNotFound: true);
        m_Attack_Perform = m_Attack.FindAction("Perform", throwIfNotFound: true);
        // Action
        m_Action = asset.FindActionMap("Action", throwIfNotFound: true);
        m_Action_Pnp = m_Action.FindAction("Pnp", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Move
    private readonly InputActionMap m_Move;
    private List<IMoveActions> m_MoveActionsCallbackInterfaces = new List<IMoveActions>();
    private readonly InputAction m_Move_Movement;
    public struct MoveActions
    {
        private @PlayerControls m_Wrapper;
        public MoveActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Move_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Move; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MoveActions set) { return set.Get(); }
        public void AddCallbacks(IMoveActions instance)
        {
            if (instance == null || m_Wrapper.m_MoveActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MoveActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
        }

        private void UnregisterCallbacks(IMoveActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
        }

        public void RemoveCallbacks(IMoveActions instance)
        {
            if (m_Wrapper.m_MoveActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMoveActions instance)
        {
            foreach (var item in m_Wrapper.m_MoveActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MoveActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MoveActions @Move => new MoveActions(this);

    // Attack
    private readonly InputActionMap m_Attack;
    private List<IAttackActions> m_AttackActionsCallbackInterfaces = new List<IAttackActions>();
    private readonly InputAction m_Attack_Perform;
    public struct AttackActions
    {
        private @PlayerControls m_Wrapper;
        public AttackActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Perform => m_Wrapper.m_Attack_Perform;
        public InputActionMap Get() { return m_Wrapper.m_Attack; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AttackActions set) { return set.Get(); }
        public void AddCallbacks(IAttackActions instance)
        {
            if (instance == null || m_Wrapper.m_AttackActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_AttackActionsCallbackInterfaces.Add(instance);
            @Perform.started += instance.OnPerform;
            @Perform.performed += instance.OnPerform;
            @Perform.canceled += instance.OnPerform;
        }

        private void UnregisterCallbacks(IAttackActions instance)
        {
            @Perform.started -= instance.OnPerform;
            @Perform.performed -= instance.OnPerform;
            @Perform.canceled -= instance.OnPerform;
        }

        public void RemoveCallbacks(IAttackActions instance)
        {
            if (m_Wrapper.m_AttackActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IAttackActions instance)
        {
            foreach (var item in m_Wrapper.m_AttackActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_AttackActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public AttackActions @Attack => new AttackActions(this);

    // Action
    private readonly InputActionMap m_Action;
    private List<IActionActions> m_ActionActionsCallbackInterfaces = new List<IActionActions>();
    private readonly InputAction m_Action_Pnp;
    public struct ActionActions
    {
        private @PlayerControls m_Wrapper;
        public ActionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pnp => m_Wrapper.m_Action_Pnp;
        public InputActionMap Get() { return m_Wrapper.m_Action; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionActions set) { return set.Get(); }
        public void AddCallbacks(IActionActions instance)
        {
            if (instance == null || m_Wrapper.m_ActionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ActionActionsCallbackInterfaces.Add(instance);
            @Pnp.started += instance.OnPnp;
            @Pnp.performed += instance.OnPnp;
            @Pnp.canceled += instance.OnPnp;
        }

        private void UnregisterCallbacks(IActionActions instance)
        {
            @Pnp.started -= instance.OnPnp;
            @Pnp.performed -= instance.OnPnp;
            @Pnp.canceled -= instance.OnPnp;
        }

        public void RemoveCallbacks(IActionActions instance)
        {
            if (m_Wrapper.m_ActionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IActionActions instance)
        {
            foreach (var item in m_Wrapper.m_ActionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ActionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ActionActions @Action => new ActionActions(this);
    public interface IMoveActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
    public interface IAttackActions
    {
        void OnPerform(InputAction.CallbackContext context);
    }
    public interface IActionActions
    {
        void OnPnp(InputAction.CallbackContext context);
    }
}
