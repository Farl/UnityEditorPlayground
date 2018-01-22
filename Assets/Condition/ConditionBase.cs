using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionBase : ScriptableObject {
	public bool inverse = false;
	public virtual bool CheckCondition()
	{
		return true;
	}
}

public class ConditionList : ConditionBase
{
	public List<ConditionBase> conditions;
}

public class AndCondition : ConditionList
{

	public override bool CheckCondition ()
	{
		bool check = true;
		foreach (ConditionBase c in conditions) {
			check &= c.CheckCondition ();
		}
		return check;
	}
}

public class OrCondition : ConditionList
{

	public override bool CheckCondition ()
	{
		bool check = true;
		foreach (ConditionBase c in conditions) {
			check |= c.CheckCondition ();
		}
		return check;
	}
}

public class BooleanCondition : ConditionBase
{
	public string id;

	public override bool CheckCondition ()
	{
		return SaveData.GetBool (id);
	}
}