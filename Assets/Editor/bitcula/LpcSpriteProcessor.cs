using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;


public class LpcSpriteProcessor : AssetPostprocessor {

	enum LpcAnimationState {
		Spellcast,
		Thrust,
		Walkcycle,
		Slash,
		Shoot,
		Hurt,
		Climb,
		Idle,
		CombatIdle,
		Jump,
		Sit1,
		Sit2,
		Sit3,
		Emote,
		Run,
		OneHandedSlash,
		OneHandedBackslash,
		OneHandedHalfslash
	}

	private const int LPC_SHEET_WIDTH = 832;
	private const int LPC_SHEET_WIDTH_EXTENDED = 832;
	private const int LPC_SHEET_HEIGHT = 1344;
	private const int LPC_SHEET_HEIGHT_EXTENDED = 2944;
	private const int LPC_SPRITE_SIZE = 64;

	private bool m_EnabledState; // Should the custom importer be used
	private bool m_extended;     // extended or normal LPC character spritesheet
	private int m_PixelsPerUnit; // Sets the Pixels Per Unit in the Importer
	private int m_ScFrames;      // Spellcast animation frames
	private int m_ThFrames;      // Thrust animation frames
	private int m_WcFrames;      // Walkcycle animation frames
	private int m_SlFrames;      // Slash animation frames
	private int m_ShFrames;      // Shoot animation frames
	private int m_HuFrames;      // Hurt animation frames
	private int m_ClFrames;      // Climb animation frames
	private int m_IdFrames;      // Idle animation frames
	private int m_CiFrames;      // Combat Idle animation frames
	private int m_JuFrames;      // Jump animation frames
	private int m_S1Frames;      // Sit1 animation frames
	private int m_S2Frames;      // Sit2 animation frames
	private int m_S3Frames;      // Sit3 animation frames
	private int m_EmFrames;      // Emote animation frames
	private int m_RuFrames;      // Run animation frames
	private int m_OsFrames;      // 1-Handed Slash animation frames
	private int m_ObFrames;      // 1-Handed Backslash animation frames
	private int m_OhFrames;      // 1-Handed Halfslash animation frames

	private bool m_ImportEmptySprites;
	private int m_ColCount;
	private int m_RowCount;
	private int m_RowCountExtended;

	void RetrieveSettings () {
		// Retrieve Basic Settings
		m_EnabledState = LpcSpriteSettings.GetEnabledState ();
		m_ImportEmptySprites = LpcSpriteSettings.GetImportEmptySprites ();
		m_PixelsPerUnit = LpcSpriteSettings.GetPixelsPerUnit ();

		// Retrieve Animation Settings
		m_ScFrames = LpcSpriteSettings.GetScFrameCount ();
		m_ThFrames = LpcSpriteSettings.GetThFrameCount ();
		m_WcFrames = LpcSpriteSettings.GetWcFrameCount ();
		m_SlFrames = LpcSpriteSettings.GetSlFrameCount ();
		m_ShFrames = LpcSpriteSettings.GetShFrameCount ();
		m_HuFrames = LpcSpriteSettings.GetHuFrameCount ();
		m_ClFrames = LpcSpriteSettings.GetClFrameCount ();
		m_IdFrames = LpcSpriteSettings.GetIdFrameCount ();
		m_CiFrames = LpcSpriteSettings.GetCiFrameCount ();
		m_JuFrames = LpcSpriteSettings.GetJuFrameCount ();
		m_S1Frames = LpcSpriteSettings.GetS1FrameCount ();
		m_S2Frames = LpcSpriteSettings.GetS2FrameCount ();
		m_S3Frames = LpcSpriteSettings.GetS3FrameCount ();
		m_EmFrames = LpcSpriteSettings.GetEmFrameCount ();
		m_RuFrames = LpcSpriteSettings.GetRuFrameCount ();
		m_OsFrames = LpcSpriteSettings.GetOsFrameCount ();
		m_ObFrames = LpcSpriteSettings.GetObFrameCount ();
		m_OhFrames = LpcSpriteSettings.GetOhFrameCount ();

		// Retrieve Other Settings
		m_ColCount = LpcSpriteSettings.GetColCount ();
		m_RowCount = LpcSpriteSettings.GetRowCount ();
		m_RowCountExtended = LpcSpriteSettings.GetRowCountExtended ();
	}

	void OnPreprocessTexture () {
		RetrieveSettings ();
		if (m_EnabledState) {
			TextureImporter textureImporter = (TextureImporter) assetImporter;
			textureImporter.GetSourceTextureWidthAndHeight (out int textureWidth, out int textureHeight);
			// Debug.Log (textureWidth + " - " + textureHeight);

			// Do nothing if it not a LPC Based Sprite
			if ((textureWidth != LPC_SHEET_WIDTH || textureHeight != LPC_SHEET_HEIGHT) && (textureWidth != LPC_SHEET_WIDTH_EXTENDED || textureHeight != LPC_SHEET_HEIGHT_EXTENDED)) {
				Debug.Log ("Preprocess: Do nothing");
				return;
			}

			m_extended = textureHeight == LPC_SHEET_HEIGHT_EXTENDED;

			if (m_extended) {
				textureImporter.maxTextureSize = 4096;
			} else {
				textureImporter.maxTextureSize = 2048;
			}
			textureImporter.textureType = TextureImporterType.Sprite;
			textureImporter.spriteImportMode = SpriteImportMode.Multiple;
			textureImporter.mipmapEnabled = false;
			textureImporter.filterMode = FilterMode.Point;
			textureImporter.spritePixelsPerUnit = m_PixelsPerUnit;
		}
	}

	public void OnPostprocessTexture (Texture2D texture) {
		if (m_EnabledState) {
			// Do nothing if it is not a LPC Based Sprite
			if (!IsLpcSpriteSheet (texture)) {
				Debug.Log ("Postprocess: Do nothing");
				return;
			}

			Debug.Log ("Importing LPC Character Sheet");
			TextureImporter textureImporter = (TextureImporter) assetImporter;

			SpriteDataProviderFactories factory = new SpriteDataProviderFactories ();
			factory.Init ();
			ISpriteEditorDataProvider dataProvider = factory.GetSpriteEditorDataProviderFromObject (textureImporter);
			dataProvider.InitSpriteEditorDataProvider ();

			try {
				SpriteRect[] existingSpriteRects = dataProvider.GetSpriteRects ();
				SpriteRect[] newSpriteRects = GetSpriteSheet (existingSpriteRects);
				dataProvider.SetSpriteRects (newSpriteRects);

				// apply name pairs
				ISpriteNameFileIdDataProvider spriteNameFileIdDataProvider = dataProvider.GetDataProvider<ISpriteNameFileIdDataProvider> ();
				List<SpriteNameFileIdPair> spriteNameFileIdPairs = new List<SpriteNameFileIdPair> ();
				for (int i = 0; i < newSpriteRects.Length; i++) {
					SpriteRect spriteRect = newSpriteRects[i];
					spriteNameFileIdPairs.Add (new SpriteNameFileIdPair (spriteRect.name, spriteRect.spriteID));
				}
				spriteNameFileIdDataProvider.SetNameFileIdPairs (spriteNameFileIdPairs);

				dataProvider.Apply ();
			} catch (Exception e) {
				Debug.LogError (e);
			}
		}
	}

	private SpriteRect[] GetSpriteSheet (SpriteRect[] previousSpriteRects) {
		List<SpriteRect> rects = new List<SpriteRect> ();
		int rows = 0;
		if (m_extended)
			rows = m_RowCountExtended;
		else
			rows = m_RowCount;
		for (int row = 0; row < rows; ++row) {
			for (int col = 0; col < m_ColCount; ++col) {
				SpriteRect rect = new SpriteRect ();
				rect.rect = new Rect (col * LPC_SPRITE_SIZE, row * LPC_SPRITE_SIZE, LPC_SPRITE_SIZE, LPC_SPRITE_SIZE);

				LpcAnimationState animState = GetAnimationState (row, col);

				if (!m_ImportEmptySprites) {
					if ((animState == LpcAnimationState.OneHandedHalfslash && col >= m_OhFrames))
						break;
					if ((animState == LpcAnimationState.Run && col >= m_RuFrames))
						break;
					if ((animState == LpcAnimationState.Emote && col >= m_S1Frames + m_S2Frames + m_S3Frames + m_EmFrames))
						break;
					if ((animState == LpcAnimationState.Jump && col >= m_JuFrames))
						break;
					if ((animState == LpcAnimationState.CombatIdle && col >= m_CiFrames + m_IdFrames))
						break;
					if ((animState == LpcAnimationState.Climb && col >= m_ClFrames))
						break;
					if ((animState == LpcAnimationState.Hurt && col >= m_HuFrames))
						break;
					if ((animState == LpcAnimationState.Shoot && col >= m_ShFrames))
						break;
					if ((animState == LpcAnimationState.Slash && col >= m_SlFrames))
						break;
					if ((animState == LpcAnimationState.Thrust && col >= m_ThFrames))
						break;
					if ((animState == LpcAnimationState.Walkcycle && col >= m_WcFrames))
						break;
					if ((animState == LpcAnimationState.Spellcast && col >= m_ScFrames))
						break;
				}

				string[] path_branch = assetImporter.assetPath.Split ('/');
				//Debug.Log("SPRITE PATH: " + assetImporter.assetPath);

				string prefix = "";
				for (int i = 6; i < path_branch.Length; i++) {
					string node = path_branch[i];
					string[] split_node = node.Split ('.');
					//Debug.Log("PATH BRANCH: " + node);

					prefix += string.Format ("{0}_", split_node[0]);
				}
				//Debug.Log("SPRITE PREFIX:" + prefix);

				string namePrefix = ResolveLpcNamePrefix (row, col, prefix);

				// Debug.Log ("SPRITE COLUMN: " + col);
				if (animState == LpcAnimationState.OneHandedBackslash)
					col -= m_OsFrames;
				else if (animState == LpcAnimationState.CombatIdle)
					col -= m_CiFrames;
				else if (animState == LpcAnimationState.Sit2)
					col -= m_S1Frames;
				else if (animState == LpcAnimationState.Sit3)
					col -= m_S1Frames + m_S2Frames;
				else if (animState == LpcAnimationState.Emote)
					col -= m_S1Frames + m_S2Frames + m_S3Frames;
				// Debug.Log ("SPRITE NAME COLUMN: " + col);
				rect.name = namePrefix + col;
				if (animState == LpcAnimationState.OneHandedBackslash)
					col += m_OsFrames;
				else if (animState == LpcAnimationState.CombatIdle)
					col += m_CiFrames;
				else if (animState == LpcAnimationState.Sit2)
					col += m_S1Frames;
				else if (animState == LpcAnimationState.Sit3)
					col += m_S1Frames + m_S2Frames;
				else if (animState == LpcAnimationState.Emote)
					col += m_S1Frames + m_S2Frames + m_S3Frames;

				// Debug.Log ("SPRITE NAME: " + meta.name);
				rects.Add (rect);
			}
		}

		for (int i = 0; i < rects.Count; i++) {
			if (i < previousSpriteRects.Length && IsValidGUID (previousSpriteRects[i].spriteID)) {
				rects[i].spriteID = previousSpriteRects[i].spriteID;
			}
		}

		return rects.ToArray ();
	}

	private static GUID[] INVALID_GUIDS = new GUID[] {
		new GUID ("00000000000000000800000000000000")
	};

	public bool IsValidGUID (GUID guid) {
		if (guid.Empty ()) {
			return false;
		}

		foreach (GUID invalidGUID in INVALID_GUIDS) {
			if (guid == invalidGUID) {
				return false;
			}
		}

		return true;
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage ("Correctness", "UNT0033:Incorrect message case", Justification = "False Positive")]
	public void OnPostprocessSprites (Texture2D texture, Sprite[] sprites) {
		AssetDatabase.ImportAsset (assetPath, ImportAssetOptions.ForceUpdate);
		Debug.Log ("Sliced Sprites: " + sprites.Length);
	}

	// Check if a texture is a LPC Spritesheet by
	// checking the textures width and height
	private bool IsLpcSpriteSheet (Texture2D texture) {
		if ((texture.width == LPC_SHEET_WIDTH && texture.height == LPC_SHEET_HEIGHT) || (texture.width == LPC_SHEET_WIDTH_EXTENDED && texture.height == LPC_SHEET_HEIGHT_EXTENDED)) {
			return true;
		} else {
			return false;
		}
	}

	private LpcAnimationState GetAnimationState (int row, int col) {
		if (m_extended) {
			switch (row) {
				case (0):
				case (1):
				case (2):
				case (3):
					return LpcAnimationState.OneHandedHalfslash;
				case (4):
				case (5):
				case (6):
				case (7):
					if (col < m_OsFrames)
						return LpcAnimationState.OneHandedSlash;
					else
						return LpcAnimationState.OneHandedBackslash;
				case (8):
				case (9):
				case (10):
				case (11):
					return LpcAnimationState.Run;
				case (12):
				case (13):
				case (14):
				case (15):
					if (col == m_S1Frames - 1)
						return LpcAnimationState.Sit1;
					else if (col == m_S1Frames + m_S2Frames - 1)
						return LpcAnimationState.Sit2;
					else if (col == m_S1Frames + m_S2Frames + m_S3Frames - 1)
						return LpcAnimationState.Sit3;
					else
						return LpcAnimationState.Emote;
				case (16):
				case (17):
				case (18):
				case (19):
					return LpcAnimationState.Jump;
				case (20):
				case (21):
				case (22):
				case (23):
					if (col < m_IdFrames)
						return LpcAnimationState.Idle;
					else
						return LpcAnimationState.CombatIdle;
				case (24):
					return LpcAnimationState.Climb;
				case (25):
					return LpcAnimationState.Hurt;
				case (26):
				case (27):
				case (28):
				case (29):
					return LpcAnimationState.Shoot;
				case (30):
				case (31):
				case (32):
				case (33):
					return LpcAnimationState.Slash;
				case (34):
				case (35):
				case (36):
				case (37):
					return LpcAnimationState.Walkcycle;
				case (38):
				case (39):
				case (40):
				case (41):
					return LpcAnimationState.Thrust;
				case (42):
				case (43):
				case (44):
				case (45):
					return LpcAnimationState.Spellcast;
				default:
					Debug.LogError ("GetAnimationState unknown row: " + row);
					return 0;
			}
		} else {
			switch (row) {
				case (0):
					return LpcAnimationState.Hurt;
				case (1):
				case (2):
				case (3):
				case (4):
					return LpcAnimationState.Shoot;
				case (5):
				case (6):
				case (7):
				case (8):
					return LpcAnimationState.Slash;
				case (9):
				case (10):
				case (11):
				case (12):
					return LpcAnimationState.Walkcycle;
				case (13):
				case (14):
				case (15):
				case (16):
					return LpcAnimationState.Thrust;
				case (17):
				case (18):
				case (19):
				case (20):
					return LpcAnimationState.Spellcast;
				default:
					Debug.LogError ("GetAnimationState unknown row: " + row);
					return 0;
			}
		}
	}

	private string ResolveLpcNamePrefix (int row, int col, string prefix) {
		if (m_extended) {
			switch (row) {
				case (0):
					return prefix + "oh_r_";
				case (1):
					return prefix + "oh_d_";
				case (2):
					return prefix + "oh_l_";
				case (3):
					return prefix + "oh_t_";
				case (4):
					if (col < m_OsFrames)
						return prefix + "os_r_";
					else
						return prefix + "ob_r_";
				case (5):
					if (col < m_OsFrames)
						return prefix + "os_d_";
					else
						return prefix + "ob_d_";
				case (6):
					if (col < m_OsFrames)
						return prefix + "os_l_";
					else
						return prefix + "ob_l_";
				case (7):
					if (col < m_OsFrames)
						return prefix + "os_t_";
					else
						return prefix + "ob_t_";
				case (8):
					return prefix + "ru_r_";
				case (9):
					return prefix + "ru_d_";
				case (10):
					return prefix + "ru_l_";
				case (11):
					return prefix + "ru_t_";
				case (12):
					if (col == m_S1Frames - 1)
						return prefix + "s1_r_";
					else if (col == m_S1Frames + m_S2Frames - 1)
						return prefix + "s2_r_";
					else if (col == m_S1Frames + m_S2Frames + m_S3Frames - 1)
						return prefix + "s3_r_";
					else
						return prefix + "em_r_";
				case (13):
					if (col == m_S1Frames - 1)
						return prefix + "s1_d_";
					else if (col == m_S1Frames + m_S2Frames - 1)
						return prefix + "s2_d_";
					else if (col == m_S1Frames + m_S2Frames + m_S3Frames - 1)
						return prefix + "s3_d_";
					else
						return prefix + "em_d_";
				case (14):
					if (col == m_S1Frames - 1)
						return prefix + "s1_l_";
					else if (col == m_S1Frames + m_S2Frames - 1)
						return prefix + "s2_l_";
					else if (col == m_S1Frames + m_S2Frames + m_S3Frames - 1)
						return prefix + "s3_l_";
					else
						return prefix + "em_l_";
				case (15):
					if (col == m_S1Frames - 1)
						return prefix + "s1_t_";
					else if (col == m_S1Frames + m_S2Frames - 1)
						return prefix + "s2_t_";
					else if (col == m_S1Frames + m_S2Frames + m_S3Frames - 1)
						return prefix + "s3_t_";
					else
						return prefix + "em_t_";
				case (16):
					return prefix + "ju_r_";
				case (17):
					return prefix + "ju_d_";
				case (18):
					return prefix + "ju_l_";
				case (19):
					return prefix + "ju_t_";
				case (20):
					if (col < m_IdFrames)
						return prefix + "id_r_";
					else
						return prefix + "ci_r_";
				case (21):
					if (col < m_IdFrames)
						return prefix + "id_d_";
					else
						return prefix + "ci_d_";
				case (22):
					if (col < m_IdFrames)
						return prefix + "id_l_";
					else
						return prefix + "ci_l_";
				case (23):
					if (col < m_IdFrames)
						return prefix + "id_t_";
					else
						return prefix + "ci_t_";
				case (24):
					return prefix + "cl_";
				case (25):
					return prefix + "hu_";
				case (26):
					return prefix + "sh_r_";
				case (27):
					return prefix + "sh_d_";
				case (28):
					return prefix + "sh_l_";
				case (29):
					return prefix + "sh_t_";
				case (30):
					return prefix + "sl_r_";
				case (31):
					return prefix + "sl_d_";
				case (32):
					return prefix + "sl_l_";
				case (33):
					return prefix + "sl_t_";
				case (34):
					return prefix + "wc_r_";
				case (35):
					return prefix + "wc_d_";
				case (36):
					return prefix + "wc_l_";
				case (37):
					return prefix + "wc_t_";
				case (38):
					return prefix + "th_r_";
				case (39):
					return prefix + "th_d_";
				case (40):
					return prefix + "th_l_";
				case (41):
					return prefix + "th_t_";
				case (42):
					return prefix + "sc_r_";
				case (43):
					return prefix + "sc_d_";
				case (44):
					return prefix + "sc_l_";
				case (45):
					return prefix + "sc_t_";
				default:
					Debug.LogError ("ResolveLpcNamePrefix unknown row: " + row);
					return "";
			}
		} else {
			switch (row) {
				case (0):
					return "hu_";
				case (1):
					return "sh_r_";
				case (2):
					return "sh_d_";
				case (3):
					return "sh_l_";
				case (4):
					return "sh_t_";
				case (5):
					return "sl_r_";
				case (6):
					return "sl_d_";
				case (7):
					return "sl_l_";
				case (8):
					return "sl_t_";
				case (9):
					return "wc_r_";
				case (10):
					return "wc_d_";
				case (11):
					return "wc_l_";
				case (12):
					return "wc_t_";
				case (13):
					return "th_r_";
				case (14):
					return "th_d_";
				case (15):
					return "th_l_";
				case (16):
					return "th_t_";
				case (17):
					return "sc_r_";
				case (18):
					return "sc_d_";
				case (19):
					return "sc_l_";
				case (20):
					return "sc_t_";
				default:
					Debug.LogError ("ResolveLpcNamePrefix unknown row: " + row);
					return "";
			}
		}
	}

}