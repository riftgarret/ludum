using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer (typeof (AISkillRule))]
public class AISkillRulePropertyDrawer : PropertyDrawer {
	

	private const int ROW_COUNT = 3;
	
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		// get properties
		SerializedProperty propWeight = property.FindPropertyRelative ("mWeight");
		SerializedProperty propSkill = property.FindPropertyRelative ("mSkill");
		SerializedProperty propConType = property.FindPropertyRelative ("mConditionType");
		SerializedProperty propConTarget = property.FindPropertyRelative ("mConditionTarget");
		SerializedProperty propConResolv = property.FindPropertyRelative ("mResolvedTarget");
		SerializedProperty propConValue = property.FindPropertyRelative ("mConditionValue");


		// dont make child fiellds indented
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 1;

		// get enum value
		AISkillRule.ConditionType type = (AISkillRule.ConditionType) propConType.enumValueIndex;
		SerializedProperty propCondRule = null; // can be null if type = ANY
		switch(type) {
		case AISkillRule.ConditionType.CLASS:
			propCondRule = property.FindPropertyRelative ("mClassCondition");
			break;
		case AISkillRule.ConditionType.HP:
			propCondRule = property.FindPropertyRelative ("mHitpointCondition");
			break;
		case AISkillRule.ConditionType.PARTY:
			propCondRule = property.FindPropertyRelative ("mPartyCondition");
			break;
		case AISkillRule.ConditionType.RES:
			propCondRule = property.FindPropertyRelative ("mResourceCondition");
			break;
		case AISkillRule.ConditionType.ROW:
			propCondRule = property.FindPropertyRelative ("mRowCondition");
			break;
		case AISkillRule.ConditionType.STATUS:
			propCondRule = property.FindPropertyRelative ("mStatusCondition");
			break;
		}


		EditorGUIUtility.labelWidth = 100f;
		//EditorGUIUtility.fieldWidth = 12f;

		float height = base.GetPropertyHeight(property, label);
		Rect cursor = new Rect(position);
		cursor.height = height;

		// row 1
		cursor.width = position.width / 2f;
		EditorGUI.PropertyField(cursor, propSkill, new GUIContent("skill")); 
		cursor.x += cursor.width;
		EditorGUI.PropertyField(cursor, propWeight, new GUIContent("weight")); 

		// row 2
		cursor.width = position.width / 3f;
		cursor.x = position.x; 
		cursor.y += height;
		EditorGUI.PropertyField(cursor, propConType, new GUIContent("type")); 
		cursor.x += cursor.width;
		EditorGUI.PropertyField(cursor, propConTarget, new GUIContent("target check")); 
		cursor.x += cursor.width;
		EditorGUI.PropertyField(cursor, propConResolv, new GUIContent("target action")); 

		// row 3 - dont show on any case
		if(type != AISkillRule.ConditionType.ANY) {
			cursor.width = position.width / 2f;
			cursor.x = position.x; 
			cursor.y += height;
			EditorGUI.PropertyField(cursor, propCondRule, new GUIContent("condition")); 
			cursor.x += cursor.width;
			if(type != AISkillRule.ConditionType.CLASS) {
				EditorGUI.PropertyField(cursor, propConValue, new GUIContent("value")); 
			}
		}

		EditorGUI.indentLevel = indent;
		
	}

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight (property, label) * ROW_COUNT;
	}
}