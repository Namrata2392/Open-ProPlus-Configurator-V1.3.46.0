using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenProPlusConfigurator
{
	/// <summary>
	/// The logical node type data.
	/// </summary>
	public class LNType
	{
		/// <summary>
		/// The data objects container instance.
		/// </summary>
		private Dictionary<string, string> dataObjects = new Dictionary<string, string>();
		/// <summary>
		/// The data objects container interface.
		/// </summary>
		public IDictionary<string, string> DataObjects
		{
			get
			{
				return this.dataObjects;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LNType"/> class.
		/// </summary>
		public LNType()
		{
		}

		/// <summary>
		/// Adds the data object to maintained collection.
		/// </summary>
		/// <param name="name">The object's name.</param>
		/// <param name="type">The object's type.</param>
		public void AddDO(string name, string type)
		{
			if (!this.dataObjects.ContainsKey(name))
			{
				this.dataObjects.Add(name, type);
			}
		}
	}
}
