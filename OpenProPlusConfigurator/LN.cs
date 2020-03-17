using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenProPlusConfigurator
{
	/// <summary>
	/// The logical node data container.
	/// </summary>
	public class LN
	{
		/// <summary>
		/// The logical node type.
		/// </summary>
		public string LNType { get; private set; }
		/// <summary>
		/// The logical node prefix.
		/// </summary>
		public string LNPrefix { get; private set; }
		/// <summary>
		/// The logical node class.
		/// </summary>
		public string LNClass { get; private set; }
		/// <summary>
		/// The logical node instance.
		/// </summary>
		public string LNInstance { get; private set; }
		/// <summary>
		/// The logical device name.
		/// </summary>
		public string LDName { get; private set; }
		/// <summary>
		/// The IED name.
		/// </summary>
		public string IedName { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LN"/> class.
		/// </summary>
		/// <param name="lnType">The logical node type.</param>
		/// <param name="lnClass">The logical node class.</param>
		/// <param name="lnPrefix">The logical node prefix.</param>
		/// <param name="lnInstance">The logical node instance.</param>
		/// <param name="iedName">The IED name.</param>
		/// <param name="ldName">The logical device name.</param>
		public LN(string lnType, string lnClass, string lnPrefix, string lnInstance, string iedName, string ldName)
		{
			this.LNType     = lnType;
			this.LNClass    = lnClass;
			this.LNPrefix   = lnPrefix;
			this.IedName    = iedName;
			this.LDName     = ldName;
			this.LNInstance = lnInstance;
		}
	}
}
