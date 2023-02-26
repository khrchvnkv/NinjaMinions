using NM.UnityLogic.Characters.Minion;
using NM.UnityLogic.Characters.Minion.SpawnLogic;
using UnityEditor;
using UnityEngine;

namespace NM.UnityLogic.Editor
{
    [CustomEditor(typeof(MinionSpawnMarker))]
    public class MinionSpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(MinionSpawnMarker spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}