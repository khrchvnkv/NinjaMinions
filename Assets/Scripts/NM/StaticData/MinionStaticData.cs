using UnityEngine;

namespace NM.StaticData
{
    [CreateAssetMenu(fileName = "MinionData", menuName = "StaticData/MinionStaticData")]
    public class MinionStaticData : ScriptableObject
    {
        [Range(1, 100)] public int MaxHp;
        [Range(0.1f, 5.0f)] public float MovementSpeed;
    }
}