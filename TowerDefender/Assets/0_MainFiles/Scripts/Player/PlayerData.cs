using UnityEngine;

namespace UltimatePlayer
{
    [System.Serializable]
    public class PlayerData
    {
        #region Variable Classes
        [System.Serializable]
        public class PhysicsVariable
        {
            [SerializeField] private float gravityScaler = 9.81f;
            [SerializeField] private float environmentalResistance = 0.05f;
            public float Gravity => gravityScaler;
            public float Resistance => environmentalResistance;
        }

        [System.Serializable]
        public class WalkVariable
        {
            [SerializeField] private float maxSpeedWalk = 1f;
            [SerializeField] private AnimationCurve accelerationWalkingCurve;
            [SerializeField] private AnimationCurve brakingWalkingCurve;

            public float MaxSpeedWalk => maxSpeedWalk;
            public AnimationCurve AccelerationWalkingCurve => accelerationWalkingCurve;
            public AnimationCurve BrakingWalkingCurve => brakingWalkingCurve;
        }
        #endregion

        [Space(5)]

        public PhysicsVariable Physics;
        public WalkVariable Walking;
    }
}

