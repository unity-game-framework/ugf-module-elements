using UGF.Module.Elements.Runtime;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UGF.Module.Elements.Editor
{
    [CustomEditor(typeof(ElementModuleInfoAsset), true)]
    internal class ElementModuleAssetInfoEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private ReorderableList m_list;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");

            SerializedProperty propertyElements = serializedObject.FindProperty("m_elements");

            m_list = new ReorderableList(serializedObject, propertyElements);
            m_list.drawHeaderCallback = OnDrawHeader;
            m_list.elementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2F;
            m_list.drawElementCallback = OnDrawElement;
            m_list.onAddCallback = OnAdd;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
            }

            m_list.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnDrawHeader(Rect rect)
        {
            GUI.Label(rect, $"{m_list.serializedProperty.displayName} (Size: {m_list.serializedProperty.arraySize})", EditorStyles.boldLabel);
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.height = EditorGUIUtility.singleLineHeight;
            rect.y += EditorGUIUtility.standardVerticalSpacing;

            SerializedProperty propertyElement = m_list.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.ObjectField(rect, propertyElement, GUIContent.none);
        }

        private void OnAdd(ReorderableList list)
        {
            list.serializedProperty.InsertArrayElementAtIndex(list.serializedProperty.arraySize);

            SerializedProperty propertyElement = list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 1);

            propertyElement.objectReferenceValue = null;
            propertyElement.serializedObject.ApplyModifiedProperties();
        }
    }
}
