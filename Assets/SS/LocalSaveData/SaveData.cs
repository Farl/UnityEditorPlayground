using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData {

	private static Dictionary<string, object> map = new Dictionary<string, object>();

	public static int GetInt (string id, int defaultValue = -1)
	{
		if (!map.ContainsKey (id)) {
			map.Add (id, defaultValue);
			return defaultValue;
		} else {
			return (int)map [id];
		}
	}

	public static void SetInt(string id, int value)
	{
		if (!map.ContainsKey (id)) {
			map.Add (id, value);
		} else {
			map [id] = value;
		}
	}

	public static bool GetBool(string id, bool defaultValue = false)
	{
		return GetInt (id, defaultValue ? 1 : 0) != 0;
	}

	public static void SetBool(string id, bool value)
	{
		SetInt (id, value ? 1 : 0);
	}
}
