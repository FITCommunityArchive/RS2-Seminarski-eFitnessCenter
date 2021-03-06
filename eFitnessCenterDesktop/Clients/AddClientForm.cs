﻿using eFitnessCenterDesktop.Membership;
using eFitnessCenterDesktop.Services;
using Models.Clients;
using Models.Requests;
using Models.Requests.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eFitnessCenterDesktop.Clients
{
    public partial class AddClientForm : Form
    {
        private string _accessToken;
        private readonly ApiService _apiService;
        private readonly Client _clientForEdit;
        private Action<object> _clientAction;

        public AddClientForm(string accessToken, Client client)
        {
            InitializeComponent();
            _accessToken = accessToken;
            _clientForEdit = client;
            _apiService = new ApiService("clients", _accessToken);
            SetClientForEdit();
        }

        public void OnAddOrUpdateClient(Action<object> clientAction)
        {
            _clientAction = clientAction;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {

            if (this.ValidateChildren())
            {
                if (_clientForEdit == null)
                {
                    CreateNewClient();
                }
                else
                {
                    UpdateClient();
                }

                ClientListForm clientListForm = new ClientListForm(_accessToken);
                clientListForm.MdiParent = this.MdiParent;
                clientListForm.WindowState = FormWindowState.Maximized;
                clientListForm.ControlBox = false;
                clientListForm.MaximizeBox = false;
                clientListForm.MinimizeBox = false;
                clientListForm.ShowIcon = false;

                clientListForm.Show();
            }
        }

        private void SetClientForEdit()
        {
            if (_clientForEdit != null)
            {
                lblStatus.Visible = true;
                cbStatus.Visible = true;

                cbStatus.Text = toStringStatus(_clientForEdit.Status);

                tbIme.Text = _clientForEdit.FirstName;
                tbPrezime.Text = _clientForEdit.LastName;
                tbEmail.Text = _clientForEdit.Email;
                tbTelefon.Text = _clientForEdit.PhoneNumber;
                tbAdresa.Text = _clientForEdit.Address;
            }
            else
            {
                btnClanarine.Visible = false;
            }
        }

        private async void CreateNewClient()
        {
            CreateClientRequest createClientRequest = new CreateClientRequest
            {
                FirstName = tbIme.Text,
                LastName = tbPrezime.Text,
                Email = tbEmail.Text,
                Password = tbPassword.Text,
                Address = tbAdresa.Text,
                PhoneNumber = tbTelefon.Text
            };

            await _apiService.Create<Client>(createClientRequest);
        }

        private async void UpdateClient()
        {
            UpdateClientRequest updateClientRequest = new UpdateClientRequest
            {
                FirstName = tbIme.Text,
                LastName = tbPrezime.Text,
                Email = tbEmail.Text,
                Address = tbAdresa.Text,
                PhoneNumber = tbTelefon.Text,
                Password = tbPassword.Text,
                Status = fromString(cbStatus.Text)
            };

            await _apiService.Update<Client>(_clientForEdit.Id ,updateClientRequest);
        }

        private UserStatus? fromString(string userStatus)
        {
            switch (userStatus)
            {
                case "Active": return UserStatus.ACTIVE;
                case "Inactive": return UserStatus.INACTIVE;
                case "Deleted": return UserStatus.DELETED;
                default: return null;
            }
        }

        private string toStringStatus(UserStatus userStatus)
        {
            switch (userStatus)
            {
                case UserStatus.ACTIVE: return "Active";
                case UserStatus.INACTIVE: return "Inactive";
                case UserStatus.DELETED: return "Deleted";
                default: return "Inactive";
            }
        }

        private void LblStatus_Click(object sender, EventArgs e)
        {

        }

        private void BtnClanarine_Click(object sender, EventArgs e)
        {
            MembershipPaymentForm paymentForm = new MembershipPaymentForm(_accessToken, _clientForEdit);
            paymentForm.MdiParent = this.MdiParent;
            paymentForm.WindowState = FormWindowState.Maximized;
            paymentForm.ControlBox = false;
            paymentForm.MaximizeBox = false;
            paymentForm.MinimizeBox = false;
            paymentForm.ShowIcon = false;

            paymentForm.Show();
        }

        private void TbIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbIme.Text))
            {
                errorProvider.SetError(tbIme, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(tbIme, null);
            }
        }

        private void TbPrezime_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPrezime.Text))
            {
                errorProvider.SetError(tbPrezime, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(tbPrezime, null);
            }
        }

        private void TbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                errorProvider.SetError(tbEmail, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(tbEmail, null);
            }
        }

        private void TbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_clientForEdit == null && string.IsNullOrEmpty(tbPassword.Text))
            {
                errorProvider.SetError(tbPassword, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(tbPassword, null);
            }
        }

        private void TbAdresa_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbAdresa.Text))
            {
                errorProvider.SetError(tbAdresa, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(tbAdresa, null);
            }
        }

        private void TbTelefon_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbTelefon.Text))
            {
                errorProvider.SetError(tbTelefon, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(tbTelefon, null);
            }
        }

        private void CbStatus_Validating(object sender, CancelEventArgs e)
        {
            if (cbStatus.Visible && string.IsNullOrEmpty(cbStatus.Text))
            {
                errorProvider.SetError(cbStatus, "Ovo polje je obavezno");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cbStatus, null);
            }
        }

        private void AddClientForm_Load(object sender, EventArgs e)
        {

        }
    }
}
