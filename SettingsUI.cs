using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RegexIgnore
{
    public partial class SettingsUI : JHSoftware.SimpleDNS.Plugin.OptionsUI
    {
        public SettingsUI()
        {
            InitializeComponent();
        }

        public override void LoadData(string config)
        {
           txtRegEx.Text=config;
        }

        public override bool ValidateData()
        {
            txtRegEx.Text = txtRegEx.Text.Trim();
            if (txtRegEx.Text.Length == 0)
            {
                MessageBox.Show("Regular Expression is empty", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegEx.Focus();
                return false;
            }
            System.Text.RegularExpressions.Regex rx;
            try
            {
                rx = new System.Text.RegularExpressions.Regex(txtRegEx.Text);
            }
            catch
            {
                MessageBox.Show("Invalid regular expression", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegEx.Focus();
                return false;
            }
            return true;
        }

        public override string SaveData()
        {
            return txtRegEx.Text.Trim();
        }

        private string LastTestStr ="";
        private void btnTest_Click(object sender, EventArgs e)
        {

            if (txtRegEx.Text.Trim().Length == 0)
            {
                MessageBox.Show("Regular Expression is empty", "Test Regular Expression", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegEx.Focus();
                return;
            }
            System.Text.RegularExpressions.Regex rx;
            try
            {
                rx = new System.Text.RegularExpressions.Regex(txtRegEx.Text.Trim());
            }
            catch 
            {
                MessageBox.Show("Invalid regular expression", "Test Regular Expression", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegEx.Focus();
                return;
            }

            string s = Microsoft.VisualBasic.Interaction.InputBox("Enter domain name to test:", "Test Regular Expression", LastTestStr).Trim();
            if (s.Length == 0) return;
            LastTestStr = s.ToLower();

            if (rx.IsMatch(LastTestStr))
            {
                MessageBox.Show("TEST SUCCESSFUL\n\nDomain name matches regular expression", "Test Regular Expression", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("TEST FAILED\n\nDomain name does NOT match regular expression", "Test Regular Expression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


    }


}
