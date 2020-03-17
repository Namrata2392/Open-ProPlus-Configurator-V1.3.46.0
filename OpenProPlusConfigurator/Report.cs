using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenProPlusConfigurator
{
	/// <summary>
	/// Maintains report control block data.
	/// </summary>
	public class Report
	{
		/// <summary>
		/// Full report address, example: TEMPLATELD_ASHIDA/LLN0.brcb1.
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		/// Report name, example: brcb1.
		/// </summary>
		public string ReportName { get; set; }
		/// <summary>
		/// Name of data set assigned to report.
		/// </summary>
		public string DSName { get; set; }
		/// <summary>
		/// Full address of data set assigned to report, example: TEMPLATELD_ASHIDA/LLN0.DS1.
		/// </summary>
		public string DSAddress { get; set; }
		/// <summary>
		/// Full address of data set assigned to report in dotted form, example: DS1.TEMPLATE.LD_ASHIDA.LLN0.
		/// </summary>
		public string DSAddressDots { get; set; }
		/// <summary>
		/// Integrity period.
		/// </summary>
		public string IntgPeriod { get; set; }
		/// <summary>
		/// Report address with replaced '.' to '$' (MMS type).
		/// </summary>
		public string RptId { get; set; }
		/// <summary>
		/// Configuration revision number.
		/// </summary>
		public string ConfRev { get; set; }
		/// <summary>
		/// Indicates the report control block is buffered.
		/// </summary>
		public string Buffered { get; set; }
		/// <summary>
		/// The buffering time.
		/// </summary>
		public string BufTime { get; set; }
		/// <summary>
		/// Trigger options in decimal.
		/// </summary>
		public string TrgOptNum { get; set; }
		/// <summary>
		/// Custom data set indicator.
		/// </summary>
		public string Custom { get; set; }
		/// <summary>
		/// The IED name.
		/// </summary>
		public string IedName { get; set; }
		/// <summary>
		/// The logical device name.
		/// </summary>
		public string LDName { get; set; }
		/// <summary>
		/// The logical node name.
		/// </summary>
		public string LNName { get; set; }
		/// <summary>
		/// Triggering options names dictionary for the form.
		/// </summary>
		public Dictionary<string, string> TrgOps { get; private set; }
		/// <summary>
		/// Optional fields names dictionary for the form.
		/// </summary>
		public Dictionary<string, string> OptFields { get; private set; }
		/// <summary>
		/// Report object constructor - create object and initialize specified variables
		/// </summary>
		/// <param name="addr">Report address.</param>
		/// <param name="iedName">The IED name.</param>
		/// <param name="ld">The logical device name.</param>
		/// <param name="lnName">The logical node name.</param>
		/// <param name="name">The RCB name.</param>
		/// <param name="dsName">The data set name.</param>
		/// <param name="dsRef">The data set reference.</param>
		/// <param name="period">The integrity period.</param>
		/// <param name="rId">The report MMS identifier.</param>
		/// <param name="rev">The configuration revision number.</param>
		/// <param name="buf">The BRCB indicator.</param>
		/// <param name="bTime">The buffering time.</param>
		/// <param name="trgOptions">The triggering options as decimal.</param>
		/// <param name="dsCustom">The custom data set indicator.</param>
		public Report(string addr, string iedName, string ld, string lnName, string name, string dsName, string dsRef, string period, string rId, string rev, string buf, string bTime, string trgOptions, string dsCustom)
		{
			this.IntgPeriod = "0";
			this.BufTime    = "0";
			this.TrgOptNum  = "0";

			this.Address       = addr;
			this.ReportName    = name;
			this.DSAddress     = dsRef;
			this.DSAddressDots = dsName + "." + iedName + "." + ld + "." + lnName;
			this.DSName        = dsName;
			if (period.Length != 0)
			{
				this.IntgPeriod = period;
			}
			this.RptId    = rId;
			this.ConfRev  = rev;
			this.Buffered = buf;
			if (bTime.Length != 0)
			{
				this.BufTime = bTime;
			}
			this.IedName = iedName;
			this.LDName  = ld;
			this.LNName  = lnName;
			if (trgOptions.Length != 0)
			{
				this.TrgOptNum = trgOptions;
			}
			this.Custom = dsCustom;
			TrgOps      = new Dictionary<string, string>
			{
				{ "dchg", "false" },
				{ "qchg", "false" },
				{ "dupd", "false" },
				{ "period", "false" },
				{ "gi", "false" }
			};
			this.OptFields = new Dictionary<string, string>();
		}
	}
}
