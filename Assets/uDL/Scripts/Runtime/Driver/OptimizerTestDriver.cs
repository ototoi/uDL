using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uDL
{
	[ExecuteInEditMode]
	public class OptimizerTestDriver : MonoBehaviour 
	{
		[SerializeField]
		GameObject[] optimizers;

		[SerializeField]
		LineRenderer[] renderers;

		[SerializeField]
		private Vector3 initialPosition;

		[SerializeField]
		private int maxIteration = 50;

		[SerializeField]
		private HyperboraGraphFunctionComponent graphFunctionComponent;

		private List< List<Vector3> > results = new List<List<Vector3>>();

		public void CalculatePosition()
		{
			results = new List<List<Vector3>>();
			results.Clear ();
			for(int i = 0; i < optimizers.Length; i++) 
			{
				GameObject go = optimizers [i];
				IOptimizerComponent opt = go.GetComponent<IOptimizerComponent> ();
				if (opt != null) 
				{
					this.CalculatePosition (opt);
				}
			}
		}

		protected void CalculatePosition(IOptimizerComponent opt)
		{
			List<Vector3> list = new List<Vector3> ();
			Dictionary<string, float> para = new Dictionary<string, float> ();
			para ["x"] = initialPosition.x;
			para ["y"] = initialPosition.y;
			Dictionary<string, float> prev = new Dictionary<string, float> ();
			prev ["x"] = initialPosition.x;
			prev ["y"] = initialPosition.y;
			Dictionary<string, float> grad = new Dictionary<string, float> ();
			grad ["x"] = graphFunctionComponent.Dx(para);
			grad ["y"] = graphFunctionComponent.Dy(para);

			list.Add (new Vector3(para["x"], para["y"], graphFunctionComponent.Calculate(para)));

			opt.Initialize ();
			for (int i = 0; i < maxIteration; i++) 
			{
				float pz = graphFunctionComponent.Calculate (prev);
				para = opt.Calculate (prev, grad);
				float nz = graphFunctionComponent.Calculate (para);
				list.Add (new Vector3(para["x"], para["y"], nz));
				float dz = (nz - pz);
				grad ["x"] = graphFunctionComponent.Dx(para);
				grad ["y"] = graphFunctionComponent.Dy(para);

				prev ["x"] = para ["x"];
				prev ["y"] = para ["y"]; 
			}

			results.Add (list);
			Debug.Log ("Log1");
		}

		public void Calculate()
		{
			CalculatePosition ();
			for (int i = 0; i < results.Count; i++) 
			{
				LineRenderer lr = renderers [i];
				if (lr.enabled) 
				{
					lr.enabled = false;
					lr.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
					lr.positionCount = results [i].Count;
					lr.SetPositions (results [i].ToArray ());
					lr.enabled = true;
				}
			}
		}

		void Update()
		{
			if (!Application.isPlaying) 
			{
				//this.Calculate ();
			}
		}
	}
}
