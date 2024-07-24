using UnityEngine;
using System;

namespace UltimatePlayer
{
    [System.Serializable]
    public class PlayerState
    {
        [SerializeField] public bool isMoved;
        [SerializeField] public bool isGraunded;
    }

    public class CanMovedPlayerEvent : EventArgs
    {
        public bool canMovedPlayer { get; private set; }

        public CanMovedPlayerEvent(bool value)
        {
            canMovedPlayer = value;
        }
    }
}