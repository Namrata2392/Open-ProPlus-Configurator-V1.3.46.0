using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenProPlusConfigurator
{
	/// <summary>
	/// Maintains the BDA information.
	/// </summary>
	public class Bda
	{
		/// <summary>
		/// The identifier.
		/// </summary>
		public int ID { get; private set; }
		/// <summary>
		/// The SCL type.
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
		/// Initializes a new instance of the <see cref="Bda"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="bType">The basic IEC type.</param>
		/// <param name="type">The SCL type.</param>
		public Bda(int id, string name, string bType, string type)
		{
			this.ID    = id;
			this.Name  = name;
			this.BType = bType;
			this.Type  = type;
		}
	}
}
