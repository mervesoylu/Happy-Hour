#define KEYBOARD
#undef KEYBOARD

using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    public class CharacterInput : MonoBehaviour
    {
        #region ------------------------------dependencies
        CharacterController _characterController;
        #endregion

        #region ------------------------------interface
        public void SetXBoxController(XboxController controller)
        {
            _controller = controller;
        }
        #endregion

        #region ------------------------------Unity messages
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
#if KEYBOARD
            _forwardDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
#else
            _forwardDirection = new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, _controller), 0, XCI.GetAxis(XboxAxis.LeftStickY, _controller));
#endif
            // Need to run the direction through a filter before passing it to the character controller.
            _characterController.Move(_forwardDirection);

#if KEYBOARD
            _aimDirection = _forwardDirection;
#else
            Vector3 bufferedAimDirection = new Vector3(XCI.GetAxisRaw(XboxAxis.RightStickX), 0f, XCI.GetAxisRaw(XboxAxis.RightStickY));
            if (bufferedAimDirection != Vector3.zero)
            { _aimDirection = bufferedAimDirection; }
#endif
            // Need to run the direction through a filter before passing it to the character controller.
            _characterController.Aim(_aimDirection);

            if (XCI.GetButtonDown(XboxButton.RightBumper, _controller))
            { _characterController.Throw(); }
            else if (XCI.GetButtonDown(XboxButton.LeftBumper, _controller))
            { _characterController.Toss(); }
        }
#endregion

#region ------------------------------details
        [SerializeField] XboxController _controller;
        Vector3 _forwardDirection;
        Vector3 _aimDirection;
#endregion
    }
}