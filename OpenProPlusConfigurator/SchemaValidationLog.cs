using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace OpenProPlusConfigurator
{
	public partial class SchemaValidationLog: Form
	{
		#region fields
		/// <summary>
		/// true when form is closing
		/// </summary>
		private bool isClosing = false;
		/// <summary>
		/// Last listViewLog control bounds
		/// </summary>
		private Rectangle lastListViewLogBounds;
		private readonly string SclFile;
		private readonly IList<string> SclValidationErrors;
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="pGroups">The profiles group.</param>
		public SchemaValidationLog(string sclFile, IList<string> sclValidationErrors)
		{
			InitializeComponent();
			this.SclFile             = sclFile;
			this.SclValidationErrors = sclValidationErrors;

		}
		#endregion

		#region Events
		/// <summary>
		/// Handles the Shown event of the SchemaValidationLog control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void SchemaValidationLog_Shown(object sender, EventArgs e)
		{
			UpdateListViewLog();
			string message = string.Format("Schema validation has failed. Check the log.\nIt is still possible to open the SCL file from {0}.", this.Text);
			MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// Handles the FormClosing event of the SchemaValidationLog control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
		private void SchemaValidationLog_FormClosing(object sender, FormClosingEventArgs e)
		{
			isClosing = true;
		}

		/// <summary>
		/// Handles the ItemSelectionChanged event of the listViewLog control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">The <see cref="ListViewItemSelectionChangedEventArgs"/> instance containing the event data.</param>
		private void listViewLog_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			// removes selection and focus
			if (e.IsSelected)
			{
				e.Item.Selected = false;
				e.Item.Focused  = false;
			}
		}

		/// <summary>
		/// Handles the SizeChanged event of the listViewLog control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void listViewLog_SizeChanged(object sender, EventArgs e)
		{
			if (lastListViewLogBounds == this.listViewLog.Bounds)
			{
				return;
			}

			lastListViewLogBounds = this.listViewLog.Bounds;
			if (!isClosing)
			{
				UpdateListViewLog();
			}
		}

		/// <summary>
		/// Handles the Click event of the copyToolStripMenuItem control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var point = this.listViewLog.PointToClient(this.contextMenuStripLog.Bounds.Location);
			var item  = this.listViewLog.GetItemAt(5, point.Y);
			if ((item != null) && (item.Tag != null))
			{
				Clipboard.SetText(item.Tag.ToString());
			}
		}

		/// <summary>
		/// Handles the Opening event of the contextMenuStripLog control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
		private void contextMenuStripLog_Opening(object sender, CancelEventArgs e)
		{
			var point = this.listViewLog.PointToClient(this.contextMenuStripLog.Bounds.Location);
			var item  = this.listViewLog.GetItemAt(5, point.Y);
			e.Cancel = (item == null);  // do not show menu when no list view item is selected
		}

		private void openSclFileButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Updates listViewLog
		/// </summary>
		private void UpdateListViewLog()
		{
			this.listViewLog.BeginUpdate();
			this.listViewLog.Items.Clear();
			this.listViewLog.Groups.Clear();

			ListViewGroup filenameGroup = new ListViewGroup(SclFile, HorizontalAlignment.Left);
			this.listViewLog.Groups.Add(filenameGroup);

			if ((this.SclValidationErrors != null) && (this.SclValidationErrors.Count > 0))
			{
				foreach (var line in this.SclValidationErrors)
				{
					foreach (var ss in WrapText(line, this.listViewLog.Font, this.listViewLog.ClientSize.Width))
					{
						this.listViewLog.Items.Add(new ListViewItem() { Text = ss, Group = filenameGroup, Tag = line });
					}
				}
			}
			else
			{
				this.listViewLog.Items.Add(new ListViewItem() { Text = "  - SCL schema validation completed without errors for this file -", Group = filenameGroup });
			}

			this.listViewLog.EndUpdate();
			listViewLog.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		/// <summary>
		/// Wraps text to list of lines. Line 2nd and next start with prefix
		/// </summary>
		/// <param name="font">Font used to measure line</param>
		/// <param name="maxLineWidth">Maximum line width</param>
		/// <param name="prefix">prefix before each separated line (first line doesn't contain prefix)</param>
		/// <returns>list of lines</returns>
		public static IList<string> WrapText(string text, System.Drawing.Font font, int maxLineWidth, string prefix = "    ")
		{
			string[]      words     = text.Split(' ');
			StringBuilder sbuilder  = new StringBuilder();
			int           lineWidth = 0;
			IList<string> result    = new List<string>();

			foreach (string word in words)
			{
				int width = System.Windows.Forms.TextRenderer.MeasureText(word, font).Width;

				if (lineWidth + width >= maxLineWidth)
				{
					result.Add(sbuilder.ToString());
					sbuilder.Length = 0;
					lineWidth       = 0;
					sbuilder.Append(prefix);
				}

				sbuilder.Append(word + " ");
				lineWidth += width;
			}

			if (sbuilder.Length > prefix.Length)
			{
				result.Add(sbuilder.ToString());
			}

			return result;
		}
        #endregion

        private void SchemaValidationLog_Load(object sender, EventArgs e)
        {

        }
    }
}
