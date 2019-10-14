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

        public void OnHappyHourRan()
        {
            //_characterController.
        }

        public void OnHappyHourStopped()
        {

        }
        #endregion

        #region ------------------------------Unity messages
        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
#if KEYBOARD
            _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
#else
            _moveDirection = new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, _controller), 0, XCI.GetAxis(XboxAxis.LeftStickY, _controller));
#endif
            // Need to run the direction through a filter before passing it to the character controller.
            _characterController.Move(_moveDirection);

#if KEYBOARD
            _aimDirection = _forwardDirection;
#else
            if (XCI.GetAxis(XboxAxis.RightStickX, _controller) != 0 || XCI.GetAxis(XboxAxis.RightStickY, _controller) != 0)
                _aimDirection = new Vector3(XCI.GetAxis(XboxAxis.RightStickX, _controller), 0, XCI.GetAxis(XboxAxis.RightStickY, _controller));

            _characterController.Aim(_aimDirection.normalized);
#endif

            if (XCI.GetButton(XboxButton.RightBumper, _controller) && _straightCoolDownTimer <= 0)
            {
                _straightCoolDownTimer = _straightCoolDown;
                _characterController.Throw();
            }
            else if (XCI.GetButton(XboxButton.LeftBumper, _controller) && _arcCoolDownTimer <= 0)
            {
                _arcCoolDownTimer = _arcCoolDown;
                _characterController.Toss();
            }

            if (_straightCoolDown > 0)
                _straightCoolDownTimer -= Time.deltaTime;

            if (_arcCoolDown > 0)
                _arcCoolDownTimer -= Time.deltaTime;
        }
        [SerializeField] XboxController _controller;
        Vector3 _aimDirection;
        #endregion

        #region ------------------------------details
        [SerializeField] float _straightCoolDown;
        float _straightCoolDownTimer;
        [SerializeField] float _arcCoolDown;
        float _arcCoolDownTimer;
        CharacterInputState _state;
        Vector3 _moveDirection;
        #endregion
    }
}