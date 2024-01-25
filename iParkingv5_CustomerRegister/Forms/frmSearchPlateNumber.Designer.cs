﻿using iPakrkingv5.Controls.Controls.Buttons;
using iPakrkingv5.Controls.Usercontrols.BuildControls;

namespace iParkingv5_CustomerRegister.Forms
{
    partial class frmSearchPlateNumber
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            ucNotify1 = new ucNotify();
            ucLoading1 = new ucLoading();
            btnOk1 = new LblOk();
            btnCancel1 = new LblCancel();
            lblSearch = new LblSearch();
            lblCustomer = new Label();
            txtKeyword = new TextBox();
            dgvData = new DataGridView();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            lblTittle = new Label();
            panelData = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            panelData.SuspendLayout();
            SuspendLayout();
            // 
            // ucNotify1
            // 
            ucNotify1.BackColor = Color.FromArgb(255, 224, 192);
            ucNotify1.Location = new Point(206, 183);
            ucNotify1.MaximumSize = new Size(333, 356);
            ucNotify1.Message = "Nội dung thông báo";
            ucNotify1.MinimumSize = new Size(333, 356);
            ucNotify1.Name = "ucNotify1";
            ucNotify1.NotiType = ucNotify.EmNotiType.Information;
            ucNotify1.Size = new Size(333, 356);
            ucNotify1.TabIndex = 18;
            // 
            // ucLoading1
            // 
            ucLoading1.BackColor = Color.FromArgb(255, 224, 192);
            ucLoading1.Language = IPaking.Ultility.TextManagement.EmLanguage.Vietnamese;
            ucLoading1.Location = new Point(131, 278);
            ucLoading1.Message = "Preparing to download";
            ucLoading1.Name = "ucLoading1";
            ucLoading1.Size = new Size(392, 188);
            ucLoading1.TabIndex = 17;
            // 
            // btnOk1
            // 
            btnOk1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk1.AutoSize = true;
            btnOk1.BackColor = Color.Gray;
            btnOk1.BorderStyle = BorderStyle.Fixed3D;
            btnOk1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnOk1.ForeColor = Color.Black;
            btnOk1.ImageAlign = ContentAlignment.MiddleLeft;
            btnOk1.Location = new Point(512, 457);
            btnOk1.Name = "btnOk1";
            btnOk1.Size = new Size(63, 22);
            btnOk1.TabIndex = 15;
            btnOk1.Text = "btnOk1";
            btnOk1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancel1
            // 
            btnCancel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel1.AutoSize = true;
            btnCancel1.BorderStyle = BorderStyle.Fixed3D;
            btnCancel1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnCancel1.ForeColor = Color.Black;
            btnCancel1.Location = new Point(581, 457);
            btnCancel1.Name = "btnCancel1";
            btnCancel1.Size = new Size(49, 22);
            btnCancel1.TabIndex = 7;
            btnCancel1.Text = "Đóng";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.BorderStyle = BorderStyle.Fixed3D;
            lblSearch.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lblSearch.Location = new Point(563, 110);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(83, 22);
            lblSearch.TabIndex = 16;
            lblSearch.Text = "lblSearch1";
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.BackColor = Color.Transparent;
            lblCustomer.Location = new Point(24, 65);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(62, 20);
            lblCustomer.TabIndex = 0;
            lblCustomer.Text = "Từ khóa";
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(144, 62);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Tên/Biển số xe";
            txtKeyword.Size = new Size(365, 27);
            txtKeyword.TabIndex = 1;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 255, 255);
            dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvData.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { Column2, Column3, Column8, Column5, Column6, Column11, Column10, Column1, Column4, Column7, Column9 });
            dgvData.Location = new Point(75, 143);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dataGridViewCellStyle3.Padding = new Padding(3);
            dgvData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvData.RowTemplate.Height = 29;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(500, 300);
            dgvData.TabIndex = 11;
            // 
            // Column2
            // 
            Column2.HeaderText = "STT";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 76;
            // 
            // Column3
            // 
            Column3.HeaderText = "Tên";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 74;
            // 
            // Column8
            // 
            Column8.HeaderText = "Biển số xe";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.Width = 133;
            // 
            // Column5
            // 
            Column5.HeaderText = "Loại phương tiện";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 196;
            // 
            // Column6
            // 
            Column6.HeaderText = "Khách hàng";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 146;
            // 
            // Column11
            // 
            Column11.HeaderText = "Mã khách hàng";
            Column11.Name = "Column11";
            Column11.ReadOnly = true;
            Column11.Width = 178;
            // 
            // Column10
            // 
            Column10.HeaderText = "Nhóm khách hàng";
            Column10.Name = "Column10";
            Column10.ReadOnly = true;
            Column10.Width = 205;
            // 
            // Column1
            // 
            Column1.HeaderText = "vehicleId";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Visible = false;
            Column1.Width = 121;
            // 
            // Column4
            // 
            Column4.HeaderText = "vehicleTypeId";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Visible = false;
            Column4.Width = 163;
            // 
            // Column7
            // 
            Column7.HeaderText = "customerId";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Visible = false;
            Column7.Width = 144;
            // 
            // Column9
            // 
            Column9.HeaderText = "customerGroupId";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            Column9.Visible = false;
            Column9.Width = 202;
            // 
            // lblTittle
            // 
            lblTittle.AutoSize = true;
            lblTittle.BackColor = Color.Transparent;
            lblTittle.Dock = DockStyle.Top;
            lblTittle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTittle.ForeColor = Color.FromArgb(0, 64, 0);
            lblTittle.Location = new Point(24, 24);
            lblTittle.Name = "lblTittle";
            lblTittle.Size = new Size(244, 30);
            lblTittle.TabIndex = 10;
            lblTittle.Text = "Danh sách khách hàng";
            // 
            // panelData
            // 
            panelData.BackColor = Color.Transparent;
            panelData.Controls.Add(ucNotify1);
            panelData.Controls.Add(ucLoading1);
            panelData.Controls.Add(btnOk1);
            panelData.Controls.Add(btnCancel1);
            panelData.Controls.Add(lblSearch);
            panelData.Controls.Add(lblCustomer);
            panelData.Controls.Add(txtKeyword);
            panelData.Controls.Add(dgvData);
            panelData.Controls.Add(lblTittle);
            panelData.Dock = DockStyle.Fill;
            panelData.Location = new Point(0, 0);
            panelData.Name = "panelData";
            panelData.Padding = new Padding(24);
            panelData.Size = new Size(1043, 583);
            panelData.TabIndex = 16;
            // 
            // frmSearchPlateNumber
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1043, 583);
            Controls.Add(panelData);
            Name = "frmSearchPlateNumber";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tìm kiếm biển số xe";
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            panelData.ResumeLayout(false);
            panelData.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ucNotify ucNotify1;
        private ucLoading ucLoading1;
        private LblOk btnOk1;
        private LblCancel btnCancel1;
        private LblSearch lblSearch;
        private Label lblCustomer;
        private TextBox txtKeyword;
        private DataGridView dgvData;
        private Label lblTittle;
        private Panel panelData;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column9;
    }
}