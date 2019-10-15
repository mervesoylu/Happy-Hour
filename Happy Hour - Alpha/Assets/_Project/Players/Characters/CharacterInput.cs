using UnityEngine;
using XboxCtrlrInput;
using Zenject;

namespace Project
{
    public class CharacterInput : MonoBehaviour
    {
        #region ------------------------------dependencies
        [Inject(Id = "defaultCharacterSettings")] CharacterSettings _defaultSettings;
        [Inject(Id = "happyHourCharacterSettings")] CharacterSettings _happyHourSettings;
        CharacterController _characterController;
        XboxController _controller;
        #endregion

        #region ------------------------------interface
        public void SetXBoxController(XboxController controller)
        {
            _controller = controller;
        }

        public void OnHappyHourRan()
        {
            _currentState = _happyHourState;
        }

        public void OnHappyHourStopped()
        {
            _currentState = _defaultState;
        }
        #endregion

        #region ------------------------------Unity messages
        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Start()
        {
            _defaultState = new DefaultCharacterInputState(_defaultSettings, _characterController);
            _happyHourState = new HappyHourCharacterInputState(_happyHourSettings, _characterController);

            _currentState = _defaultState;
        }

        void Update()
        {
            _currentState.Update(_controller);
        }
        #endregion

        #region ------------------------------details
        #region --------------------state
        CharacterInputState _currentState;
        CharacterInputState _defaultState;
        CharacterInputState _happyHourState;
        #endregion
        #endregion
    }
}