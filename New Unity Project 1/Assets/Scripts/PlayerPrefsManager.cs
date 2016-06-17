using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFF_KEY = "difficulty";
    const string LEVEL_KEY = "level_unlocked_";
	const string HIGHSCORE_KEY = "highscore";
	const string NAME_KEY = "name";

//========================Master Volume====================================

    public static void SetMasterVolume(float volume) {
        if (volume >= 0f && volume <= 1f) {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        } else {
            Debug.LogError("Mastervolume out of range");
        }
    }

    public static float GetMasterVolume() {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

//===========================Difficulty=======================================

    public static void SetDifficulty(float difficulty) {
        if(difficulty >= 1f && difficulty <= 3f) {
            PlayerPrefs.SetFloat(DIFF_KEY, difficulty);
        } else {
            Debug.LogError("Difficulty out of range");
        }
    }

    public static float GetDifficulty() {
        return PlayerPrefs.GetFloat(DIFF_KEY);
    }

//============================Highscore========================================
	public static void SetHighscore(int highscore) {
		PlayerPrefs.SetInt(HIGHSCORE_KEY, highscore);
	}

	public static float GetHighscore() {
		return PlayerPrefs.GetInt(HIGHSCORE_KEY);
	}

//============================name========================================

	public static void SetName(string name) {
		PlayerPrefs.SetString(NAME_KEY, name);
	}

	public static string GetName() {
		return PlayerPrefs.GetString(NAME_KEY);
	}

//==========================Level================================================
//not used in this project

	public static void UnlockLevel(int level) {
        if(level <= SceneManager.sceneCountInBuildSettings - 1) {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);
        } else {
            Debug.LogError("Trying to unlock level not in build order");
        }
    }

    public static bool IsLevelUnlocked(int level) {
        int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());
        bool isLevelUnlocked = (levelValue == 1);
        if(level <= SceneManager.sceneCountInBuildSettings - 1) {
            return isLevelUnlocked;
        } else {
            Debug.LogError("Trying to unlock level not in build order");
            return false;
        }
    }
}
