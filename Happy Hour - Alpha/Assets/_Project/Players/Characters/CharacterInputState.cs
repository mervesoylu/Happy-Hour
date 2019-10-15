using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    public abstract class CharacterInputState
    {
        #region ------------------------------dependencies
        protected CharacterSettings _settings;
        protected CharacterController _characterController;
        #endregion

        #region ------------------------------interface
        public CharacterInputState(CharacterSettings settings, CharacterController characterController)
        {
            _settings = settings;
            _characterController = characterController;
        }

        public abstract void Update(XboxController controller);
        #endregion

        #region ------------------------------details
        protected float _straightCoolDownTimer;
        protected float _arcCoolDownTimer;
        #endregion
    }

    public class DefaultCharacterInputState : CharacterInputState
    {
        public DefaultCharacterInputState(CharacterSettings settings, CharacterController characterController) : base(settings, characterController) { }

        public override void Update(XboxController controller)
        {
            // movement
            Vector3 moveDirection = new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, controller), 0, XCI.GetAxis(XboxAxis.LeftStickY, controller));
            _characterController.Move(moveDirection);

            // aim
            Vector3 aimDirection = Vector3.zero;
            if (XCI.GetAxis(XboxAxis.RightStickX, controller) != 0 || XCI.GetAxis(XboxAxis.RightStickY, controller) != 0)
                aimDirection = new Vector3(XCI.GetAxisRaw(XboxAxis.RightStickX, controller), 0, XCI.GetAxisRaw(XboxAxis.RightStickY, controller));

            _characterController.Aim(aimDirection.normalized);

            // throw
            if (XCI.GetButton(XboxButton.RightBumper, controller) && _straightCoolDownTimer <= 0)
            {
                _straightCoolDownTimer = _settings.StraightCoolDown;
                _characterController.Throw();
            }

            if (_settings.StraightCoolDown > 0)
                _straightCoolDownTimer -= Time.deltaTime;

            // toss
            else if (XCI.GetButton(XboxButton.LeftBumper, controller) && _arcCoolDownTimer <= 0)
            {
                _arcCoolDownTimer = _settings.ArcCoolDown;
                _characterController.Toss();
            }

            if (_settings.ArcCoolDown > 0)
                _arcCoolDownTimer -= Time.deltaTime;
        }
    }

    public class HappyHourCharacterInputState : CharacterInputState
    {
        public HappyHourCharacterInputState(CharacterSettings settings, CharacterController characterController) : base(settings, characterController)
        {
        }

        public override void Update(XboxController controller)
        {
            throw new System.NotImplementedException();
        }
    }
}
