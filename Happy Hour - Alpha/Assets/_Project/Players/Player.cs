using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    [CreateAssetMenu(fileName = "Player 0", menuName = "CustomData/Player")]
    public class Player : ScriptableObject
    {
        public XboxController Controller;
        public Sprite Sprite;
        public int Score;
    } 
}
