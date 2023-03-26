﻿using LevelDB;
using NBitcoin.RPC;
using NBitcoin;
using SUP.P2FK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SUP
{
    public partial class SupMain : Form
    {
        private readonly static object SupLocker = new object();
        private List<string> BTCMemPool = new List<string>();
        private List<string> BTCTMemPool = new List<string>();
        private List<string> MZCMemPool = new List<string>();
        private List<string> LTCMemPool = new List<string>();
        private List<string> DOGMemPool = new List<string>();
        private bool ipfsActive;
        private bool btctActive;
        private bool btcActive;
        private bool mzcActive;
        private bool ltcActive;
        private bool dogActive;
        private RichTextBox richTextBox1;
        ObjectBrowserControl OBcontrol = new ObjectBrowserControl();
        private int numMessagesDisplayed;
        private int numPrivateMessagesDisplayed;
        FlowLayoutPanel supPrivateFlow = new FlowLayoutPanel();

        public SupMain()
        {
            InitializeComponent();
        }

        private void SupMaincs_Load(object sender, EventArgs e)
        {
            if (Directory.Exists("root")) { lblAdultsOnly.Visible = false; }

            OBcontrol.Dock = DockStyle.Fill;
            OBcontrol.ProfileURNChanged += OBControl_ProfileURNChanged;
            splitContainer1.Panel2.Controls.Add(OBcontrol);
            
        }

        private void OBControl_ProfileURNChanged(object sender, EventArgs e)
        {
            if (sender is ObjectBrowserControl objectBrowserControl)
            {
                var objectBrowserForm = objectBrowserControl.Controls[0].Controls[0] as ObjectBrowser;
                if (objectBrowserForm != null)
                {

                    profileURN.Links[0].LinkData = objectBrowserForm.profileURN.Links[0].LinkData;
                    profileURN.Text = objectBrowserForm.profileURN.Text;
                    profileURN.Enabled = true;
                    btnBlock.Enabled = true;
                    btnFollow.Enabled = true;
                    btnMute.Enabled = true;
                    btnPrivateMessage.Enabled = true;
                    btnPublicMessage.Enabled = true;
                    btnRefresh.Enabled = true;
                    MakeActiveProfile(objectBrowserForm.profileURN.Links[0].LinkData.ToString());
                    numMessagesDisplayed = 0;
                    numPrivateMessagesDisplayed = 0;
                    supFlow.Controls.Clear();
                    //btnRefresh.PerformClick();

                }
            }
        }

        private void MakeActiveProfile(string address)
        {
            Regex regexTransactionId = new Regex(@"\b[0-9a-f]{64}\b");
            PROState activeProfile = PROState.GetProfileByAddress(address,"good-user","better-password", @"http://127.0.0.1:18332");

            if (activeProfile.URN == null) { profileURN.Text = "anon"; profileBIO.Text = "";profileCreatedDate.Text = ""; profileIMG.ImageLocation = ""; activeProfile.Image = "";  return; }

            profileBIO.Text = activeProfile.Bio;
            profileCreatedDate.Text = "since: " + activeProfile.CreatedDate.ToString("MM/dd/yyyy hh:mm:ss tt");

            string imgurn = "";

            if (activeProfile.Image != "")
            {
                imgurn = activeProfile.Image;

                if (!activeProfile.Image.ToLower().StartsWith("http"))
                {
                    imgurn = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + activeProfile.Image.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", "").Replace("IPFS:", "").Replace("btc:", "").Replace("mzc:", "").Replace("ltc:", "").Replace("dog:", "").Replace("ipfs:", "").Replace(@"/", @"\");

                    if (activeProfile.Image.ToLower().StartsWith("ipfs:")) { imgurn = imgurn.Replace(@"\root\", @"\ipfs\"); }
                }
            }
            
            List<string> allowedExtensions = new List<string> { ".bmp", ".gif", ".ico", ".jpeg", ".jpg", ".png", ".tif", ".tiff", "" };
            string extension = Path.GetExtension(imgurn).ToLower();
            if (allowedExtensions.Contains(extension))
            {

                if (File.Exists(imgurn) || imgurn.ToUpper().StartsWith("HTTP"))
                {

                    profileIMG.ImageLocation = imgurn;
                }
                else
                {
                    Random rnd = new Random();
                    string[] gifFiles = Directory.GetFiles("includes", "*.gif");
                    if (gifFiles.Length > 0)
                    {
                        int randomIndex = rnd.Next(gifFiles.Length);
                        string randomGifFile = gifFiles[randomIndex];

                        profileIMG.ImageLocation = randomGifFile;

                    }
                    else
                    {
                        try
                        {
                            profileIMG.ImageLocation = @"includes\HugPuddle.jpg";
                        }
                        catch { }
                    }


                }


            }
           
            // update buttons to reference current status.


        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            foreach(Control control in supFlow.Controls)
            { 
               
                control.Width = supFlow.Width - 42;
              
            }
        }

        private void btnMint_Click(object sender, EventArgs e)
        {

            if (splitContainer1.Panel2.Controls.Contains(supPrivateFlow))
            {
                splitContainer1.Panel2.Controls.Clear();
                numPrivateMessagesDisplayed = 0;
                splitContainer1.Panel2.Controls.Add(OBcontrol);
                
            }
            else
            {
                // Create the form that will contain the buttons
                Form buttonForm = new Form();
                buttonForm.FormBorderStyle = FormBorderStyle.None;
                buttonForm.BackColor = Color.White;
                buttonForm.Size = new Size(300, 150);

                // Create the "Object Mint" button
                Button objectMintButton = new Button();
                objectMintButton.Text = @"Object Mint \ Update";
                objectMintButton.Font = new Font("Arial", 16, FontStyle.Bold);
                objectMintButton.Size = new Size(250, 50);
                objectMintButton.Location = new Point(25, 25);
                objectMintButton.Click += (s, ev) =>
                {
                    // Close the button form
                    buttonForm.Close();

                    // Show the "ObjectMint" form and set focus to it
                    ObjectMint mintform = new ObjectMint();
                    mintform.StartPosition = FormStartPosition.CenterScreen;
                    mintform.Show(this);
                    mintform.Focus();
                };
                buttonForm.Controls.Add(objectMintButton);

                // Create the "Mint Profile" button
                Button mintProfileButton = new Button();
                mintProfileButton.Text = @"Profile Mint \ Update";
                mintProfileButton.Font = new Font("Arial", 16, FontStyle.Bold);
                mintProfileButton.Size = new Size(250, 50);
                mintProfileButton.Location = new Point(25, 85);
                mintProfileButton.Click += (s, ev) =>
                {
                    // Close the button form
                    buttonForm.Close();

                    // Show the "ProfileMint" form and set focus to it
                    ProfileMint mintprofile = new ProfileMint();
                    mintprofile.StartPosition = FormStartPosition.CenterScreen;
                    mintprofile.Show(this);
                    mintprofile.Focus();
                };
                buttonForm.Controls.Add(mintProfileButton);

                // Show the button form centered on the launching program and set focus to it
                buttonForm.StartPosition = FormStartPosition.CenterParent;
                buttonForm.ShowDialog(this);
                buttonForm.Focus();
            }
        }


        private async void btnLive_Click(object sender, EventArgs e)
        {

            if (btnLive.BackColor == Color.White)
            {
                btnLive.BackColor = Color.Blue;
                btnLive.ForeColor = Color.Yellow;

                string walletUsername = "good-user";
                string walletPassword = "better-password";
                string walletUrl = @"http://127.0.0.1:18332";
                NetworkCredential credentials = new NetworkCredential(walletUsername, walletPassword);
                RPCClient rpcClient = new RPCClient(credentials, new Uri(walletUrl), NBitcoin.Network.Main);
                Task.Run(() =>
                {
                    try
                    {
                        string isActive = rpcClient.GetBalance().ToString();
                        this.Invoke((MethodInvoker)delegate
                        {
                            btctActive = true;
                        });

                    }
                    catch { }
                });


                Task.Run(() =>
                {
                    try
                    {
                        walletUrl = @"http://127.0.0.1:8332";
                        rpcClient = new RPCClient(credentials, new Uri(walletUrl), NBitcoin.Network.Main);
                        string isActive = rpcClient.GetBalance().ToString();
                        this.Invoke((MethodInvoker)delegate
                        {
                            btcActive = true;
                        });

                    }
                    catch { }
                });



                Task.Run(() =>
                {
                    try
                    {
                        walletUrl = @"http://127.0.0.1:9332";
                        rpcClient = new RPCClient(credentials, new Uri(walletUrl), NBitcoin.Network.Main);
                        string isActive = rpcClient.GetBalance().ToString();
                        if (decimal.TryParse(isActive, out decimal _))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                ltcActive = true;
                            });

                        }
                    }
                    catch { }
                });


                Task.Run(() =>
                {
                    try
                    {
                        walletUrl = @"http://127.0.0.1:12832";
                        rpcClient = new RPCClient(credentials, new Uri(walletUrl), NBitcoin.Network.Main);
                        string isActive = rpcClient.GetBalance().ToString();
                        if (decimal.TryParse(isActive, out decimal _))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                mzcActive = true;
                            });

                        }
                    }
                    catch { }
                });

                Task.Run(() =>
                {
                    try
                    {
                        walletUrl = @"http://127.0.0.1:22555";
                        rpcClient = new RPCClient(credentials, new Uri(walletUrl), NBitcoin.Network.Main);
                        string isActive = rpcClient.GetBalance().ToString();
                        if (decimal.TryParse(isActive, out decimal _))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                dogActive = true;
                            });

                        }
                    }
                    catch { }
                });

                var SUP = new Options { CreateIfMissing = true };
                using (var db = new DB(SUP, @"ipfs"))
                {

                    string ipfsdaemon = db.Get("ipfs-daemon");

                    if (ipfsdaemon == "true")
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            ipfsActive = true;
                        });
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
                    }

                }


                tmrSearchMemoryPool.Enabled = true;
                
            }
            else
            {
                btnLive.BackColor = Color.White;
                btnLive.ForeColor = Color.Black;
                tmrSearchMemoryPool.Enabled = false;             

            }
        }





        private void AddToSearchResults(List<OBJState> objects)
        {

            foreach (OBJState objstate in objects)
            {
                try
                {
                    supFlow.Invoke((MethodInvoker)delegate
                    {
                        supFlow.SuspendLayout();
                        if (objstate.Owners != null)
                        {
                            string transid = "";
                            FoundObjectControl foundObject = new FoundObjectControl();
                            foundObject.SuspendLayout();
                            if (objstate.Image != null)
                            {
                                try { transid = objstate.Image.Substring(4, 64).Replace(":", ""); } catch { try { transid = objstate.Image.Substring(5, 46); } catch { } }
                                try { foundObject.ObjectImage.ImageLocation = @"root/" + objstate.Image.Replace("BTC:", "").Replace("MZC:", "").Replace("LTC:", "").Replace("DOG:", ""); } catch { }
                            }
                                foundObject.ObjectName.Text = objstate.Name;
                            foundObject.ObjectDescription.Text = objstate.Description;
                            foundObject.ObjectAddress.Text = objstate.Creators.First().Key;
                            foundObject.ObjectQty.Text = objstate.Owners.Values.Sum().ToString() + "x";
                            foundObject.ObjectId.Text = objstate.TransactionId;
                            try
                            {
                                if (objstate.Image != null)
                                {
                                    switch (objstate.Image.ToUpper().Substring(0, 4))
                                    {
                                        case "BTC:":
                                            if (btcActive)
                                            {
                                                if (!System.IO.Directory.Exists("root/" + transid))
                                                {
                                                    Root.GetRootByTransactionId(transid, "good-user", "better-password", @"http://127.0.0.1:8332", "0");
                                                }
                                            }
                                            break;
                                        case "MZC:":
                                            if (mzcActive)
                                            {
                                                if (!System.IO.Directory.Exists("root/" + transid))
                                                {
                                                    Root.GetRootByTransactionId(transid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");
                                                }
                                            }
                                            break;
                                        case "LTC:":
                                            if (ltcActive)
                                            {
                                                if (!System.IO.Directory.Exists("root/" + transid))
                                                {
                                                    Root.GetRootByTransactionId(transid, "good-user", "better-password", @"http://127.0.0.1:9332", "48");
                                                }
                                            }
                                            break;
                                        case "DOG:":
                                            if (dogActive)
                                            {
                                                if (!System.IO.Directory.Exists("root/" + transid))
                                                {
                                                    Root.GetRootByTransactionId(transid, "good-user", "better-password", @"http://127.0.0.1:22555", "30");
                                                }
                                            }
                                            break;
                                        case "IPFS":
                                            if (ipfsActive)
                                            {
                                                if (!System.IO.Directory.Exists("ipfs/" + transid))
                                                {
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
                                                        System.IO.File.Move("ipfs/" + transid + "_tmp", @"ipfs/" + transid + @"/" + fileName);
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


                                                }

                                                if (objstate.Image.Length == 51)
                                                { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/") + @"/artifact"; }
                                                else { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/"); }
                                            }
                                            break;
                                        case "HTTP":
                                            foundObject.ObjectImage.ImageLocation = objstate.Image;
                                            break;


                                        default:
                                            if (btctActive)
                                            {
                                                transid = objstate.Image.Substring(0, 64);
                                                if (!System.IO.Directory.Exists("root/" + transid))
                                                {
                                                    Root root = Root.GetRootByTransactionId(transid, "good-user", "better-password", @"http://127.0.0.1:18332");
                                                }
                                                foundObject.ObjectImage.ImageLocation = @"root/" + objstate.Image;
                                            }
                                            break;
                                    }
                                }
                            }
                            catch { }

                            foreach (KeyValuePair<string, DateTime> creator in objstate.Creators.Skip(1))
                            {
                                                              

                                    if (foundObject.ObjectCreators.Text == "")
                                    {


                                        foundObject.ObjectCreators.Text = TruncateAddress(creator.Key);
                                        foundObject.ObjectCreators.Links.Add(0, creator.Key.Length, creator.Key);
                                        System.Windows.Forms.ToolTip myTooltip = new System.Windows.Forms.ToolTip();
                                        myTooltip.SetToolTip(foundObject.ObjectCreators, creator.Key);
                                    }
                                    else
                                    {


                                        if (foundObject.ObjectCreators2.Text == "")
                                        {
                                            foundObject.ObjectCreators2.Text = TruncateAddress(creator.Key);
                                            foundObject.ObjectCreators2.Links.Add(0, creator.Key.Length, creator.Key);
                                            System.Windows.Forms.ToolTip myTooltip = new System.Windows.Forms.ToolTip();
                                            myTooltip.SetToolTip(foundObject.ObjectCreators2, creator.Key);
                                        }

                                    }

                                

                            }
                      
                            foundObject.ResumeLayout();
                                                                                      
                                supFlow.Controls.Add(foundObject);
                                supFlow.Controls.SetChildIndex(foundObject, 0);                                                     

                        }
                        supFlow.ResumeLayout();
                    });
                }
                catch { }
            }


        }
        private void ButtonLoadWorkBench(object sender, EventArgs e)
        {
            new WorkBench().Show();
        }

        private void ButtonLoadConnections(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
            {
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                new Connections().Show();
            }

            
        }

        private void tmrSearchMemoryPool_Tick(object sender, EventArgs e)
        {
            lock (SupLocker)
            {
                tmrSearchMemoryPool.Stop();

                try
                {
                    Task SearchMemoryTask = Task.Run(() =>
                    {
                        var SUP = new Options { CreateIfMissing = true };
                        List<string> differenceQuery = new List<string>();
                        List<string> newtransactions = new List<string>();
                        string flattransactions;
                        OBJState isobject = new OBJState();
                        List<OBJState> foundobjects = new List<OBJState>();
                        NetworkCredential credentials = new NetworkCredential("good-user", "better-password");
                        RPCClient rpcClient;

                        string filter = "";

                         if (btctActive)
                        {
                            try
                            {
                                rpcClient = new RPCClient(credentials, new Uri(@"http://127.0.0.1:18332"), Network.Main);
                                flattransactions = rpcClient.SendCommand("getrawmempool").ResultString;
                                flattransactions = flattransactions.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
                                newtransactions = flattransactions.Split(',').ToList();

                                if (BTCTMemPool.Count == 0)
                                {
                                    BTCTMemPool = newtransactions;
                                }
                                else
                                {
                                    differenceQuery =
                                    (List<string>)newtransactions.Except(BTCTMemPool).ToList(); ;

                                    BTCTMemPool = newtransactions;

                                    foreach (var s in differenceQuery)
                                    {
                                        try
                                        {

                                            Root root = Root.GetRootByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:18332");
                                            if (root.Signed == true)
                                            {
                                                string isBlocked = "";
                                                var OBJ = new Options { CreateIfMissing = true };
                                                try
                                                {
                                                    using (var db = new DB(OBJ, @"root\oblock"))
                                                    {
                                                        isBlocked = db.Get(root.Signature);
                                                        db.Close();
                                                    }
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        using (var db = new DB(OBJ, @"root\oblock2"))
                                                        {
                                                            isBlocked = db.Get(root.Signature);
                                                            db.Close();
                                                        }
                                                        Directory.Move(@"root\oblock2", @"root\oblock");
                                                    }
                                                    catch
                                                    {
                                                        try { Directory.Delete(@"root\oblock", true); }
                                                        catch { }
                                                    }

                                                }


                                                if (isBlocked != "true")
                                                {
                                                    bool find = false;

                                                    if (filter != "")
                                                    {

                                                        if (filter.StartsWith("#"))
                                                        {
                                                            find = root.Keyword.ContainsKey(Root.GetPublicAddressByKeyword(filter.Substring(1)));
                                                        }
                                                        else
                                                        {

                                                            find = root.Keyword.ContainsKey(filter);


                                                        }
                                                    }
                                                    else { find = true; }

                                                    isobject = OBJState.GetObjectByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:18332");
                                                    if (isobject.URN != null && find == true)
                                                    {
                                                        isobject.TransactionId = s;
                                                        foundobjects.Add(isobject);
                                                        try { Directory.Delete(@"root\" + s, true); } catch { }

                                                        using (var db = new DB(SUP, @"root\found"))
                                                        {
                                                            db.Put("found!" + root.BlockDate.ToString("yyyyMMddHHmmss") + "!" + root.SignedBy, "1");
                                                        }




                                                    }


                                                }
                                                else { try { System.IO.Directory.Delete(@"root\" + s, true); } catch { } }

                                            }
                                            else
                                            {

                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            string error = ex.Message;
                                        }
                                    }

                                }
                            }
                            catch
                            {

                            }
                        }

                        if (btcActive)
                        {
                            newtransactions = new List<string>();

                            try
                            {
                                rpcClient = new RPCClient(credentials, new Uri(@"http://127.0.0.1:8332"), Network.Main);
                                flattransactions = rpcClient.SendCommand("getrawmempool").ResultString;
                                flattransactions = flattransactions.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
                                newtransactions = flattransactions.Split(',').ToList();

                                if (BTCMemPool.Count == 0)
                                {
                                    BTCMemPool = newtransactions;
                                }
                                else
                                {
                                    differenceQuery =
                                    (List<string>)newtransactions.Except(BTCMemPool).ToList(); ;

                                    BTCMemPool = newtransactions;

                                    foreach (var s in differenceQuery)
                                    {
                                        try
                                        {

                                            Root root = Root.GetRootByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:8332", "0");
                                            if (root.Signed == true)
                                            {
                                                string isBlocked = "";
                                                var OBJ = new Options { CreateIfMissing = true };
                                                try
                                                {
                                                    using (var db = new DB(OBJ, @"root\oblock"))
                                                    {
                                                        isBlocked = db.Get(root.Signature);
                                                        db.Close();
                                                    }
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        using (var db = new DB(OBJ, @"root\oblock2"))
                                                        {
                                                            isBlocked = db.Get(root.Signature);
                                                            db.Close();
                                                        }
                                                        Directory.Move(@"root\oblock2", @"root\oblock");
                                                    }
                                                    catch
                                                    {
                                                        try { Directory.Delete(@"root\oblock", true); }
                                                        catch { }
                                                    }

                                                }


                                                if (isBlocked != "true")
                                                {
                                                    bool find = false;

                                                    if (filter != "")
                                                    {

                                                        if (filter.StartsWith("#"))
                                                        {
                                                            find = root.Keyword.ContainsKey(Root.GetPublicAddressByKeyword(filter.Substring(1)));
                                                        }
                                                        else
                                                        {

                                                            find = root.Keyword.ContainsKey(filter);


                                                        }
                                                    }
                                                    else { find = true; }

                                                    isobject = OBJState.GetObjectByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:8332", "0");
                                                    if (isobject.URN != null && find == true)
                                                    {
                                                        isobject.TransactionId = s;
                                                        foundobjects.Add(isobject);
                                                        try { Directory.Delete(@"root\" + s, true); } catch { }


                                                        using (var db = new DB(SUP, @"root\found"))
                                                        {
                                                            db.Put("found!" + root.BlockDate.ToString("yyyyMMddHHmmss") + "!" + root.SignedBy, "1");
                                                        }


                                                    }


                                                }
                                                else { try { System.IO.Directory.Delete(@"root\" + s, true); } catch { } }

                                            }
                                            else { }

                                        }
                                        catch { }

                                    }

                                }
                            }
                            catch { }
                        }

                        if (mzcActive)
                        {
                            newtransactions = new List<string>();

                            try
                            {
                                rpcClient = new RPCClient(credentials, new Uri(@"http://127.0.0.1:12832"), Network.Main);
                                flattransactions = rpcClient.SendCommand("getrawmempool").ResultString;
                                flattransactions = flattransactions.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
                                newtransactions = flattransactions.Split(',').ToList();
                                if (MZCMemPool.Count == 0)
                                {
                                    MZCMemPool = newtransactions;
                                }
                                else
                                {
                                    differenceQuery =
                                    (List<string>)newtransactions.Except(MZCMemPool).ToList(); ;

                                    MZCMemPool = newtransactions;

                                    foreach (var s in differenceQuery)
                                    {
                                        try
                                        {

                                            Root root = Root.GetRootByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:12832", "50");
                                            if (root.Signed == true)
                                            {
                                                string isBlocked = "";
                                                var OBJ = new Options { CreateIfMissing = true };
                                                try
                                                {
                                                    using (var db = new DB(OBJ, @"root\oblock"))
                                                    {
                                                        isBlocked = db.Get(root.Signature);
                                                        db.Close();
                                                    }
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        using (var db = new DB(OBJ, @"root\oblock2"))
                                                        {
                                                            isBlocked = db.Get(root.Signature);
                                                            db.Close();
                                                        }
                                                        Directory.Move(@"root\oblock2", @"root\oblock");
                                                    }
                                                    catch
                                                    {
                                                        try { Directory.Delete(@"root\oblock", true); }
                                                        catch { }
                                                    }

                                                }


                                                if (isBlocked != "true")
                                                {
                                                    bool find = false;

                                                    if (filter != "")
                                                    {

                                                        if (filter.StartsWith("#"))
                                                        {
                                                            find = root.Keyword.ContainsKey(Root.GetPublicAddressByKeyword(filter.Substring(1)));
                                                        }
                                                        else
                                                        {

                                                            find = root.Keyword.ContainsKey(filter);


                                                        }
                                                    }
                                                    else { find = true; }

                                                    isobject = OBJState.GetObjectByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:12832", "50");
                                                    if (isobject.URN != null && find == true)
                                                    {

                                                        isobject.TransactionId = s;
                                                        foundobjects.Add(isobject);
                                                        try { Directory.Delete(@"root\" + s, true); } catch { }
                                                        using (var db = new DB(SUP, @"root\found"))
                                                        {
                                                            db.Put("found!" + root.BlockDate.ToString("yyyyMMddHHmmss") + "!" + root.SignedBy, "1");
                                                        }



                                                    }


                                                }
                                                else { try { System.IO.Directory.Delete(@"root\" + s, true); } catch { } }

                                            }
                                            else { }

                                        }
                                        catch { }

                                    }

                                }
                            }
                            catch { }
                        }

                        if (ltcActive)
                        {
                            newtransactions = new List<string>();

                            try
                            {
                                rpcClient = new RPCClient(credentials, new Uri(@"http://127.0.0.1:9332"), Network.Main);
                                flattransactions = rpcClient.SendCommand("getrawmempool").ResultString;
                                flattransactions = flattransactions.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
                                newtransactions = flattransactions.Split(',').ToList();
                                if (LTCMemPool.Count == 0)
                                {
                                    LTCMemPool = newtransactions;
                                }
                                else
                                {
                                    differenceQuery =
                                    (List<string>)newtransactions.Except(LTCMemPool).ToList(); ;

                                    LTCMemPool = newtransactions;

                                    foreach (var s in differenceQuery)
                                    {
                                        try
                                        {

                                            Root root = Root.GetRootByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:9332", "48");
                                            if (root.Signed == true)
                                            {
                                                string isBlocked = "";
                                                var OBJ = new Options { CreateIfMissing = true };
                                                try
                                                {
                                                    using (var db = new DB(OBJ, @"root\oblock"))
                                                    {
                                                        isBlocked = db.Get(root.Signature);
                                                        db.Close();
                                                    }
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        using (var db = new DB(OBJ, @"root\oblock2"))
                                                        {
                                                            isBlocked = db.Get(root.Signature);
                                                            db.Close();
                                                        }
                                                        Directory.Move(@"root\oblock2", @"root\oblock");
                                                    }
                                                    catch
                                                    {
                                                        try { Directory.Delete(@"root\oblock", true); }
                                                        catch { }
                                                    }

                                                }

                                                if (isBlocked != "true")
                                                {
                                                    bool find = false;

                                                    if (filter != "")
                                                    {

                                                        if (filter.StartsWith("#"))
                                                        {
                                                            find = root.Keyword.ContainsKey(Root.GetPublicAddressByKeyword(filter.Substring(1)));
                                                        }
                                                        else
                                                        {

                                                            find = root.Keyword.ContainsKey(filter);


                                                        }
                                                    }
                                                    else { find = true; }

                                                    isobject = OBJState.GetObjectByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:9332", "48");
                                                    if (isobject.URN != null && find == true)
                                                    {
                                                        isobject.TransactionId = s;
                                                        foundobjects.Add(isobject);
                                                        try { Directory.Delete(@"root\" + s, true); } catch { }

                                                        using (var db = new DB(SUP, @"root\found"))
                                                        {
                                                            db.Put("found!" + root.BlockDate.ToString("yyyyMMddHHmmss") + "!" + root.SignedBy, "1");
                                                        }


                                                    }


                                                }
                                                else { try { System.IO.Directory.Delete(@"root\" + s, true); } catch { } }

                                            }
                                            else { }

                                        }
                                        catch { }

                                    }

                                }
                            }
                            catch { }
                        }

                        if (dogActive)
                        {
                            newtransactions = new List<string>();

                            try
                            {
                                rpcClient = new RPCClient(credentials, new Uri(@"http://127.0.0.1:22555"), Network.Main);
                                flattransactions = rpcClient.SendCommand("getrawmempool").ResultString;
                                flattransactions = flattransactions.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
                                newtransactions = flattransactions.Split(',').ToList();

                                if (DOGMemPool.Count == 0)
                                {
                                    DOGMemPool = newtransactions;
                                }
                                else
                                {
                                    differenceQuery =
                                    (List<string>)newtransactions.Except(DOGMemPool).ToList(); ;

                                    DOGMemPool = newtransactions;

                                    foreach (var s in differenceQuery)
                                    {
                                        try
                                        {

                                            Root root = Root.GetRootByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:22555", "30");
                                            if (root.Signed == true)
                                            {

                                                string isBlocked = "";
                                                var OBJ = new Options { CreateIfMissing = true };
                                                try
                                                {
                                                    using (var db = new DB(OBJ, @"root\oblock"))
                                                    {
                                                        isBlocked = db.Get(root.Signature);
                                                        db.Close();
                                                    }
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        using (var db = new DB(OBJ, @"root\oblock2"))
                                                        {
                                                            isBlocked = db.Get(root.Signature);
                                                        }
                                                        Directory.Move(@"root\oblock2", @"root\oblock");
                                                    }
                                                    catch
                                                    {
                                                        try { Directory.Delete(@"root\oblock", true); }
                                                        catch { }
                                                    }

                                                }

                                                if (isBlocked != "true")
                                                {
                                                    bool find = false;

                                                    if (filter.Length > 0)
                                                    {

                                                        if (filter.StartsWith("#"))
                                                        {
                                                            find = root.Keyword.ContainsKey(Root.GetPublicAddressByKeyword(filter.Substring(1)));
                                                        }
                                                        else
                                                        {

                                                            find = root.Keyword.ContainsKey(filter);


                                                        }
                                                    }
                                                    else { find = true; }

                                                    isobject = OBJState.GetObjectByTransactionId(s, "good-user", "better-password", @"http://127.0.0.1:22555", "30");
                                                    if (isobject.URN != null && find == true)
                                                    {
                                                        isobject.TransactionId = s;
                                                        foundobjects.Add(isobject);
                                                        try { Directory.Delete(@"root\" + s, true); } catch { }

                                                        using (var db = new DB(SUP, @"root\found"))
                                                        {
                                                            db.Put("found!" + root.BlockDate.ToString("yyyyMMddHHmmss") + "!" + root.SignedBy, "1");
                                                        }



                                                    }


                                                }
                                                else { try { System.IO.Directory.Delete(@"root\" + s, true); } catch { } }

                                            }
                                            else { }

                                        }
                                        catch { }

                                    }

                                }
                            }
                            catch { }
                        }




                        if (foundobjects.Count > 0)
                        {

                            this.Invoke((MethodInvoker)delegate
                            {
                                AddToSearchResults(foundobjects);
                            });

                        }


                        this.Invoke((MethodInvoker)delegate
                        {
                            tmrSearchMemoryPool.Start();
                        });

                    });



                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    this.Invoke((MethodInvoker)delegate
                    {
                        tmrSearchMemoryPool.Start();
                    });

                }



            }
        }


        private void RefreshSupMessages()
        {
            // Clear controls if no messages have been displayed yet
            if (numMessagesDisplayed == 0)
            {
                supFlow.Controls.Clear();
            }

            Dictionary<string, string[]> profileAddress = new Dictionary<string, string[]> { };
            OBJState objstate = OBJState.GetObjectByAddress(profileURN.Links[0].LinkData.ToString(), "good-user", "better-password", "http://127.0.0.1:18332");
            int rownum = 1;

            var SUP = new Options { CreateIfMissing = true };

            using (var db = new DB(SUP, @"root\sup"))
            {
                string lastKey = db.Get("lastkey!" + profileURN.Links[0].LinkData.ToString());
                if (lastKey == null) { lastKey = profileURN.Links[0].LinkData.ToString(); }
                LevelDB.Iterator it = db.CreateIterator();
                for (
                   it.Seek(lastKey);
                   it.IsValid() && it.KeyAsString().StartsWith(profileURN.Links[0].LinkData.ToString()) && rownum <= numMessagesDisplayed + 10; // Only display next 10 messages
                    it.Prev()
                 )
                {
                    // Display only if rownum > numMessagesDisplayed to skip already displayed messages
                    if (rownum > numMessagesDisplayed)
                    {
                        string process = it.ValueAsString();

                        List<string> supMessagePacket = JsonConvert.DeserializeObject<List<string>>(process);

                        string message = System.IO.File.ReadAllText(@"root/" + supMessagePacket[1] + @"/MSG").Replace("@" + profileURN.Links[0].LinkData.ToString(), "");

                        string fromAddress = supMessagePacket[0];
                        string imagelocation = "";


                        if (!profileAddress.ContainsKey(fromAddress))
                        {

                            PROState profile = PROState.GetProfileByAddress(fromAddress, "good-user", "better-password", "http://127.0.0.1:18332");

                            if (profile.URN != null)
                            {
                                fromAddress = TruncateAddress(profile.URN);
                                imagelocation = profile.Image;


                                if (imagelocation.StartsWith("BTC:") || imagelocation.StartsWith("MZC:"))
                                {
                                    if (imagelocation.Length > 64)
                                    {
                                        string transid = imagelocation.Substring(4, 64);
                                        imagelocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + imagelocation.Replace("BTC:", "").Replace("MZC:", "").Replace(@"/", @"\");


                                        if (!System.IO.Directory.Exists("root/" + transid))
                                        {
                                            if (profile.Image.StartsWith("BTC:"))
                                            {
                                                Root.GetRootByTransactionId(transid, "good-user", "better-password", "http://127.0.0.1:8332", "0");
                                            }
                                            else
                                            {
                                                Root.GetRootByTransactionId(transid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");

                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (imagelocation.Length > 64)
                                    {
                                        string transid = imagelocation.Substring(0, 64);
                                        imagelocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + imagelocation.Replace(@" / ", @"\");
                                        if (!System.IO.Directory.Exists("root/" + transid))
                                        {
                                            Root.GetRootByTransactionId(transid, "good-user", "better-password", "http://127.0.0.1:18332");

                                        }
                                    }


                                    if (imagelocation.StartsWith("IPFS:"))
                                    {

                                        string transid = imagelocation.Substring(5, 46);
                                        if (!System.IO.Directory.Exists("ipfs/" + transid))
                                        {

                                            string isLoading;
                                            using (var db2 = new DB(SUP, @"ipfs"))
                                            {
                                                isLoading = db2.Get(transid);

                                            }

                                            if (isLoading != "loading")
                                            {
                                                using (var db2 = new DB(SUP, @"ipfs"))
                                                {

                                                    db2.Put(transid, "loading");

                                                }

                                                Task ipfsTask = Task.Run(() =>
                                                {
                                                    Process process2 = new Process();
                                                    process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                                    process2.StartInfo.Arguments = "get " + transid + @"-p -o ipfs\" + transid;
                                                    process2.Start();
                                                    process2.WaitForExit();

                                                    if (System.IO.File.Exists("ipfs/" + transid))
                                                    {
                                                        System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                                        System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                                        string fileName = objstate.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                        if (fileName == "")
                                                        {
                                                            fileName = "artifact";

                                                        }
                                                        else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                                        System.IO.File.Move("ipfs/" + transid + "_tmp", @"ipfs/" + transid + @"/" + fileName);
                                                    }


                                                    //attempt to pin fails silently if daemon is not running
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


                                                    using (var db2 = new DB(SUP, @"ipfs"))
                                                    {
                                                        db2.Delete(transid);

                                                    }
                                                });
                                            }

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

                        CreateRow(imagelocation, fromAddress, supMessagePacket[0], DateTime.ParseExact(tstamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture), message, bgcolor, supFlow);

                    }
                    rownum++;
                }
                it.Dispose();
            }

            // Update number of messages displayed
            numMessagesDisplayed += 10;

            supFlow.ResumeLayout();

        }


        private void RefreshPrivateSupMessages()
        {
           
            // Clear controls if no messages have been displayed yet
            if (numPrivateMessagesDisplayed == 0)
            {
                splitContainer1.Panel2.Controls.Clear();
                supPrivateFlow.Controls.Clear();
                supPrivateFlow.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Add(supPrivateFlow);
            }

            Dictionary<string, string[]> profileAddress = new Dictionary<string, string[]> { };
            OBJState objstate = OBJState.GetObjectByAddress(profileURN.Links[0].LinkData.ToString(), "good-user", "better-password", "http://127.0.0.1:18332");
            int rownum = 1;

            var SUP = new Options { CreateIfMissing = true };

            using (var db = new DB(SUP, @"root\sec"))
            {
                string lastKey = db.Get("lastkey!" + profileURN.Links[0].LinkData.ToString());
                if (lastKey == null) { lastKey = profileURN.Links[0].LinkData.ToString(); }
                LevelDB.Iterator it = db.CreateIterator();
                for (
                   it.Seek(lastKey);
                   it.IsValid() && it.KeyAsString().StartsWith(profileURN.Links[0].LinkData.ToString()) && rownum <= numPrivateMessagesDisplayed + 10; // Only display next 10 messages
                    it.Prev()
                 )
                {
                    if (rownum > numPrivateMessagesDisplayed)
                    {
                        string process = it.ValueAsString();

                        List<string> supMessagePacket = JsonConvert.DeserializeObject<List<string>>(process);
                        Root root = Root.GetRootByTransactionId(supMessagePacket[1], "good-user", "better-password", "http://127.0.0.1:18332");
                        byte[] result = Root.GetRootBytesByFile(new string[] { @"root/" + supMessagePacket[1] + @"/SEC" });
                        result = Root.DecryptRootBytes("good-user", "better-password", "http://127.0.0.1:18332", profileURN.Links[0].LinkData.ToString(), result);
                        root = Root.GetRootByTransactionId(supMessagePacket[1], null, null, null, "111", result);
                                         

                        string message = string.Join(" ", root.Message);

                        string fromAddress = supMessagePacket[0];
                        string imagelocation = "";


                        if (!profileAddress.ContainsKey(fromAddress))
                        {

                            PROState profile = PROState.GetProfileByAddress(fromAddress, "good-user", "better-password", "http://127.0.0.1:18332");

                            if (profile.URN != null)
                            {
                                fromAddress = TruncateAddress(profile.URN);
                                imagelocation = profile.Image;


                                if (imagelocation.StartsWith("BTC:") || imagelocation.StartsWith("MZC:"))
                                {
                                    if (imagelocation.Length > 64)
                                    {
                                        string transid = imagelocation.Substring(4, 64);
                                        imagelocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + imagelocation.Replace("BTC:", "").Replace("MZC:", "").Replace(@"/", @"\");


                                        if (!System.IO.Directory.Exists("root/" + transid))
                                        {
                                            if (profile.Image.StartsWith("BTC:"))
                                            {
                                                Root.GetRootByTransactionId(transid, "good-user", "better-password", "http://127.0.0.1:8332", "0");
                                            }
                                            else
                                            {
                                                Root.GetRootByTransactionId(transid, "good-user", "better-password", @"http://127.0.0.1:12832", "50");

                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (imagelocation.Length > 64)
                                    {
                                        string transid = imagelocation.Substring(0, 64);
                                        imagelocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + imagelocation.Replace(@" / ", @"\");
                                        if (!System.IO.Directory.Exists("root/" + transid))
                                        {
                                            Root.GetRootByTransactionId(transid, "good-user", "better-password", "http://127.0.0.1:18332");

                                        }
                                    }


                                    if (imagelocation.StartsWith("IPFS:"))
                                    {

                                        string transid = imagelocation.Substring(5, 46);
                                        if (!System.IO.Directory.Exists("ipfs/" + transid))
                                        {

                                            string isLoading;
                                            using (var db2 = new DB(SUP, @"ipfs"))
                                            {
                                                isLoading = db2.Get(transid);

                                            }

                                            if (isLoading != "loading")
                                            {
                                                using (var db2 = new DB(SUP, @"ipfs"))
                                                {

                                                    db2.Put(transid, "loading");

                                                }

                                                Task ipfsTask = Task.Run(() =>
                                                {
                                                    Process process2 = new Process();
                                                    process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                                    process2.StartInfo.Arguments = "get " + transid + @"-p -o ipfs\" + transid;
                                                    process2.Start();
                                                    process2.WaitForExit();

                                                    if (System.IO.File.Exists("ipfs/" + transid))
                                                    {
                                                        System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                                        System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                                        string fileName = objstate.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                                        if (fileName == "")
                                                        {
                                                            fileName = "artifact";

                                                        }
                                                        else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                                        System.IO.File.Move("ipfs/" + transid + "_tmp", @"ipfs/" + transid + @"/" + fileName);
                                                    }


                                                    //attempt to pin fails silently if daemon is not running
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


                                                    using (var db2 = new DB(SUP, @"ipfs"))
                                                    {
                                                        db2.Delete(transid);

                                                    }
                                                });
                                            }

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

                        CreateRow(imagelocation, fromAddress, supMessagePacket[0], DateTime.ParseExact(tstamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture), message, bgcolor, supPrivateFlow);

                    }
                    rownum++;
                }
                it.Dispose();
            }

            // Update number of messages displayed
            numPrivateMessagesDisplayed += 10;

            supPrivateFlow.ResumeLayout();

        }


        void CreateRow(string imageLocation, string ownerName, string ownerId, DateTime timestamp, string messageText, System.Drawing.Color bgcolor, FlowLayoutPanel layoutPanel)
        {

            // Create a table layout panel for each row
            TableLayoutPanel row = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 3,
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



            TableLayoutPanel msg = new TableLayoutPanel
            {
                RowCount = 3,
                ColumnCount = 1,
                Dock = DockStyle.Top,
                BackColor = bgcolor,
                AutoSize = true,
                Margin = new System.Windows.Forms.Padding(0),
                Padding = new System.Windows.Forms.Padding(0)
            };

            msg.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            layoutPanel.Controls.Add(msg);

            // Create a Label with the message text
            Label tpadding = new Label
            {
                AutoSize = true,
                Text = " ",
                Margin = new System.Windows.Forms.Padding(0)
            };
            msg.Controls.Add(tpadding, 0, 0);

            // Create a Label with the message text
            Label message = new Label
            {
                AutoSize = true,
                Text = messageText,
                MinimumSize = new Size(100,50) ,
                Margin = new System.Windows.Forms.Padding(0),
                TextAlign = System.Drawing.ContentAlignment.TopLeft
            };
            msg.Controls.Add(message, 1, 0);


            // Create a Label with the message text
            Label bpadding = new Label
            {
                AutoSize = true,
                Text = " ",
                Margin = new System.Windows.Forms.Padding(0)
            };
            msg.Controls.Add(bpadding, 2, 0);


            // Add padding row at the end
            Label bottomPadding2 = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                Text = " ",
                BackColor = Color.Black,
                ForeColor = Color.White,
                Margin = new System.Windows.Forms.Padding(0)
            };
            msg.Controls.Add(bottomPadding2, 3, 0);

            // Add padding row at the end
            Label bottomPadding3 = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                Text = " ",
                BackColor = Color.Black,
                ForeColor = Color.White,
                Margin = new System.Windows.Forms.Padding(0)
            };
            msg.Controls.Add(bottomPadding3, 4, 0);


        }


        void Owner_LinkClicked(string ownerId)
        {

            new ObjectBrowser(ownerId).Show();
        }


        string TruncateAddress(string input)
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


        private void btnPublicMessage_Click(object sender, EventArgs e)
        {
            RefreshSupMessages();
        }


        private void splitContainer1_DoubleClick(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
            {
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {

                splitContainer1.Panel2Collapsed = true;
            }
        }

        private void profileURN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new ProfileMint(profileURN.Links[0].LinkData.ToString()).Show();
        }

        private void btnPrivateMessage_Click(object sender, EventArgs e)
        {
         

            RefreshPrivateSupMessages();

            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

            //richTextBox1 = new RichTextBox();
            //richTextBox1.Height = 180; // set a fixed height
            //richTextBox1.BackColor = Color.Black;
            //richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            //richTextBox1.ForeColor = Color.White;
            //supFlow.Controls.Add(richTextBox1);
            //richTextBox1.Width = supFlow.Width - 42;

            //supFlow.SizeChanged += new EventHandler(flowLayoutPanel1_SizeChanged);
        }
    }
}