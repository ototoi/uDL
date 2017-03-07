using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uDL
{
	/// <summary>
	/// Adam optimizer comonent.
	/// </summary>
	public class AdamOptimizerComonent : MonoBehaviour
	{
		[SerializeField]
		[Range(0.01f, 0.5f)]
		private float LearningRate = 0.01f;

		public Dictionary<string, float> Calculate(IDictionary<string, float> p, IDictionary<string, float> g)
		{
			Dictionary<string, float> ret = new Dictionary<string, float> ();
			foreach (var key in p.Keys) 
			{
				p [key] -= this.LearningRate * g [key];
			}
			return ret;
		}
	}
}