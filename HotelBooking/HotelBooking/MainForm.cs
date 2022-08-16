using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HotelBooking
{
    public partial class MainForm : Form

    { /// <summary>
    ///  declare variables
    /// </summary>
        private static string applicationFolder = Application.StartupPath;
        private ReservationManager reservationManager;
        private string dbFileFilter = "Text files(*.rsvndb)|*.rsvndb|All files(*.*)|*.*"; // customize file type

        public MainForm()
        {
            InitializeComponent();
            this.Text = "HOTEL RESERVATION APP BY ISAAC";
            InitializeGUI();
        }
        /// <summary>
        /// initialize inputs
        /// </summary>
        private void InitializeReservationInput()
        {
            txtBoxFirstName.Text = string.Empty;
            txtBoxLastName.Text = string.Empty;
            
            txtBoxAddressLine1.Text = string.Empty;
            txtBoxAddressLine2.Text = string.Empty;
            txtBoxStreet1.Text = string.Empty;
            txtBoxStreet2.Text = string.Empty;
            cmbCountry.SelectedItem = string.Empty;
            txtBoxCity.Text = string.Empty;
            txtBoxZipCode.Text = string.Empty;

            txtBoxHomePhone.Text = string.Empty;
            txtBoxWorkPhone.Text = string.Empty;
            txtBoxHomeEmail.Text = string.Empty;
            txtBoxWorkEmail.Text = string.Empty;

            checkInDatePicker.Format = DateTimePickerFormat.Custom;
            checkInDatePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            checkInDatePicker.Value = DateTime.Now;
            checkOutDatePicker.Format = DateTimePickerFormat.Custom;
            checkOutDatePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            checkOutDatePicker.Value = checkInDatePicker.Value.AddDays(1);

            txtBoxAdults.Text = "1";
            txtBoxChildren.Text = "0";
            
            txtBoxCardNo.Text = string.Empty;
            txtBoxCVC.Text = string.Empty;
            txtBoxCVC.MaxLength = 3;
            txtBoxCardName.Text = string.Empty;
            txtBoxValidity.Text = string.Empty;

            // Room type
            rdbtnStandard.Checked = true;
            // Card type
            rdbtnVisa.Checked = true;
        }
        /// <summary>
        /// Initialize GUI
        /// set tooltips
        /// enable and disable some buttons on initialization
        /// </summary>
        private void InitializeGUI()
        {
            reservationManager = new ReservationManager();
            InitializeReservationInput();
            lstViewBookings.Items.Clear();
            lstBoxOutput.Items.Clear();
            

            toolTip1.ShowAlways = true;
            string tips = "Input all infor here!" + Environment.NewLine;
            toolTip1.SetToolTip(grpBoxReservation, tips);
            string addtips = "Press thid button to display items in the list box " + Environment.NewLine;

            toolTip2.SetToolTip(btnAdd, addtips);
            string deltips = " Select one or more bookings in the listbox then press this button to delete" + Environment.NewLine;
            toolTip1.SetToolTip(btnDelete, deltips);
            string Changetips = " Select a booking in the listbox then press this button to Change" + Environment.NewLine;
            toolTip1.SetToolTip(btnEdit, Changetips);

            menuFileOpen.Enabled = true;
            menuFileSave.Enabled = true;
            menuFileExit.Enabled = true;
           
            // Change and Delete buttons disabled on initialization
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
        /// <summary>
        /// Exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        { DialogResult dialogResult = MessageBox.Show("Do you want to exit the program?", "Confirm", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        /// <summary>
        /// set radiobuttons to card index
        /// set radiobuttons room index
        /// validate and store user input
        /// use an error string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
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
                txtBoxValidity.Text
                );
            string errorString;
            bool inputOK = inputs.ValidateInputs(out errorString);

            if (inputOK)
            {   // Populate address
                Address address = new Address(
                    txtBoxCity.Text, (Countries)cmbCountry.SelectedIndex,
                    txtBoxStreet1.Text, txtBoxStreet2.Text, txtBoxZipCode.Text,
                    txtBoxAddressLine1.Text, txtBoxAddressLine2.Text);

                // Populate contact
                Contact contact = new Contact();
                contact.FirstName = txtBoxFirstName.Text;
                contact.LastName = txtBoxLastName.Text;
               

                // validate email input
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
                int rmTypIndex = 0;
                if (rdbtnSuite.Checked)
                {
                    rmTypIndex = 2;
                }
                else if (rdbtnDeluxSuite.Checked)
                {
                    rmTypIndex = 1;
                }
                else if (rdbtnStandard.Checked)
                {
                    // rdbtnStandard.Checked
                    rmTypIndex = 0;
                }                
                Reservation reservation = new Reservation(
                    contact,
                    checkInDatePicker.Value.Date,
                    checkOutDatePicker.Value.Date,
                    Convert.ToUInt16(txtBoxAdults.Text),
                    Convert.ToUInt16(txtBoxChildren.Text),
                    new Room((RoomType)rmTypIndex));

                // Add card to the reservation
                Card card = new Card(
                    (CardType)crdTypIndex, 
                    txtBoxCardName.Text,
                     Convert.ToUInt64(txtBoxCardNo.Text),
                     txtBoxCVC.Text,
                     txtBoxValidity.Text);                  

                reservation.Card = card;
                reservationManager.AddReservation(reservation);
                
                UpdateReservations();
                InitializeReservationInput();
            }
            else
            {
                MessageBox.Show($"Please enter all (*) mandatory fields! {errorString}", "Missing/Invalid data");
            }
        }
         /// <summary>
         /// Update reservations and add items to list box
         /// </summary>

        private void UpdateReservations()
        {
            lstViewBookings.Items.Clear();
            for(int i = 0; i < reservationManager.ReservationCount(); i++)
            {
                string[] itemRow = reservationManager.GetReservation(i).GetColumnStrings();
                ListViewItem item = new ListViewItem(itemRow);
                lstViewBookings.Items.Add(item);
            }

            ComputeReservationsSummary();
        }
           /// <summary>
           /// Summarize output
           /// </summary>
        private void ComputeReservationsSummary()

        {
            string textInput;
            Reservation resvtn;
            int totalAdults = 0;
            int totalChildren = 0;
            int totalNights = 0;
            int totalStdRooms = 0;
            int totalDlxRooms = 0;
            int totalSuiteRooms = 0;
            lstBoxOutput.Items.Clear();

            // statistical output
            // increment similar selections
            // Everytime a similar room is selected an increase by one is registered 
            for (int i = 0; i < reservationManager.ReservationCount(); i++)
            {
                resvtn = reservationManager.GetReservation(i);
                totalAdults += resvtn.NumberOfAdults;
                totalChildren += resvtn.NumberOfChildren;
                totalNights += resvtn.NumberOfNights;
                if (resvtn.Room.RmType == RoomType.Standard)
                {
                    totalStdRooms += 1;
                }
                else if (resvtn.Room.RmType == RoomType.DeluxeSuite)
                {
                    totalDlxRooms += 1;
                }
                else if (resvtn.Room.RmType == RoomType.Suite)
                {
                    totalSuiteRooms += 1;
                }
            }
            // strings for display as items in a summary listbox
            lstBoxOutput.Items.Add("Reservation Statistics");
            lstBoxOutput.Items.Add("----------------------");
            lstBoxOutput.Items.Add("Total No. Adults: " + totalAdults.ToString());
            lstBoxOutput.Items.Add("Total No. Children: " + totalChildren.ToString());
            lstBoxOutput.Items.Add("Total No. Nights: " + totalNights.ToString());
            lstBoxOutput.Items.Add("Total No. Standard rooms: " + totalStdRooms.ToString());
            lstBoxOutput.Items.Add("Total No. Deluxe Suite rooms: " + totalDlxRooms.ToString());
            lstBoxOutput.Items.Add("Total No. Suite rooms: " + totalSuiteRooms.ToString());

            // All reservations as strings
            lstBoxOutput.Items.Add("");
            lstBoxOutput.Items.Add("Reservation Details");
            lstBoxOutput.Items.Add("-------------------");
            for (int i = 0; i < reservationManager.ReservationCount(); i++)
            {
                textInput = (i + 1).ToString() + ". "; // e.g. "1. "
                textInput += reservationManager.GetReservation(i).ToString();
                lstBoxOutput.Items.Add(textInput);
            }
        }

        /// <summary>
        /// finds the number of nights or days 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_Click(object sender, EventArgs e)
        {   
                DateTime dateobjstart = this.checkInDatePicker.Value.Date;
                DateTime dateobjend = this.checkOutDatePicker.Value.Date;
                TimeSpan diffResult = dateobjend.Subtract(dateobjstart);

                string NoOfNights = diffResult.ToString();

                MessageBox.Show($" You are spending {NoOfNights} days at the hotel","Number of nights ");                
        }
        
        private void deleteItemAt(int index)
        {
            Reservation reservation = reservationManager.GetReservation(index);
            reservationManager.DeleteReservation(reservation);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstViewBookings.SelectedItems.Count > 0)                               
            { 
                // create a list of selected indices and
                // delete from highest index to lowest
                List<int> selectedIndicies  = lstViewBookings.SelectedIndices.Cast<int>().ToList();
                selectedIndicies.Reverse();
                foreach (int index in selectedIndicies)
                {
                    deleteItemAt(index);
                }
                // Nothing is selected at this point
                lstViewBookings.SelectedIndices.Clear();
                UpdateReservations();
            }
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Edit only when one item is clicked.
            ListView.SelectedIndexCollection selIndexCol = lstViewBookings.SelectedIndices;
            if (selIndexCol.Count == 1)
            {                
                int selIndex = selIndexCol[0];
                Reservation reservation = reservationManager.GetReservation(selIndex);

                EditForm editForm = new EditForm(reservation);
                DialogResult result = editForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Reservation editedreservation = editForm.GetUpdatedReservation;
                    reservationManager.UpdateReservationAt(selIndex, editedreservation);
                    UpdateReservations();
                    lstViewBookings.Items[selIndex].Selected = true;
                }
            }
        }
       /// <summary>
       /// Append input to get card name
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
        /// enable Delete and Edit once items is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstViewBookings_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            btnDelete.Enabled = (lstViewBookings.SelectedItems.Count >= 1);
            btnEdit.Enabled = (lstViewBookings.SelectedItems.Count == 1);
        }
        /// <summary>
        /// Save reservations to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.FileName = "Reservations";
            saveFileDialog1.DefaultExt = "rsvndb";
            saveFileDialog1.Filter = dbFileFilter;
            saveFileDialog1.InitialDirectory = applicationFolder;
            DialogResult dialogResult = saveFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                    FileManager filemanager = new FileManager(saveFileDialog1.FileName, reservationManager);
                    filemanager.SaveToFile();
            }
        }
        /// <summary>
        /// initialize GUI on newfile click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileNew_Click(object sender, EventArgs e)
        {
            InitializeGUI();
        }
        /// <summary>
        /// ReadFile from folder
        /// and after update the Reservations in the GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.Filter = dbFileFilter;
            openFileDialog1.InitialDirectory = applicationFolder;
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                InitializeGUI();
                FileManager filemanager = new FileManager(openFileDialog1.FileName);
                reservationManager = filemanager.ReadFromFile();
                UpdateReservations();
            }
        }
        /// <summary>
        /// Events to control user input, allow only numerical input on phonetextbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxWorkPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
        }
        /// <summary>
        /// 
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
        /// allow only numerical input on txtBoxCvc
        private void txtBoxCVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 
        /// Allows only numerical input on txtBoxAdults
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
        /// Allows only Numerical input on txtBoxChildren
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
        /// Allows only Numerical input on txtBoxValdity
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
        /// Allows only Numerical input on txtBoxZipCode
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
    }
}
