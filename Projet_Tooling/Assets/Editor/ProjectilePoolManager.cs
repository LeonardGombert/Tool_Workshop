using Gameplay.Player;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShootingManager))]
public class ProjectilePoolManager : Editor
{
    SerializedProperty projectilePoolSize;
    SerializedProperty projectilePrefab;
    SerializedProperty projectilePool;
    private ShootingManager shootingBehavior;

    private void OnEnable()
    {
        shootingBehavior = target as ShootingManager;
        projectilePoolSize = serializedObject.FindProperty(nameof(shootingBehavior.projectilePoolSize));
        projectilePrefab = serializedObject.FindProperty(nameof(shootingBehavior.projectilePrefab));
        projectilePool = serializedObject.FindProperty(nameof(shootingBehavior.projectilePool));
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Projectile Pool Size");
        EditorGUILayout.IntField(projectilePoolSize.intValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Projectile Prefab");
        projectilePrefab.objectReferenceValue = EditorGUILayout.ObjectField(projectilePrefab.objectReferenceValue, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        shootingBehavior.playerTransform = GameObject.Find("Player").transform;
        shootingBehavior.projectileContainer = GameObject.Find("Projectile Pool container").transform;
        shootingBehavior.playerReticule = GameObject.Find("Player Reticule").GetComponent<RectTransform>();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Instantiate Bullets"))
            shootingBehavior.projectilePool = new ProjectilePool(projectilePrefab.objectReferenceValue as GameObject, projectilePoolSize.intValue, shootingBehavior.projectileContainer, shootingBehavior.playerTransform);

        if (GUILayout.Button("Delete Bullets"))
            foreach (Transform bullet in shootingBehavior.projectileContainer.transform)
                DestroyImmediate(bullet.gameObject, false);
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
