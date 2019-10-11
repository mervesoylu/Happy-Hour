using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "Movement Settings", menuName = "Config/Movement Settings", order = 0)]
    public class MovementSettings : ScriptableObject
    {
        public float Speed;
        public float StunDuration;
    }
}