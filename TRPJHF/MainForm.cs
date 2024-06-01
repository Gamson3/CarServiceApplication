using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRPJHF
{
    public partial class MainForm : Form
    {
        private Loader loader;
        private List<Work> works;
        private PaymentForm paymentForm;
        private int totalMaterialCost;
        private int totalServiceCost;
        private int registeredWorkCount;
        private double totalInvoicedServiceTime;
        private int registeredWorksheetCount;

        public MainForm()
        {
            InitializeComponent();
            loader = new Loader();


            // Subscribe to the Load event of MainForm
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initially disable menu items that require loaded works
            worksheetToolStripMenuItem.Enabled = false;
            paymentToolStripMenuItem.Enabled = false;
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Select a Text File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Load the file using the Loader class
                works = loader.LoadFile(filePath);
            }

            // Enable menu items that require loaded works
            worksheetToolStripMenuItem.Enabled = true;
        }

        private void worksheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if works is not null or empty before opening the form
            if (works != null && works.Any())
            {
                // Create an instance of WorksheetRegistrationForm and have it show
                WorksheetRegistrationForm worksheetRegistrationForm = new WorksheetRegistrationForm(works);
                worksheetRegistrationForm.WorksheetRegistered += WorksheetRegistrationForm_WorksheetRegistered;
                worksheetRegistrationForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No works available. Please load the correct file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of PaymentForm and pass the parameters in the correct order
            if (paymentForm == null)
            {
                paymentForm = new PaymentForm(totalMaterialCost, totalServiceCost, registeredWorkCount, totalInvoicedServiceTime, registeredWorksheetCount);
                paymentForm.FormClosed += PaymentForm_FormClosed; // Subscribe to the FormClosed event
            }

            // Show the PaymentForm
            paymentForm.ShowDialog();
        }

        // Event handler for the PaymentForm's FormClosed event
        private void PaymentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Reset all data to zero and set paymentForm to null
            ResetData();
            paymentForm = null;
        }

        private void ResetData()
        {
            registeredWorksheetCount = 0;
            registeredWorkCount = 0;
            totalMaterialCost = 0;
            totalServiceCost = 0;
            totalInvoicedServiceTime = 0;
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Current Date: {DateTime.Now:yyyy.MM.dd}\nNeptun Code: TRPJHF", "About"); 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Prompt confirmation message only if the form is being closed by the user
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Check if the user clicked No
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancel form closing
                }
            }
        }

        private void WorksheetRegistrationForm_WorksheetRegistered(object sender, WorksheetRegistrationForm.WorksheetRegistrationEventArgs e)
        {
            if (e.RegisteredWorkCount > 0) // Check if any work was registered
            {
                totalMaterialCost += e.TotalMaterialCost;
                totalServiceCost += e.TotalServiceCost;
                registeredWorkCount += e.RegisteredWorkCount;
                totalInvoicedServiceTime += e.TotalInvoicedServiceTime;
                registeredWorksheetCount++; // Increment registeredWorksheetCount only if at least one work was registered

                // Enable payment menu item
                paymentToolStripMenuItem.Enabled = true;
            }
        }
    }
}
