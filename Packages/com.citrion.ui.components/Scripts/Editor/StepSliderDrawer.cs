using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace CitrioN.UI.Editor
{
  [CustomEditor(typeof(StepSlider))]

  public class StepSliderDrawer : SliderEditor
  {
    SerializedProperty stepSize;
    SerializedProperty stepCount;

    protected override void OnEnable()
    {
      base.OnEnable();
      stepSize = serializedObject.FindProperty("singleStepSize");
      stepCount = serializedObject.FindProperty("stepCount");
    }

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      EditorGUI.BeginChangeCheck();

      float newStepSize = EditorGUILayout.FloatField(new GUIContent("Step Size", stepSize.tooltip), stepSize.floatValue);
      int newStepCount = EditorGUILayout.IntField(new GUIContent("Step Count", stepCount.tooltip), stepCount.intValue);

      if (EditorGUI.EndChangeCheck())
      {
        stepSize.floatValue = Mathf.Clamp(newStepSize, 0/*.0001f*/, float.MaxValue);
        stepCount.intValue = Mathf.Max(newStepCount, 0);
      }

      //EditorGUILayout.PropertyField(m_stepSize);

      serializedObject.ApplyModifiedProperties();
    }

    //public override VisualElement CreateInspectorGUI()
    //{
    //  //return base.CreateInspectorGUI();
    //  var root = new VisualElement();
    //  //var slider = (Slider)serializedObject.targetObject;
    //  //var editor = Editor.CreateEditor(serializedObject.context);

    //  //var script = MonoScript.FromScriptableObject(editor);
    //  //string path = AssetDatabase.GetAssetPath(script);
    //  //if (path == string.Empty)
    //  //  return;
    //  //var editorGui = editor.CreateInspectorGUI();
    //  //root.Add(editorGui);
    //  //root.Add(new InspectorElement(serializedObject));
    //  return root;
    //}
  }
}