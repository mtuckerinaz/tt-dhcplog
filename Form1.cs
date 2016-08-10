using System;
using System.IO;
using System.Windows.Forms;

namespace DHCP_Analyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            StreamReader recfile = null;
            int x = 0;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open log file";
            ofd.Filter = "All files(*.*)|*.*";
            ofd.InitialDirectory = @"C:\";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                using (FileStream fs = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            x++;
                            line = sr.ReadLine();
                            string[] values = line.Split(',');
                            dataGridView1.Rows.Add(new object[]
                            {
                        values[0],values[1],values[2],values[3],values[4],values[5],values[6],values[7],values[8],values[9],values[10],values[11],values[12],values[13],values[14],values[15],values[16],values[17],values[18]
                            });
                        }

                        catch (Exception ex)
                        {
                            //
                        }
                    }
                    sr.Close();
                    toolStripStatusLabel1.Text = x + " records analyzed.";
                }

            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == this.dataGridView1.Columns[0].Index)
                && e.Value != null)
            {
                DataGridViewCell cell =
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.Value.Equals("00"))
                {
                    cell.ToolTipText = "The log was started";
                }
                else if (e.Value.Equals("01"))
                {
                    cell.ToolTipText = "The log was stopped";
                }
                else if (e.Value.Equals("02"))
                {
                    cell.ToolTipText = "The log was temporarily paused due to low disk space.";
                }
                else if (e.Value.Equals("10"))
                {
                    cell.ToolTipText = "a new IP address was leased to a client.";
                }
                else if (e.Value.Equals("11"))
                {
                    cell.ToolTipText = "A lease was renewed by a client.";
                }
                else if (e.Value.Equals("12"))
                {
                    cell.ToolTipText = "A lease was released by a client.";
                }
                else if (e.Value.Equals("13"))
                {
                    cell.ToolTipText = "An IP address was found to be in use on the network.";
                }
                else if (e.Value.Equals("14"))
                {
                    cell.ToolTipText = "A lease request could not be satisfied because the scope's address pool was exhausted.";
                }
                else if (e.Value.Equals("15"))
                {
                    cell.ToolTipText = "A lease was denied.";
                }
                else if (e.Value.Equals("16"))
                {
                    cell.ToolTipText = "A lease was deleted.";
                }
                else if (e.Value.Equals("17"))
                {
                    cell.ToolTipText = "A lease was expired and DNS records for an expired leases have not been deleted.";
                }
                else if (e.Value.Equals("18"))
                {
                    cell.ToolTipText = "A lease was expired and DNS records were deleted.";
                }
                else if (e.Value.Equals("20"))
                {
                    cell.ToolTipText = "A BOOTP address was leased to a client.";
                }
                else if (e.Value.Equals("21"))
                {
                    cell.ToolTipText = "A dynamic BOOTP address was leased to a client.";
                }
                else if (e.Value.Equals("22"))
                {
                    cell.ToolTipText = "A BOOTP request could not be satisfied because the scope's address pool for BOOTP was exhausted.";
                }
                else if (e.Value.Equals("23"))
                {
                    cell.ToolTipText = "A BOOTP IP address was deleted after checking to see it was not in use.";
                }
                else if (e.Value.Equals("24"))
                {
                    cell.ToolTipText = "IP address cleanup operation has began.";
                }
                else if (e.Value.Equals("25"))
                {
                    cell.ToolTipText = "IP address cleanup statistics.";
                }
                else if (e.Value.Equals("30"))
                {
                    cell.ToolTipText = "DNS update request to the named DNS server.";
                }
                else if (e.Value.Equals("31"))
                {
                    cell.ToolTipText = "DNS update failed.";
                }
                else if (e.Value.Equals("32"))
                {
                    cell.ToolTipText = "DNS update successful.";
                }
                else if (e.Value.Equals("33"))
                {
                    cell.ToolTipText = "Packet dropped due to NAP policy.";
                }
                else if (e.Value.Equals("34"))
                {
                    cell.ToolTipText = "DNS update request failed.as the DNS update request queue limit exceeded.";
                }
                else if (e.Value.Equals("35"))
                {
                    cell.ToolTipText = "DNS update request failed.";
                }
                else if (e.Value.Equals("36"))
                {
                    cell.ToolTipText = "Packet dropped because the server is in failover standby role or the hash of the client ID does not match.";
                }
                else if (e.Value.Equals("50"))
                {
                    cell.ToolTipText = "Codes above 50 are used for Rogue Server Detection information.";
                }

            }
            if ((e.ColumnIndex == this.dataGridView1.Columns[3].Index)
    && e.Value != null)
            {
                DataGridViewCell cell =
    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.Value.Equals("NACK"))
                {
                    cell.ToolTipText = "The DHCP service issued a NACK (negative acknowledgement message) to the client, computer name, for the address, address.";
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                    else if (row.Cells[1].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                    else if (row.Cells[2].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                    else if (row.Cells[3].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                    else if (row.Cells[4].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                    else if (row.Cells[5].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
