using System.Linq;
using NM.StaticData;
using NM.UnityLogic.Characters;
using NM.UnityLogic.Characters.Enemies;
using NM.UnityLogic.Characters.Minion;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NM.UnityLogic.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Init"))
            {
                var levelData = (LevelStaticData)target;
                levelData.MinionSpawners = FindObjectsOfType<MinionSpawnMarker>()
                    .Select(x => new MinionSpawnerData(x.GetComponent<UniqueId>().Id,
                        x.transform.position))
                    .ToList();
                levelData.EnemySpawners = FindObjectsOfType<EnemySpawnMarker>()
                    .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id,
                        x.EnemyTypeId, x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }
            EditorUtility.SetDirty(target);
        }
    }
}