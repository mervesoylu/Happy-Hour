﻿using UnityEngine;
using XboxCtrlrInput;

namespace Project
{
    [CreateAssetMenu(fileName = "Player 0", menuName = "CustomData/Player")]
    public class Player : ScriptableObject
    {
        public XboxController Controller;
        public Color Color;
        [HideInInspector] public Sprite Sprite;
        [HideInInspector] public int Score;
    } 
}
