using UnityEngine;

namespace Project
{
    [System.Serializable]
    public class CharacterSettings
    {
        public float Speed;
        public float StunDuration;
        public StraightBottleController StraightBottle;
        public ArcBottleController ArcBottle;
        public Sprite Sprite;
    }
}