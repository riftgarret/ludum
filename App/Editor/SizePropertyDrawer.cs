using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer (typeof (Size))]
public class PropertyDrawerTest : PropertyDrawer {

	private const float START_POS = 0.405f;
	private const float LABEL_WIDTH = 15f;


	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {
		// get properties
		SerializedProperty propWidth = prop.FindPropertyRelative ("width");
		SerializedProperty propHeight = prop.FindPropertyRelative ("height");

		// dont make child fiellds indented
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		// calculate rects
		Rect prefixRect = new Rect(pos.x, pos.y, 100, pos.height);
		float cursor = START_POS * pos.width + pos.x;
		float scaledWidth = (pos.width - (cursor - (3f * LABEL_WIDTH ))) / 3f;
		Rect prefixWidthRect = new Rect(cursor, pos.y, LABEL_WIDTH, pos.height);
		cursor = prefixWidthRect.xMax;
		Rect widthRect = new Rect(cursor, pos.y, scaledWidth ,pos.height);
		cursor = widthRect.xMax;
		Rect prefixHeightRect = new Rect(cursor, pos.y, LABEL_WIDTH, pos.height);
		cursor = prefixHeightRect.xMax;
		Rect heightRect = new Rect(cursor, pos.y, scaledWidth, pos.height);


		EditorGUI.PrefixLabel(prefixRect, label);
		EditorGUI.PrefixLabel(prefixWidthRect, new GUIContent("W"));
		EditorGUI.PropertyField(widthRect, propWidth,GUIContent.none);
		EditorGUI.PrefixLabel(prefixHeightRect, new GUIContent("H"));
		EditorGUI.PropertyField(heightRect,propHeight,GUIContent.none);

		EditorGUI.indentLevel = indent;

	}
}