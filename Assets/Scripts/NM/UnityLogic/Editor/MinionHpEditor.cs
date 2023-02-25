using NM.UnityLogic.Characters.Minion;
using UnityEditor;
using UnityEngine;

namespace NM.UnityLogic.Editor
{
    [CustomEditor(typeof(MinionHp))]
    public class MinionHpEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Take Damage"))
            {
                var minion = (MinionHp)target;
                minion.TakeDamage();
            }
        }
    }
}