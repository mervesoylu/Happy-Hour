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

        public virtual void Update(XboxController controller)
        {
            // movement
            Vector3 moveDirection = new Vector3(XCI.GetAxisRaw(XboxAxis.LeftStickX, controller), 0, XCI.GetAxisRaw(XboxAxis.LeftStickY, controller));
            _characterController.Move(moveDirection);

            // throw
            if (XCI.GetButton(XboxButton.RightBumper, controller) && _straightCoolDownTimer <= 0 && Time.time >= _straightThrowAvailableTime)
            {
                _straightCoolDownTimer = _settings.StraightCoolDown;
                _tossAvailableTime = Time.time + _settings.ThrowDeadzoneDuration;
                _characterController.Throw();
            }

            if (_settings.StraightCoolDown > 0)
                _straightCoolDownTimer -= Time.deltaTime;

            // toss
            if (XCI.GetButton(XboxButton.LeftBumper, controller) && _arcCoolDownTimer <= 0 && Time.time >= _tossAvailableTime)
            {
                _arcCoolDownTimer = _settings.ArcCoolDown;
                _straightThrowAvailableTime = Time.time + _settings.ThrowDeadzoneDuration;
                _characterController.Toss();
            }

            if (_settings.ArcCoolDown > 0)
                _arcCoolDownTimer -= Time.deltaTime;
        }
        #endregion

        #region ------------------------------details
        protected float _straightCoolDownTimer;
        protected float _arcCoolDownTimer;
        protected float _straightThrowAvailableTime;
        protected float _tossAvailableTime;
        #endregion
    }

    public class DeactivatedCharacterInputState : CharacterInputState
    {
        public DeactivatedCharacterInputState(CharacterSettings settings, CharacterController characterController) : base(settings, characterController) { }

        public override void Update(XboxController controller) { }
    }

    public class DefaultCharacterInputState : CharacterInputState
    {
        public DefaultCharacterInputState(CharacterSettings settings, CharacterController characterController) : base(settings, characterController) { }

        public override void Update(XboxController controller)
        {
            // aim
            Vector3 aimDirection = Vector3.zero;
            if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) != 0 || XCI.GetAxisRaw(XboxAxis.RightStickY, controller) != 0)
                aimDirection = new Vector3(XCI.GetAxisRaw(XboxAxis.RightStickX, controller), 0, XCI.GetAxisRaw(XboxAxis.RightStickY, controller));

            _characterController.Aim(aimDirection.normalized);

            base.Update(controller);
        }
    }

    public class HappyHourCharacterInputState : CharacterInputState
    {
        public HappyHourCharacterInputState(CharacterSettings settings, CharacterController characterController) : base(settings, characterController) { }

        public override void Update(XboxController controller)
        {
            // aim
            Vector3 aimDirection = Vector3.zero;
            if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) != 0 || XCI.GetAxisRaw(XboxAxis.RightStickY, controller) != 0)
                aimDirection = new Vector3(XCI.GetAxisRaw(XboxAxis.RightStickX, controller), 0, XCI.GetAxisRaw(XboxAxis.RightStickY, controller));

            _characterController.Aim(aimDirection.normalized);

            base.Update(controller);
        }
    }
}
