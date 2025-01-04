using UnityEditor;
using UnityEngine;

public class LpcSpriteSettings : Object {
	// Animations in LPC Sprite Sheets do have various number of frames per animation.
	// This Setting determines if empty frames should be imported.
	private const bool EnabledStateInit = true;
	private const string EnabledState = "LpcSS_EnabledState";
	private const bool ImportEmptySpritesInit = false;
	private const string ImportEmptySprites = "LpcSS_ImportEmptySprites";

	private const bool ExpertModeInit = false;
	private const string ExpertMode = "LpcSS_ExpertMode";
	private const int ColCountInit = 13;
	private const string ColCount = "LpcSS_ColCount";
	private const int RowCountInit = 21;
	private const string RowCount = "LpcSS_RowCount";
	private const int RowCountExtendedInit = 46;
	private const string RowCountExtended = "LpcSS_RowCountExtended";
	private const int PixelsPerUnitInit = 64;
	private const string PixelsPerUnit = "LpcSS_PixelsPerUnit";
	/* Animation Frames */
	private const int ScFrameCountInit = 7;
	private const string ScFrameCount = "LpcSS_ScFrameCount";
	private const int ThFrameCountInit = 8;
	private const string ThFrameCount = "LpcSS_ThFrameCount";
	private const int WaFrameCountInit = 9;
	private const string WaFrameCount = "LpcSS_WaFrameCount";
	private const int SlFrameCountInit = 6;
	private const string SlFrameCount = "LpcSS_SlFrameCount";
	private const int ShFrameCountInit = 13;
	private const string ShFrameCount = "LpcSS_ShFrameCount";
	private const int HuFrameCountInit = 6;
	private const string HuFrameCount = "LpcSS_HuFrameCount";
	private const int ClFrameCountInit = 6;
	private const string ClFrameCount = "LpcSS_ClFrameCount";
	private const int IdFrameCountInit = 2;
	private const string IdFrameCount = "LpcSS_IdFrameCount";
	private const int CiFrameCountInit = 2;
	private const string CiFrameCount = "LpcSS_CiFrameCount";
	private const int JuFrameCountInit = 5;
	private const string JuFrameCount = "LpcSS_JuFrameCount";
	private const int S1FrameCountInit = 1;
	private const string S1FrameCount = "LpcSS_S1FrameCount";
	private const int S2FrameCountInit = 1;
	private const string S2FrameCount = "LpcSS_S2FrameCount";
	private const int S3FrameCountInit = 1;
	private const string S3FrameCount = "LpcSS_S3FrameCount";
	private const int EmFrameCountInit = 3;
	private const string EmFrameCount = "LpcSS_EmFrameCount";
	private const int RuFrameCountInit = 8;
	private const string RuFrameCount = "LpcSS_RuFrameCount";
	private const int OsFrameCountInit = 6;
	private const string OsFrameCount = "LpcSS_OsFrameCount";
	private const int ObFrameCountInit = 7;
	private const string ObFrameCount = "LpcSS_ObFrameCount";
	private const int OhFrameCountInit = 6;
	private const string OhFrameCount = "LpcSS_OhFrameCount";

	public static void RestoreInitialValues () {
		SetEnabledState (EnabledStateInit);
		SetImportEmptySprites (ImportEmptySpritesInit);
		SetExpertMode (ExpertModeInit);
		SetPixelsPerUnit (PixelsPerUnitInit);
		SetColCount (ColCountInit);
		SetRowCount (RowCountInit);
		SetRowCountExtended (RowCountExtendedInit);
		// Animation Frame Counts
		SetScFrameCount (ScFrameCountInit);
		SetThFrameCount (ThFrameCountInit);
		SetWaFrameCount (WaFrameCountInit);
		SetSlFrameCount (SlFrameCountInit);
		SetShFrameCount (ShFrameCountInit);
		SetHuFrameCount (HuFrameCountInit);
		SetClFrameCount (ClFrameCountInit);
		SetIdFrameCount (IdFrameCountInit);
		SetCiFrameCount (CiFrameCountInit);
		SetJuFrameCount (JuFrameCountInit);
		SetS1FrameCount (S1FrameCountInit);
		SetS2FrameCount (S2FrameCountInit);
		SetS3FrameCount (S3FrameCountInit);
		SetEmFrameCount (EmFrameCountInit);
		SetRuFrameCount (RuFrameCountInit);
		SetOsFrameCount (OsFrameCountInit);
		SetObFrameCount (ObFrameCountInit);
		SetOhFrameCount (OhFrameCountInit);
	}

	/* Getter */
	public static bool GetEnabledState () {
		if (EditorPrefs.HasKey (EnabledState))
			return EditorPrefs.GetBool (EnabledState);
		else
			return EnabledStateInit;
	}

	public static bool GetImportEmptySprites () {
		if (EditorPrefs.HasKey (ImportEmptySprites))
			return EditorPrefs.GetBool (ImportEmptySprites);
		else
			return ImportEmptySpritesInit;
	}

	public static bool GetExpertMode () {
		if (EditorPrefs.HasKey (ExpertMode))
			return EditorPrefs.GetBool (ExpertMode);
		else
			return ExpertModeInit;
	}

	public static int GetColCount () {
		if (EditorPrefs.HasKey (ColCount))
			return EditorPrefs.GetInt (ColCount);
		else
			return ColCountInit;
	}

	public static int GetRowCount () {
		if (EditorPrefs.HasKey (RowCount))
			return EditorPrefs.GetInt (RowCount);
		else
			return RowCountInit;
	}

	public static int GetRowCountExtended () {
		if (EditorPrefs.HasKey (RowCountExtended))
			return EditorPrefs.GetInt (RowCountExtended);
		else
			return RowCountExtendedInit;
	}

	public static int GetPixelsPerUnit () {
		if (EditorPrefs.HasKey (PixelsPerUnit))
			return EditorPrefs.GetInt (PixelsPerUnit);
		else
			return PixelsPerUnitInit;
	}

	/* Animation Frame Count Getter */
	public static int GetScFrameCount () {
		if (EditorPrefs.HasKey (ScFrameCount))
			return EditorPrefs.GetInt (ScFrameCount);
		else
			return ScFrameCountInit;
	}

	public static int GetThFrameCount () {
		if (EditorPrefs.HasKey (ThFrameCount))
			return EditorPrefs.GetInt (ThFrameCount);
		else
			return ThFrameCountInit;
	}

	public static int GetWcFrameCount () {
		if (EditorPrefs.HasKey (WaFrameCount))
			return EditorPrefs.GetInt (WaFrameCount);
		else
			return WaFrameCountInit;
	}

	public static int GetSlFrameCount () {
		if (EditorPrefs.HasKey (SlFrameCount))
			return EditorPrefs.GetInt (SlFrameCount);
		else
			return SlFrameCountInit;
	}

	public static int GetShFrameCount () {
		if (EditorPrefs.HasKey (ShFrameCount))
			return EditorPrefs.GetInt (ShFrameCount);
		else
			return ShFrameCountInit;
	}

	public static int GetHuFrameCount () {
		if (EditorPrefs.HasKey (HuFrameCount))
			return EditorPrefs.GetInt (HuFrameCount);
		else
			return HuFrameCountInit;
	}

	public static int GetClFrameCount () {
		if (EditorPrefs.HasKey (ClFrameCount))
			return EditorPrefs.GetInt (ClFrameCount);
		else
			return ClFrameCountInit;
	}

	public static int GetIdFrameCount () {
		if (EditorPrefs.HasKey (IdFrameCount))
			return EditorPrefs.GetInt (IdFrameCount);
		else
			return IdFrameCountInit;
	}

	public static int GetCiFrameCount () {
		if (EditorPrefs.HasKey (CiFrameCount))
			return EditorPrefs.GetInt (CiFrameCount);
		else
			return CiFrameCountInit;
	}

	public static int GetJuFrameCount () {
		if (EditorPrefs.HasKey (JuFrameCount))
			return EditorPrefs.GetInt (JuFrameCount);
		else
			return JuFrameCountInit;
	}

	public static int GetS1FrameCount () {
		if (EditorPrefs.HasKey (S1FrameCount))
			return EditorPrefs.GetInt (S1FrameCount);
		else
			return S1FrameCountInit;
	}

	public static int GetS2FrameCount () {
		if (EditorPrefs.HasKey (S2FrameCount))
			return EditorPrefs.GetInt (S2FrameCount);
		else
			return S2FrameCountInit;
	}

	public static int GetS3FrameCount () {
		if (EditorPrefs.HasKey (S3FrameCount))
			return EditorPrefs.GetInt (S3FrameCount);
		else
			return S3FrameCountInit;
	}

	public static int GetEmFrameCount () {
		if (EditorPrefs.HasKey (EmFrameCount))
			return EditorPrefs.GetInt (EmFrameCount);
		else
			return EmFrameCountInit;
	}

	public static int GetRuFrameCount () {
		if (EditorPrefs.HasKey (RuFrameCount))
			return EditorPrefs.GetInt (RuFrameCount);
		else
			return RuFrameCountInit;
	}

	public static int GetOsFrameCount () {
		if (EditorPrefs.HasKey (OsFrameCount))
			return EditorPrefs.GetInt (OsFrameCount);
		else
			return OsFrameCountInit;
	}

	public static int GetObFrameCount () {
		if (EditorPrefs.HasKey (ObFrameCount))
			return EditorPrefs.GetInt (ObFrameCount);
		else
			return ObFrameCountInit;
	}

	public static int GetOhFrameCount () {
		if (EditorPrefs.HasKey (OhFrameCount))
			return EditorPrefs.GetInt (OhFrameCount);
		else
			return OhFrameCountInit;
	}

	/* Setter */
	public static void SetEnabledState (bool enabledState) {
		EditorPrefs.SetBool (EnabledState, enabledState);
	}

	public static void SetImportEmptySprites (bool importEmptySprites) {
		EditorPrefs.SetBool (ImportEmptySprites, importEmptySprites);
	}

	public static void SetExpertMode (bool expertMode) {
		EditorPrefs.SetBool (ExpertMode, expertMode);
	}

	public static void SetColCount (int cols) {
		EditorPrefs.SetInt (ColCount, cols);
	}

	public static void SetRowCount (int rows) {
		EditorPrefs.SetInt (RowCount, rows);
	}

	public static void SetRowCountExtended (int rows) {
		EditorPrefs.SetInt (RowCountExtended, rows);
	}

	public static void SetPixelsPerUnit (int ppu) {
		EditorPrefs.SetInt (PixelsPerUnit, ppu);
	}

	/* Animation Frame Count Setter */
	public static void SetScFrameCount (int frames) {
		EditorPrefs.SetInt (ScFrameCount, frames);
	}

	public static void SetThFrameCount (int frames) {
		EditorPrefs.SetInt (ThFrameCount, frames);
	}

	public static void SetWaFrameCount (int frames) {
		EditorPrefs.SetInt (WaFrameCount, frames);
	}

	public static void SetSlFrameCount (int frames) {
		EditorPrefs.SetInt (SlFrameCount, frames);
	}

	public static void SetShFrameCount (int frames) {
		EditorPrefs.SetInt (ShFrameCount, frames);
	}

	public static void SetHuFrameCount (int frames) {
		EditorPrefs.SetInt (HuFrameCount, frames);
	}

	public static void SetClFrameCount (int frames) {
		EditorPrefs.SetInt (ClFrameCount, frames);
	}

	public static void SetIdFrameCount (int frames) {
		EditorPrefs.SetInt (IdFrameCount, frames);
	}

	public static void SetCiFrameCount (int frames) {
		EditorPrefs.SetInt (CiFrameCount, frames);
	}

	public static void SetJuFrameCount (int frames) {
		EditorPrefs.SetInt (JuFrameCount, frames);
	}

	public static void SetS1FrameCount (int frames) {
		EditorPrefs.SetInt (S1FrameCount, frames);
	}

	public static void SetS2FrameCount (int frames) {
		EditorPrefs.SetInt (S2FrameCount, frames);
	}

	public static void SetS3FrameCount (int frames) {
		EditorPrefs.SetInt (S3FrameCount, frames);
	}

	public static void SetEmFrameCount (int frames) {
		EditorPrefs.SetInt (EmFrameCount, frames);
	}

	public static void SetRuFrameCount (int frames) {
		EditorPrefs.SetInt (RuFrameCount, frames);
	}

	public static void SetOsFrameCount (int frames) {
		EditorPrefs.SetInt (OsFrameCount, frames);
	}

	public static void SetObFrameCount (int frames) {
		EditorPrefs.SetInt (ObFrameCount, frames);
	}

	public static void SetOhFrameCount (int frames) {
		EditorPrefs.SetInt (OhFrameCount, frames);
	}
}
