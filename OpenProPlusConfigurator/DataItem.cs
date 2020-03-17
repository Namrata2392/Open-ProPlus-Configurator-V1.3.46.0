using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenProPlusConfigurator
{
	/// <summary>
	/// Maintains data item data.
	/// </summary>
	public class DataItem
	{
		/// <summary>
		/// The identifier.
		/// </summary>
		public int ID { get; private set; }
		/// <summary>
		/// SDO type if true.
		/// </summary>
		public bool Sdo { get; private set; }
		/// <summary>
		/// Data Item functional constrains
		/// </summary>
		public string FC { get; private set; }
		/// <summary>
		/// The SCL defined type.
		/// </summary>
		public string Type { get; private set; }
		/// <summary>
		/// The basic IEC type.
		/// </summary>
		public string BType { get; private set; }
		/// <summary>
		/// The name.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// The number of elements in case of array.
		/// </summary>
		public string Count { get; private set; }
		/// <summary>
		/// The control model.
		/// </summary>
		public string CtlModel { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DataItem"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="sdo">SDO type if true.</param>
		/// <param name="name">The name.</param>
		/// <param name="fc">The functional constraint.</param>
		/// <param name="bType">The basic IEC type.</param>
		/// <param name="type">The SCL defined type.</param>
		/// <param name="count">The number of elements in case of array.</param>
		/// <param name="ctlmodel">The control model.</param>
		public DataItem(int id, bool sdo, string name, string fc, string bType, string type, string count, string ctlmodel)
		{
			this.ID       = id;
			this.Sdo      = sdo;
			this.Name     = name;
			this.FC       = fc;
			this.BType    = bType;
			this.Type     = type;
			this.Count    = count;
			this.CtlModel = ctlmodel;
		}
	}
}
