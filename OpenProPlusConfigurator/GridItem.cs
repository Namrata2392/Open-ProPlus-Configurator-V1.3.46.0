using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenProPlusConfigurator
{
	/// <summary>
	/// Maintains data of a grid item.
	/// </summary>
	public class GridItem
	{
		/// <summary>
		/// The object reference.
		/// </summary>
		public string ObjectReference { get; private set; }
		/// <summary>
		/// The functional constraint.
		/// </summary>
		public string FC { get; private set; }
		/// <summary>
		/// The user-defined description.
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// The ID number.
		/// </summary>
		public int GID { get; set; }
		/// <summary>
		/// The internal ID number.
		/// </summary>
		public int IID { get; set; }
		/// <summary>
		/// The mapping (SCADA) index.
		/// </summary>
		public string Index { get; set; }
		/// <summary>
		/// The flag to set state change.
		/// </summary>
		public bool IsChecked { get; set; }
		/// <summary>
		/// The logical node name.
		/// </summary>
		public string LogicalNodeName { get; private set; }
		/// <summary>
		/// The mapping type.
		/// </summary>
		public string MappingType { get; set; }
		/// <summary>
		/// The IEC-61850 type.
		/// </summary>
		public string IecType { get; private set; }
		/// <summary>
		/// The IED name.
		/// </summary>
		public string IedName { get; private set; }
		/// <summary>
		/// The user-defined refresh type.
		/// </summary>
		public string RefreshType { get; set; }
		/// <summary>
		/// The control model.
		/// </summary>
		public string ControlModel { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GridItem"/> class.
		/// </summary>
		/// <param name="gid">The global item identifier.</param>
		/// <param name="iid">The internal item identifier.</param>
		/// <param name="selected">The selected indicator.</param>
		/// <param name="type">The IEC type.</param>
		/// <param name="objRef">The reference address.</param>
		/// <param name="fc">The functional constraint.</param>
		/// <param name="desc">The user description.</param>
		/// <param name="ln">The logical node.</param>
		/// <param name="iedName">The IED name.</param>
		/// <param name="model">The control model.</param>
		/// <returns></returns>
		public GridItem(int gid, int iid, bool selected, string type, string objRef, string fc, string desc, string ln, string iedName, string model)
		{
			this.IsChecked       = selected;
			this.IecType         = type;
			this.ObjectReference = objRef;
			this.FC              = fc;
			this.Description     = desc;
			this.GID             = gid;
			this.IID             = iid;
			this.LogicalNodeName = ln;
			this.IedName         = iedName;
			this.MappingType     = "";
			this.RefreshType     = "";
			this.Index           = "";
			this.ControlModel    = model;
		}
	}
}
