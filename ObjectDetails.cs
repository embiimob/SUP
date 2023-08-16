﻿using AngleSharp.Text;
using Ganss.Xss;
using LevelDB;
using NAudio.Wave;
using NBitcoin;
using Newtonsoft.Json;
using SUP.P2FK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.NBitcoin;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace SUP
{
    public partial class ObjectDetails : Form
    {
        private readonly string _objectaddress;
        private bool isVerbose = false;
        private int numMessagesDisplayed = 0;
        private int numChangesDisplayed = 0;
        private bool isUserControl = false;
        private bool isInitializing = true;
        List<Microsoft.Web.WebView2.WinForms.WebView2> webviewers = new List<Microsoft.Web.WebView2.WinForms.WebView2>();

        public ObjectDetails(string objectaddress, bool isusercontrol = false)
        {
            InitializeComponent();
            _objectaddress = objectaddress;
            isUserControl = isusercontrol;

        }

        private void ObjectDetails_Load(object sender, EventArgs e)
        {
            // Check if the parent form has a button named "btnLive" with blue background color
            // Get a reference to the parent form
            Form parentForm = this.Owner;
            bool isBlue = false;

            // Check if the parent form has a button named "btnLive" with blue background color
            try
            {
                if (parentForm != null)
                {
                    isBlue = parentForm.Controls.OfType<System.Windows.Forms.Button>().Any(b => b.Name == "btnLive" && b.BackColor == System.Drawing.Color.Blue);
                }
            }
            catch
            {
            }

            if (isBlue)
            {
                // If there is a button with blue background color, show a message box
                DialogResult result = System.Windows.Forms.MessageBox.Show("disable Live monitoring to browse sup!? objects.\r\nignoring this warning may cause temporary data corruption that could require a full purge of the cache", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    // If the user clicks OK, close the form
                    this.Close();
                }
            }
            else
            {
                this.Text = String.Empty;

                if (!isUserControl)
                {
                    this.Text = "[ " + _objectaddress + " ]";
                    registrationPanel.Visible = true;
                }


                btnReloadObject.PerformClick();
                lblPleaseStandBy.Visible = false;
            }
        }

        void deleteme_LinkClicked(string transactionid)
        {

            string unfilteredmessage = "";
            try { unfilteredmessage = System.IO.File.ReadAllText(@"root/" + transactionid + @"/MSG"); } catch { }


            string pattern = "<<.*?>>";
            MatchCollection matches = Regex.Matches(unfilteredmessage, pattern);
            foreach (Match match in matches)
            {
                string content = match.Value.Substring(2, match.Value.Length - 4);
                if (!int.TryParse(content, out int id))
                {

                    string imagelocation = "";
                    if (content != null)
                    {
                        imagelocation = content;

                        if (!content.ToLower().StartsWith("http"))
                        {
                            imagelocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + content.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", "").Replace("IPFS:", "").Replace("btc:", "").Replace("mzc:", "").Replace("ltc:", "").Replace("dog:", "").Replace("ipfs:", "").Replace(@"/", @"\");
                            if (content.ToLower().StartsWith("ipfs:"))
                            {
                                imagelocation = imagelocation.Replace(@"\root\", @"\ipfs\");
                                if (content.Length == 51) { imagelocation += @"\artifact"; }
                            }

                            string parentDir = Path.GetDirectoryName(imagelocation);

                            if (Directory.Exists(parentDir))
                            {
                                Directory.Delete(parentDir, true);
                            }
                        }
                    }


                }
            }

            try
            {
                Directory.Delete(@"root\" + transactionid, true);
                Directory.CreateDirectory(@"root\" + transactionid);
            }
            catch { }
            Root P2FKRoot = new Root();
            var rootSerialized = JsonConvert.SerializeObject(P2FKRoot);
            System.IO.File.WriteAllText(@"root\" + transactionid + @"\" + "ROOT.json", rootSerialized);
        }


        private string TruncateAddress(string input)
        {
            if (input.Length <= 13)
            {
                return input;
            }
            else
            {
                return input.Substring(0, 5) + "..." + input.Substring(input.Length - 5);
            }
        }

        private void ShowFullScreenModeClick(object sender, EventArgs e)
        {
            new FullScreenView(pictureBox1.ImageLocation).Show();
        }

        private void LaunchURN(object sender, EventArgs e)
        {
            string src = lblURNFullPath.Text;
            try
            { System.Diagnostics.Process.Start(src); }
            catch { System.Media.SystemSounds.Exclamation.Play(); }
        }

        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            var linkData = e.Link.LinkData;
            new ObjectBrowser((string)linkData).Show();

        }

        private void ButtonShowObjectDetailsClick(object sender, EventArgs e)
        {
            CreatorsPanel.SuspendLayout();
            OwnersPanel.SuspendLayout();
            CreatorsPanel.Controls.Clear();
            OwnersPanel.Controls.Clear();
            RoyaltiesPanel.Controls.Clear();
            supPanel.Visible = false;
            numMessagesDisplayed = 0;


            OBJState objstate = OBJState.GetObjectByAddress(_objectaddress, "good-user", "better-password", "http://127.0.0.1:18332");
            Dictionary<string, string> profileAddress = new Dictionary<string, string> { };


            if (objstate.Owners != null)
            {


                OwnersPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
                OwnersPanel.AutoScroll = true;

                int row = 0;
                foreach (KeyValuePair<string, long> item in objstate.Owners)
                {


                    TableLayoutPanel rowPanel = new TableLayoutPanel
                    {
                        RowCount = 1,
                        ColumnCount = 2,
                        Dock = DockStyle.Top,
                        AutoSize = true,
                        Padding = new System.Windows.Forms.Padding(3)
                    };

                    rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
                    rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));


                    LinkLabel keyLabel = new LinkLabel();


                    string searchkey = item.Key;


                    if (!profileAddress.ContainsKey(searchkey))
                    {

                        PROState profile = PROState.GetProfileByAddress(searchkey, "good-user", "better-password", "http://127.0.0.1:18332");

                        if (profile.URN != null)
                        {
                            keyLabel.Text = profile.URN;

                        }
                        else
                        {
                            keyLabel.Text = item.Key;
                        }
                        profileAddress.Add(searchkey, keyLabel.Text);
                    }
                    else
                    {
                        profileAddress.TryGetValue(searchkey, out string ShortName);
                        keyLabel.Text = ShortName;
                    }

                    keyLabel.Links[0].LinkData = item.Key;
                    keyLabel.AutoSize = true;
                    keyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    keyLabel.LinkBehavior = LinkBehavior.NeverUnderline;
                    keyLabel.LinkColor = System.Drawing.Color.Black;
                    keyLabel.ActiveLinkColor = System.Drawing.Color.Black;
                    keyLabel.VisitedLinkColor = System.Drawing.Color.Black;
                    keyLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClicked);
                    keyLabel.Dock = DockStyle.Left;


                    Label valueLabel = new Label
                    {
                        Text = item.Value.ToString(),
                        AutoSize = true,
                        Dock = DockStyle.Right
                    };


                    rowPanel.Controls.Add(keyLabel, 0, 0);
                    rowPanel.Controls.Add(valueLabel, 1, 0);


                    if (row % 2 == 0)
                    {
                        rowPanel.BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        rowPanel.BackColor = System.Drawing.Color.LightGray;
                    }


                    OwnersPanel.Controls.Add(rowPanel);
                    row++;



                }

                long totalQty = objstate.Owners.Values.Sum();

                lblTotalOwnedDetail.Text = "total: " + totalQty.ToString("N0");


                ///royalties 

                if (objstate.Royalties != null)
                {


                    RoyaltiesPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
                    RoyaltiesPanel.AutoScroll = true;

                    row = 0;
                    foreach (KeyValuePair<string, decimal> item in objstate.Royalties)
                    {


                        TableLayoutPanel rowPanel = new TableLayoutPanel
                        {
                            RowCount = 1,
                            ColumnCount = 2,
                            Dock = DockStyle.Top,
                            AutoSize = true,
                            Padding = new System.Windows.Forms.Padding(3)
                        };

                        rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
                        rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));


                        LinkLabel keyLabel = new LinkLabel();


                        string searchkey = item.Key;


                        if (!profileAddress.ContainsKey(searchkey))
                        {

                            PROState profile = PROState.GetProfileByAddress(searchkey, "good-user", "better-password", "http://127.0.0.1:18332");

                            if (profile.URN != null)
                            {
                                keyLabel.Text = profile.URN;

                            }
                            else
                            {
                                keyLabel.Text = item.Key;
                            }
                            profileAddress.Add(searchkey, keyLabel.Text);
                        }
                        else
                        {
                            profileAddress.TryGetValue(searchkey, out string ShortName);
                            keyLabel.Text = ShortName;
                        }

                        keyLabel.Links[0].LinkData = item.Key;
                        keyLabel.AutoSize = true;
                        keyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        keyLabel.LinkBehavior = LinkBehavior.NeverUnderline;
                        keyLabel.LinkColor = System.Drawing.Color.Black;
                        keyLabel.ActiveLinkColor = System.Drawing.Color.Black;
                        keyLabel.VisitedLinkColor = System.Drawing.Color.Black;
                        keyLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClicked);
                        keyLabel.Dock = DockStyle.Left;


                        Label valueLabel = new Label
                        {
                            Text = item.Value.ToString(),
                            AutoSize = true,
                            Dock = DockStyle.Right
                        };


                        rowPanel.Controls.Add(keyLabel, 0, 0);
                        rowPanel.Controls.Add(valueLabel, 1, 0);


                        if (row % 2 == 0)
                        {
                            rowPanel.BackColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            rowPanel.BackColor = System.Drawing.Color.LightGray;
                        }


                        RoyaltiesPanel.Controls.Add(rowPanel);
                        row++;



                    }

                    decimal totalRoytalties = objstate.Royalties.Values.Sum();

                    lblTotalRoyaltiesDetail.Text = "royalties: " + totalRoytalties.ToString();
                }

                ///


                foreach (KeyValuePair<string, DateTime> item in objstate.Creators)
                {

                    if (item.Value.Year > 1)

                    {


                        TableLayoutPanel rowPanel = new TableLayoutPanel
                        {
                            RowCount = 1,
                            ColumnCount = 2,
                            Dock = DockStyle.Top,
                            AutoSize = true,
                            Padding = new System.Windows.Forms.Padding(3)
                        };


                        rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300));
                        rowPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0));



                        LinkLabel keyLabel = new LinkLabel();

                        string searchkey = item.Key;
                        PROState profile = PROState.GetProfileByAddress(searchkey, "good-user", "better-password", "http://127.0.0.1:18332");

                        if (profile.URN != null)
                        {
                            keyLabel.Text = profile.URN;
                        }
                        else
                        {


                            keyLabel.Text = item.Key;
                        }
                        keyLabel.Links[0].LinkData = item.Key;
                        keyLabel.AutoSize = true;
                        keyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        keyLabel.LinkBehavior = LinkBehavior.NeverUnderline;
                        keyLabel.LinkColor = System.Drawing.Color.Black;
                        keyLabel.ActiveLinkColor = System.Drawing.Color.Black;
                        keyLabel.VisitedLinkColor = System.Drawing.Color.Black;
                        keyLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClicked);
                        keyLabel.Dock = DockStyle.Left; // set dock property for key label 
                        rowPanel.Controls.Add(keyLabel, 0, 0);



                        if (row % 2 == 0)
                        {
                            rowPanel.BackColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            rowPanel.BackColor = System.Drawing.Color.LightGray;
                        }


                        CreatorsPanel.Controls.Add(rowPanel);
                        row++;
                    }
                }

            }
            CreatorsPanel.ResumeLayout();
            OwnersPanel.ResumeLayout();
            supPanel.Visible = false;
            OwnersPanel.Visible = true;

        }

        private void ShowSupPanel(object sender, EventArgs e)
        {
            supPanel.Visible = true;
            RefreshSupMessages();
        }

        private void RefreshSupMessages()
        {
           // ShowSupPanel().;
            // Clear controls if no messages have been displayed yet
            if (numMessagesDisplayed == 0)
            {
                Task.Run(() =>
                {
                    foreach (var viewer in webviewers)
                    {
                        viewer.Dispose();
                    }

                });


                // supFlow.SuspendLayout();
                supFlow.Controls.Clear();
                //supFlow.ResumeLayout();

                btnRefreshSup.Enabled = false;

                Root[] roots = Root.GetRootsByAddress(_objectaddress, "good-user", "better-password", "http://127.0.0.1:18332");

            }

            Task BuildMessage = Task.Run(() =>
            {
                if (_objectaddress != null)
                {



                    Dictionary<string, string[]> profileAddress = new Dictionary<string, string[]> { };
                    int rownum = 1;

                    var SUP = new Options { CreateIfMissing = true };
                    try
                    {
                        using (var db = new DB(SUP, @"root\" + _objectaddress + @"\sup"))
                        {

                            LevelDB.Iterator it = db.CreateIterator();
                            for (
                               it.SeekToLast();
                               it.IsValid() && rownum <= numMessagesDisplayed + 10; // Only display next 10 messages
                                it.Prev()
                             )
                            {
                                try
                                {
                                    // Display only if rownum > numMessagesDisplayed to skip already displayed messages
                                    if (rownum > numMessagesDisplayed)
                                    {
                                        string process = it.ValueAsString();

                                        List<string> supMessagePacket = JsonConvert.DeserializeObject<List<string>>(process);

                                        string message = "";
                                        try
                                        {
                                            message = System.IO.File.ReadAllText(@"root/" + supMessagePacket[1] + @"/MSG").Replace("@" + _objectaddress, "");

                                            string relativeFolderPath = @"root\" + supMessagePacket[1];
                                            string folderPath = Path.Combine(Environment.CurrentDirectory, relativeFolderPath);

                                            string[] files = Directory.GetFiles(folderPath);

                                            foreach (string file in files)
                                            {
                                                string extension = Path.GetExtension(file);

                                                if (!string.IsNullOrEmpty(extension) && !file.Contains("ROOT.json"))
                                                {
                                                    message = message + @"<<" + supMessagePacket[1] + @"/" + Path.GetFileName(file) + ">>";
                                                }
                                            }

                                            string fromAddress = supMessagePacket[0];
                                            string imagelocation = "";


                                            if (!profileAddress.ContainsKey(fromAddress))
                                            {

                                                PROState profile = PROState.GetProfileByAddress(fromAddress, "good-user", "better-password", "http://127.0.0.1:18332");

                                                if (profile.URN != null)
                                                {
                                                    fromAddress = TruncateAddress(profile.URN);

                                                    if (profile.Image != null)
                                                    {
                                                        imagelocation = profile.Image;


                                                        if (!profile.Image.ToLower().StartsWith("http"))
                                                        {
                                                            imagelocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + profile.Image.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", "").Replace("IPFS:", "").Replace(@"/", @"\");
                                                            if (profile.Image.ToLower().StartsWith("ipfs:")) { imagelocation = imagelocation.Replace(@"\root\", @"\ipfs\"); if (profile.Image.Length == 51) { imagelocation += @"\artifact"; } }
                                                        }
                                                        Regex regexTransactionId = new Regex(@"\b[0-9a-f]{64}\b");
                                                        Match imgurnmatch = regexTransactionId.Match(imagelocation);
                                                        string transactionid = imgurnmatch.Value;
                                                        Root root = new Root();
                                                        if (!File.Exists(imagelocation))
                                                        {
                                                            switch (profile.Image.ToUpper().Substring(0, 4))
                                                            {
                                                                case "MZC:":
                                                                    root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");

                                                                    break;
                                                                case "BTC:":

                                                                    root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:8332", "0");

                                                                    break;
                                                                case "LTC:":

                                                                    root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:9332", "48");


                                                                    break;
                                                                case "DOG:":
                                                                    root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:22555", "30");

                                                                    break;
                                                                case "IPFS":
                                                                    string transid = "empty";
                                                                    try { transid = profile.Image.Substring(5, 46); } catch { }

                                                                    if (!System.IO.Directory.Exists("ipfs/" + transid + "-build"))
                                                                    {
                                                                        try
                                                                        {
                                                                            Directory.CreateDirectory("ipfs/" + transid);
                                                                        }
                                                                        catch { };

                                                                        Directory.CreateDirectory("ipfs/" + transid + "-build");
                                                                        Process process2 = new Process();
                                                                        process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                                                        process2.StartInfo.Arguments = "get " + transid + @" -o ipfs\" + transid;
                                                                        process2.StartInfo.UseShellExecute = false;
                                                                        process2.StartInfo.CreateNoWindow = true;
                                                                        process2.Start();
                                                                        if (process2.WaitForExit(5000))
                                                                        {
                                                                            string fileName;
                                                                            if (System.IO.File.Exists("ipfs/" + transid))
                                                                            {
                                                                                System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                                                                System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                                                                fileName = profile.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                                                if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                                                                Directory.CreateDirectory("ipfs/" + transid);
                                                                                System.IO.File.Move("ipfs/" + transid + "_tmp", imagelocation);
                                                                            }

                                                                            if (System.IO.File.Exists("ipfs/" + transid + "/" + transid))
                                                                            {
                                                                                fileName = profile.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                                                if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }

                                                                                System.IO.File.Move("ipfs/" + transid + "/" + transid, imagelocation);
                                                                            }

                                                                            Process process3 = new Process
                                                                            {
                                                                                StartInfo = new ProcessStartInfo
                                                                                {
                                                                                    FileName = @"ipfs\ipfs.exe",
                                                                                    Arguments = "pin add " + transid,
                                                                                    UseShellExecute = false,
                                                                                    CreateNoWindow = true
                                                                                }
                                                                            };
                                                                            process3.Start();

                                                                            try { Directory.Delete("ipfs/" + transid + "-build", true); } catch { }
                                                                        }
                                                                        else
                                                                        {
                                                                            process2.Kill();

                                                                            Task.Run(() =>
                                                                            {
                                                                                process2 = new Process();
                                                                                process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                                                                process2.StartInfo.Arguments = "get " + transid + @" -o ipfs\" + transid;
                                                                                process2.StartInfo.UseShellExecute = false;
                                                                                process2.StartInfo.CreateNoWindow = true;
                                                                                process2.Start();
                                                                                if (process2.WaitForExit(550000))
                                                                                {
                                                                                    string fileName;
                                                                                    if (System.IO.File.Exists("ipfs/" + transid))
                                                                                    {
                                                                                        System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                                                                        System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                                                                        fileName = profile.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                                                        if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                                                                        Directory.CreateDirectory("ipfs/" + transid);
                                                                                        System.IO.File.Move("ipfs/" + transid + "_tmp", imagelocation);
                                                                                    }

                                                                                    if (System.IO.File.Exists("ipfs/" + transid + "/" + transid))
                                                                                    {
                                                                                        fileName = profile.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                                                        if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }

                                                                                        System.IO.File.Move("ipfs/" + transid + "/" + transid, imagelocation);
                                                                                    }

                                                                                    Process process3 = new Process
                                                                                    {
                                                                                        StartInfo = new ProcessStartInfo
                                                                                        {
                                                                                            FileName = @"ipfs\ipfs.exe",
                                                                                            Arguments = "pin add " + transid,
                                                                                            UseShellExecute = false,
                                                                                            CreateNoWindow = true
                                                                                        }
                                                                                    };
                                                                                    process3.Start();

                                                                                    try { Directory.Delete("ipfs/" + transid + "-build", true); } catch { }


                                                                                }
                                                                                else
                                                                                {
                                                                                    process2.Kill();
                                                                                }
                                                                            });

                                                                        }


                                                                    }

                                                                    break;
                                                                default:
                                                                    if (!profile.Image.ToUpper().StartsWith("HTTP") && transactionid != "")
                                                                    {
                                                                        root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:18332");

                                                                    }
                                                                    break;
                                                            }
                                                        }



                                                    }


                                                }
                                                else
                                                { fromAddress = TruncateAddress(fromAddress); }

                                                string[] profilePacket = new string[2];

                                                profilePacket[0] = fromAddress;
                                                profilePacket[1] = imagelocation;
                                                profileAddress.Add(supMessagePacket[0], profilePacket);

                                            }
                                            else
                                            {
                                                string[] profilePacket = new string[] { };
                                                profileAddress.TryGetValue(fromAddress, out profilePacket);
                                                fromAddress = profilePacket[0];
                                                imagelocation = profilePacket[1];

                                            }


                                            string tstamp = it.KeyAsString().Split('!')[1];
                                            System.Drawing.Color bgcolor = System.Drawing.Color.White;
                                            string unfilteredmessage = message;
                                            message = Regex.Replace(message, "<<.*?>>", "");

                                            this.Invoke((MethodInvoker)delegate
                                            {
                                                CreateRow(imagelocation, fromAddress, supMessagePacket[0], DateTime.ParseExact(tstamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture), message, supMessagePacket[1], false, supFlow);
                                            });

                                            string pattern = "<<.*?>>";
                                            List<string> imgExtensions = new List<string> { ".bmp", ".gif", ".ico", ".jpeg", ".jpg", ".png", ".tif", ".tiff", ".mp4", ".avi", ".wav", ".mp3" };

                                            MatchCollection matches = Regex.Matches(unfilteredmessage, pattern);
                                            foreach (Match match in matches)
                                            {


                                                string content = match.Value.Substring(2, match.Value.Length - 4);

                                                if (!int.TryParse(content, out int cnt) && !content.Trim().StartsWith("#"))
                                                {



                                                    string imgurn = content;

                                                    if (!content.ToLower().StartsWith("http"))
                                                    {
                                                        imgurn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + content.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", "").Replace("IPFS:", "").Replace("btc:", "").Replace("mzc:", "").Replace("ltc:", "").Replace("dog:", "").Replace("ipfs:", "").Replace(@"/", @"\");

                                                        if (content.ToLower().StartsWith("ipfs:")) { imgurn = imgurn.Replace(@"\root\", @"\ipfs\"); }
                                                    }

                                                    string extension = Path.GetExtension(imgurn).ToLower();
                                                    if (!imgExtensions.Contains(extension) && !imgurn.Contains("youtube.com") && !imgurn.Contains("youtu.be"))
                                                    {


                                                        try
                                                        {
                                                            // Create a WebClient object to fetch the webpage
                                                            WebClient client = new WebClient();
                                                            string html = client.DownloadString(content.StripLeadingTrailingSpaces());

                                                            // Use regular expressions to extract the metadata from the HTML
                                                            string title = Regex.Match(html, @"<title>\s*(.+?)\s*</title>").Groups[1].Value;
                                                            string description = Regex.Match(html, @"<meta\s+name\s*=\s*""description""\s+content\s*=\s*""(.+?)""\s*/?>").Groups[1].Value;
                                                            string imageUrl = Regex.Match(html, @"<meta\s+property\s*=\s*""og:image""\s+content\s*=\s*""(.+?)""\s*/?>").Groups[1].Value;

                                                            if (description != "")
                                                            {
                                                                // Create a new panel to display the metadata
                                                                Panel panel = new Panel();
                                                                panel.BorderStyle = BorderStyle.FixedSingle;
                                                                panel.Size = new Size(supFlow.Width - 30, 100);

                                                                // Create a label for the title
                                                                Label titleLabel = new Label();
                                                                titleLabel.Text = title;
                                                                titleLabel.Dock = DockStyle.Top;
                                                                titleLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                                                                titleLabel.ForeColor = Color.White;
                                                                titleLabel.MinimumSize = new Size(supFlow.Width - 130, 30);
                                                                titleLabel.Padding = new Padding(5);
                                                                titleLabel.MouseClick += (sender, e) => { Attachment_Clicked(content); };
                                                                panel.Controls.Add(titleLabel);

                                                                // Create a label for the description
                                                                Label descriptionLabel = new Label();
                                                                descriptionLabel.Text = description;
                                                                descriptionLabel.ForeColor = Color.White;
                                                                descriptionLabel.Dock = DockStyle.Fill;
                                                                descriptionLabel.Padding = new Padding(5, 40, 5, 5);
                                                                descriptionLabel.MouseClick += (sender, e) => { Attachment_Clicked(content); };
                                                                panel.Controls.Add(descriptionLabel);

                                                                // Add an image to the panel if one is defined
                                                                if (!String.IsNullOrEmpty(imageUrl))
                                                                {
                                                                    try
                                                                    {
                                                                        // Create a MemoryStream object from the image data
                                                                        byte[] imageData = client.DownloadData(imageUrl);
                                                                        MemoryStream memoryStream = new MemoryStream(imageData);

                                                                        // Create a new PictureBox control and add it to the panel
                                                                        PictureBox pictureBox = new PictureBox();
                                                                        pictureBox.Dock = DockStyle.Left;
                                                                        pictureBox.Size = new Size(100, 100);
                                                                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                                                        pictureBox.Image = Image.FromStream(memoryStream);
                                                                        pictureBox.MouseClick += (sender, e) => { Attachment_Clicked(content); };
                                                                        panel.Controls.Add(pictureBox);
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }

                                                                // Add the panel to the flow layout panel
                                                                this.Invoke((MethodInvoker)delegate
                                                                {
                                                                    this.supFlow.Controls.Add(panel);
                                                                });
                                                            }
                                                            else
                                                            {
                                                                // Create a new panel to display the metadata
                                                                Panel panel = new Panel();
                                                                panel.BorderStyle = BorderStyle.FixedSingle;
                                                                panel.Size = new Size(supFlow.Width - 20, 30);

                                                                // Create a label for the title
                                                                LinkLabel titleLabel = new LinkLabel();
                                                                titleLabel.Text = content;
                                                                titleLabel.Links[0].LinkData = content;
                                                                titleLabel.Dock = DockStyle.Top;
                                                                titleLabel.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                                                                titleLabel.LinkColor = System.Drawing.SystemColors.GradientActiveCaption;
                                                                titleLabel.MinimumSize = new Size(supFlow.Width - 130, 30);
                                                                titleLabel.Padding = new Padding(5);
                                                                titleLabel.MouseClick += (sender, e) => { Attachment_Clicked(content); };
                                                                panel.Controls.Add(titleLabel);

                                                                this.Invoke((MethodInvoker)delegate
                                                                {
                                                                    this.supFlow.Controls.Add(panel);
                                                                });

                                                            }
                                                        }
                                                        catch
                                                        {

                                                            // Create a new panel to display the metadata
                                                            Panel panel = new Panel();
                                                            panel.BorderStyle = BorderStyle.FixedSingle;
                                                            panel.Size = new Size(supFlow.Width - 30, 30);

                                                            // Create a label for the title
                                                            LinkLabel titleLabel = new LinkLabel();
                                                            titleLabel.Text = content;
                                                            titleLabel.Links[0].LinkData = content;
                                                            titleLabel.Dock = DockStyle.Top;
                                                            titleLabel.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                                                            titleLabel.LinkColor = System.Drawing.SystemColors.GradientActiveCaption;
                                                            titleLabel.MinimumSize = new Size(supFlow.Width - 130, 30);
                                                            titleLabel.Padding = new Padding(5);
                                                            titleLabel.MouseClick += (sender, e) => { Attachment_Clicked(content); };
                                                            panel.Controls.Add(titleLabel);
                                                            this.Invoke((MethodInvoker)delegate
                                                            {
                                                                this.supFlow.Controls.Add(panel);
                                                            });


                                                        }
                                                    }
                                                    else
                                                    {


                                                        if (!int.TryParse(content, out int id))
                                                        {

                                                            if (extension == ".mp4" || extension == ".avi" || content.Contains("youtube.com") || content.Contains("youtu.be") || extension == ".wav" || extension == ".mp3")
                                                            {
                                                                this.Invoke((MethodInvoker)delegate
                                                                {
                                                                    AddVideo(content);
                                                                });

                                                            }
                                                            else
                                                            {

                                                                this.Invoke((MethodInvoker)delegate
                                                                {
                                                                    AddImage(content);
                                                                });
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            TableLayoutPanel padding = new TableLayoutPanel
                                            {
                                                RowCount = 1,
                                                ColumnCount = 1,
                                                Dock = DockStyle.Top,
                                                BackColor = Color.Black,
                                                ForeColor = Color.White,
                                                AutoSize = true,
                                                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                                                Margin = new System.Windows.Forms.Padding(0, 0, 0, 40),
                                                Padding = new System.Windows.Forms.Padding(0)

                                            };

                                            padding.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, supFlow.Width - 20));
                                            this.Invoke((MethodInvoker)delegate
                                            {
                                                supFlow.Controls.Add(padding);
                                            });

                                        }
                                        catch { }//deleted file

                                    }

                                }
                                catch { }
                                rownum++;

                            }
                            it.Dispose();
                        }

                        // Update number of messages displayed
                        numMessagesDisplayed += 10;

                        // supFlow.ResumeLayout();
                    }
                    catch { }

                    this.Invoke((MethodInvoker)delegate
                    {
                        btnRefreshSup.Enabled = true;
                    });




                }
            });
        }

        void AddImage(string imagepath, bool isprivate = false, bool addtoTop = false)
        {
            string imagelocation = "";
            if (imagepath != null)
            {
                imagelocation = imagepath;


                if (!imagepath.ToLower().StartsWith("http"))
                {
                    imagelocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + imagepath.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", "").Replace("IPFS:", "").Replace(@"/", @"\");
                    if (imagepath.ToLower().StartsWith("ipfs:")) { imagelocation = imagelocation.Replace(@"\root\", @"\ipfs\"); if (imagepath.Length == 51) { imagelocation += @"\artifact"; } }
                }
                Regex regexTransactionId = new Regex(@"\b[0-9a-f]{64}\b");
                Match imgurnmatch = regexTransactionId.Match(imagelocation);
                string transactionid = imgurnmatch.Value;
                Root root = new Root();
                if (!File.Exists(imagelocation))
                {
                    switch (imagepath.ToUpper().Substring(0, 4))
                    {
                        case "MZC:":
                            Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");

                            break;
                        case "BTC:":

                            Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:8332", "0");

                            break;
                        case "LTC:":

                            Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:9332", "48");


                            break;
                        case "DOG:":
                            Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:22555", "30");

                            break;
                        case "IPFS":
                            string transid = "empty";
                            try { transid = imagepath.Substring(5, 46); } catch { }

                            if (!System.IO.Directory.Exists("ipfs/" + transid + "-build"))
                            {
                                try
                                {
                                    Directory.CreateDirectory("ipfs/" + transid);
                                }
                                catch { };

                                Directory.CreateDirectory("ipfs/" + transid + "-build");
                                Process process2 = new Process();
                                process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                process2.StartInfo.Arguments = "get " + imagepath.Substring(5, 46) + @" -o ipfs\" + transid;
                                process2.StartInfo.UseShellExecute = false;
                                process2.StartInfo.CreateNoWindow = true;
                                process2.Start();
                                if (process2.WaitForExit(55000))
                                {
                                    string fileName;
                                    if (System.IO.File.Exists("ipfs/" + transid))
                                    {
                                        System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                        System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                        fileName = imagepath.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                        if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                        Directory.CreateDirectory("ipfs/" + transid);
                                        System.IO.File.Move("ipfs/" + transid + "_tmp", imagelocation);
                                    }

                                    if (System.IO.File.Exists("ipfs/" + transid + "/" + transid))
                                    {
                                        fileName = imagepath.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                        if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }

                                        System.IO.File.Move("ipfs/" + transid + "/" + transid, imagelocation);
                                    }

                                    Process process3 = new Process
                                    {
                                        StartInfo = new ProcessStartInfo
                                        {
                                            FileName = @"ipfs\ipfs.exe",
                                            Arguments = "pin add " + transid,
                                            UseShellExecute = false,
                                            CreateNoWindow = true
                                        }
                                    };
                                    process3.Start();

                                    try { Directory.Delete("ipfs/" + transid + "-build", true); } catch { }
                                }
                                else
                                {
                                    process2.Kill();

                                    Task.Run(() =>
                                    {
                                        process2 = new Process();
                                        process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                        process2.StartInfo.Arguments = "get " + imagepath.Substring(5, 46) + @" -o ipfs\" + transid;
                                        process2.StartInfo.UseShellExecute = false;
                                        process2.StartInfo.CreateNoWindow = true;
                                        process2.Start();
                                        if (process2.WaitForExit(550000))
                                        {
                                            string fileName;
                                            if (System.IO.File.Exists("ipfs/" + transid))
                                            {
                                                System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                                System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                                fileName = imagepath.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                                Directory.CreateDirectory("ipfs/" + transid);
                                                System.IO.File.Move("ipfs/" + transid + "_tmp", imagelocation);
                                            }

                                            if (System.IO.File.Exists("ipfs/" + transid + "/" + transid))
                                            {
                                                fileName = imagepath.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }

                                                System.IO.File.Move("ipfs/" + transid + "/" + transid, imagelocation);
                                            }

                                            Process process3 = new Process
                                            {
                                                StartInfo = new ProcessStartInfo
                                                {
                                                    FileName = @"ipfs\ipfs.exe",
                                                    Arguments = "pin add " + transid,
                                                    UseShellExecute = false,
                                                    CreateNoWindow = true
                                                }
                                            };
                                            process3.Start();

                                            try { Directory.Delete("ipfs/" + transid + "-build", true); } catch { }


                                        }
                                        else
                                        {
                                            process2.Kill();
                                        }
                                    });

                                }


                            }

                            break;
                        default:
                            if (!imagepath.ToUpper().StartsWith("HTTP") && transactionid != "")
                            {
                                Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:18332");

                            }
                            break;
                    }
                }



            }


            TableLayoutPanel msg = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 1,
                Dock = DockStyle.Top,
                BackColor = Color.Black,
                ForeColor = Color.White,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                Margin = new System.Windows.Forms.Padding(0, 0, 0, 0),
                Padding = new System.Windows.Forms.Padding(0)

            };

            msg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 420));
 
                if (addtoTop)
                {
                    supFlow.Controls.Add(msg);
                    supFlow.Controls.SetChildIndex(msg, 2);
                }
                else
                {
                    supFlow.Controls.Add(msg);
                }


            
            PictureBox pictureBox = new PictureBox();

            // Set the PictureBox properties

            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Width = 400;
            pictureBox.Height = 400;
            pictureBox.BackColor = Color.Black;
            pictureBox.ImageLocation = imagelocation;
            pictureBox.MouseClick += (sender, e) => { Attachment_Clicked(imagelocation); };
            msg.Controls.Add(pictureBox);


        }

        async void AddVideo(string videopath, bool isprivate = false, bool addtoTop = false, bool autoPlay = false)
        {
            string videolocation = "";
            if (videopath != null)
            {
                videolocation = videopath;


                if (!videopath.ToLower().StartsWith("http"))
                {
                    videolocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + videopath.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", "").Replace("IPFS:", "").Replace(@"/", @"\");
                    if (videopath.ToLower().StartsWith("ipfs:")) { videolocation = videolocation.Replace(@"\root\", @"\ipfs\"); if (videopath.Length == 51) { videolocation += @"\artifact"; } }
                }
                else
                {
                    string pattern = @"(?:youtu\.be/|youtube(?:-nocookie)?\.com/(?:[^/\n\s]*[/\n\s]*(?:v/|e(?:mbed)?/|.*[?&]v=))?)?([a-zA-Z0-9_-]{11})";

                    Match match = Regex.Match(videopath, pattern);
                    if (match.Success)
                    {
                        videolocation = @"https://www.youtube.com/embed/" + match.Groups[1].Value;
                    }

                }

                Regex regexTransactionId = new Regex(@"\b[0-9a-f]{64}\b");
                Match imgurnmatch = regexTransactionId.Match(videolocation);
                string transactionid = imgurnmatch.Value;
                Root root = new Root();
                if (!videolocation.Contains("www.youtube.com") && !File.Exists(videolocation))
                {
                    switch (videopath.ToUpper().Substring(0, 4))
                    {
                        case "MZC:":
                            Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");

                            break;
                        case "BTC:":

                            Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:8332", "0");

                            break;
                        case "LTC:":

                            Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:9332", "48");


                            break;
                        case "DOG:":
                            Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:22555", "30");

                            break;

                        case "IPFS":
                            string transid = "empty";
                            try { transid = videopath.Substring(5, 46); } catch { }

                            if (!System.IO.Directory.Exists("ipfs/" + transid + "-build"))
                            {
                                try
                                {
                                    Directory.CreateDirectory("ipfs/" + transid);
                                }
                                catch { };

                                Directory.CreateDirectory("ipfs/" + transid + "-build");
                                Process process2 = new Process();
                                process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                process2.StartInfo.Arguments = "get " + videopath.Substring(5, 46) + @" -o ipfs\" + transid;
                                process2.StartInfo.UseShellExecute = false;
                                process2.StartInfo.CreateNoWindow = true;
                                process2.Start();
                                if (process2.WaitForExit(55000))
                                {
                                    string fileName;
                                    if (System.IO.File.Exists("ipfs/" + transid))
                                    {
                                        System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                        System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                        fileName = videopath.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                        if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                        Directory.CreateDirectory("ipfs/" + transid);
                                        System.IO.File.Move("ipfs/" + transid + "_tmp", videolocation);
                                    }

                                    if (System.IO.File.Exists("ipfs/" + transid + "/" + transid))
                                    {
                                        fileName = videopath.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                        if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }

                                        System.IO.File.Move("ipfs/" + transid + "/" + transid, videolocation);
                                    }

                                    Process process3 = new Process
                                    {
                                        StartInfo = new ProcessStartInfo
                                        {
                                            FileName = @"ipfs\ipfs.exe",
                                            Arguments = "pin add " + transid,
                                            UseShellExecute = false,
                                            CreateNoWindow = true
                                        }
                                    };
                                    process3.Start();

                                    try { Directory.Delete("ipfs/" + transid + "-build", true); } catch { }
                                }
                                else
                                {
                                    process2.Kill();

                                    Task.Run(() =>
                                    {
                                        process2 = new Process();
                                        process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                        process2.StartInfo.Arguments = "get " + videopath.Substring(5, 46) + @" -o ipfs\" + transid;
                                        process2.StartInfo.UseShellExecute = false;
                                        process2.StartInfo.CreateNoWindow = true;
                                        process2.Start();
                                        if (process2.WaitForExit(550000))
                                        {
                                            string fileName;
                                            if (System.IO.File.Exists("ipfs/" + transid))
                                            {
                                                System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                                System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                                fileName = videopath.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                                Directory.CreateDirectory("ipfs/" + transid);
                                                System.IO.File.Move("ipfs/" + transid + "_tmp", videolocation);
                                            }

                                            if (System.IO.File.Exists("ipfs/" + transid + "/" + transid))
                                            {
                                                fileName = videopath.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }

                                                System.IO.File.Move("ipfs/" + transid + "/" + transid, videolocation);
                                            }

                                            Process process3 = new Process
                                            {
                                                StartInfo = new ProcessStartInfo
                                                {
                                                    FileName = @"ipfs\ipfs.exe",
                                                    Arguments = "pin add " + transid,
                                                    UseShellExecute = false,
                                                    CreateNoWindow = true
                                                }
                                            };
                                            process3.Start();

                                            try { Directory.Delete("ipfs/" + transid + "-build", true); } catch { }


                                        }
                                        else
                                        {
                                            process2.Kill();
                                        }
                                    });

                                }


                            }

                            break;

                        default:
                            if (!videopath.ToUpper().StartsWith("HTTP") && transactionid != "")
                            {
                                Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:18332");

                            }
                            break;
                    }
                }



            }


            TableLayoutPanel msg = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 1,
                Dock = DockStyle.Top,
                BackColor = Color.Black,
                ForeColor = Color.White,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                Margin = new System.Windows.Forms.Padding(0, 0, 0, 0),
                Padding = new System.Windows.Forms.Padding(0)

            };

            msg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 420));

                if (addtoTop)
                {

                    supFlow.Controls.Add(msg);
                    supFlow.Controls.SetChildIndex(msg, 2);


                }
                else
                {

                    supFlow.Controls.Add(msg);


                }


            

            Microsoft.Web.WebView2.WinForms.WebView2 webviewer = new Microsoft.Web.WebView2.WinForms.WebView2();
            webviewer.AllowExternalDrop = true;
            webviewer.BackColor = System.Drawing.Color.Black;
            webviewer.CreationProperties = null;
            webviewer.DefaultBackgroundColor = System.Drawing.Color.Black;

            webviewer.Name = "webviewer";
            webviewer.Size = new System.Drawing.Size(400, 300);
            webviewer.ZoomFactor = 1D;

            string viewerPath = Path.GetDirectoryName(videolocation) + @"\urnviewer.html";

            if (videolocation.Contains("www.youtube.com"))
            {
                try { Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + videolocation.Substring(29)); }
                catch { }
                viewerPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + videolocation.Substring(29) + @"\urnviewer.html";
            }

            string htmlstring = "<html><body><embed src=\"" + videolocation + "\" width=100% height=100% ></body></html>";

            webviewers.Add(webviewer);
            msg.Controls.Add(webviewer);


            try
            {
                System.IO.File.WriteAllText(viewerPath, htmlstring);
                await webviewer.EnsureCoreWebView2Async();
                // Load the HTML content into the WebView2 control
                webviewer.CoreWebView2.Navigate(viewerPath);

                // If it's a .wav file and autoplay is enabled, trigger the audio playback
                if (videolocation.ToLower().EndsWith(".wav") && autoPlay)

                {
                    WaveOut waveOut = new WaveOut();
                    WaveFileReader reader = new WaveFileReader(videolocation);
                    waveOut.Init(reader);
                    waveOut.Play();


                }

                // If it's a .mp3 file and autoplay is enabled, trigger the audio playback
                if (videolocation.ToLower().EndsWith(".mp3") && autoPlay)

                {
                    WaveOut waveOut = new WaveOut();
                    Mp3FileReader reader = new Mp3FileReader(videolocation);
                    waveOut.Init(reader);
                    waveOut.Play();

                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;// Handle exceptions here
            }



        }

        void CreateRow(string imageLocation, string ownerName, string ownerId, DateTime timestamp, string messageText, string transactionid, bool isprivate, FlowLayoutPanel layoutPanel)
        {

            // Create a table layout panel for each row
            TableLayoutPanel row = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 4,
                AutoSize = true,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            // Add the width of the first column to fixed value and second to fill remaining space
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20));

            layoutPanel.Controls.Add(row);

            // Create a PictureBox with the specified image

            if (File.Exists(imageLocation) || imageLocation.ToUpper().StartsWith("HTTP"))
            {
                PictureBox picture = new PictureBox
                {
                    Size = new System.Drawing.Size(50, 50),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ImageLocation = imageLocation,
                    Margin = new System.Windows.Forms.Padding(0),

                };
                row.Controls.Add(picture, 0, 0);
            }
            else
            {
                Random rnd = new Random();
                string randomGifFile;
                string[] gifFiles = Directory.GetFiles("includes", "*.gif");
                if (gifFiles.Length > 0)
                {
                    int randomIndex = rnd.Next(gifFiles.Length);
                    randomGifFile = gifFiles[randomIndex];
                }
                else
                {
                    randomGifFile = @"includes\HugPuddle.jpg";
                }



                PictureBox picture = new PictureBox
                {
                    Size = new System.Drawing.Size(50, 50),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    ImageLocation = randomGifFile,
                    Margin = new System.Windows.Forms.Padding(0),
                };
                row.Controls.Add(picture, 0, 0);
            }


            // Create a LinkLabel with the owner name
            LinkLabel owner = new LinkLabel
            {
                Text = ownerName,
                BackColor = Color.Black,
                ForeColor = Color.White,
                AutoSize = true

            };
            owner.LinkClicked += (sender, e) => { Owner_LinkClicked(ownerId); };
            owner.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            owner.Margin = new System.Windows.Forms.Padding(3);
            owner.Dock = DockStyle.Bottom;
            row.Controls.Add(owner, 1, 0);


            // Create a LinkLabel with the owner name
            Label tstamp = new Label
            {
                AutoSize = true,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 7.77F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Text = timestamp.ToString("MM/dd/yyyy hh:mm:ss"),
                Margin = new System.Windows.Forms.Padding(0),
                Dock = DockStyle.Bottom
            };
            row.Controls.Add(tstamp, 2, 0);


            Label deleteme = new Label
            {
                AutoSize = true,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 7.77F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Text = "🗑",
                Margin = new System.Windows.Forms.Padding(0),
                Dock = DockStyle.Bottom
            };
            deleteme.Click += (sender, e) => { deleteme_LinkClicked(transactionid); };
            row.Controls.Add(deleteme, 3, 0);


            if (messageText != "")
            {
                TableLayoutPanel msg = new TableLayoutPanel
                {
                    RowCount = 1,
                    ColumnCount = 1,
                    Dock = DockStyle.Top,
                    BackColor = Color.Black,
                    ForeColor = Color.White,
                    AutoSize = true,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                    Margin = new System.Windows.Forms.Padding(0, 0, 0, 0),
                    Padding = new System.Windows.Forms.Padding(0)

                };
                msg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, layoutPanel.Width - 20));


                layoutPanel.Controls.Add(msg);


                Label message = new Label
                {
                    AutoSize = true,
                    Text = messageText,
                    MinimumSize = new Size(200, 46),
                    Font = new System.Drawing.Font("Segoe UI", 7.77F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Margin = new System.Windows.Forms.Padding(0),
                    Padding = new System.Windows.Forms.Padding(10, 20, 10, 20),
                    TextAlign = System.Drawing.ContentAlignment.TopLeft
                };
                msg.Controls.Add(message, 1, 0);
            }


        }

        void CreateTransRow(string fromName, string fromId, string toName, string toId, string action, string qty, string amount, DateTime timestamp, string status, System.Drawing.Color bgcolor, FlowLayoutPanel layoutPanel)
        {

            // Create a table layout panel for each row
            TableLayoutPanel row = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 3,
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new System.Windows.Forms.Padding(0),
                BackColor = bgcolor,
                Margin = new System.Windows.Forms.Padding(0)
            };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 107));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 62));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 107));
            layoutPanel.Controls.Add(row);


            LinkLabel fromname = new LinkLabel
            {
                Text = fromName,
                AutoSize = true
            };
            fromname.LinkClicked += (sender, e) => { Owner_LinkClicked(fromId); };
            fromname.Dock = DockStyle.Left;
            fromname.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            fromname.Margin = new System.Windows.Forms.Padding(0);
            row.Controls.Add(fromname, 0, 0);

            Label laction = new Label
            {
                Text = action,
                AutoSize = true,
                Dock = DockStyle.Left,
                TextAlign = System.Drawing.ContentAlignment.TopLeft,
                Margin = new System.Windows.Forms.Padding(0)
            };
            row.Controls.Add(laction, 1, 0);


            LinkLabel toname = new LinkLabel
            {
                Text = toName,
                AutoSize = true
            };
            toname.LinkClicked += (sender, e) => { Owner_LinkClicked(toId); };
            toname.Dock = DockStyle.Right;
            toname.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            toname.Margin = new System.Windows.Forms.Padding(0);
            row.Controls.Add(toname, 2, 0);


            if (qty.Length + amount.Length > 0)
            {


                TableLayoutPanel stats = new TableLayoutPanel
                {
                    RowCount = 1,
                    ColumnCount = 2,
                    Dock = DockStyle.Top,
                    AutoSize = true,
                    BackColor = bgcolor,
                    Padding = new System.Windows.Forms.Padding(0),
                    Margin = new System.Windows.Forms.Padding(0)
                };
                // Add the width of the first column to fixed value and second to fill remaining space
                stats.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 138));
                stats.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 138));
                layoutPanel.Controls.Add(stats);


                Label lqty = new Label
                {
                    Text = qty,
                    AutoSize = true,
                    TextAlign = System.Drawing.ContentAlignment.TopLeft,
                    Margin = new System.Windows.Forms.Padding(0)
                };
                stats.Controls.Add(lqty, 0, 0);


                Label lamount = new Label
                {
                    Text = amount,
                    AutoSize = true,
                    TextAlign = System.Drawing.ContentAlignment.TopLeft,
                    Margin = new System.Windows.Forms.Padding(0)
                };
                stats.Controls.Add(lamount, 1, 0);
            }


            TableLayoutPanel msg = new TableLayoutPanel
            {
                RowCount = 2,
                ColumnCount = 1,
                Dock = DockStyle.Bottom,
                AutoSize = true,
                BackColor = bgcolor,
                Padding = new System.Windows.Forms.Padding(0),
                Margin = new System.Windows.Forms.Padding(0)
            };
            // Add the width of the first column to fixed value and second to fill remaining space
            msg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 276));
            layoutPanel.Controls.Add(msg);

            Label lstatus = new Label
            {
                Text = status,
                AutoSize = true,
                TextAlign = System.Drawing.ContentAlignment.TopLeft,
                Margin = new System.Windows.Forms.Padding(0)
            };
            msg.Controls.Add(lstatus, 0, 0);

            // Create a LinkLabel with the owner name
            Label tstamp = new Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 7.77F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Text = timestamp.ToString("MM/dd/yyyy hh:mm:ss"),
                TextAlign = System.Drawing.ContentAlignment.TopLeft,
                Margin = new System.Windows.Forms.Padding(0)
            };
            msg.Controls.Add(tstamp, 0, 1);
        }

        void Owner_LinkClicked(string ownerId)
        {

            new ObjectBrowser(ownerId).Show();
        }


        private async void MainRefreshClick(object sender, EventArgs e)
        {
            transFlow.Visible = false;
            KeysFlow.Visible = false;
            txtdesc.Visible = true;
            KeysFlow.Controls.Clear();
            string transactionid;
            string ipfsurn = null;
            string ipfsimg = null;
            string ipfsuri = null;
            bool isWWW = false;
            Regex regexTransactionId = new Regex(@"\b[0-9a-f]{64}\b");

            OBJState objstate = OBJState.GetObjectByAddress(_objectaddress, "good-user", "better-password", "http://127.0.0.1:18332");

            if (objstate.Owners != null)
            {

                string urn = "";
                if (objstate.URN != null)
                {
                    urn = objstate.URN;

                    if (!objstate.URN.ToLower().StartsWith("http"))
                    {
                        urn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + objstate.URN.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", "").Replace("IPFS:", "").Replace(@"/", @"\");
                        if (objstate.URN.ToLower().StartsWith("ipfs:")) { urn = urn.Replace(@"\root\", @"\ipfs\"); if (objstate.URN.Length == 51) { urn += @"\artifact"; } }


                    }
                    else
                    {
                        webviewer.Visible = true;
                        await webviewer.EnsureCoreWebView2Async();
                        webviewer.CoreWebView2.Navigate(objstate.URN);
                        lblURNBlockDate.Text = "http get: " + DateTime.UtcNow.ToString("ddd, dd MMM yyyy hh:mm:ss");
                        isWWW = true;
                    }
                }


                string imgurn = "";
                if (objstate.Image != null)
                {
                    imgurn = objstate.Image;

                    if (!objstate.Image.ToLower().StartsWith("http"))
                    {
                        imgurn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + objstate.Image.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", "").Replace("IPFS:", "").Replace(@"/", @"\");
                        if (objstate.Image.ToLower().StartsWith("ipfs:")) { imgurn = imgurn.Replace(@"\root\", @"\ipfs\"); if (objstate.Image.Length == 51) { imgurn += @"\artifact"; } }
                    }
                }


                string uriurn = "";
                if (objstate.URI != null)
                {
                    uriurn = objstate.URI;

                    if (!objstate.URI.ToLower().StartsWith("http"))
                    {
                        uriurn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + objstate.URI.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", "").Replace("IPFS:", "").Replace(@"/", @"\");
                        if (objstate.URI.ToLower().StartsWith("ipfs:")) { uriurn = uriurn.Replace(@"\root\", @"\ipfs\"); if (objstate.URI.Length == 51) { uriurn += @"\artifact"; } }

                    }
                }


                DateTime urnblockdate = new DateTime();
                DateTime imgblockdate = new DateTime();
                DateTime uriblockdate = new DateTime();
                lblObjectCreatedDate.Text = objstate.CreatedDate.ToString("ddd, dd MMM yyyy hh:mm:ss");
                try
                {

                    Match imgurnmatch = regexTransactionId.Match(imgurn);
                    transactionid = imgurnmatch.Value;
                    Root root = new Root();
                    if (imgurn != "")
                    {
                        switch (objstate.Image.ToUpper().Substring(0, 4))
                        {
                            case "MZC:":
                                root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");
                                try
                                {

                                    lblIMGBlockDate.Text = "mazacoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                }
                                catch { }



                                break;
                            case "BTC:":

                                root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:8332", "0");
                                try
                                {

                                    lblIMGBlockDate.Text = "bitcoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                }
                                catch { }



                                break;
                            case "LTC:":


                                root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:9332", "48");

                                try
                                {

                                    lblIMGBlockDate.Text = "litecoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                }
                                catch { }



                                break;
                            case "DOG:":

                                root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:22555", "30");

                                try
                                {

                                    lblIMGBlockDate.Text = "dogecoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                }
                                catch { }



                                break;
                            case "IPFS":
                                string transid = "empty";
                                try { transid = objstate.Image.Substring(5, 46); } catch { }

                                if (!File.Exists(imgurn) && !File.Exists("ipfs/" + transid + "/artifact") && !File.Exists("ipfs/" + transid + "/artifact" + objstate.Image.Substring(objstate.Image.LastIndexOf('.'))))
                                {

                                    if (!System.IO.Directory.Exists("ipfs/" + transid + "-build"))
                                    {
                                        try { Directory.Delete("ipfs/" + transid, true); } catch { }
                                        try { Directory.CreateDirectory("ipfs/" + transid); } catch { };
                                        Directory.CreateDirectory("ipfs/" + transid + "-build");
                                        Process process2 = new Process();
                                        process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                        process2.StartInfo.Arguments = "get " + objstate.Image.Substring(5, 46) + @" -o ipfs\" + transid;
                                        process2.StartInfo.UseShellExecute = false;
                                        process2.StartInfo.CreateNoWindow = true;
                                        process2.Start();
                                        process2.WaitForExit();
                                        string fileName;
                                        if (System.IO.File.Exists("ipfs/" + transid))
                                        {
                                            System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                            System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                            fileName = objstate.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                            if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                            Directory.CreateDirectory("ipfs/" + transid);

                                            try { System.IO.File.Move("ipfs/" + transid + "_tmp", imgurn); }
                                            catch (System.ArgumentException ex)
                                            {

                                                System.IO.File.Move("ipfs/" + transid + "_tmp", "ipfs/" + transid + "/artifact" + objstate.Image.Substring(objstate.Image.LastIndexOf('.')));
                                                imgurn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + transid + @"\artifact" + objstate.Image.Substring(objstate.Image.LastIndexOf('.'));

                                            }


                                        }

                                        if (System.IO.File.Exists("ipfs/" + transid + "/" + transid))
                                        {
                                            fileName = objstate.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                            if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }


                                            try { System.IO.File.Move("ipfs/" + transid + "/" + transid, imgurn); }
                                            catch (System.ArgumentException ex)
                                            {

                                                System.IO.File.Move("ipfs/" + transid + "/" + transid, "ipfs/" + transid + "/artifact" + objstate.Image.Substring(objstate.Image.LastIndexOf('.')));
                                                imgurn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + transid + @"\artifact" + objstate.Image.Substring(objstate.Image.LastIndexOf('.'));

                                            }



                                        }


                                        Process process3 = new Process
                                        {
                                            StartInfo = new ProcessStartInfo
                                            {
                                                FileName = @"ipfs\ipfs.exe",
                                                Arguments = "pin add " + transid,
                                                UseShellExecute = false,
                                                CreateNoWindow = true
                                            }
                                        };
                                        process3.Start();

                                        try { Directory.Delete("ipfs/" + transid + "-build", true); } catch { }


                                    }
                                }
                                else
                                {
                                    if (File.Exists("ipfs/" + transid + "/artifact")) { imgurn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + transid + @"\artifact"; }
                                    else
                                    {
                                        if (File.Exists("ipfs/" + objstate.Image.Substring(5, 46) + "/artifact" + objstate.Image.Substring(objstate.Image.LastIndexOf('.')))) { imgurn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + objstate.Image.Substring(5, 46) + @"\artifact" + objstate.Image.Substring(objstate.Image.LastIndexOf('.')); }

                                    }

                                }
                                break;
                            default:
                                if (!txtIMG.Text.ToUpper().StartsWith("HTTP") && transactionid != "")
                                {

                                    root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:18332");

                                    try
                                    {

                                        lblIMGBlockDate.Text = "bitcoin-t verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                    }
                                    catch { }

                                }

                                break;
                        }
                    }

                }
                catch { }



                try
                {
                    transactionid = "";
                    Match urnmatch = regexTransactionId.Match(urn);
                    transactionid = urnmatch.Value;
                    Root root = new Root();


                    switch (objstate.URN.Substring(0, 4))
                    {
                        case "MZC:":


                            root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");

                            try
                            {

                                lblURNBlockDate.Text = "mazacoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                            }
                            catch { }


                            break;
                        case "BTC:":


                            root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:8332", "0");

                            try
                            {

                                lblURNBlockDate.Text = "bitcoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                            }
                            catch { }



                            break;
                        case "LTC:":


                            root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:9332", "48");

                            try
                            {

                                lblURNBlockDate.Text = "litecoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                            }
                            catch { }


                            break;
                        case "DOG:":


                            root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:22555", "30");

                            try
                            {

                                lblURNBlockDate.Text = "dogecoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                            }
                            catch { }



                            break;
                        case "IPFS":
                            if (!File.Exists(urn) && !File.Exists("ipfs/" + objstate.URN.Substring(5, 46) + "/artifact") && !File.Exists("ipfs/" + objstate.URN.Substring(5, 46) + "/artifact" + objstate.URN.Substring(objstate.URN.LastIndexOf('.'))))
                            {

                                if (!System.IO.Directory.Exists(@"ipfs/" + objstate.URN.Substring(5, 46) + "-build"))
                                {

                                    Task ipfsTask = Task.Run(() =>
                                    {
                                        try { Directory.Delete("ipfs/" + objstate.URN.Substring(5, 46), true); } catch { }
                                        try { Directory.CreateDirectory("ipfs/" + objstate.URN.Substring(5, 46)); } catch { };
                                        Directory.CreateDirectory(@"ipfs/" + objstate.URN.Substring(5, 46) + "-build");
                                        Process process2 = new Process();
                                        process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                        process2.StartInfo.Arguments = "get " + objstate.URN.Substring(5, 46) + @" -o ipfs\" + objstate.URN.Substring(5, 46);
                                        process2.Start();
                                        process2.WaitForExit();

                                        string fileName;
                                        if (System.IO.File.Exists("ipfs/" + objstate.URN.Substring(5, 46)))
                                        {
                                            System.IO.File.Move("ipfs/" + objstate.URN.Substring(5, 46), "ipfs/" + objstate.URN.Substring(5, 46) + "_tmp");
                                            System.IO.Directory.CreateDirectory("ipfs/" + objstate.URN.Substring(5, 46));
                                            fileName = objstate.URN.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                            if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                            Directory.CreateDirectory("ipfs/" + objstate.URN.Substring(5, 46));

                                            try { System.IO.File.Move("ipfs/" + objstate.URN.Substring(5, 46) + "_tmp", urn); }
                                            catch (System.ArgumentException ex)
                                            {

                                                System.IO.File.Move("ipfs/" + objstate.URN.Substring(5, 46) + "_tmp", "ipfs/" + objstate.URN.Substring(5, 46) + "/artifact" + objstate.URN.Substring(objstate.URN.LastIndexOf('.')));
                                                urn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + objstate.URN.Substring(5, 46) + @"\artifact" + objstate.URN.Substring(objstate.URN.LastIndexOf('.'));

                                            }



                                        }

                                        if (System.IO.File.Exists("ipfs/" + objstate.URN.Substring(5, 46) + "/" + objstate.URN.Substring(5, 46)))
                                        {
                                            fileName = objstate.URN.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                            if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }

                                            try { System.IO.File.Move("ipfs/" + objstate.URN.Substring(5, 46) + "/" + objstate.URN.Substring(5, 46), urn); }
                                            catch (System.ArgumentException ex)
                                            {

                                                System.IO.File.Move("ipfs/" + objstate.URN.Substring(5, 46) + "/" + objstate.URN.Substring(5, 46), "ipfs/" + objstate.URN.Substring(5, 46) + "/artifact" + objstate.URN.Substring(objstate.URN.LastIndexOf('.')));
                                                urn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + objstate.URN.Substring(5, 46) + @"\artifact" + objstate.URN.Substring(objstate.URN.LastIndexOf('.'));

                                            }
                                        }

                                        var SUP = new Options { CreateIfMissing = true };

                                        using (var db = new DB(SUP, @"ipfs"))
                                        {

                                            string ipfsdaemon = db.Get("ipfs-daemon");

                                            if (ipfsdaemon == "true")
                                            {
                                                Process process3 = new Process
                                                {
                                                    StartInfo = new ProcessStartInfo
                                                    {
                                                        FileName = @"ipfs\ipfs.exe",
                                                        Arguments = "pin add " + objstate.URN.Substring(5, 46),
                                                        UseShellExecute = false,
                                                        CreateNoWindow = true
                                                    }
                                                };
                                                process3.Start();
                                            }
                                        }


                                        try { Directory.Delete(@"ipfs/" + objstate.URN.Substring(5, 46) + "-build"); } catch { }



                                    });
                                }
                                else
                                {
                                    lblURNBlockDate.Text = "ipfs verified: " + System.DateTime.UtcNow.ToString("ddd, dd MMM yyyy hh:mm:ss");
                                }
                            }
                            else
                            {
                                if (File.Exists("ipfs/" + objstate.URN.Substring(5, 46) + "/artifact")) { urn = "ipfs/" + objstate.URN.Substring(5, 46) + "/artifact"; }
                                else
                                {
                                    if (File.Exists("ipfs/" + objstate.URN.Substring(5, 46) + "/artifact" + objstate.URN.Substring(objstate.URN.LastIndexOf('.')))) { urn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + objstate.URN.Substring(5, 46) + @"\artifact" + objstate.URN.Substring(objstate.URN.LastIndexOf('.')); }

                                }
                            }
                            break;
                        default:
                            if (!txtURN.Text.ToUpper().StartsWith("HTTP"))
                            {
                                if (transactionid != "")
                                {

                                    root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:18332");

                                    try
                                    {

                                        lblURNBlockDate.Text = "bitcoin-t verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                    }
                                    catch { }

                                }


                            }


                            break;
                    }



                }
                catch { urn = imgurn; }


                try
                {
                    transactionid = "";
                    Root root = new Root();
                    Match urimatch = regexTransactionId.Match(uriurn);
                    transactionid = urimatch.Value;

                    if (uriurn != "")
                    {
                        switch (objstate.URI.Substring(0, 4))
                        {
                            case "MZC:":


                                root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");

                                try
                                {

                                    lblURIBlockDate.Text = "mazacoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                }
                                catch { }


                                break;
                            case "BTC:":


                                root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:8332", "0");

                                try
                                {

                                    lblURIBlockDate.Text = "bitcoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                }
                                catch { }


                                break;
                            case "LTC:":


                                root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:9332", "48");

                                try
                                {

                                    lblURIBlockDate.Text = "litecoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                }
                                catch { }



                                break;
                            case "DOG:":



                                root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:22555", "30");

                                try
                                {

                                    lblURIBlockDate.Text = "dogecoin verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                }
                                catch { }


                                break;
                            case "IPFS":


                                if (!System.IO.Directory.Exists(@"ipfs/" + objstate.URI.Substring(5, 46) + "-build") && !System.IO.Directory.Exists(@"ipfs/" + objstate.URI.Substring(5, 46)))
                                {


                                    Task ipfsTask = Task.Run(() =>
                                    {

                                        try { Directory.Delete("ipfs/" + objstate.URI.Substring(5, 46), true); } catch { }
                                        try { Directory.CreateDirectory("ipfs/" + objstate.URI.Substring(5, 46)); } catch { };
                                        Directory.CreateDirectory(@"ipfs/" + objstate.URI.Substring(5, 46) + "-build");
                                        Process process2 = new Process();
                                        process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                        process2.StartInfo.Arguments = "get " + objstate.URI.Substring(5, 46) + @" -o ipfs\" + objstate.URI.Substring(5, 46);
                                        process2.Start();
                                        process2.WaitForExit();

                                        string fileName;
                                        if (System.IO.File.Exists("ipfs/" + objstate.URI.Substring(5, 46)))
                                        {
                                            System.IO.File.Move("ipfs/" + objstate.URI.Substring(5, 46), "ipfs/" + objstate.URI.Substring(5, 46) + "_tmp");
                                            System.IO.Directory.CreateDirectory("ipfs/" + objstate.URI.Substring(5, 46));
                                            fileName = objstate.URI.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                            if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                            Directory.CreateDirectory("ipfs/" + objstate.URI.Substring(5, 46));
                                            System.IO.File.Move("ipfs/" + objstate.URI.Substring(5, 46) + "_tmp", uriurn);

                                            try { System.IO.File.Move("ipfs/" + objstate.URI.Substring(5, 46) + "_tmp", uriurn); }
                                            catch (System.ArgumentException ex)
                                            {

                                                System.IO.File.Move("ipfs/" + objstate.URI.Substring(5, 46) + "_tmp", "ipfs/" + objstate.URI.Substring(5, 46) + "/artifact" + objstate.URI.Substring(objstate.URI.LastIndexOf('.')));
                                                uriurn = "ipfs/" + objstate.URI.Substring(5, 46) + "/artifact" + objstate.URI.Substring(objstate.URI.LastIndexOf('.'));

                                            }


                                        }

                                        if (System.IO.File.Exists("ipfs/" + objstate.URI.Substring(5, 46) + "/" + objstate.URI.Substring(5, 46)))
                                        {
                                            fileName = objstate.URI.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                            if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }

                                            try { System.IO.File.Move("ipfs/" + objstate.URI.Substring(5, 46) + "/" + objstate.URI.Substring(5, 46), uriurn); }
                                            catch (System.ArgumentException ex)
                                            {

                                                System.IO.File.Move("ipfs/" + objstate.URI.Substring(5, 46) + "/" + objstate.URI.Substring(5, 46), "ipfs/" + objstate.URI.Substring(5, 46) + "/artifact" + objstate.URI.Substring(objstate.URI.LastIndexOf('.')));
                                                uriurn = "ipfs/" + objstate.URI.Substring(5, 46) + "/artifact" + objstate.URI.Substring(objstate.URI.LastIndexOf('.'));

                                            }
                                        }
                                        var SUP = new Options { CreateIfMissing = true };

                                        using (var db = new DB(SUP, @"ipfs"))
                                        {

                                            string ipfsdaemon = db.Get("ipfs-daemon");

                                            if (ipfsdaemon == "true")
                                            {
                                                Process process3 = new Process
                                                {
                                                    StartInfo = new ProcessStartInfo
                                                    {
                                                        FileName = @"ipfs\ipfs.exe",
                                                        Arguments = "pin add " + objstate.URI.Substring(5, 46),
                                                        UseShellExecute = false,
                                                        CreateNoWindow = true
                                                    }
                                                };
                                                process3.Start();
                                            }
                                        }


                                        Directory.Delete(@"ipfs/" + objstate.URI.Substring(5, 46) + "-build");



                                    });
                                }
                                else
                                {
                                    lblURIBlockDate.Text = "ipfs verified: " + DateTime.UtcNow.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                }

                                break;
                            default:
                                if (!txtURI.Text.ToUpper().StartsWith("HTTP") && transactionid != "")
                                {
                                    root = Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:18332");

                                    try
                                    {

                                        lblURIBlockDate.Text = "bitcoin-t verified: " + root.BlockDate.ToString("ddd, dd MMM yyyy hh:mm:ss");

                                    }
                                    catch { }

                                }
                                break;
                        }
                    }

                }
                catch { }



                // Get the file extension
                string extension = Path.GetExtension(urn).ToLower();
                Match match = regexTransactionId.Match(urn);
                transactionid = match.Value;
                string filePath = urn;
                filePath = filePath.Replace(@"\root\root\", @"\root\");
                lblURNFullPath.Text = filePath;
                txtURN.Text = objstate.URN;
                txtIMG.Text = objstate.Image;
                txtURI.Text = objstate.URI;
                lblLicense.Text = objstate.License;

                List<string> keywords = new List<string>();

                keywords = OBJState.GetKeywordsByAddress(_objectaddress, "good-user", "better-password", @"http://127.0.0.1:18332");
                foreach (string keyword in keywords)
                {

                    if (IsStandardASCII(keyword))
                    {
                        LinkLabel keywordLabel = new LinkLabel
                        {
                            Text = keyword,
                            AutoSize = true
                        };

                        keywordLabel.LinkClicked += (Ksender, b) => { Owner_LinkClicked("#" + keyword); };
                        keywordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        keywordLabel.Margin = new System.Windows.Forms.Padding(0);
                        keywordLabel.Dock = DockStyle.Bottom;
                        KeysFlow.Controls.Add(keywordLabel);
                    }

                }

                lblProcessHeight.Text = objstate.ProcessHeight.ToString();
                lblLastChangedDate.Text = objstate.ChangeDate.ToString("ddd, dd MMM yyyy hh:mm:ss");
                if (urnblockdate.Year > 1)
                {
                    lblURNBlockDate.Text = urnblockdate.ToString("ddd, dd MMM yyyy hh:mm:ss");
                }
                if (imgblockdate.Year > 1)
                {
                    lblIMGBlockDate.Text = imgblockdate.ToString("ddd, dd MMM yyyy hh:mm:ss");
                }
                if (uriblockdate.Year > 1)
                {
                    lblURIBlockDate.Text = uriblockdate.ToString("ddd, dd MMM yyyy hh:mm:ss");
                }

                txtdesc.Text = objstate.Description;
                txtName.Text = objstate.Name;
                long totalQty = objstate.Owners.Values.Sum();


                if (OwnersPanel.Visible)
                {

                    btnRefreshOwners.PerformClick();

                }
                else
                {
                    btnRefreshSup.PerformClick();

                }

                if (!isUserControl) { registrationPanel.Visible = true; }


                OBJState isOfficial = OBJState.GetObjectByURN(objstate.URN, "good-user", "better-password", "http://127.0.0.1:18332");
                if (isOfficial.URN != null)
                {
                    if (isOfficial.Creators.First().Key != this._objectaddress)
                    {
                        txtOfficialURN.Text = isOfficial.Creators.First().Key;
                        btnLaunchURN.Visible = false;
                        btnOfficial.Visible = true;
                    }
                    else
                    {

                        lblOfficial.Visible = true;
                        System.Windows.Forms.ToolTip myTooltip = new System.Windows.Forms.ToolTip();
                        myTooltip.SetToolTip(lblOfficial, isOfficial.URN);

                    }
                }


                switch (extension.ToLower())
                {
                    case "":

                        if (File.Exists(urn))
                        {
                            pictureBox1.ImageLocation = urn;
                        }
                        else
                        {
                            lblPleaseStandBy.Text = txtURN.Text;
                            lblPleaseStandBy.Visible = true;
                        }
                        break;
                    case ".exe":
                    case ".dll":
                    case ".bat":
                    case ".cmd":
                    case ".com":
                    case ".msi":
                    case ".scr":
                    case ".vbs":
                    case ".wsf":
                    case ".ps1":
                    case ".psm1":
                    case ".psd1":
                    case ".reg":
                    case ".hta":
                    case ".jar":
                    case ".jse":
                    case ".lnk":
                    case ".mht":
                    case ".mhtml":
                    case ".msc":
                    case ".msp":
                    case ".mst":
                    case ".pif":
                    case ".py":
                    case ".pyc":
                    case ".pyo":
                    case ".pyw":
                    case ".pyz":
                    case ".pyzw":
                    case ".sct":
                    case ".shb":
                    case ".u3p":
                    case ".vb":
                    case ".vbe":
                    case ".vbscript":
                    case ".ws":
                    case ".xla":
                    case ".xlam":
                    case ".xls":
                    case ".xlsb":
                    case ".xlsm":
                    case ".xlsx":
                    case ".xltm":
                    case ".xltx":
                    case ".xml":
                    case ".xsl":
                    case ".xslt":
                        pictureBox1.SuspendLayout();
                        if (File.Exists(imgurn))
                        {

                            pictureBox1.ImageLocation = imgurn;
                        }
                        else
                        {
                            Random rnd = new Random();
                            string[] gifFiles = Directory.GetFiles("includes", "*.gif");
                            if (gifFiles.Length > 0)
                            {
                                int randomIndex = rnd.Next(gifFiles.Length);
                                string randomGifFile = gifFiles[randomIndex];

                                pictureBox1.ImageLocation = randomGifFile;

                            }
                            else
                            {
                                try
                                {
                                    pictureBox1.ImageLocation = @"includes\HugPuddle.jpg";
                                }
                                catch { }
                            }


                        }
                        pictureBox1.ResumeLayout();


                        if (btnOfficial.Visible == false)
                        {
                            btnLaunchURN.Visible = true;
                            lblWarning.Visible = true;
                        }
                        break;

                    case ".glb":
                        //Show image in main box and show open file button
                        pictureBox1.SuspendLayout();
                        if (File.Exists(imgurn))
                        {

                            pictureBox1.ImageLocation = imgurn;
                        }
                        else
                        {
                            Random rnd = new Random();
                            string[] gifFiles = Directory.GetFiles("includes", "*.gif");
                            if (gifFiles.Length > 0)
                            {
                                int randomIndex = rnd.Next(gifFiles.Length);
                                string randomGifFile = gifFiles[randomIndex];

                                pictureBox1.ImageLocation = randomGifFile;

                            }
                            else
                            {
                                try
                                {
                                    pictureBox1.ImageLocation = @"includes\HugPuddle.jpg";
                                }
                                catch { }
                            }


                        }
                        pictureBox1.ResumeLayout();
                        if (btnOfficial.Visible == false) { btnLaunchURN.Visible = true; }
                        break;
                    case ".bmp":
                    case ".gif":
                    case ".ico":
                    case ".jpeg":
                    case ".jpg":
                    case ".png":
                    case ".tif":
                    case ".tiff":
                        // Create a new PictureBox
                        pictureBox1.SuspendLayout();
                        if (File.Exists(urn))
                        {

                            pictureBox1.ImageLocation = urn;
                        }
                        else
                        {
                            Random rnd = new Random();
                            string[] gifFiles = Directory.GetFiles("includes", "*.gif");
                            if (gifFiles.Length > 0)
                            {
                                int randomIndex = rnd.Next(gifFiles.Length);
                                string randomGifFile = gifFiles[randomIndex];

                                pictureBox1.ImageLocation = randomGifFile;

                            }
                            else
                            {
                                try
                                {
                                    pictureBox1.ImageLocation = @"includes\HugPuddle.jpg";
                                }
                                catch { }
                            }


                        }
                        pictureBox1.ResumeLayout();


                        if (btnOfficial.Visible == false) { btnLaunchURN.Visible = true; }
                        break;
                    case ".mp4":
                    case ".avi":
                    case ".mp3":
                    case ".wav":
                    case ".pdf":


                        flowPanel.Visible = false;
                        string viewerPath = Path.GetDirectoryName(urn) + @"\urnviewer.html";
                        flowPanel.Controls.Clear();

                        string htmlstring = "<html><body><embed src=\"" + urn + "\" width=100% height=100%></body></html>";

                        try
                        {
                            System.IO.File.WriteAllText(Path.GetDirectoryName(urn) + @"\urnviewer.html", htmlstring);
                            if (btnOfficial.Visible == false) { btnLaunchURN.Visible = true; }
                            await webviewer.EnsureCoreWebView2Async();
                            webviewer.CoreWebView2.Navigate(viewerPath);
                        }
                        catch
                        {
                            Thread.Sleep(1000);
                            try
                            {
                                await webviewer.EnsureCoreWebView2Async();
                                webviewer.CoreWebView2.Navigate(viewerPath);
                            }
                            catch { }
                        }




                        break;
                    case ".htm":
                    case ".html":
                        if (isWWW == false)
                        {
                            chkRunTrustedObject.Visible = true;
                            flowPanel.Visible = false;
                            flowPanel.Controls.Clear();

                            string htmlembed = "<html><body><embed src=\"" + urn + "\" width=100% height=100%></body></html>";
                            string potentialyUnsafeHtml = "";

                            try
                            {
                                potentialyUnsafeHtml = System.IO.File.ReadAllText(urn);

                            }
                            catch { }


                            if (chkRunTrustedObject.Checked)
                            {
                                try
                                {

                                    try { System.IO.Directory.Delete(Path.GetDirectoryName(urn), true); } catch { }

                                    switch (objstate.URN.Substring(0, 4))
                                    {
                                        case "MZC:":
                                            if (!System.IO.Directory.Exists(@"root/" + transactionid))
                                            {
                                                Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");
                                            }
                                            break;
                                        case "BTC:":
                                            if (!System.IO.Directory.Exists(@"root/" + transactionid))
                                            {
                                                Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:8332", "0");
                                            }
                                            break;
                                        case "LTC:":
                                            if (!System.IO.Directory.Exists(@"root/" + transactionid))
                                            {
                                                Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:9332", "48");
                                            }
                                            break;
                                        case "DOG:":
                                            if (!System.IO.Directory.Exists(@"root/" + transactionid))
                                            {
                                                Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:22555", "30");
                                            }
                                            break;
                                        case "IPFS":
                                            ipfsurn = urn;
                                            if (objstate.URN.Length == 51) { ipfsurn += @"\artifact"; }

                                            if (!System.IO.Directory.Exists(@"ipfs/" + objstate.URN.Substring(5, 46) + "-build") && !System.IO.Directory.Exists(@"ipfs/" + objstate.URN.Substring(5, 46)))
                                            {


                                                Task ipfsTask = Task.Run(() =>
                                                {
                                                    Directory.CreateDirectory(@"ipfs/" + objstate.URN.Substring(5, 46) + "-build");
                                                    Process process2 = new Process();
                                                    process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                                    process2.StartInfo.Arguments = "get " + objstate.URN.Substring(5, 46) + @" -o ipfs\" + objstate.URN.Substring(5, 46);
                                                    process2.Start();
                                                    process2.WaitForExit();

                                                    if (System.IO.File.Exists("ipfs/" + objstate.URN.Substring(5, 46)))
                                                    {
                                                        System.IO.File.Move("ipfs/" + objstate.URN.Substring(5, 46), "ipfs/" + objstate.URN.Substring(5, 46) + "_tmp");

                                                        string fileName = objstate.URN.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                        if (fileName == "")
                                                        {
                                                            fileName = "artifact";
                                                        }
                                                        else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                                        Directory.CreateDirectory(@"ipfs/" + objstate.URN.Substring(5, 46));
                                                        try { System.IO.File.Move("ipfs/" + objstate.URN.Substring(5, 46) + "_tmp", ipfsurn); } catch { }
                                                    }

                                                    var SUP = new Options { CreateIfMissing = true };

                                                    using (var db = new DB(SUP, @"ipfs"))
                                                    {

                                                        string ipfsdaemon = db.Get("ipfs-daemon");

                                                        if (ipfsdaemon == "true")
                                                        {
                                                            Process process3 = new Process
                                                            {
                                                                StartInfo = new ProcessStartInfo
                                                                {
                                                                    FileName = @"ipfs\ipfs.exe",
                                                                    Arguments = "pin add " + objstate.URN.Substring(5, 46),
                                                                    UseShellExecute = false,
                                                                    CreateNoWindow = true
                                                                }
                                                            };
                                                            process3.Start();
                                                        }
                                                    }

                                                    Directory.Delete(@"ipfs/" + objstate.URN.Substring(5, 46) + "-build");

                                                });
                                            }

                                            break;
                                        default:
                                            if (!System.IO.Directory.Exists(@"root/" + transactionid))
                                            {
                                                Root.GetRootByTransactionId(transactionid, "good-user", "better-password", @"http://127.0.0.1:18332");
                                            }
                                            break;
                                    }
                                    try
                                    {
                                        if (Uri.TryCreate(urn, UriKind.Absolute, out Uri uri) && uri.Scheme == Uri.UriSchemeHttp)
                                        {
                                            using (var client = new WebClient())
                                            {
                                                potentialyUnsafeHtml = client.DownloadString(uri);
                                                System.Security.Cryptography.SHA256 mySHA256 = SHA256Managed.Create();
                                                byte[] hashValue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(potentialyUnsafeHtml));
                                                urn = @"root\" + BitConverter.ToString(hashValue).Replace("-", String.Empty) + @"\index.html";
                                            }

                                        }
                                        else
                                        {
                                            potentialyUnsafeHtml = System.IO.File.ReadAllText(urn);
                                        }
                                    }
                                    catch { }

                                    var matches = regexTransactionId.Matches(potentialyUnsafeHtml);
                                    foreach (Match transactionID in matches)
                                    {

                                        switch (objstate.URN.Substring(0, 4))
                                        {
                                            case "MZC:":
                                                if (!System.IO.Directory.Exists(@"root/" + transactionID.Value))
                                                {
                                                    Task.Run(() =>
                                                    {
                                                        Root.GetRootByTransactionId(transactionID.Value, "good-user", "better-password", @"http://127.0.0.1:12832", "50");
                                                    });
                                                }
                                                break;
                                            case "BTC:":
                                                if (!System.IO.Directory.Exists(@"root/" + transactionID.Value))
                                                {
                                                    Task.Run(() =>
                                                    {
                                                        Root.GetRootByTransactionId(transactionID.Value, "good-user", "better-password", @"http://127.0.0.1:8332", "0");
                                                    });
                                                }
                                                break;
                                            case "LTC:":
                                                if (!System.IO.Directory.Exists(@"root/" + transactionID.Value))
                                                {
                                                    Task.Run(() =>
                                                    {
                                                        Root.GetRootByTransactionId(transactionID.Value, "good-user", "better-password", @"http://127.0.0.1:9332", "48");
                                                    });
                                                }
                                                break;
                                            case "DOG:":
                                                if (!System.IO.Directory.Exists(@"root/" + transactionID.Value))
                                                {
                                                    Task.Run(() =>
                                                    {
                                                        Root.GetRootByTransactionId(transactionID.Value, "good-user", "better-password", @"http://127.0.0.1:22555", "30");
                                                    });
                                                }
                                                break;
                                            default:
                                                if (!System.IO.Directory.Exists(@"root/" + transactionID.Value))
                                                {
                                                    Task.Run(() =>
                                                    {
                                                        Root.GetRootByTransactionId(transactionID.Value, "good-user", "better-password", @"http://127.0.0.1:18332");
                                                    });
                                                }
                                                break;
                                        }

                                    }

                                    string _address = _objectaddress;
                                    string _viewer = objstate.Owners.Last().Key;
                                    string _viewername = null; //to be implemented
                                    string _creator = objstate.Creators.Last().Key;
                                    int _owner = objstate.Owners.Count();
                                    string _urn = HttpUtility.UrlEncode(objstate.URN);
                                    string _uri = HttpUtility.UrlEncode(objstate.URI);
                                    string _img = HttpUtility.UrlEncode(objstate.Image);

                                    string querystring = "?address=" + _address + "&viewer=" + _viewer + "&viewername=" + _viewername + "&creator=" + _creator + "&owner=" + _owner + "&urn=" + _urn + "&uri=" + _uri + "&img=" + _img;
                                    htmlembed = "<html><body><embed src=\"" + urn + querystring + "\" width=100% height=100%></body></html>";


                                }
                                catch { }



                            }
                            else
                            {
                                var sanitizer = new HtmlSanitizer();
                                var sanitized = sanitizer.Sanitize(potentialyUnsafeHtml);
                                try { System.IO.File.WriteAllText(urn, sanitized); } catch { }
                            }

                            try
                            {
                                System.IO.File.WriteAllText(Path.GetDirectoryName(urn) + @"\urnviewer.html", htmlembed);
                                if (btnOfficial.Visible == false) { btnLaunchURN.Visible = true; }

                                await webviewer.EnsureCoreWebView2Async();
                                webviewer.CoreWebView2.Navigate(Path.GetDirectoryName(urn) + @"\urnviewer.html");
                            }
                            catch
                            {
                                Thread.Sleep(500);
                                try
                                {
                                    await webviewer.EnsureCoreWebView2Async();
                                    webviewer.CoreWebView2.Navigate(Path.GetDirectoryName(urn) + @"\urnviewer.html");
                                }
                                catch { }
                            }

                        }
                        break;
                    default:

                        pictureBox1.Invoke(new Action(() => pictureBox1.ImageLocation = imgurn));
                        if (btnOfficial.Visible == false)
                        {
                            btnLaunchURN.Visible = true;
                            lblWarning.Visible = true;
                        }
                        break;
                }


                imgPicture.SuspendLayout();
                if (File.Exists(imgurn))
                {

                    imgPicture.ImageLocation = imgurn;
                }
                else
                {
                    Random rnd = new Random();
                    string[] gifFiles = Directory.GetFiles("includes", "*.gif");
                    if (gifFiles.Length > 0)
                    {
                        int randomIndex = rnd.Next(gifFiles.Length);
                        string randomGifFile = gifFiles[randomIndex];

                        imgPicture.ImageLocation = randomGifFile;

                    }
                    else
                    {
                        try
                        {
                            imgPicture.ImageLocation = @"includes\HugPuddle.jpg";
                        }
                        catch { }
                    }


                }

                imgPicture.ResumeLayout();
                if (lblOfficial.Visible) { lblOfficial.Refresh(); }


            }
        }


        private void CopyAddressByCreatedDateClick(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(_objectaddress);
        }

        private void CopyDescriptionByDescriptionClick(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(txtdesc.Text);
        }


        private void ButtonRefreshTransactionsClick(object sender, EventArgs e)
        {

            transFlow.SuspendLayout();
            txtdesc.Visible = false;
            registrationPanel.Visible = false;
            // Clear controls if no messages have been displayed yet
            if (numChangesDisplayed == 0)
            {
                transFlow.Controls.Clear();
            }

            int rownum = 1;
            bool isverbose;

            // fetch current JSONOBJ from disk if it exists
            try
            {
                string JSONOBJ = System.IO.File.ReadAllText(@"root\" + _objectaddress + @"\OBJ.json");
                OBJState objectState = JsonConvert.DeserializeObject<OBJState>(JSONOBJ);
                if (objectState.Verbose) { isverbose = true; }
                else
                {
                    try { System.IO.Directory.Delete(@"root/" + _objectaddress, true); isVerbose = true; } catch { }
                }
            }
            catch { }

            OBJState.GetObjectByAddress(_objectaddress, "good-user", "better-password", "http://127.0.0.1:18332", "111", true);


            var trans = new Options { CreateIfMissing = true };

            using (var db = new DB(trans, @"root\event"))
            {
                string lastKey = db.Get("lastkey!" + _objectaddress);
                if (lastKey == null) { return; }
                LevelDB.Iterator it = db.CreateIterator();
                for (
                   it.Seek(lastKey);
                  it.IsValid() && it.KeyAsString().StartsWith(_objectaddress) && rownum <= numChangesDisplayed + 10;
                    it.Prev()
                 )


                {
                    if (rownum > numChangesDisplayed)
                    {

                        string process = it.ValueAsString();

                        List<string> transMessagePacket = JsonConvert.DeserializeObject<List<string>>(process);

                        string fromAddress = TruncateAddress(transMessagePacket[0]);
                        string toAddress = TruncateAddress(transMessagePacket[1]);
                        string action = transMessagePacket[2];
                        string qty = transMessagePacket[3];
                        string amount = transMessagePacket[4];
                        string status = transMessagePacket[5];
                        string tstamp = it.KeyAsString().Split('!')[1];

                        System.Drawing.Color bgcolor;
                        if (rownum % 2 == 0)
                        {
                            bgcolor = System.Drawing.Color.White;
                        }
                        else
                        {
                            bgcolor = System.Drawing.Color.LightGray;
                        }




                        CreateTransRow(fromAddress, transMessagePacket[0], toAddress, transMessagePacket[1], action, qty, amount, DateTime.ParseExact(tstamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture), status, bgcolor, transFlow);

                    }
                    rownum++;
                }
                it.Dispose();
            }

            numChangesDisplayed += 10;
            transFlow.ResumeLayout();
            transFlow.Visible = true;
            KeysFlow.Visible = true;



        }

        void Attachment_Clicked(string path)
        {
            if (path.ToUpper().StartsWith("IPFS:") || path.ToUpper().StartsWith("BTC:") || path.ToUpper().StartsWith("MZC:") || path.ToUpper().StartsWith("LTC:") || path.ToUpper().StartsWith("DOG:"))
            {
                new ObjectBrowser(path).Show();
            }
            else
            {
                try
                { System.Diagnostics.Process.Start(path); }
                catch { System.Media.SystemSounds.Exclamation.Play(); }
            }
        }

        private bool IsStandardASCII(string input)
        {
            foreach (char c in input)
            {
                if (c < 32 || c > 127)
                {
                    return false;
                }
            }
            return true;
        }

        private void btnOfficial_Click(object sender, EventArgs e)
        {
            new ObjectDetails(txtOfficialURN.Text).Show();
        }

        private void imgPicture_Validated(object sender, EventArgs e)
        {
            lblOfficial.Refresh();
        }

        private void imgPicture_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(_objectaddress);
        }

        private void txtName_Click(object sender, EventArgs e)
        {
            System.Windows.Clipboard.SetText(_objectaddress);
        }

        private void lblLaunchURI_Click(object sender, EventArgs e)
        {
            string src = txtURI.Text;
            try
            { System.Diagnostics.Process.Start(src); }
            catch { System.Media.SystemSounds.Exclamation.Play(); }
        }

        private void btnBurn_Click(object sender, EventArgs e)
        {
            new ObjectBurn(_objectaddress).Show();
        }

        private void btnGive_Click(object sender, EventArgs e)
        {
            new ObjectGive(_objectaddress).Show();
        }

        private void btnDisco_Click(object sender, EventArgs e)
        {
            DiscoBall disco = new DiscoBall("", "", _objectaddress, imgPicture.ImageLocation, false);
            disco.StartPosition = FormStartPosition.CenterScreen;
            disco.Show(this);
            disco.Focus();
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            new ObjectBuy(_objectaddress).Show();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {

            JukeBox jukeBoxForm = new JukeBox(_objectaddress);
            jukeBoxForm.Show();// Pass the reference to the current form as the parent form
            
        }
    }

}

