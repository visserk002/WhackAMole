using UnityEngine;

namespace KevinV.WhackAMole.Interfaces
{
    public interface IMole
    {
        int ScoreValue { get; }
        void Spawn(Vector3 position);
        void Hide();
        void Whack();
    }
}