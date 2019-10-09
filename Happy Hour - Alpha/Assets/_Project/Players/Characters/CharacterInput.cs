using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    public class CharacterInput : MonoBehaviour
    {
        #region ------------------------------dependencies
        #endregion

        #region ------------------------------interface
        public void SetXBoxController(XboxController controller)
        {
            _controller = controller;
        }
        #endregion

        #region ------------------------------Unity messages
        #endregion

        #region ------------------------------details
        [SerializeField] XboxController _controller;
        #endregion
    }
}
