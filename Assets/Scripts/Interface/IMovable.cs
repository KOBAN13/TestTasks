using Character.Buff;
using UnityEngine;

namespace Interface
{
    public interface IMovable
    {
        void Move(Vector2 input, float speed);
    }
}