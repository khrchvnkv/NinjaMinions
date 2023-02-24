using UnityEngine;

namespace NM.UnityLogic.Characters
{
    public class UniqueId : MonoBehaviour
    {
        [SerializeField] private string _id;

        public string Id => _id;

        public void SetId(string id) => _id = id;
    }
}