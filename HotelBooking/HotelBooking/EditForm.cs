using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBooking
{
    partial class EditForm : Form
    {  /// <summary>
    /// declare Reservation
    /// </summary>
        private Reservation reservation;
        public EditForm(Reservation reservation)
        {
            this.reservation = reservation;

            InitializeComponent();
            InitializeGUI();            
        }

        /// <summary>
        /// initializing GUI 
        /// </summary>
        private void InitializeGUI()
        {
            Contact contact = reservation.ContactData;
            txtBoxFirstName.Text = contact.FirstName;
            txtBoxLastName.Text = contact.LastName;
            txtBoxHomePhone.Text = contact.HomePhone;
            txtBoxWorkPhone.Text = contact.WorkPhone;
            txtBoxHomeEmail.Text = contact.HomeEmail;
            txtBoxWorkEmail.Text = contact.WorkEmail;

            Address address = contact.Address;
            txtBoxAddressLine1.Text = address.AddressLine1;
            txtBoxAddressLine2.Text = address.AddressLine2;
            cmbCountry.SelectedItem = address.Country.ToString();
            txtBoxCity.Text = address.City;
            txtBoxZipCode.Text = address.ZipCode;
            txtBoxStreet1.Text = address.Street1;
            txtBoxStreet2.Text = address.Street2;

            txtBoxAdults.Text = reservation.NumberOfAdults.ToString();
            txtBoxChildren.Text = reservation.NumberOfChildren.ToString();

            // Handle dates
            checkInDatePicker.Format = DateTimePickerFormat.Custom;
            checkInDatePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            checkInDatePicker.Value = reservation.CheckInDate;

            checkOutDatePicker.Format = DateTimePickerFormat.Custom;
            checkOutDatePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            checkOutDatePicker.Value = reservation.CheckOutDate;

            // Room type
            RoomType rmType = reservation.Room.RmType;
            if (rmType == RoomType.Suite)
            {
                rdbtnSuite.Checked = true;
            }
            else if (rmType == RoomType.DeluxeSuite)
            {
                rdbtnDeluxSuite.Checked = true;
            }
            else
            {
                // rdbtnStandard.Checked
                rdbtnStandard.Checked = true;
            }

            // Card info
            Card card = reservation.Card;
            CardType cdType = card.CardType;
            if (cdType == CardType.MasterCard)
            {
                rdbtnMastercard.Checked = true;
            }
            else if (cdType == CardType.VisaElectron)
            {
                rdbtnVisaElectron.Checked = true;
            }
            else
            {
                // rdbtnVisa.Checked
                rdbtnVisa.Checked = true;
            }            

            txtBoxCardName.Text = card.AccountName;
            txtBoxCardNo.Text = card.CardNo.ToString();
            txtBoxCVC.MaxLength = 3;// ensures input doesnot exceed three digits
            txtBoxCVC.Text = card.CVC;
            txtBoxCVC.Text = card.CVC;
            txtBoxValidity.Text = card.Validity;
        }
        /// <summary>
        /// Property to get the reservation 
        /// </summary>
        public Reservation GetUpdatedReservation
        {
            get { return reservation; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Edit form items have same names as Main form
            int crdTypIndex = 0; // Default is rdbtnVisa
            if (rdbtnVisaElectron.Checked)
            {
                crdTypIndex = 1;
            }
            else if (rdbtnMastercard.Checked)
            {
                crdTypIndex = 2;
            }

            Utilities inputs = new Utilities(
                txtBoxFirstName.Text,
                txtBoxLastName.Text,
                txtBoxCity.Text,
                cmbCountry.SelectedIndex,
                checkInDatePicker.Value.Date,
                checkOutDatePicker.Value.Date,
                crdTypIndex,
                txtBoxCardNo.Text,
                txtBoxValidity.Text);
                string errorString;
                bool inputOK = inputs.ValidateInputs(out errorString);

            if (inputOK)
            {
                // Update the reservation
                Address address = new Address(
                txtBoxCity.Text, (Countries)cmbCountry.SelectedIndex,
                txtBoxStreet1.Text, txtBoxStreet2.Text, txtBoxZipCode.Text,
                txtBoxAddressLine1.Text, txtBoxAddressLine2.Text);

               // Populate contact
               Contact contact = new Contact();
               contact.FirstName = txtBoxFirstName.Text;
               contact.LastName = txtBoxLastName.Text;

                //Validate email input
                Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (emailRegex.IsMatch(txtBoxHomeEmail.Text) || (emailRegex.IsMatch(txtBoxWorkEmail.Text)))

                {
                    contact.WorkEmail = txtBoxWorkEmail.Text;
                    contact.HomeEmail = txtBoxHomeEmail.Text;
                }
                else
                {
                    MessageBox.Show("InvalidEmail address");
                  
                }
                
               contact.WorkPhone = txtBoxWorkPhone.Text;
               contact.HomePhone = txtBoxHomePhone.Text;
               contact.Address = address;

                // Create reservation

                reservation.ContactData = contact;
                reservation.CheckInDate = checkInDatePicker.Value.Date;
                reservation.CheckOutDate = checkOutDatePicker.Value.Date;
                reservation.NumberOfAdults = Convert.ToUInt16(txtBoxAdults.Text);
                reservation.NumberOfChildren = Convert.ToUInt16(txtBoxChildren.Text);
                reservation.CheckOutDate = checkOutDatePicker.Value.Date;
                // add room info
               int rmTypIndex;
               if (rdbtnSuite.Checked)
               {
                   rmTypIndex = 2;
               }
               else if (rdbtnDeluxSuite.Checked)
               {
                   rmTypIndex = 1;
               }
               else
               {
                   // rdbtnStandard.Checked
                   rmTypIndex = 0;
               }               
                reservation.Room = new Room((RoomType)rmTypIndex);

                //Add payment card info

                     Card card = new Card(
                    (CardType)crdTypIndex,
                    txtBoxCardName.Text,
                     Convert.ToUInt64(txtBoxCardNo.Text),
                     txtBoxCVC.Text,
                     txtBoxValidity.Text);
                     reservation.Card = card;
            }
            else
            {
                MessageBox.Show($"Please enter all (*) mandatory fields! {errorString}", "Missing/Invalid data");
            }
        }
        /// <summary>
        /// sets name to card
        /// </summary>
        private void setCardNameText()
        {
            txtBoxCardName.Text = txtBoxFirstName.Text + " " + txtBoxLastName.Text;
        }

        private void txtBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            setCardNameText();
        }

        private void txtBoxLastName_TextChanged(object sender, EventArgs e)
        {
            setCardNameText();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        ///ensures CVC input is numerical
        private void txtBoxCVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Ensures ZipCode is numerical
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxZipCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Ensures Adult input is numerical
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxAdults_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Ensures Children input is a number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxChildren_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Ensures that the validity period is a number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxValidity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Method ensures Homephone input is a number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                  (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        //Only accept workPhone input as a number
        private void txtBoxWorkPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                  (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
