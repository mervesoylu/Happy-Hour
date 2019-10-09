using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class CharacterController : MonoBehaviour
    {
        #region ------------------------------dependencies
        [SerializeField] CharacterSettings _settings;
        #endregion

        #region ------------------------------interface
        public void Move(Vector3 direction)
        {
            throw new System.NotImplementedException();
        }

        public void Aim(Vector3 direction)
        {
            throw new System.NotImplementedException();
        }

        public void Throw()
        {
            throw new System.NotImplementedException();
        }

        public void Toss()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage()
        {
            throw new System.NotImplementedException();
        }
        int _hp;

        public void GetStunned()
        {
            throw new System.NotImplementedException();
        }

        public Sprite Sprite
        {
            get { return _settings.Sprite; }
        }
        #endregion

        #region ------------------------------Unity messages
        #endregion

        #region ------------------------------details
        #endregion
    }
}
