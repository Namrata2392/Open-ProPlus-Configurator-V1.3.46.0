using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenProPlusConfigurator
{
	/// <summary>
	/// Maintains FCDA data set information (functionally constrained data attribute).
	/// </summary>
	public class Fcda
	{
		/// <summary>
		/// The IED name.
		/// </summary>
		public string Ied { get; set; }
		/// <summary>
		/// The logical device name.
		/// </summary>
		public string LDInst { get; set; }
		/// <summary>
		/// The logical node prefix.
		/// </summary>
		public string LNPrefix { get; set; }
		/// <summary>
		/// The logical node class.
		/// </summary>
		public string LNClass { get; set; }
		/// <summary>
		/// The logical node instance.
		/// </summary>
		public string LNInst { get; set; }
		/// <summary>
		/// The data object name.
		/// </summary>
		public string DOName { get; set; }
		/// <summary>
		/// The data attribute name.
		/// </summary>
		public string DAName { get; set; }
		/// <summary>
		/// The functional constraint.
		/// </summary>
		public string FC { get; set; }
		/// <summary>
		/// The data reference.
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		/// The owner data set name.
		/// </summary>
		public string DataSet { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Fcda"/> class.
		/// </summary>
		/// <param name="ied">The IED name.</param>
		/// <param name="ds">The owner data set name.</param>
		/// <param name="ld">The logical device name.</param>
		/// <param name="lnPrefix">The logical node prefix.</param>
		/// <param name="lnClass">The logical node class.</param>
		/// <param name="lnInstance">The logical node instance.</param>
		/// <param name="don">The data object name.</param>
		/// <param name="dan">The data attribute name.</param>
		/// <param name="fc">The functional constraint.</param>
		public Fcda(string ied, string ds, string ld, string lnPrefix, string lnClass, string lnInstance, string don, string dan, string fc)
		{
			this.Ied      = ied;
			this.LDInst   = ld;
			this.LNPrefix = lnPrefix;
			this.LNClass  = lnClass;
			this.LNInst   = lnInstance;
			this.DOName   = don;
			this.FC       = fc;
			this.DAName   = dan;
			this.DataSet  = ds;
			if (dan.Length == 0)
			{
				this.Address = ied + ld + "/" + lnPrefix + lnClass + lnInstance + "." + don;
			}
			else
			{
				this.Address = ied + ld + "/" + lnPrefix + lnClass + lnInstance + "." + don + "." + dan;
			}
		}
	}
}
