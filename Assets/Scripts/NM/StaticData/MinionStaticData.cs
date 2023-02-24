using UnityEngine;

namespace NM.StaticData
{
    [CreateAssetMenu(fileName = "MinionData", menuName = "StaticData/MinionStaticData")]
    public class MinionStaticData : ScriptableObject
    {
        [Range(1, 100)] public int MaxHp;
        public GameObject Prefab;
    }
}