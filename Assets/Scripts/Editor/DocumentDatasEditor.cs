using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DocumentDatas))]
public class DocumentDatasEditor : Editor
{
    SerializedProperty prefabEntries;
    SerializedProperty docType;
    SerializedProperty stampAns;

    SerializedProperty nameAns, originAns, roleAns, IDPicAns;
    SerializedProperty shipAns, captainAns, cargoAns, signs, logoAns;
    SerializedProperty goodsAns, quantityAns;

    private void OnEnable()
    {
        prefabEntries = serializedObject.FindProperty("prefabEntries");
        docType = serializedObject.FindProperty("docType");
        stampAns = serializedObject.FindProperty("stampAns");

        //NPCID
        nameAns = serializedObject.FindProperty("nameAns");
        originAns = serializedObject.FindProperty("originAns");
        roleAns = serializedObject.FindProperty("roleAns");
        IDPicAns = serializedObject.FindProperty("IDPicAns");

        //ShipRegistration
        shipAns = serializedObject.FindProperty("shipAns");
        captainAns = serializedObject.FindProperty("captainAns");
        cargoAns = serializedObject.FindProperty("cargoAns");
        signs = serializedObject.FindProperty("signs");
        logoAns = serializedObject.FindProperty("logoAns");

        //GoodsPermit
        goodsAns = serializedObject.FindProperty("goodsAns");
        quantityAns = serializedObject.FindProperty("quantityAns");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(prefabEntries);
        EditorGUILayout.PropertyField(docType);
        EditorGUILayout.PropertyField(stampAns);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Type-Specific Data", EditorStyles.boldLabel);

        DocumentType type = (DocumentType)docType.enumValueIndex;

        switch (type)
        {
            case DocumentType.NPCID:
                EditorGUILayout.PropertyField(nameAns);
                EditorGUILayout.PropertyField(originAns);
                EditorGUILayout.PropertyField(roleAns);
                EditorGUILayout.PropertyField(IDPicAns);
                break;

            case DocumentType.ShipRegistration:
                EditorGUILayout.PropertyField(shipAns);
                EditorGUILayout.PropertyField(captainAns);
                EditorGUILayout.PropertyField(originAns);
                EditorGUILayout.PropertyField(cargoAns);
                EditorGUILayout.PropertyField(signs);
                EditorGUILayout.PropertyField(logoAns);
                break;

            case DocumentType.GoodsPermit:
                EditorGUILayout.PropertyField(nameAns);
                EditorGUILayout.PropertyField(goodsAns);
                EditorGUILayout.PropertyField(quantityAns);
                EditorGUILayout.PropertyField(signs);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}