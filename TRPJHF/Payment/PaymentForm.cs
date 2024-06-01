using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRPJHF
{
    public partial class PaymentForm : Form
    {
        int totalMaterialCost;
        int totalServiceCost;
        int registeredWorkCount;
        double totalInvoicedServiceTime;
        int registeredWorksheetCount;
        int totalAmountToPay;

        public PaymentForm(int totalMaterialCost, int totalServiceCost, int registeredWorkCount, double totalInvoicedServiceTime, int registeredWorksheetCount)
        {
            InitializeComponent();
            this.FormClosing += PaymentForm_FormClosing;

            this.totalMaterialCost = totalMaterialCost;
            this.totalServiceCost = totalServiceCost;
            this.registeredWorkCount = registeredWorkCount;
            this.totalInvoicedServiceTime = totalInvoicedServiceTime;
            this.registeredWorksheetCount = registeredWorksheetCount;

            // Initialize totalAmountToPay after assigning values to totalMaterialCost and totalServiceCost
            totalAmountToPay = totalMaterialCost + totalServiceCost;


            // Create labels for displaying summarized data
            Label labelNumWorks = new Label
            {
                Text = $"Number of registered works:",
                AutoSize = true,
                Location = new Point(20, 20)
            };
            Controls.Add(labelNumWorks);

            Label labelMaterialCost = new Label
            {
                Text = $"Material Cost:",
                AutoSize = true,
                Location = new Point(20, 50)
            };
            Controls.Add(labelMaterialCost);

            Label labelServiceCost = new Label
            {
                Text = $"Service Cost:",
                AutoSize = true,
                Location = new Point(20, 80)
            };
            Controls.Add(labelServiceCost);

            Label labelNumWorksheets = new Label
            {
                Text = $"Number of registered worksheets:",
                AutoSize = true,
                Location = new Point(20, 110)
            };
            Controls.Add(labelNumWorksheets);

            Label labelTotalServiceTime = new Label
            {
                Text = $"Total Invoiced Service Time:",
                AutoSize = true,
                Location = new Point(20, 140)
            };
            Controls.Add(labelTotalServiceTime);

            Label labelTotalAmount = new Label
            {
                Text = $"Total:",
                AutoSize = true,
                Location = new Point(20, 170)
            };
            Controls.Add(labelTotalAmount);


            // Create labels for displaying summarized values with custom colors and formatting
            Label labelNumWorksValue = new Label
            {
                Text = $"{registeredWorkCount} db",
                AutoSize = true,
                Location = new Point(labelNumWorks.Right + 10, 20),
                ForeColor = Color.Orange // color for work count value
            };
            labelNumWorksValue.Font = new Font(labelNumWorksValue.Font, FontStyle.Bold); // Bold font
            labelNumWorksValue.Font = new Font(labelNumWorksValue.Font.FontFamily, 10); // Larger font size
            Controls.Add(labelNumWorksValue);

            Label labelMaterialCostValue = new Label
            {
                Text = $"{totalMaterialCost} Ft",
                AutoSize = true,
                Location = new Point(labelMaterialCost.Right + 10, 50),
                ForeColor = Color.FromArgb(0, 128, 146) // color for material cost value (Craftsman Blue)
            };
            labelMaterialCostValue.Font = new Font(labelMaterialCostValue.Font, FontStyle.Bold); // Bold font
            labelMaterialCostValue.Font = new Font(labelMaterialCostValue.Font.FontFamily, 10); // Larger font size
            Controls.Add(labelMaterialCostValue);

            Label labelServiceCostValue = new Label
            {
                Text = $"{totalServiceCost} Ft",
                AutoSize = true,
                Location = new Point(labelServiceCost.Right + 10, 80),
                ForeColor = Color.Red // color for service cost value
            };
            labelServiceCostValue.Font = new Font(labelServiceCostValue.Font, FontStyle.Bold); // Bold font
            labelServiceCostValue.Font = new Font(labelServiceCostValue.Font.FontFamily, 10); // Larger font size
            Controls.Add(labelServiceCostValue);

            Label labelNumWorksheetsValue = new Label
            {
                Text = $"{registeredWorksheetCount} db",
                AutoSize = true,
                Location = new Point(labelNumWorksheets.Right + 10, 110)
            };
            labelNumWorksheetsValue.Font = new Font(labelNumWorksheetsValue.Font, FontStyle.Bold); // Bold font
            labelNumWorksheetsValue.Font = new Font(labelNumWorksheetsValue.Font.FontFamily, 10); // Larger font size
            Controls.Add(labelNumWorksheetsValue);

            Label labelTotalServiceTimeValue = new Label
            {
                Text = $"{totalInvoicedServiceTime} hours",
                AutoSize = true,
                Location = new Point(labelTotalServiceTime.Right + 10, 140)
            };
            labelTotalServiceTimeValue.Font = new Font(labelTotalServiceTimeValue.Font, FontStyle.Bold); // Bold font
            labelTotalServiceTimeValue.Font = new Font(labelTotalServiceTimeValue.Font.FontFamily, 10); // Larger font size
            Controls.Add(labelTotalServiceTimeValue);

            Label labelTotalAmountValue = new Label
            {
                Text = $"{totalAmountToPay} Ft",
                AutoSize = true,
                Location = new Point(labelTotalAmount.Right + 10, 170),
                ForeColor = Color.FromArgb(140, 137, 253)  // color total amount value (Lavender Blue Shadow)
            };
            labelTotalAmountValue.Font = new Font(labelTotalAmountValue.Font, FontStyle.Bold); // Bold font
            labelTotalAmountValue.Font = new Font(labelTotalAmountValue.Font.FontFamily, 10); // Larger font size
            Controls.Add(labelTotalAmountValue);


            // Create and configure the payment button
            Button paymentButton =  new Button
            {
                Text = "Pay",
                ForeColor = Color.White, // Text color
                BackColor = Color.FromArgb(0, 123, 155), // Blue Background color 
                Font = new Font("Arial", 10, FontStyle.Bold), // Font style
                Size = new Size(100, 30), // Size of the button
                Location = new Point(20, 220) // Position of the button
            };

            // Wire up the event handler for the payment button
            paymentButton.Click += paymentButton_Click;

            // Add the payment button to the form's controls
            this.Controls.Add(paymentButton);
        }


        private void paymentButton_Click(object sender, EventArgs e)
        {
            // Check if any of the values are greater than zero
            if (registeredWorksheetCount > 0 ||
                registeredWorkCount > 0 ||
                totalMaterialCost > 0 ||
                totalServiceCost > 0 ||
                totalInvoicedServiceTime > 0)
            {
                // Display a message box confirming the payment
                MessageBox.Show("Payment successful.", "Payment Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset the data
                ResetData();

                // Close the form
                this.Close();
            }
            else
            {
                // Display a message indicating there is nothing to pay
                MessageBox.Show("There are no registered worksheets to pay for.", "Payment Not Needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Close the form
                this.Close();
            }
        }

        private void PaymentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Reset all data to zero
            ResetData();
        }

        private void ResetData()
        {
            registeredWorksheetCount = 0;
            registeredWorkCount = 0;
            totalMaterialCost = 0;
            totalServiceCost = 0;
            totalInvoicedServiceTime = 0;
        }
    }
}
