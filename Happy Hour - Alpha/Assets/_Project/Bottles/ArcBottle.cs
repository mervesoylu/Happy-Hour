using UnityEngine;

namespace Project
{
    public class ArcBottle : MonoBehaviour
    {
        #region ---------------------------dependencies
        #endregion

        #region ---------------------------interfaces
        #endregion

        #region ---------------------------unity messages
        #endregion

        #region ---------------------------details
        [SerializeField] float _speed;
        [SerializeField] AnimationCurve _trajectory;
        [SerializeField] Vector2 _arcDisplacement;
        #endregion
    }
}
