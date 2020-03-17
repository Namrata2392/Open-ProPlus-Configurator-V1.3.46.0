﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OpenProPlusConfigurator
{
    /**
    * \brief     <b>ucMasterMODBUS</b> is a user interface to display all IED's & MODBUS parameters
    * \details   This is a user interface to display all IED's & MODBUS parameters. It provides interface
    * to add multiple IED's. The user can also modify IED's parameters.
    * 
    * 
    */
    public partial class ucMasterMODBUS : UserControl
    {
        public event EventHandler btnAddClick;
        public event EventHandler btnDeleteClick;
        public event EventHandler btnExportIEDClick;
        public event EventHandler btnImportIEDClick;
        public event EventHandler btnDoneClick;
        public event EventHandler btnCancelClick;
        public event EventHandler btnFirstClick;
        public event EventHandler btnPrevClick;
        public event EventHandler btnNextClick;
        public event EventHandler btnLastClick;
        public event EventHandler lvIEDListDoubleClick;

        public ucMasterMODBUS()
        {
            InitializeComponent();
           // Utils.createPBTitleBar(pbHdr, lblHdrText, this.PointToScreen(lblHdrText.Location));
            txtMasterNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            cmbProtocolType.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtPollingInterval.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtClockSyncInterval.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtRefreshInterval.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtDebug.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtPortNo.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtFirmwareVer.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtMBDescription.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtUnitID.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtRemoteIP.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtTCPPort.BackColor = System.Drawing.SystemColors.Window;//To make background white for disabled control...
            txtDescription.MaxLength = Globals.MAX_DESCRIPTION_LEN;

            //Ajay: 23/11/2018
            if (ProtocolGateway.AppMode == ProtocolGateway.AppModes.Restricted)
            {
                if (ProtocolGateway.OppMODBUSGroup_ReadOnly)
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    btnExportIED.Enabled = false;
                    btnImportIED.Enabled = false;
                    return;
                }
                else { }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAddClick != null)
                btnAddClick(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDeleteClick != null)
                btnDeleteClick(sender, e);
        }

        private void btnExportIED_Click(object sender, EventArgs e)
        {
            if (btnExportIEDClick != null)
                btnExportIEDClick(sender, e);
        }

        private void btnImportIED_Click(object sender, EventArgs e)
        {
            if (btnImportIEDClick != null)
                btnImportIEDClick(sender, e);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (btnDoneClick != null)
                btnDoneClick(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancelClick != null)
                btnCancelClick(sender, e);
        }

        private void lvIEDList_DoubleClick(object sender, EventArgs e)
        {
            if (lvIEDListDoubleClick != null)
                lvIEDListDoubleClick(sender, e);
        }

        private void pbHdr_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void pbHdr_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIED, sender, e);
        }

        private void lblHdrText_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.handleMouseDown(sender, e);
        }

        private void lblHdrText_MouseMove(object sender, MouseEventArgs e)
        {
            Utils.handleMouseMove(grpIED, sender, e);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (btnFirstClick != null)
                btnFirstClick(sender, e);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (btnPrevClick != null)
                btnPrevClick(sender, e);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNextClick != null)
                btnNextClick(sender, e);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (btnLastClick != null)
                btnLastClick(sender, e);
        }

        private void txtRetries_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtTimeOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }

        private void txtRemoteIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowIP(sender, e);
        }

        private void txtTCPPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.allowNumbersOnly(sender, e, false, false);
        }
        public event ItemCheckEventHandler lvIEDListItemCheck;
        private void lvIEDList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvIEDListItemCheck != null)
                lvIEDListItemCheck(sender, e);
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.SpecialCharacter_Validation(e);
        }
    }
}
