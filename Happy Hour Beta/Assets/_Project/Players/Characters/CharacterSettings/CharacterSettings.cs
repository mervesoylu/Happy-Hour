using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "CharacterSettings", menuName = "CustomData/CharacterSettings", order = 1)]
    public class CharacterSettings : ScriptableObject
    {
        public float Speed;
        public float StunDuration;
        public float ImmobilityDuration;
        public StraightBottleController StraightBottle;
        public ArcBottleController ArcBottle;
        public float StraightCoolDown;
        public float ArcCoolDown; 
        public float ThrowDeadzoneDuration;
        public float Recovery;
        public float MaxDeviationAmount;
    } 
}
