using NM.UnityLogic.Characters.Enemies;
using NM.UnityLogic.Characters.Enemies.SpawnLogic;
using UnityEditor;
using UnityEngine;

namespace NM.UnityLogic.Editor
{
    [CustomEditor(typeof(EnemySpawnMarker))]
    public class EnemySpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawnMarker spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}