using System;
using UnityEngine;

namespace KevinV.WhackAMole.Interfaces
{
    public interface IMole
    {
        int ScoreValue { get; }
        int NotWhackedValue { get; }
        bool whacked { get; }
        void Spawn();
        void Hide();
        void Whack();
        bool IsActive();
        MoleType GetMoleType();
    }

    public enum MoleType
    {
        Normal,
        Bonus,
        Subtractor,
        Disabler,
    }
}