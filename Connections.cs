﻿using LevelDB;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SUP
{
    public partial class Connections : Form
    {
        public Connections()
        {
            InitializeComponent();
        }

        private void btnMainConnection_Click(object sender, EventArgs e)
        {
            string bitcoinDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\bitcoin";
            string bitcoindPath = AppDomain.CurrentDomain.BaseDirectory + "\\bitcoin-qt.exe";
            System.IO.Directory.CreateDirectory("bitcoin");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = bitcoindPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-testnet -txindex=1 -addrindex=1 -datadir={bitcoinDirectory} -server -rpcuser=good-user -rpcpassword=better-password -rpcport=18332"
            };

            Process.Start(startInfo);
        }

        private void btnBTC_Click(object sender, EventArgs e)
        {
            string bitcoinDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\bitcoin";
            string bitcoindPath = AppDomain.CurrentDomain.BaseDirectory + "\\bitcoin-qt.exe";
            System.IO.Directory.CreateDirectory("bitcoin");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = bitcoindPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-txindex=1 -addrindex=1 -datadir={bitcoinDirectory} -server -rpcuser=good-user -rpcpassword=better-password -rpcport=8332"
            };

            Process.Start(startInfo);
        }

        private void btnMZC_Click(object sender, EventArgs e)
        {
            string bitcoinDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\mazacoin";
            string bitcoindPath = AppDomain.CurrentDomain.BaseDirectory + "\\maza-qt.exe";
            System.IO.Directory.CreateDirectory("mazacoin");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = bitcoindPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-txindex=1 -addrindex=1 -datadir={bitcoinDirectory} -server -rpcuser=good-user -rpcpassword=better-password -rpcport=12832"
            };

            Process.Start(startInfo);
        }

        private void Connections_Load(object sender, EventArgs e)
        {
           
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"ipfs\ipfs.exe",
                        Arguments = "swarm peers",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                if (output.Length > 0)
                {
                    button1.Text = "disable IPFS pinning";
                btnPinIPFS.Enabled = true;
                btnUnpinIPFS.Enabled = true;
               
            }
                else
                {
                    button1.Text = "enable IPFS pinning";
                }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "disable IPFS pinning")
            {
                button1.Text = "enable IPFS pinning";

                     var process = new Process
                     {
                         StartInfo = new ProcessStartInfo
                         {
                             FileName = @"ipfs\ipfs.exe",
                             Arguments = "shutdown",
                             UseShellExecute = false,
                             CreateNoWindow = true
                         }
                     };
                process.Start();

                btnPinIPFS.Enabled = false;
                btnUnpinIPFS.Enabled = false;

                var SUP = new Options { CreateIfMissing = true };

                using (var db = new DB(SUP, @"sup"))
                {

                    db.Delete("ipfs-daemon");
                }


            }
            else {
                button1.Text = "disable IPFS pinning";

                var init = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"ipfs\ipfs.exe",
                        Arguments = "init",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                init.Start();
                init.WaitForExit();

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"ipfs\ipfs.exe",
                        Arguments = "daemon",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                btnPinIPFS.Enabled = true;
                btnUnpinIPFS.Enabled = true;

                var SUP = new Options { CreateIfMissing = true };

                using (var db = new DB(SUP, @"sup"))
                {

                    db.Put("ipfs-daemon", "true");

                }

                }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] subfolderNames = Directory.GetDirectories("ipfs");

            foreach (string subfolder in subfolderNames)
            {
                string hash = Path.GetFileName(subfolder);

                // Call the Kubo local Pin command using the hash
                Process process2 = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"ipfs\ipfs.exe",
                        Arguments = "pin add " + hash,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process2.Start();
                process2.WaitForExit();

            }
        }

        private void btnUnpinIPFS_Click(object sender, EventArgs e)
        {
            string[] subfolderNames = Directory.GetDirectories("ipfs");

            foreach (string subfolder in subfolderNames)
            {
                string hash = Path.GetFileName(subfolder);

                // Call the Kubo local Pin command using the hash
                Process process2 = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"ipfs\ipfs.exe",
                        Arguments = "pin rm " + hash,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process2.Start();
                process2.WaitForExit();

            }
        }

        private void btnPurgeIPFS_Click(object sender, EventArgs e)
        {
            string[] subfolderNames = Directory.GetDirectories("ipfs");

            foreach (string subfolder in subfolderNames)
            {
                Directory.Delete(subfolder, true);
            }
        }
    }
}
