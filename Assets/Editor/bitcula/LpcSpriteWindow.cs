using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class LpcSpriteWindow : EditorWindow {
	private bool m_EnabledState;
	private bool m_ImportEmptySprites;
	private bool m_ExpertMode;
	private int m_ColCount;
	private int m_RowCount;
	private int m_RowCountExtended;
	private int m_PixelsPerUnit;

	private int m_ScFrameCount;
	private int m_ThFrameCount;
	private int m_WaFrameCount;
	private int m_SlFrameCount;
	private int m_ShFrameCount;
	private int m_HuFrameCount;
	private int m_ClFrameCount;
	private int m_IdFrameCount;
	private int m_CiFrameCount;
	private int m_JuFrameCount;
	private int m_S1FrameCount;
	private int m_S2FrameCount;
	private int m_S3FrameCount;
	private int m_EmFrameCount;
	private int m_RuFrameCount;
	private int m_OsFrameCount;
	private int m_ObFrameCount;
	private int m_OhFrameCount;

	private int tab;

	[MenuItem ("Tools/LPC Spritesheet Settings")]
	public static void ShowWindow () {
		EditorWindow.GetWindow (typeof (LpcSpriteWindow));
	}

	void OnEnable () {
		LoadSettings ();
	}

	void OnGUI () {
		GUILayout.Label ("LPC Spritesheet Settings", EditorStyles.boldLabel);

		string[] tabRegister;
		if (!m_ExpertMode) {
			tabRegister = new string[] { "Basic" };
		} else {
			tabRegister = new string[] { "Basic", "Animation", "Other" };
		}

		tab = GUILayout.Toolbar (tab, tabRegister);
		switch (tab) {
			case (0):
				m_EnabledState = EditorGUILayout.Toggle ("Enabled", m_EnabledState);
				m_ImportEmptySprites = EditorGUILayout.Toggle ("Import Empty Sprites", m_ImportEmptySprites);
				m_PixelsPerUnit = EditorGUILayout.IntField ("Pixels Per Unit", m_PixelsPerUnit);
				break;

			case (1):
				m_ScFrameCount = EditorGUILayout.IntField ("Spellcast Frame Count", m_ScFrameCount);
				m_ThFrameCount = EditorGUILayout.IntField ("Thrust Frame Count", m_ThFrameCount);
				m_WaFrameCount = EditorGUILayout.IntField ("Walk Frame Count", m_WaFrameCount);
				m_SlFrameCount = EditorGUILayout.IntField ("Slash Frame Count", m_SlFrameCount);
				m_ShFrameCount = EditorGUILayout.IntField ("Shoot Frame Count", m_ShFrameCount);
				m_HuFrameCount = EditorGUILayout.IntField ("Hurt Frame Count", m_HuFrameCount);
				m_ClFrameCount = EditorGUILayout.IntField ("Climb Frame Count", m_ClFrameCount);
				m_IdFrameCount = EditorGUILayout.IntField ("Idle Frame Count", m_IdFrameCount);
				m_CiFrameCount = EditorGUILayout.IntField ("CombatIdle Frame Count", m_CiFrameCount);
				m_JuFrameCount = EditorGUILayout.IntField ("Jump Frame Count", m_JuFrameCount);
				m_S1FrameCount = EditorGUILayout.IntField ("Sit1 Frame Count", m_S1FrameCount);
				m_S2FrameCount = EditorGUILayout.IntField ("Sit2 Frame Count", m_S2FrameCount);
				m_S3FrameCount = EditorGUILayout.IntField ("Sit3 Frame Count", m_S3FrameCount);
				m_EmFrameCount = EditorGUILayout.IntField ("Emote Frame Count", m_EmFrameCount);
				m_RuFrameCount = EditorGUILayout.IntField ("Run Frame Count", m_RuFrameCount);
				m_OsFrameCount = EditorGUILayout.IntField ("1-Handed Slash Frame Count", m_OsFrameCount);
				m_ObFrameCount = EditorGUILayout.IntField ("1-Handed Backslash Frame Count", m_ObFrameCount);
				m_OhFrameCount = EditorGUILayout.IntField ("1-Handed Halfslash Frame Count", m_OhFrameCount);
				break;

			case (2):
				m_ColCount = EditorGUILayout.IntField ("Total Cloumns", m_ColCount);
				m_RowCount = EditorGUILayout.IntField ("Total Rows", m_RowCount);
				m_RowCountExtended = EditorGUILayout.IntField ("Total Rows Extended", m_RowCountExtended);
				break;
		}

		GUILayout.FlexibleSpace ();
		m_ExpertMode = EditorGUILayout.Toggle ("Expert Mode", m_ExpertMode);
		if (GUILayout.Button ("Restore Initial Values"))
			RestoreInitialValues ();
		if (GUILayout.Button ("Close"))
			Close ();
	}

	void OnLostFocus () {
		StoreSettings ();
	}

	void OnDestroy () {
		StoreSettings ();
	}

	void RestoreInitialValues () {
		LpcSpriteSettings.RestoreInitialValues ();
		LoadSettings ();
	}

	void LoadSettings () {
		m_EnabledState = LpcSpriteSettings.GetEnabledState ();
		m_ImportEmptySprites = LpcSpriteSettings.GetImportEmptySprites ();
		m_PixelsPerUnit = LpcSpriteSettings.GetPixelsPerUnit ();
		m_ColCount = LpcSpriteSettings.GetColCount ();
		m_RowCount = LpcSpriteSettings.GetRowCount ();
		m_RowCountExtended = LpcSpriteSettings.GetRowCountExtended ();
		m_ScFrameCount = LpcSpriteSettings.GetScFrameCount ();
		m_ThFrameCount = LpcSpriteSettings.GetThFrameCount ();
		m_WaFrameCount = LpcSpriteSettings.GetWcFrameCount ();
		m_SlFrameCount = LpcSpriteSettings.GetSlFrameCount ();
		m_ShFrameCount = LpcSpriteSettings.GetShFrameCount ();
		m_HuFrameCount = LpcSpriteSettings.GetHuFrameCount ();
		m_ClFrameCount = LpcSpriteSettings.GetClFrameCount ();
		m_IdFrameCount = LpcSpriteSettings.GetIdFrameCount ();
		m_CiFrameCount = LpcSpriteSettings.GetCiFrameCount ();
		m_JuFrameCount = LpcSpriteSettings.GetJuFrameCount ();
		m_S1FrameCount = LpcSpriteSettings.GetS1FrameCount ();
		m_S2FrameCount = LpcSpriteSettings.GetS2FrameCount ();
		m_S3FrameCount = LpcSpriteSettings.GetS3FrameCount ();
		m_EmFrameCount = LpcSpriteSettings.GetEmFrameCount ();
		m_RuFrameCount = LpcSpriteSettings.GetRuFrameCount ();
		m_OsFrameCount = LpcSpriteSettings.GetOsFrameCount ();
		m_ObFrameCount = LpcSpriteSettings.GetObFrameCount ();
		m_OhFrameCount = LpcSpriteSettings.GetOhFrameCount ();
		m_ExpertMode = LpcSpriteSettings.GetExpertMode ();
	}

	void StoreSettings () {
		LpcSpriteSettings.SetEnabledState (m_EnabledState);
		LpcSpriteSettings.SetImportEmptySprites (m_ImportEmptySprites);
		LpcSpriteSettings.SetPixelsPerUnit (m_PixelsPerUnit);
		LpcSpriteSettings.SetColCount (m_ColCount);
		LpcSpriteSettings.SetRowCount (m_RowCount);
		LpcSpriteSettings.SetRowCountExtended (m_RowCountExtended);
		LpcSpriteSettings.SetScFrameCount (m_ScFrameCount);
		LpcSpriteSettings.SetThFrameCount (m_ThFrameCount);
		LpcSpriteSettings.SetWaFrameCount (m_WaFrameCount);
		LpcSpriteSettings.SetSlFrameCount (m_SlFrameCount);
		LpcSpriteSettings.SetShFrameCount (m_ShFrameCount);
		LpcSpriteSettings.SetHuFrameCount (m_HuFrameCount);
		LpcSpriteSettings.SetClFrameCount (m_ClFrameCount);
		LpcSpriteSettings.SetIdFrameCount (m_IdFrameCount);
		LpcSpriteSettings.SetCiFrameCount (m_CiFrameCount);
		LpcSpriteSettings.SetJuFrameCount (m_JuFrameCount);
		LpcSpriteSettings.SetS1FrameCount (m_S1FrameCount);
		LpcSpriteSettings.SetS2FrameCount (m_S2FrameCount);
		LpcSpriteSettings.SetS3FrameCount (m_S3FrameCount);
		LpcSpriteSettings.SetEmFrameCount (m_EmFrameCount);
		LpcSpriteSettings.SetRuFrameCount (m_RuFrameCount);
		LpcSpriteSettings.SetOsFrameCount (m_OsFrameCount);
		LpcSpriteSettings.SetObFrameCount (m_ObFrameCount);
		LpcSpriteSettings.SetOhFrameCount (m_OhFrameCount);
		LpcSpriteSettings.SetExpertMode (m_ExpertMode);
	}
}
