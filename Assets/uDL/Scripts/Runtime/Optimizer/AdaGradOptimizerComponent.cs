using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uDL
{
	/// <summary>
	/// AdaGrad optimizer comonent.
	/// </summary>
	public class AdaGradOptimizerComponent : MonoBehaviour, IOptimizerComponent
	{
		[SerializeField]
		[Range(0.001f, 0.5f)]
		private float LearningRate = 0.01f;

		[SerializeField]
		[Range(1e-6f, 1e-1f)]
		private float Epsilon = 1e-6f;

		/// <summary>
		/// h Squared Gradiants.
		/// </summary>
		private Dictionary<string, float> h = new Dictionary<string, float>();

		public void Initialize()
		{
			h.Clear ();
		}

		public Dictionary<string, float> Calculate(IDictionary<string, float> p, IDictionary<string, float> g)
		{
			Dictionary<string, float> ret = new Dictionary<string, float> ();
			foreach (var key in p.Keys)
			{
				if(!this.h.ContainsKey(key))
				{
					this.h [key] = 0.0f;
				}
			}
			foreach (var key in p.Keys) 
			{
				this.h [key] += (g [key] * g [key]);
				ret[key] = p [key] - (this.LearningRate * g [key]) / (Mathf.Sqrt(this.h [key]) + this.Epsilon);
			}
			return ret;
		}
	}
}