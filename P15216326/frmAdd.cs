using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace P15216326
{
    public partial class frmAdd : Form
    {

       public clsAndroidPhone AndroidPhoneDetails;
       public clsApplePhone ApplePhoneDetails;

        bool isAndroid = false;

        public frmAdd(clsApplePhone applePhone)
        {
            InitializeComponent();
            this.ApplePhoneDetails = applePhone;
            isAndroid = false;
            this.dtpDate.MaxDate = DateTime.Today;
        }                

        public frmAdd(clsAndroidPhone phone)
        {
            InitializeComponent();
            this.AndroidPhoneDetails = phone;
            isAndroid = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidatePhone())
            {
                if (isAndroid)
                {
                    AndroidPhoneDetails.Make = txtMake.Text;
                    AndroidPhoneDetails.Model = txtModel.Text;
                    AndroidPhoneDetails.OperatingSystem = txtOpareting.Text;
                    AndroidPhoneDetails.OriginalPrice = Convert.ToDecimal(txtPrice.Text);
                    AndroidPhoneDetails.DatePurchase = Convert.ToDateTime(dtpDate.Text);
                    AndroidPhoneDetails.CurrentCondition = (clsMobilePhone.Condition)cmbCondition.SelectedIndex;
                    //clsMobilePhone.Condition = cmbCondition.Text;
                    // clsAndroidPhone samsung = new clsAndroidPhone(txtMake.Text, txtModel.Text, txtOpareting.Text, new DateTime(2014, 4, 15), Convert.ToDecimal(txtPrice.Text), clsMobilePhone.Condition.fair);
                }
                else
                {
                    ApplePhoneDetails.Make = txtMake.Text;
                    ApplePhoneDetails.Model = txtModel.Text;
                    ApplePhoneDetails.OperatingSystem = txtOpareting.Text;
                    ApplePhoneDetails.OriginalPrice = Convert.ToDecimal(txtPrice.Text);
                    ApplePhoneDetails.DatePurchase = Convert.ToDateTime(dtpDate.Text);

                    ApplePhoneDetails.CurrentCondition = (clsMobilePhone.Condition)cmbCondition.SelectedIndex;
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            if (isAndroid)
            {
                txtMake.Text = AndroidPhoneDetails.Make;

                txtPrice.Text = AndroidPhoneDetails.OriginalPrice.ToString();
                txtModel.Text = AndroidPhoneDetails.Model;
                dtpDate.Text = AndroidPhoneDetails.DatePurchase.ToShortDateString();
                txtOpareting.Text = AndroidPhoneDetails.OperatingSystem;
                cmbCondition.Text = AndroidPhoneDetails.CurrentCondition.ToString();
            }
            else
            {
                txtMake.Text = ApplePhoneDetails.Make;

                txtPrice.Text = ApplePhoneDetails.OriginalPrice.ToString();
                txtModel.Text = ApplePhoneDetails.Model;
                dtpDate.Text = ApplePhoneDetails.DatePurchase.ToShortDateString();
                txtOpareting.Text = ApplePhoneDetails.OperatingSystem;
                cmbCondition.Text = ApplePhoneDetails.CurrentCondition.ToString();
            }

        }

        private bool ValidatePhone()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(txtMake.Text))
            {
                MessageBox.Show("Please enter make", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                isValid = false;
                txtMake.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Please enter model", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                isValid = false;
                txtModel.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtOpareting.Text))
            {
                MessageBox.Show("Please enter Operating system", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                isValid = false;
                txtOpareting.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Please enter price", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                isValid = false;
                txtPrice.Focus();
            }
            else if (cmbCondition.SelectedIndex < 0)
            {
                MessageBox.Show("Please select condition", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                isValid = false;
                cmbCondition.Focus();
            }

            return isValid;
        }
    }
}
