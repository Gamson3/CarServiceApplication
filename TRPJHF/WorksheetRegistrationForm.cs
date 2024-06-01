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
    public partial class WorksheetRegistrationForm : Form
    {
        public event EventHandler<WorksheetRegistrationEventArgs> WorksheetRegistered;


        private bool formClosingConfirmed = false;
        private List<Work> works;
        private int totalMaterialCost;
        private int totalServiceCost;
        private double totalInvoicedServiceTime;
        private int registeredWorkCount = 0;
        private int registeredWorksheetCount = 0;
        private Dictionary<CheckBox, Label> checkBoxTotalCostLabelMap;
        private Label materialCostTextLabel;
        private Label serviceCostTextLabel;
        private Label materialCostValueLabel;
        private Label serviceCostValueLabel;

        public WorksheetRegistrationForm(List<Work> works)
        {
            InitializeComponent();
            this.works = works;
            checkBoxTotalCostLabelMap = new Dictionary<CheckBox, Label>();

            PopulateWorks();
        }

        private void PopulateWorks()
        {
            int y = 20; // Initial y position for rows
            int rowHeight = 20; // Height of each row
            int spacing = 10; // Spacing between rows

            panelWorks.Controls.Clear(); // Clear existing controls in panelWorks

            // Header row
            Label materialCostHeaderLabel = CreateHeaderLabel("Material Costs", new Point(220, y));
            panelWorks.Controls.Add(materialCostHeaderLabel);

            Label executionTimeHeaderLabel = CreateHeaderLabel("Time", new Point(320, y));
            panelWorks.Controls.Add(executionTimeHeaderLabel);

            Label totalCostsHeaderLabel = CreateHeaderLabel("Total Costs", new Point(480, y));
            panelWorks.Controls.Add(totalCostsHeaderLabel);

            y += rowHeight + spacing; // Increase y position for the first row

            foreach (Work work in works)
            {
                Label nameLabelRow = CreateValueLabel(work.Name, new Point(20, y));
                panelWorks.Controls.Add(nameLabelRow);

                Label materialCostLabel = CreateValueLabel($"{work.MaterialCosts} Ft", new Point(220, y));
                panelWorks.Controls.Add(materialCostLabel);

                int hours = work.ServiceHours;
                int minutes = work.ServiceMinutes;
                Label executionTimeLabel = CreateValueLabel($"{hours} hrs {minutes} mins", new Point(320, y));
                panelWorks.Controls.Add(executionTimeLabel);

                CheckBox checkBox = new CheckBox
                {
                    Tag = work, // Store the associated work object with the checkbox
                    AutoSize = true
                };
                checkBox.Location = new Point(420, y + (rowHeight - checkBox.Height) / 2);
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                panelWorks.Controls.Add(checkBox);

                Label totalCostLabel = CreateValueLabel("", new Point(480, y));
                panelWorks.Controls.Add(totalCostLabel);

                // Map the checkbox to its respective total cost label
                checkBoxTotalCostLabelMap.Add(checkBox, totalCostLabel);

                y += rowHeight + spacing; // Increase y position for the next row
            }

            // Create the labels for material costs and service costs in panelTotals
            materialCostTextLabel = CreateValueLabel("Total Material Costs: ", new Point(20, 10));
            panelTotals.Controls.Add(materialCostTextLabel);

            materialCostValueLabel = CreateHeaderLabel($"{totalMaterialCost} Ft", new Point(materialCostTextLabel.Right + 10, 10));
            materialCostValueLabel.ForeColor = Color.Green; // Set text color to green
            panelTotals.Controls.Add(materialCostValueLabel);

            serviceCostTextLabel = CreateValueLabel("Total Service Costs:", new Point(materialCostValueLabel.Right + 50, 10));
            panelTotals.Controls.Add(serviceCostTextLabel);

            serviceCostValueLabel = CreateHeaderLabel($"{totalServiceCost} Ft", new Point(serviceCostTextLabel.Right + 10, 10));
            serviceCostValueLabel.ForeColor = Color.Red; // Set text color to red
            panelTotals.Controls.Add(serviceCostValueLabel);

            // Register button
            Button registerButton = new Button
            {
                Text = "Register",
                BackColor = Color.LightGray,
                Location = new Point(serviceCostValueLabel.Right + 50, 10)
            };
            registerButton.Click += RegisterButton_Click;
            panelTotals.Controls.Add(registerButton);
        }


        private Label CreateHeaderLabel(string text, Point location)
        {
            Label label = new Label
            {
                Text = text,
                AutoSize = true
            };
            label.Font = new Font(label.Font, FontStyle.Bold);
            label.Location = location;
            return label;
        }

        private Label CreateValueLabel(string text, Point location)
        {
            Label label = new Label
            {
                Text = text,
                AutoSize = true,
                Location = location
            };
            return label;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Checked)
            {
                Work work = checkBox.Tag as Work;
                int individualTotalCost = CalculateIndividualTotalCost(work.RequiredTimeInMinutes);
                if (checkBoxTotalCostLabelMap.ContainsKey(checkBox))
                {
                    Label totalCostLabel = checkBoxTotalCostLabelMap[checkBox];
                    totalCostLabel.Text = $"{individualTotalCost} Ft";
                    totalCostLabel.Visible = true;
                }
            }
            else
            {
                if (checkBoxTotalCostLabelMap.ContainsKey(checkBox))
                {
                    Label totalCostLabel = checkBoxTotalCostLabelMap[checkBox];
                    totalCostLabel.Visible = false;
                }
            }

            CalculateTotalCosts();
        }

        private void CalculateTotalCosts()
        {
            totalMaterialCost = 0;
            totalServiceCost = 0;
            totalInvoicedServiceTime = 0;
            registeredWorkCount = 0;

            foreach (Control control in panelWorks.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    if (checkBox.Checked)
                    {
                        Work work = (Work)checkBox.Tag;
                        totalMaterialCost += work.MaterialCosts;
                        (double totalHours, double serviceCost) = CalculateServiceCost(work.RequiredTimeInMinutes);
                        totalServiceCost += (int)serviceCost;
                        totalInvoicedServiceTime += totalHours;
                        registeredWorkCount++;
                    }
                }
            }

            materialCostValueLabel.Text = $"{totalMaterialCost} Ft";
            serviceCostValueLabel.Text = $"{totalServiceCost} Ft";
        }

        private int CalculateIndividualTotalCost(int timeInMinutes)
        {
            float hours = timeInMinutes / 60f;
            int totalCost = (int)(hours * 15000);
            return totalCost;
        }

        private (double totalHours, double totalCost) CalculateServiceCost(int timeInMinutes)
        {
            int hours = timeInMinutes / 60;
            int minutes = timeInMinutes % 60;
            double totalHours = hours;

            if (minutes > 0 && minutes <= 30)
            {
                totalHours += 0.5f;
            }
            else if (minutes > 30 && minutes < 60)
            {
                totalHours += 1;
            }

            double totalCost = totalHours * 15000;
            return (totalHours, totalCost);
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (registeredWorkCount > 0)
            {
                CalculateTotalCosts();
                OnWorksheetRegistered(new WorksheetRegistrationEventArgs(totalMaterialCost, totalServiceCost, registeredWorkCount, totalInvoicedServiceTime, registeredWorksheetCount));
                formClosingConfirmed = true;
                this.Close();
            }
            else
            {
                formClosingConfirmed = true;
                this.Close();
            }
        }


        protected virtual void OnWorksheetRegistered(WorksheetRegistrationEventArgs e)
        {
            WorksheetRegistered?.Invoke(this, e);
        }

        public class WorksheetRegistrationEventArgs : EventArgs
        {
            public int TotalMaterialCost { get; }
            public int TotalServiceCost { get; }
            public int RegisteredWorkCount { get; }
            public double TotalInvoicedServiceTime { get; }
            public int RegisteredWorksheetCount { get; } // Count of registered worksheets


            public WorksheetRegistrationEventArgs(int totalMaterialCost, int totalServiceCost, int registeredWorkCount, double totalInvoicedServiceTime, int registeredWorksheetCount)
            {
                TotalMaterialCost = totalMaterialCost;
                TotalServiceCost = totalServiceCost;
                RegisteredWorkCount = registeredWorkCount;
                TotalInvoicedServiceTime = totalInvoicedServiceTime;
                RegisteredWorksheetCount = registeredWorksheetCount; // Assign count of registered worksheets
            }
        }      


        private void WorksheetRegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((!formClosingConfirmed && e.CloseReason == CloseReason.UserClosing) || (totalMaterialCost == 0 && totalServiceCost == 0))
            {
                // Prompt confirmation message
                DialogResult result = MessageBox.Show("Are you sure you want to close without registering the worksheet?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancel form closing
                }
            }
            else
            {
            registeredWorksheetCount++; ; // Increment count only if closing is confirmed
            }
        }
    }

}
