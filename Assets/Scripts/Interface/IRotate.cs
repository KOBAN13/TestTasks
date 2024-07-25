using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Interface
{
    public interface IRotate
    {
        UniTask RotateCharacter(Vector3 mousePosition);
    }
}