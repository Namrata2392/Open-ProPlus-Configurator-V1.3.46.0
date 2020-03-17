using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenProPlusConfigurator
{
	/// <summary>
	/// Stores single entry of the private section.
	/// </summary>
	public class PrivateTag
	{
		/// <summary>
		/// The ID number.
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The mapping index number.
		/// </summary>
		public string Index { get; set; }
		/// <summary>
		/// User added description.
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// The reference path of the data item.
		/// </summary>
		public string Reference { get; set; }
		/// <summary>
		/// The functional constraint.
		/// </summary>
		public string FC { get; set; }
		/// <summary>
		/// The mapping data type.
		/// </summary>
		public string MappingType { get; set; }
		/// <summary>
		/// The refresh type.
		/// </summary>
		public string Rtype { get; set; }
		/// <summary>
		/// The IED name.
		/// </summary>
		public string IedName { get; set; }

		/// <summary>
		/// Creates object which shall be visible as row in output configuration file after mapping changes in the configuration.
		/// </summary>
		/// <param name="id">Unique ID number.</param>
		/// <param name="mapping">User-selected mapping type.</param>
		/// <param name="index">User-selected index number for specified mapping type.</param>
		/// <param name="reference">Object reference.</param>
		/// <param name="fc">Functional constraint.</param>
		/// <param name="desc">User-defined description.</param>
		/// <param name="iedname">IED name.</param>
		/// <param name="fetchType">Refresh type.</param>
		public PrivateTag(int id, string mapping, string index, string reference, string fc, string desc, string iedname, string fetchType)
		{
			this.Id          = id;
			this.Reference   = reference;
			this.FC          = fc;
			this.MappingType = mapping;
			this.Description = desc;
			this.IedName     = iedname;
			this.Rtype       = fetchType;
			this.Index       = index;
		}
	}
}
