using System;
using UnityEngine;

namespace NM.Data
{
    [Serializable]
    public class EnemyData
    {
        public string Id;
        public Vector3Data Position;
        public bool IsDied;

        public EnemyData(string id, Vector3Data position, bool isDied)
        {
            Id = id;
            Position = position;
            IsDied = isDied;
        }
    }
}