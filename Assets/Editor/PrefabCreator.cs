#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using SimpleMOBA.Gameplay.Minions;
using SimpleMOBA.Gameplay.Heroes;
using SimpleMOBA.Gameplay.Towers;

public static class PrefabCreator
{
    [MenuItem("SimpleMOBA/Create Example Prefabs")]
    public static void CreatePrefabs()
    {
        var prefabsPath = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(prefabsPath))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }

        // Create Minion prefab
        var minionGO = new GameObject("Minion_Placeholder");
        minionGO.AddComponent<SimpleMOBA.Core.HealthComponent>();
        minionGO.AddComponent<MinionAI>();
        minionGO.AddComponent<MinionController>();
        minionGO.AddComponent<Rigidbody2D>();
        var minionPrefabPath = prefabsPath + "/MinionPrefab.prefab";
        PrefabUtility.SaveAsPrefabAsset(minionGO, minionPrefabPath);
        Object.DestroyImmediate(minionGO);

        // Create Hero prefab
        var heroGO = new GameObject("Hero_Placeholder");
        heroGO.AddComponent<SimpleMOBA.Core.HealthComponent>();
        heroGO.AddComponent<HeroAutoAttack>();
        heroGO.AddComponent<HeroController>();
        heroGO.AddComponent<Rigidbody2D>();
        var heroPrefabPath = prefabsPath + "/HeroPrefab.prefab";
        PrefabUtility.SaveAsPrefabAsset(heroGO, heroPrefabPath);
        Object.DestroyImmediate(heroGO);

        // Create Tower prefab
        var towerGO = new GameObject("Tower_Placeholder");
        towerGO.AddComponent<SimpleMOBA.Core.HealthComponent>();
        towerGO.AddComponent<TowerAI>();
        towerGO.AddComponent<TowerController>();
        var towerPrefabPath = prefabsPath + "/TowerPrefab.prefab";
        PrefabUtility.SaveAsPrefabAsset(towerGO, towerPrefabPath);
        Object.DestroyImmediate(towerGO);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Prefabs Created", "Example prefabs were created at Assets/Prefabs/. You can assign them to ScriptableObjects in the Inspector.", "OK");
    }
}
#endif
