using System.Collections.Generic;
using System.Xml.XPath;

namespace OpenProPlusConfigurator
{
	/// <summary>
	/// Configuration interface providing necessary dictionaries and collections.
	/// </summary>
	public interface IConfigure
	{
		/// <summary>
		/// Types dictionary.
		/// </summary>
		IDictionary<string, string> ITypes
		{
			get;
		}
		/// <summary>
		/// IED dictionary.
		/// </summary>
		IDictionary<string, XPathNodeIterator> IIeds
		{
			get;
		}
		/// <summary>
		/// Additional tags dictionary.
		/// </summary>
		IDictionary<string, PrivateTag> IAdditionalTags
		{
			get;
		}
		/// <summary>
		/// Grid items collection.
		/// </summary>
		ICollection<GridItem> IGridItems
		{
			get;
		}
		/// <summary>
		/// DA types dictionary.
		/// </summary>
		IDictionary<string, Dictionary<string, Bda> > IDATypes
		{
			get;
		}
		/// <summary>
		/// Logical node types dictionary.
		/// </summary>
		IDictionary<string, LNType> ILNodeTypes
		{
			get;
		}
		/// <summary>
		/// DO type dictionary.
		/// </summary>
		IDictionary<string, Dictionary<string, DataItem> > IDOTypes
		{
			get;
		}
		/// <summary>
		/// Logical node dictionary.
		/// </summary>
		IDictionary<string, LN> ILogicalNodes
		{
			get;
		}
		/// <summary>
		/// Predefined data sets dictionary.
		/// </summary>
		IDictionary<string, List<Fcda> > IDataSets
		{
			get;
		}
		/// <summary>
		/// Dynamic data sets dictionary.
		/// </summary>
		IDictionary<string, List<Fcda> > ICustomDataSets
		{
			get;
		}
		/// <summary>
		/// Temporary data sets dictionary.
		/// </summary>
		IDictionary<string, List<Fcda> > ITempDataSets
		{
			get;
		}
		/// <summary>
		/// Reports dictionary.
		/// </summary>
		IDictionary<string, Report> IReports
		{
			get;
		}
		/// <summary>
		/// Reports collection.
		/// </summary>
		ICollection<Report> ILocalRcbs
		{
			get;
		}
		/// <summary>
		/// Used reports collection.
		/// </summary>
		ICollection<Report> IUsedRcbs
		{
			get;
		}
		/// <summary>
		/// Dictionary of mapped IEDs.
		/// </summary>
		IDictionary<string, List<PrivateTag> > IIedMappings
		{
			get;
		}
		/// <summary>
		/// Not included types dictionary.
		/// </summary>
		ICollection<string> IIgnoredObjects
		{
			get;
		}
		/// <summary>
		/// Supported types dictionary.
		/// </summary>
		ICollection<string> ISupportedTypes
		{
			get;
		}
		/// <summary>
		/// Column headers dictionary.
		/// </summary>
		IDictionary<string, int> IColumnHeaders
		{
			get;
		}
		/// <summary>
		/// Supported functional constrains dictionary.
		/// </summary>
		IDictionary<string, string> IUsedFCs
		{
			get;
		}
		/// <summary>
		/// Name of the edited file.
		/// </summary>
		string IOpenedFileName
		{
			get;
			set;
		}
		/// <summary>
		/// Opened file name full path.
		/// </summary>
		string IFullFileNamePath
		{
			get;
			set;
		}
	}
}
