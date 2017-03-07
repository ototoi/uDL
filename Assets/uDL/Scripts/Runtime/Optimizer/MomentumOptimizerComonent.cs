using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uDL
{
	/// <summary>
	/// Momemtum optimizer comonent.
	/// </summary>
	public class MomentumOptimizerComonent : MonoBehaviour, IOptimizerComponent
	{
		[SerializeField]
		[Range(0.001f, 0.5f)]
		private float LearningRate = 0.01f;

		[SerializeField]
		[Range(0.0f, 2.0f)]
		private float Momentum = 0.9f;

		/// <summary>
		/// V velocity 
		/// </summary>
		private Dictionary<string, float> Velocity = new Dictionary<string, float>();

		public void Initialize()
		{
			this.Velocity.Clear ();
		}

		public Dictionary<string, float> Calculate(IDictionary<string, float> p, IDictionary<string, float> g)
		{
			Dictionary<string, float> ret = new Dictionary<string, float> ();
			foreach (var key in p.Keys)
			{
				if(!this.Velocity.ContainsKey(key))
				{
					this.Velocity [key] = 0.0f;
				}
			}
			foreach (var key in p.Keys) 
			{
				this.Velocity [key] = this.Momentum * this.Velocity [key] - LearningRate * g [key];
				ret[key] = p [key] + this.Velocity [key];
			}
			return ret;
		}
	}
}