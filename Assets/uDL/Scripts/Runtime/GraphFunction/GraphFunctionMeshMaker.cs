using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uDL
{
	/// <summary>
	/// Graph function mesh maker.
	/// </summary>
	[ExecuteInEditMode]
	[RequireComponent(typeof(MeshFilter))]
	public class GraphFunctionMeshMaker : MonoBehaviour 
	{
		[SerializeField]
		private Vector2 xRange = new Vector2(-1, 1);

		[SerializeField]
		private Vector2 yRange = new Vector2(-1, 1);

		[SerializeField]
		private int width = 40;

		[SerializeField]
		private int height = 40;

		[SerializeField]
		private bool backFace = false;

		[SerializeField]
		private HyperboraGraphFunctionComponent graphFunctionComponent;

		//bool isCalced = false;

		void Update()
		{
			if (Application.isPlaying) 
			{
				MeshFilter mf = this.GetComponent<MeshFilter> ();
				if (mf != null && graphFunctionComponent != null) 
				{
					Mesh mesh = new Mesh ();
					UpdateMesh (mesh);
					if (mf.mesh != null) 
					{
						DestroyImmediate (mf.mesh);
					}
					mf.mesh = mesh;
				}
				//isCalced = true;
			}
		}

		/// <summary>
		/// Updates the mesh.
		/// </summary>
		protected void UpdateMesh(Mesh mesh)
		{
			mesh.Clear ();
			int width = this.width;
			int height = this.height;
			Vector3[] positions = new Vector3[width * height];

			float offset_x = xRange[0];
			float dx = (xRange [1] - xRange [0]) / (width - 1);
			float offset_y = yRange[0];
			float dy = (yRange[1] - yRange[0]) / (height - 1);

			Dictionary<string, float> dic = new Dictionary<string, float> ();
			for (int y = 0; y < height; y++) 
			{
				float yy = offset_y + y * dy;
				dic ["y"] = yy;
				for (int x = 0; x < width; x++) 
				{
					float xx = offset_x + x * dx;
					dic ["x"] = xx;
					float zz = graphFunctionComponent.Calculate (dic);
					positions [y * width + x] = new Vector3 (xx, yy, zz);
				}
			}

			Vector2[] uvs = new Vector2[width * height];
			for (int y = 0; y < height; y++) 
			{
				float vv = ((float)y) / (height - 1);
				for (int x = 0; x < width; x++) 
				{
					float uu = ((float)x) / (width - 1);
					uvs [y * width + x] = new Vector2 (uu, vv);
				}
			}

			int[] indices = new int[(width-1)*(height-1) * 3 * 2];

			for (int y = 0; y < height-1; y++) 
			{
				for (int x = 0; x < width-1; x++) 
				{
					int i0 = y * width + x;
					int i1 = (y + 1) * width + x;
					int i2 = (y + 1) * width + (x + 1);
					int i3 = y * width + (x + 1);

					int index = y * (width - 1) + x;
					if (backFace) 
					{
						indices [6 * index + 0] = i0;
						indices [6 * index + 1] = i1;
						indices [6 * index + 2] = i2;
						indices [6 * index + 3] = i0;
						indices [6 * index + 4] = i2;
						indices [6 * index + 5] = i3;
					}
					else 
					{
						indices [6 * index + 0] = i0;
						indices [6 * index + 1] = i2;
						indices [6 * index + 2] = i1;
						indices [6 * index + 3] = i0;
						indices [6 * index + 4] = i3;
						indices [6 * index + 5] = i2;
					}
				}
			}
				
			mesh.vertices = positions;
			mesh.uv = uvs;
			mesh.triangles = indices;
			mesh.RecalculateNormals ();
		}
	}
}
