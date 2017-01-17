using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;        // for Dierctory.GetCurrentDirectiory()

using System.Diagnostics;
using System.Net;

namespace P15216326
{
    public partial class Form1 : Form
    {
        clsPhoneLot phoneLot;
        int currentPhone = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            phoneLot = new clsPhoneLot();

            // add some vehicles here...
            // in the real world we would pick this data up from a database


            // clsAndroidPhone samsung = new clsAndroidPhone("Samsung","Galaxiys6","Lolipop",new DateTime(2014, 4, 15),200,clsMobilePhone.Condition.fair);
            // clsAndroidPhone apple = new clsAndroidPhone("Iphone", "6s", "IOS10", new DateTime(2016, 4, 15), 200, clsMobilePhone.Condition.fair);
            // clsAndroidPhone samsung2 = new clsAndroidPhone("Samsung", "Galaxiys7", "Lolipop", new DateTime(2016, 5, 15), 500, clsMobilePhone.Condition.excellent);
            // phoneLot.AddPhone(samsung);
            //phoneLot.AddPhone(apple);
            // phoneLot.AddPhone(samsung2);

            DisplayPhone();
        }

        private void DisplayPhone()
        {
            lblText.Text = string.Format("Viewing {0} of {1}", phoneLot.PhoneStock.Count > 0 ? phoneLot.PhoneCurrentlyDisplayed + 1 : phoneLot.PhoneCurrentlyDisplayed, phoneLot.NumberOfPhone);

            //textBoxVehicle.Text = carLot.DescribeCurrentVehicle();
            txtList.Text = phoneLot.DescribeCurrentPhone(currentPhone);
            if (phoneLot.PhoneStock.Count == 0)
            {
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            phoneLot.StepToPreviousPhone();
            DisplayPhone();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            phoneLot.StepToNextPhone();
            DisplayPhone();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete?", "Shop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                int index;
                if (txtList.Text != "")
                {
                    index = Convert.ToInt32(phoneLot.PhoneCurrentlyDisplayed);
                    phoneLot.RemovePhoneAt(index);
                }
                else
                {
                    MessageBox.Show("There si nothing to delete");
                }
                DisplayPhone();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clsAndroidPhone androidPhone = new clsAndroidPhone("", "", "Android", DateTime.Now, 0, clsMobilePhone.Condition.good);
            frmAdd addAndroid = new frmAdd(androidPhone);
            DialogResult dr = addAndroid.ShowDialog();

            if (dr == DialogResult.OK)
            {
                phoneLot.AddPhone(addAndroid.AndroidPhoneDetails);
                DisplayPhone();
            }
        }

        private void btnAddA_Click(object sender, EventArgs e)
        {
            clsApplePhone applePhone = new clsApplePhone("", " ", "IOS", DateTime.Now, 0, clsMobilePhone.Condition.good);

            frmAdd addApple = new frmAdd(applePhone);
            DialogResult dr = addApple.ShowDialog();
            if (dr == DialogResult.OK)
            {
                phoneLot.AddPhone(addApple.ApplePhoneDetails);
                DisplayPhone();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            clsMobilePhone phone = phoneLot.GetPhoneCurrentlyDisplayed();
            if (phone is clsAndroidPhone)
            {
                // edit android phone
                frmAdd editAndroid = new frmAdd((clsAndroidPhone)phone);
                DialogResult dr = editAndroid.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    phoneLot.EditPhone(editAndroid.AndroidPhoneDetails);
                    DisplayPhone();
                }
            }
            else
            {
                // edit apple phone
                frmAdd ediApple = new frmAdd((clsApplePhone)phone);
                DialogResult dr = ediApple.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    phoneLot.EditPhone(ediApple.ApplePhoneDetails);
                    DisplayPhone();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //create the save dialog and give it sensible default values
            if (phoneLot.NumberOfPhone != 0)
            {
                string saveFilename = null;
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "*.txt";
                saveDialog.InitialDirectory = Directory.GetCurrentDirectory();
                saveDialog.Filter = "Phone list files (text)|*.txt";
                saveDialog.FileName = "phone_data.txt";

                DialogResult dr = saveDialog.ShowDialog();

                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    // this could fail so we need a try catch block around it
                    try
                    {
                        saveFilename = saveDialog.FileName;

                        //this is the net recipe for saving an list of serializable objects
                        //serializable means able to be sent to a filestream

                        System.IO.FileStream s = new System.IO.FileStream(saveFilename, System.IO.FileMode.Create);
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        //clsPhoneLot aphoneLot = new clsPhoneLot();
                        f.Serialize(s, phoneLot.PhoneStock);
                        s.Close();
                    }
                    catch (System.IO.IOException ex)
                    {
                        MessageBox.Show(ex.Message, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("You cannot save empty list","Error", MessageBoxButtons.OK);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string fileName = null;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = "*.txt";
            openDialog.Filter = "Phone list files (text)|*.txt";
            openDialog.FileName = "phone_data.txt";
            openDialog.InitialDirectory = Directory.GetCurrentDirectory();

            DialogResult dr = openDialog.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                //could fail - so we have a try catch block
                //also we load to a second copy of the animal list
                //then if the load is ok we copy the data to the original list
                //
                //this means that if the load fails we won't lose any original list

                try
                {
                    fileName = openDialog.FileName;

                    System.IO.FileStream filestream = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    List<clsMobilePhone> restoredPhoneList = null;
                    restoredPhoneList = (List<clsMobilePhone>)f.Deserialize(filestream);
                    filestream.Close();
                    //clsPhoneLot aphoneLot = new clsPhoneLot();
                    // copy this across to the real one

                    phoneLot.PhoneStock = restoredPhoneList;

                    //if (restoredPhoneList.Count > 0)
                    //{
                    currentPhone = 0;
                    phoneLot.DescribeCurrentPhone(currentPhone);
                    DisplayPhone();
                    //}
                    //looking at 1st animal (if it exists)

                    //EnableValidControls();

                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(ex.Message, "File Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }      
    }
}
