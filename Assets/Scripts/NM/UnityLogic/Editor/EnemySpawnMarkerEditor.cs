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
            var transform = spawner.transform;
            var position = transform.position;
            Gizmos.DrawSphere(position, 0.5f);
            Gizmos.DrawLine(position, position + transform.forward * 2.5f);
        }
    }
}