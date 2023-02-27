using System;

namespace NM.Data
{
    [Serializable]
    public class EnemyData
    {
        public string Id;
        public Vector3Data Position;
        public Vector3Data Rotation;
        public bool IsDied;

        public EnemyData(string id, Vector3Data position, Vector3Data rotation, bool isDied)
        {
            Id = id;
            Position = position;
            Rotation = rotation;
            IsDied = isDied;
        }
    }
}