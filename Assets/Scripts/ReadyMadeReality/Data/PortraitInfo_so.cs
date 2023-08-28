using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReadyMadeReality
{
	[CreateAssetMenu(fileName = "PortraitInfo", menuName = "Container/Portrait Info", order = 1)]
	public class PortraitInfo_so : ScriptableObject
	{
		[SerializeField] public List<Sprite> portraitList = new List<Sprite>();
	} 
}
