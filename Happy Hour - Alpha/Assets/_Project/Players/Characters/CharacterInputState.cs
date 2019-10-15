﻿using UnityEngine;
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
            Vector3 moveDirection = new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, controller), 0, XCI.GetAxis(XboxAxis.LeftStickY, controller));
            _characterController.Move(moveDirection);

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
            // aim
            Vector3 aimDirection = Vector3.zero;
            if (XCI.GetAxis(XboxAxis.RightStickX, controller) != 0 || XCI.GetAxis(XboxAxis.RightStickY, controller) != 0)
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
            Vector3 aimDirection = _characterController.transform.forward;
            if (XCI.GetAxis(XboxAxis.RightStickX, controller) != 0 || XCI.GetAxis(XboxAxis.RightStickY, controller) != 0)
                aimDirection = new Vector3(XCI.GetAxisRaw(XboxAxis.RightStickX, controller), 0, XCI.GetAxisRaw(XboxAxis.RightStickY, controller));

            _characterController.Aim(deviateDirection(aimDirection.normalized));

            base.Update(controller);
        }

        Vector3 deviateDirection(Vector3 direction)
        {
            Vector3 result;

            float randomAngle = Random.Range(-_settings.MaxDeviationAmount, _settings.MaxDeviationAmount);
            result = Quaternion.AngleAxis(randomAngle, Vector3.up) * direction;

            return result;
        }
    }
}
