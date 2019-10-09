using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    [CreateAssetMenu(fileName = "Player 0", menuName = "Player")]
    public class Player : ScriptableObject
    {
        [HideInInspector] public XboxController Controller;
        public Sprite Sprite;
        public int Score;

        void OnEnable()
        {
            //Score = 0;
        }
    } 
}
