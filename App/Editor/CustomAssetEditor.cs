using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class CustomAssetEditor {

	private static System.Type ProjectWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ObjectBrowser");
	private static EditorWindow projectWindow = null;

	[MenuItem("Assets/Create/Character Class")]	
	public static void CreateClassAsset() {		
		CompleteAssetCreation(X.CreateInstance<CharacterClassSO>(), "CharacterClass");
	}

	[MenuItem("Assets/Create/Equipment/Weapon")]	
	public static void CreateWeaponAsset() {		
		CompleteAssetCreation(X.CreateInstance<WeaponSO>(), "Weapon");
	}

	[MenuItem("Assets/Create/Equipment/Armor")]	
	public static void CreateArmorAsset() {		
		CompleteAssetCreation(X.CreateInstance<ArmorSO>(), "Armor");
	}

	[MenuItem("Assets/Create/Enemy/Enemy Character Config")]	
	public static void CreateNPCAsset() {
		CompleteAssetCreation(X.CreateInstance<EnemyCharacterSO>(), "EnemyConfig");
	}	

	[MenuItem("Assets/Create/Enemy/Enemy Party")]	
	public static void CreateEnemyPartyAsset() {		
		CompleteAssetCreation(X.CreateInstance<EnemyPartySO>(), "Enemy Party");
	}

	[MenuItem("Assets/Create/Enemy/Enemy Skill Set")]	
	public static void CreateEnemySkillSetSetAsset() {
		CompleteAssetCreation(X.CreateInstance<EnemySkillSetSO>(), "EnemySkillSet");
	}
	/*
	[MenuItem("Assets/Create/Enemy/Enemy AI Rule")]	
	public static void CreateEnemyAIRuleSetAsset() {
		CompleteAssetCreation(X.CreateInstance<AISkillRule>(), "EnemyAIRule");
	}
	*/

	[MenuItem("Assets/Create/Skill/Combat Skill")]	
	public static void CreateCombatSkillAsset() {
		CompleteAssetCreation(X.CreateInstance<CombatSkillSO>(), "CombatSkill");
	}

	[MenuItem("Assets/Create/Skill/Target Condition/Life")]	
	public static void CreateTargetConditionLifeAsset() {
		CompleteAssetCreation(X.CreateInstance<TargetLifeConditionSO>(), "TargetConditionLife");
	}

	[MenuItem("Assets/Create/Skill/Status Effect")]	
	public static void CreateStatusEffectAsset() {
		CompleteAssetCreation(X.CreateInstance<StatusEffectSO>(), "StatusEffect");
	}

	[MenuItem("Assets/Create/Skill/Status Effect Group")]	
	public static void CreateStatusEffectGroupAsset() {
		CompleteAssetCreation(X.CreateInstance<StatusEffectGroupSO>(), "StatusEffectGroup");
	}

	[MenuItem("Assets/Create/PC/PC Skill Set Config")]	
	public static void CreatePCSkillSetAsset() {
		CompleteAssetCreation(X.CreateInstance<PCSkillSetSO>(), "SkillSet");
	}

	[MenuItem("Assets/Create/Test/PC Character Config")]	
	public static void CreateTestPCAsset() {
		CompleteAssetCreation(X.CreateInstance<PCCharacterSO>(), "NPC");
	}	



	private static void CompleteAssetCreation(ScriptableObject asset, string entityName) {
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);		
		if (path == "") {			
			path = "Assets";		
		}		
		else if (Path.GetExtension(path) != "") {			
			path = path.Replace(Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");		
		}		
		AssetDatabase.CreateAsset (asset, AssetDatabase.GenerateUniqueAssetPath (path + "/New " + entityName + ".asset"));
		EditorUtility.FocusProjectWindow();		
		Selection.activeObject = asset;  

		//StartRenameSelectedAsset();
	}

	public static void StartRenameSelectedAsset()
	{
		if (projectWindow == null) {
			projectWindow = EditorWindow.GetWindow(ProjectWindowType);
		}
		//should never be null but still ;)
		if (projectWindow != null) {
			var e = new Event();
			e.keyCode = KeyCode.F2;
			e.type = EventType.KeyDown;
			projectWindow.SendEvent(e);
		}
	}

	private class X : ScriptableObject{}
}
