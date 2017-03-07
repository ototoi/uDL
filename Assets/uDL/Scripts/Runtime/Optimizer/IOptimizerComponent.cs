using System.Collections;
using System.Collections.Generic;

namespace uDL
{
	public interface IOptimizerComponent  
	{
		void Initialize();
		Dictionary<string, float> Calculate (IDictionary<string, float> p, IDictionary<string, float> g);
	}
}
