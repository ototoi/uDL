using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uDL
{
	/// <summary>
	/// Hyperbora graph function component.
	/// </summary>
	public class HyperboraGraphFunctionComponent : MonoBehaviour 
	{
		[SerializeField]
		float a = 1.0f;

		[SerializeField]
		float b = 1.0f;

		public float Dx(IDictionary<string, float> p)
		{
			return this.a * 2.0f * p ["x"];
		}

		public float Dy(IDictionary<string, float> p)
		{
			return this.b * 2.0f * p ["y"];
		}

		public float Calculate(IDictionary<string, float> p)
		{
			return this.a * Mathf.Pow (p ["x"], 2.0f) + this.b * Mathf.Pow (p ["y"], 2.0f);
		}
	}
}