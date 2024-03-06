﻿using IPaking.Ultility;
using iPakrkingv5.Controls;
using iPakrkingv5.Controls.Controls.Buttons;
using iParkingv5.Objects;
using iParkingv5_window.Forms.DataForms;
using iParkingv5_window.Usercontrols.BuildControls;
using iParkingv6.ApiManager.KzParkingv3Apis;
using IPGS.Object.Databases;
using SpreadsheetLight.Charts;
using System.Runtime.InteropServices;
using static iParkingv6.ApiManager.KzParkingv3Apis.KzParkingApiHelper;

namespace iParkingv5_window.Forms.ReportForms
{
    public partial class frmReportIn : Form
    {
        #region Properties

        #endregion End Properties
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;
        private int totalPages = 0;
        private int totalEvents = 0;
        private bool isAllowSelect = false;

        public string selectedIdentityId = String.Empty;
        public string selectedPlateNumber = String.Empty;
        #region Forms
        public frmReportIn(bool isAllowSelect = false)
        {
            InitializeComponent();
            dtpStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtpEndTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            this.KeyPreview = true;
            this.KeyDown += FrmReportIn_KeyDown;
            dgvData.SelectionChanged += DgvData_SelectionChanged;
            dgvData.CellContentClick += DgvData_CellContentClick;
            ucPages1.OnpageSelect += UcPages1_OnpageSelect;
            ucEventInInfo1.onBackClickEvent += UcEventInfo1_onBackClickEvent;
            this.isAllowSelect = isAllowSelect;

            this.Load += FrmReportIn_Load;
            this.SizeChanged += FrmReportIn_SizeChanged;
        }

        private void FrmReportIn_Load(object? sender, EventArgs e)
        {
            CreateUI();
            this.ActiveControl = btnSearch;
            panelData.ToggleDoubleBuffered(true);
            if (this.isAllowSelect)
            {
                dgvData.CellDoubleClick += DgvData_CellDoubleClick;
            }
            picOverviewImageIn.LoadCompleted += Pic_LoadCompleted;
            picVehicleImageIn.LoadCompleted += Pic_LoadCompleted;

            cbVehicleType.SelectedIndexChanged += ChangeSearchConditionEvent;
            cbIdentityGroupType.SelectedIndexChanged += ChangeSearchConditionEvent;
            cbLane.SelectedIndexChanged += ChangeSearchConditionEvent;
            btnSearch.PerformClick();
        }

        private void ChangeSearchConditionEvent(object? sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void FrmReportIn_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnSearch_Click(null, null);
                return;
            }
        }
        private void FrmReportIn_SizeChanged(object? sender, EventArgs e)
        {
            if (ucPages1.Visible)
            {
                dgvData.Height = btnCancel.Location.Y - dgvData.Location.Y - TextManagement.ROOT_SIZE * 2 - ucPages1.Height;
            }
            else
            {
                dgvData.Height = btnCancel.Location.Y - dgvData.Location.Y - TextManagement.ROOT_SIZE * 2;
            }
            tablePic.Height = dgvData.Height;
            tablePic.Width = tablePic.Height * 9 / 16;
            dgvData.Width = this.DisplayRectangle.Width - TextManagement.ROOT_SIZE * 5 - tablePic.Width;

            tablePic.Location = new Point(panelData.Width - tablePic.Width - TextManagement.ROOT_SIZE * 2,
                                          dgvData.Location.Y);

            lblTotalEvents.Location = new Point(btnSearch.Location.X + btnSearch.Width + TextManagement.ROOT_SIZE,
                                               btnSearch.Location.Y - lblTotalEvents.Height + btnSearch.Height);
            ucPages1.Width = panelData.Width - TextManagement.ROOT_SIZE * 4;
            ucPages1.Location = new Point(dgvData.Location.X, dgvData.Location.Y + dgvData.Height + TextManagement.ROOT_SIZE);
        }
        #endregion End Forms

        #region Controls In Form
        private async void btnSearch_Click(object? sender, EventArgs e)
        {
            picOverviewImageIn.Image = Properties.Resources.defaultImage;
            picVehicleImageIn.Image = Properties.Resources.defaultImage;

            string keyword = txtKeyword.Text;
            DateTime startTime = dtpStartTime.Value;
            DateTime endTime = dtpEndTime.Value;
            string vehicleTypeId = ((ListItem)cbVehicleType.SelectedItem)?.Value ?? "";
            string identityGroupId = ((ListItem)cbIdentityGroupType.SelectedItem)?.Value ?? "";
            string laneId = ((ListItem)cbLane.SelectedItem)?.Value ?? "";
            Tuple<List<EventInReport>, int, int> eventInData = await KzParkingApiHelper.GetEventIns(keyword, startTime, endTime, identityGroupId, vehicleTypeId, laneId);
            if (eventInData!.Item1 == null)
            {
                panelData.BackColor = Color.White;
                ucLoading1.HideLoading();
                ucNotify1.Show(ucNotify.EmNotiType.Error, "Không tải được thông tin xe trong bãi. Vui lòng thử lại!");
                return;
            }

            totalPages = eventInData.Item2;
            totalEvents = eventInData.Item3;
            List<EventInReport> eventInReports = eventInData.Item1 ?? new List<EventInReport>();

            panelData.SuspendLayout();
            EnableFastLoading();
            DisplayNavigation();
            DisplayEventInData(eventInReports);
            DisableFastLoading();
            eventInReports.Clear();
            panelData.ResumeLayout();
        }

        private void Pic_LoadCompleted(object? sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            PictureBox pictureBox = (sender as PictureBox)!;
            if (e.Error != null)
            {
                pictureBox.Image = Properties.Resources.defaultImage;
            }
        }
        private async void DgvData_SelectionChanged(object? sender, EventArgs e)
        {
            try
            {
                string physicalFileId = dgvData.CurrentRow?.Cells[dgvData.ColumnCount - 2].Value.ToString() ?? "";
                string[] physicalFileIds = physicalFileId.Split(';');
                if (physicalFileIds.Length >= 2)
                {
                    string displayOverviewInPath = await MinioHelper.GetImage(physicalFileIds[0]);
                    string vehicleInPath = await MinioHelper.GetImage(physicalFileIds[1]);
                    Task task1 = ShowImage(physicalFileIds[0], picOverviewImageIn);
                    Task task2 = ShowImage(physicalFileIds[1], picVehicleImageIn);
                    await Task.WhenAll(task1, task2);
                }
                else if (physicalFileIds.Length > 0)
                {
                    await ShowImage(physicalFileIds[0], picOverviewImageIn);
                    this.Invoke(() =>
                    {
                        picVehicleImageIn.Image = Properties.Resources.defaultImage;
                    });
                }
                else
                {
                    this.Invoke(() =>
                    {
                        picOverviewImageIn.Image = Properties.Resources.defaultImage;
                        picVehicleImageIn.Image = Properties.Resources.defaultImage;
                    });
                }
            }
            catch (Exception)
            {
            }

        }
        private void DgvData_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvData.ColumnCount - 1 && e.RowIndex >= 0)
            {
                panelData.SuspendLayout();
                panelData.BackColor = Color.FromArgb((int)(0.37 * 255), 42, 47, 48);
                foreach (Control item in panelData.Controls)
                {
                    if (item is UserControl)
                    {
                        continue;
                    }
                    if (item is IDesignControl)
                    {
                        ((IDesignControl)item).EnableWaitMode();
                    }
                    else if (!ControlExtensions.IsSupportsTransparency(item))
                    {
                        item.Enabled = false;
                        continue;
                    }
                }

                string identityId = dgvData.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                string plateNumber = dgvData.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
                string datetimeIn = dgvData.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "";
                string laneID = dgvData.Rows[e.RowIndex].Cells[4].Value?.ToString() ?? "";
                string createdById = dgvData.Rows[e.RowIndex].Cells[5].Value?.ToString() ?? "";
                ucEventInInfo1.ShowInfo(Cursor.Position, laneID, datetimeIn, plateNumber, identityId, createdById);
                this.ActiveControl = ucEventInInfo1;
                panelData.ResumeLayout();
            }
        }
        private void DgvData_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            this.selectedIdentityId = dgvData.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
            this.selectedPlateNumber = dgvData.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";

            this.DialogResult = DialogResult.OK;
        }

        private void UcPages1_OnpageSelect(int pageIndex)
        {
            this.Invoke(new Action(async () =>
            {
                picOverviewImageIn.Image = Properties.Resources.defaultImage;
                picVehicleImageIn.Image = Properties.Resources.defaultImage;

                EnableFastLoading();

                string keyword = txtKeyword.Text;
                DateTime startTime = dtpStartTime.Value;
                DateTime endTime = dtpEndTime.Value;
                string vehicleTypeId = ((ListItem)cbVehicleType.SelectedItem)?.Value ?? "";
                string identityGroupId = ((ListItem)cbIdentityGroupType.SelectedItem)?.Value ?? "";
                string laneId = ((ListItem)cbLane.SelectedItem)?.Value ?? "";

                Application.DoEvents();
                Tuple<List<EventInReport>, int, int> eventInData = await KzParkingApiHelper.GetEventIns(keyword, startTime, endTime, identityGroupId, vehicleTypeId, laneId, pageIndex);

                if (eventInData.Item1 == null)
                {
                    DisableFastLoading();
                    ucNotify1.Show(ucNotify.EmNotiType.Error, "Không tải được thông tin xe trong bãi. Vui lòng thử lại!");
                    return;
                }

                totalPages = eventInData.Item2;
                totalEvents = eventInData.Item3;

                List<EventInReport> eventInReports = eventInData.Item1 ?? new List<EventInReport>();
                DisplayEventInData(eventInReports);
                DisableFastLoading();
                eventInReports.Clear();
            }));

        }
        private void UcEventInfo1_onBackClickEvent(object sender)
        {
            panelData.BackColor = Color.White;
            this.Invoke(new Action(() =>
            {
                foreach (Control item in panelData.Controls)
                {
                    if (item is UserControl)
                    {
                        continue;
                    }
                    if (item is IDesignControl)
                    {
                        ((IDesignControl)item).Reset();
                    }
                    else if (!ControlExtensions.IsSupportsTransparency(item))
                    {
                        item.Enabled = true;
                        continue;
                    }
                }
            }));
        }
        
        private void btnExportExcel_Click(object? sender, EventArgs e)
        {
            ExcelTools.CreatReportFile(dgvData, "Báo cáo Xe Đang Trong Bãi");
        }
        private void btnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
        #endregion End Controls In Form

        #region Private Function
        private async void CreateUI()
        {
            btnCancel.InitControl(btnCancel_Click);
            btnExportExcel.InitControl(btnExportExcel_Click);
            btnSearch.InitControl(btnSearch_Click);
            panelData.ToggleDoubleBuffered(true);

            lblTitle.Text = "Danh Sách Xe Đang Trong Bãi";
            lblTitle.Location = new Point(TextManagement.ROOT_SIZE * 2, TextManagement.ROOT_SIZE * 2);
            lblTitle.Font = new Font(lblTitle.Font.Name, TextManagement.ROOT_SIZE * 2, FontStyle.Bold);
            lblTitle.BackColor = Color.Transparent;

            lblKeyword.Location = new Point(lblTitle.Location.X, lblTitle.Location.Y + lblTitle.Height + TextManagement.ROOT_SIZE);
            txtKeyword.Location = new Point(lblKeyword.Location.X + lblKeyword.Width + TextManagement.ROOT_SIZE,
                                            lblKeyword.Location.Y + (lblKeyword.Height - txtKeyword.Height) / 2);

            lblIdentityType.Location = new Point(txtKeyword.Location.X + txtKeyword.Width + TextManagement.ROOT_SIZE * 2, lblKeyword.Location.Y);
            cbIdentityGroupType.Location = new Point(lblIdentityType.Location.X + lblIdentityType.Width + TextManagement.ROOT_SIZE, txtKeyword.Location.Y);

            dtpStartTime.Location = new Point(txtKeyword.Location.X, txtKeyword.Location.Y + txtKeyword.Height + TextManagement.ROOT_SIZE);
            lblStartTime.Location = new Point(lblKeyword.Location.X,
                                              dtpStartTime.Location.Y + (dtpStartTime.Height - lblStartTime.Height) / 2);

            lblEndTime.Location = new Point(lblIdentityType.Location.X, lblStartTime.Location.Y);
            dtpEndTime.Location = new Point(cbIdentityGroupType.Location.X, dtpStartTime.Location.Y);

            cbVehicleType.Location = new Point(txtKeyword.Location.X, dtpStartTime.Location.Y + dtpStartTime.Height + TextManagement.ROOT_SIZE);
            lblVehicleType.Location = new Point(lblKeyword.Location.X,
                                               cbVehicleType.Location.Y + (cbVehicleType.Height - lblVehicleType.Height) / 2);

            cbLane.Location = new Point(dtpEndTime.Location.X, cbVehicleType.Location.Y);
            lblLane.Location = new Point(lblEndTime.Location.X, lblVehicleType.Location.Y);

            btnSearch.Location = new Point(cbLane.Location.X + cbLane.Width + TextManagement.ROOT_SIZE,
                                          cbLane.Location.Y + cbLane.Height - btnSearch.Height);
            btnCancel.Location = new Point(panelData.Width - btnCancel.Width - TextManagement.ROOT_SIZE * 2,
                                           panelData.Height - btnCancel.Height - TextManagement.ROOT_SIZE * 2);
            btnExportExcel.Location = new Point(btnCancel.Location.X - btnExportExcel.Width - TextManagement.ROOT_SIZE,
                                                btnCancel.Location.Y);

            ucPages1.Location = new Point(TextManagement.ROOT_SIZE * 2, btnCancel.Location.Y - ucPages1.Height - TextManagement.ROOT_SIZE);
            ucPages1.Width = panelData.Width - TextManagement.ROOT_SIZE * 4;

            dgvData.Location = new Point(TextManagement.ROOT_SIZE * 2, cbVehicleType.Location.Y + cbVehicleType.Height + TextManagement.ROOT_SIZE);

            if (ucPages1.Visible)
            {
                dgvData.Height = ucPages1.Location.Y - dgvData.Location.Y - TextManagement.ROOT_SIZE * 2;
            }
            else
            {
                dgvData.Height = ucPages1.Location.Y + ucPages1.Height - dgvData.Location.Y - TextManagement.ROOT_SIZE * 2;
            }
            tablePic.Height = dgvData.Height;
            tablePic.Width = tablePic.Height * 9 * 2 / 16;

            dgvData.Width = this.DisplayRectangle.Width - TextManagement.ROOT_SIZE * 5 - tablePic.Width;

            tablePic.Location = new Point(panelData.Width - tablePic.Width - TextManagement.ROOT_SIZE * 2,
                                          dgvData.Location.Y);

            lblTotalEvents.Location = new Point(btnSearch.Location.X + btnSearch.Width + TextManagement.ROOT_SIZE,
                                                btnSearch.Location.Y - lblTotalEvents.Height + btnSearch.Height);

            cbVehicleType.DisplayMember = cbIdentityGroupType.DisplayMember = cbLane.DisplayMember = "Name";
            cbVehicleType.ValueMember = cbIdentityGroupType.ValueMember = cbLane.ValueMember = "Value";

            await LoadVehicleTypeData();
            await LoadIdentityGroup();
            await LoadLaneType();
        }
        private void DisableFastLoading()
        {
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            SendMessage(dgvData.Handle, WM_SETREDRAW, true, 0);
            dgvData.Refresh();
        }
        private void EnableFastLoading()
        {
            dgvData.Rows.Clear();
            dgvData.Refresh();

            SendMessage(dgvData.Handle, WM_SETREDRAW, false, 0);
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }
        private void DisplayNavigation()
        {
            lblTotalEvents.Visible = true;
            lblTotalEvents.Text = "Tổng số sự kiện: " + totalEvents;
            lblTotalEvents.Refresh();
            ucPages1.Visible = totalPages > 1;
            ucPages1.UpdateMaxPage(totalPages);

            if (ucPages1.Visible)
            {
                dgvData.Height = btnCancel.Location.Y - dgvData.Location.Y - TextManagement.ROOT_SIZE * 2 - ucPages1.Height;
            }
            else
            {
                dgvData.Height = btnCancel.Location.Y - dgvData.Location.Y - TextManagement.ROOT_SIZE * 2;
            }
            tablePic.Height = dgvData.Height;
            tablePic.Width = tablePic.Height * 9 / 16;
            dgvData.Width = this.DisplayRectangle.Width - TextManagement.ROOT_SIZE * 5 - tablePic.Width;

            tablePic.Location = new Point(panelData.Width - tablePic.Width - TextManagement.ROOT_SIZE * 2,
                                          dgvData.Location.Y);

            lblTotalEvents.Location = new Point(btnSearch.Location.X + btnSearch.Width + TextManagement.ROOT_SIZE,
                                               btnSearch.Location.Y - lblTotalEvents.Height + btnSearch.Height);
            ucPages1.Width = panelData.Width - TextManagement.ROOT_SIZE * 4;
            ucPages1.Location = new Point(dgvData.Location.X, dgvData.Location.Y + dgvData.Height + TextManagement.ROOT_SIZE);
            panelData.Refresh();
        }
        private void DisplayEventInData(List<EventInReport> eventInReports)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (var item in eventInReports)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvData);
                int i = 0;
                row.Cells[i++].Value = (rows.Count + 1).ToString();//0
                row.Cells[i++].Value = item.identityId;            //1
                row.Cells[i++].Value = item.plateNumber;           //2
                row.Cells[i++].Value = item.DatetimeIn?.ToString("dd/MM/yyyy HH:mm:ss"); //3
                row.Cells[i++].Value = item.laneId;                //4
                row.Cells[i++].Value = item.createdBy;             //5
                row.Cells[i++].Value = item.fileKeys?.Length > 0 ? string.Join(";", item.fileKeys) : "";//6
                row.Cells[i++].Value = "Xem Thêm";//7
                rows.Add(row);
            }
            dgvData.Rows.AddRange(rows.ToArray());
            if (dgvData.RowCount > 0)
            {
                dgvData.CurrentCell = dgvData.Rows[0].Cells[0];
            }
        }

        private async Task ShowImage(string fileKey, PictureBox pic)
        {
            if (!string.IsNullOrEmpty(fileKey))
            {
                string displayPath = await MinioHelper.GetImage(fileKey);
                if (!string.IsNullOrEmpty(displayPath))
                {
                    pic.LoadAsync(displayPath);
                    return;
                }
            }
            pic.Image = Properties.Resources.defaultImage;
        }
        private async Task LoadVehicleTypeData()
        {
            cbVehicleType.Invoke(new Action(() =>
            {
                cbVehicleType.Items.Add(new ListItem()
                {
                    Name = "Tất cả",
                    Value = ""
                });
            }));


            var vehicleTypes = await KzParkingApiHelper.GetAllVehicleTypes() ?? new List<iParkingv5.Objects.Enums.VehicleType>();
            cbVehicleType.Invoke(new Action(() =>
            {
                foreach (var item in vehicleTypes)
                {
                    ListItem vehicleTypeItem = new ListItem()
                    {
                        Name = item.Name,
                        Value = item.Id.ToString()
                    };
                    cbVehicleType.Items.Add(vehicleTypeItem);
                }
                cbVehicleType.SelectedIndex = 0;
            }));
        }
        private async Task LoadIdentityGroup()
        {
            cbIdentityGroupType.Invoke(new Action(() =>
            {
                cbIdentityGroupType.Items.Add(new ListItem()
                {
                    Name = "Tất cả",
                    Value = ""
                });
            }));

            var identities = await KzParkingApiHelper.GetIdentityGroupsAsync() ?? new List<iParkingv5.Objects.Datas.IdentityGroup>();
            cbIdentityGroupType.Invoke(new Action(() =>
            {
                foreach (var item in identities)
                {
                    ListItem identityGroupItem = new ListItem()
                    {
                        Name = item.Name,
                        Value = item.Id.ToString()
                    };
                    cbIdentityGroupType.Items.Add(identityGroupItem);
                }
                cbIdentityGroupType.SelectedIndex = 0;
            }));
        }
        private async Task LoadLaneType()
        {
            cbLane.Invoke(new Action(() =>
            {
                cbLane.Items.Add(new ListItem()
                {
                    Name = "Tất cả",
                    Value = ""
                });
            }));

            var lanes = await KzParkingApiHelper.GetLanesAsync() ?? new List<iParkingv6.Objects.Datas.Lane>();
            cbLane.Invoke(new Action(() =>
            {
                foreach (var item in lanes)
                {
                    ListItem laneItem = new ListItem()
                    {
                        Name = item.name,
                        Value = item.id
                    };
                    cbLane.Items.Add(laneItem);
                }
                cbLane.SelectedIndex = 0;
            }));
        }
        #endregion End Private Function
    }
}
