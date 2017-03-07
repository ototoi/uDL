using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uDL
{
	/// <summary>
	/// SGD optimizer comonent.
	/// </summary>
	public class SGDOptimizerComponent : MonoBehaviour, IOptimizerComponent
	{
		[SerializeField]
		[Range(0.001f, 0.5f)]
		private float LearningRate = 0.01f;

		public void Initialize()
		{
			;//
		}

		public Dictionary<string, float> Calculate(IDictionary<string, float> p, IDictionary<string, float> g)
		{
			Dictionary<string, float> ret = new Dictionary<string, float> ();
			foreach(string key in p.Keys)
			{
				ret [key] = p[key] - this.LearningRate * g [key];
			}
			return ret;
		}
	}
}