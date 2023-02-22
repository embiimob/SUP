﻿using LevelDB;
using SUP.P2FK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NBitcoin;
using System.Threading.Tasks;

namespace SUP
{

    public partial class ObjectBrowser : Form
    {
        Button lastClickedButton;
        private readonly string _objectaddress;
        private List<String> SearchHistory = new List<String>();
        private int SearchId = 0;
        private HashSet<string> loadedObjects = new HashSet<string>();
        private bool isBuilding = false;
        private IEnumerable<string> BTCMempool = new List<string>();
        private IEnumerable<string> BTCTMempool = new List<string>();
        private IEnumerable<string> MZCMempool = new List<string>();
        public ObjectBrowser(string objectaddress)
        {
            InitializeComponent();
            if (objectaddress != null)
            {
                _objectaddress = objectaddress;
            }
            else
            { _objectaddress = ""; }

        }

        private void GetObjectsbyAddress(string address)
        {

            string profileCheck = address;
            PROState searchprofile = PROState.GetProfileByAddress(address, txtLogin.Text, txtPassword.Text, txtUrl.Text);

            if (searchprofile.URN != null)
            {
                linkLabel1.Text = searchprofile.URN;
                linkLabel1.LinkColor = System.Drawing.SystemColors.Highlight;
            }
            else
            {


                searchprofile = PROState.GetProfileByURN(address, txtLogin.Text, txtPassword.Text, txtUrl.Text);

                if (searchprofile.URN != null)
                {
                    linkLabel1.Text = TruncateAddress(searchprofile.Creators.First());
                    linkLabel1.LinkColor = System.Drawing.SystemColors.Highlight;
                    profileCheck = searchprofile.Creators.First();
                }
                else
                {
                    linkLabel1.Text = "anon";
                    linkLabel1.LinkColor = System.Drawing.SystemColors.GradientActiveCaption;

                }
            }



            flowLayoutPanel1.SuspendLayout();
            List<OBJState> createdObjects = new List<OBJState>();


            if (btnCreated.BackColor == Color.Yellow)
            {
                createdObjects = OBJState.GetObjectsCreatedByAddress(profileCheck, txtLogin.Text, txtPassword.Text, txtUrl.Text, txtVersionByte.Text, int.Parse(txtLast.Text), int.Parse(txtQty.Text));
            }
            else if (btnOwned.BackColor == Color.Yellow)
            {
                createdObjects = OBJState.GetObjectsOwnedByAddress(profileCheck, txtLogin.Text, txtPassword.Text, txtUrl.Text, txtVersionByte.Text, int.Parse(txtLast.Text), int.Parse(txtQty.Text));
            }
            else
            {
                if (txtSearchAddress.Text == "") { createdObjects = OBJState.GetFoundObjects( txtLogin.Text, txtPassword.Text, txtUrl.Text, txtVersionByte.Text, int.Parse(txtLast.Text), int.Parse(txtQty.Text)); }
                else
                {
                    createdObjects = OBJState.GetObjectsByAddress(profileCheck, txtLogin.Text, txtPassword.Text, txtUrl.Text, txtVersionByte.Text, int.Parse(txtLast.Text), int.Parse(txtQty.Text));

                }
            }





            foreach (OBJState objstate in createdObjects)
            {
                if (objstate.Owners != null)
                {

                    FoundObjectControl foundObject = new FoundObjectControl();

                    switch (objstate.Image.ToUpper().Substring(0, 4))
                    {
                        case "BTC:":
                            string transid = objstate.Image.Substring(4, 64);
                            if (!System.IO.Directory.Exists("root/" + transid))
                            {
                                Root root = Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:8332", "0");
                            }
                            foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("BTC:", @"root/");
                            break;
                        case "MZC:":
                            transid = objstate.Image.Substring(4, 64);
                            if (!System.IO.Directory.Exists("root/" + transid))
                            {
                                Root root = Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:12832", "50");
                            }
                            foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("MZC:", @"root/");
                            break;
                        case "IPFS":
                            transid = objstate.Image.Substring(5, 46);

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
                                                Arguments = "pin add " + transid,
                                                UseShellExecute = false,
                                                CreateNoWindow = true
                                            }
                                        };
                                        process3.Start();
                                    }
                                }
                            }
                            if (objstate.Image.Length == 51)
                            { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/") + @"/artifact"; }
                            else { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/"); }

                            break;
                        case "HTTP":
                            foundObject.ObjectImage.ImageLocation = objstate.Image;
                            break;


                        default:
                            transid = objstate.Image.Substring(0, 64);
                            if (!System.IO.Directory.Exists("root/" + transid))
                            {
                                Root root = Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:18332");
                            }
                            foundObject.ObjectImage.ImageLocation = @"root/" + objstate.Image;

                            break;
                    }
                    foundObject.ObjectName.Text = objstate.Name;
                    foundObject.ObjectDescription.Text = objstate.Description;
                    foundObject.ObjectAddress.Text = objstate.Creators.First().Key;
                    foundObject.ObjectQty.Text = objstate.Owners.Values.Sum().ToString() + "x";

                    foreach (KeyValuePair<string, DateTime> creator in objstate.Creators.Skip(1))
                    {

                        if (creator.Value.Year > 1)
                        {
                            PROState profile = PROState.GetProfileByAddress(creator.Key, txtLogin.Text, txtPassword.Text, txtUrl.Text);

                            if (profile.URN != null && foundObject.ObjectCreators.Text == "")
                            {


                                foundObject.ObjectCreators.Text = TruncateAddress(profile.URN);
                                foundObject.ObjectCreators.Links.Add(0, creator.Key.Length, creator.Key);
                            }
                            else
                            {


                                if (profile.URN != null && foundObject.ObjectCreators2.Text == "")
                                {
                                    foundObject.ObjectCreators2.Text = TruncateAddress(profile.URN);
                                    foundObject.ObjectCreators2.Links.Add(0, creator.Key.Length, creator.Key);
                                }

                            }
                        }
                        else
                        {

                            if (foundObject.ObjectCreators.Text == "")
                            {


                                foundObject.ObjectCreators.Text = TruncateAddress(creator.Key);
                                foundObject.ObjectCreators.Links.Add(0, creator.Key.Length, creator.Key);
                            }
                            else
                            {


                                if (foundObject.ObjectCreators2.Text == "")
                                {
                                    foundObject.ObjectCreators2.Text = TruncateAddress(creator.Key);
                                    foundObject.ObjectCreators2.Links.Add(0, creator.Key.Length, creator.Key);
                                }

                            }

                        }

                    }
                    foundObject.ObjectId.Text = objstate.Id.ToString();
                    

                    if (!loadedObjects.Contains(foundObject.ObjectAddress.Text))
                    {
                        txtLast.Text = objstate.Id.ToString();
                        OBJState isOfficial = OBJState.GetObjectByURN(objstate.URN, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:18332");
                        if (isOfficial.URN != null)
                        {
                            if (isOfficial.Creators.First().Key == foundObject.ObjectAddress.Text)
                            {
                                foundObject.lblOfficial.Visible = true;
                                foundObject.lblOfficial.Text = TruncateAddress(isOfficial.URN);
                            }
                            else
                            {
                                foundObject.txtOfficialURN.Text = isOfficial.Creators.First().Key;
                                foundObject.btnOfficial.Visible = true;

                            }
                        }


      
                        flowLayoutPanel1.Controls.Add(foundObject);
                    }
                    loadedObjects.Add(foundObject.ObjectAddress.Text);


                }
            }
            flowLayoutPanel1.ResumeLayout();


        }

        private void GetObjectsByKeyword(string keyword)
        {

            List<string> keywords = keyword.Split(',').ToList();

            flowLayoutPanel1.SuspendLayout();


            List<OBJState> createdObjects = OBJState.GetObjectsByKeyword(keywords, txtLogin.Text, txtPassword.Text, txtUrl.Text, txtVersionByte.Text, int.Parse(txtLast.Text), int.Parse(txtQty.Text));


            foreach (OBJState objstate in createdObjects)
            {
                if (objstate.Owners != null)
                {

                    FoundObjectControl foundObject = new FoundObjectControl();

                    switch (objstate.Image.ToUpper().Substring(0, 4))
                    {
                        case "BTC:":
                            string transid = objstate.Image.Substring(4, 64);
                            if (!System.IO.Directory.Exists("root/" + transid))
                            {
                                Root root = Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:8332", "0");
                            }
                            foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("BTC:", @"root/");
                            break;
                        case "MZC:":
                            transid = objstate.Image.Substring(4, 64);
                            if (!System.IO.Directory.Exists("root/" + transid))
                            {
                                Root root = Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:12832", "50");
                            }
                            foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("MZC:", @"root/");
                            break;
                        case "IPFS":
                            transid = objstate.Image.Substring(5, 46);

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
                                                Arguments = "pin add " + transid,
                                                UseShellExecute = false,
                                                CreateNoWindow = true
                                            }
                                        };
                                        process3.Start();
                                    }
                                }
                            }
                            if (objstate.Image.Length == 51)
                            { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/") + @"/artifact"; }
                            else { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/"); }

                            break;
                        case "HTTP":
                            foundObject.ObjectImage.ImageLocation = objstate.Image;
                            break;


                        default:
                            transid = objstate.Image.Substring(0, 64);
                            if (!System.IO.Directory.Exists("root/" + transid))
                            {
                                Root root = Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:18332");
                            }
                            foundObject.ObjectImage.ImageLocation = @"root/" + objstate.Image;

                            break;
                    }
                    foundObject.ObjectName.Text = objstate.Name;
                    foundObject.ObjectDescription.Text = objstate.Description;
                    foundObject.ObjectAddress.Text = objstate.Creators.First().Key;
                    foundObject.ObjectQty.Text = objstate.Owners.Values.Sum().ToString() + "x";

                    foreach (KeyValuePair<string, DateTime> creator in objstate.Creators.Skip(1))
                    {

                        if (creator.Value.Year > 1)
                        {
                            PROState profile = PROState.GetProfileByAddress(creator.Key, txtLogin.Text, txtPassword.Text, txtUrl.Text);

                            if (profile.URN != null && foundObject.ObjectCreators.Text == "")
                            {


                                foundObject.ObjectCreators.Text = TruncateAddress(profile.URN);
                                foundObject.ObjectCreators.Links.Add(0, creator.Key.Length, creator.Key);
                            }
                            else
                            {


                                if (profile.URN != null && foundObject.ObjectCreators2.Text == "")
                                {
                                    foundObject.ObjectCreators2.Text = TruncateAddress(profile.URN);
                                    foundObject.ObjectCreators2.Links.Add(0, creator.Key.Length, creator.Key);
                                }

                            }
                        }
                        else
                        {

                            if (foundObject.ObjectCreators.Text == "")
                            {


                                foundObject.ObjectCreators.Text = TruncateAddress(creator.Key);
                                foundObject.ObjectCreators.Links.Add(0, creator.Key.Length, creator.Key);
                            }
                            else
                            {


                                if (foundObject.ObjectCreators2.Text == "")
                                {
                                    foundObject.ObjectCreators2.Text = TruncateAddress(creator.Key);
                                    foundObject.ObjectCreators2.Links.Add(0, creator.Key.Length, creator.Key);
                                }

                            }

                        }

                    }
                    foundObject.ObjectId.Text = objstate.Id.ToString();
                    if (!loadedObjects.Contains(foundObject.ObjectAddress.Text))
                    {
                        txtLast.Text = objstate.Id.ToString();
                        OBJState isOfficial = OBJState.GetObjectByURN(objstate.URN, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:18332");
                        if (isOfficial.URN != null)
                        {
                            if (isOfficial.Creators.First().Key == foundObject.ObjectAddress.Text)
                            {
                                foundObject.lblOfficial.Visible = true;
                                foundObject.lblOfficial.Text = TruncateAddress(isOfficial.URN);
                                
                            }
                            else
                            {
                                foundObject.txtOfficialURN.Text = isOfficial.Owners.First().Key;
                                foundObject.btnOfficial.Visible = true;

                            }
                        }

                        loadedObjects.Add(foundObject.ObjectAddress.Text);
                        flowLayoutPanel1.Controls.Add(foundObject);
                    }

                }
            }
            flowLayoutPanel1.ResumeLayout();


        }

        private void GetObjectsByURN(string urn)
        {

            flowLayoutPanel1.Controls.Clear();

            OBJState objstate = OBJState.GetObjectByURN(urn, txtLogin.Text, txtPassword.Text, txtUrl.Text);

            if (objstate.Owners != null)
            {

                FoundObjectControl foundObject = new FoundObjectControl();

                switch (objstate.Image.ToUpper().Substring(0, 4))
                {
                    case "BTC:":
                        string transid = objstate.Image.Substring(4, 64);
                        if (!System.IO.Directory.Exists("root/" + transid))
                        {
                            Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:8332", "0");
                        }
                        foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("BTC:", @"root/");
                        break;
                    case "MZC:":
                        transid = objstate.Image.Substring(4, 64);
                        if (!System.IO.Directory.Exists("root/" + transid))
                        {
                            Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:12832", "50");
                        }
                        foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("MZC:", @"root/");
                        break;
                    case "IPFS":
                        transid = objstate.Image.Substring(5, 46);

                        if (!System.IO.Directory.Exists("ipfs/" + transid))
                        {
                            Process process2 = new Process();
                            process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                            process2.StartInfo.Arguments = "get " + objstate.Image.Substring(5, 46) + @" -o ipfs\" + transid;
                            process2.StartInfo.UseShellExecute = false;
                            process2.StartInfo.CreateNoWindow = true;
                            process2.Start();
                            process2.WaitForExit();

                            if (System.IO.File.Exists("ipfs/" + transid))
                            {
                                System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                string fileName = objstate.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                System.IO.File.Move("ipfs/" + transid + "_tmp", @"ipfs/" + transid + @"/" + fileName);
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
                                            Arguments = "pin add " + transid,
                                            UseShellExecute = false,
                                            CreateNoWindow = true
                                        }
                                    };
                                    process3.Start();
                                }
                            }

                        }
                        if (objstate.Image.Length == 51)
                        { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/") + @"/artifact"; }
                        else { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/"); }

                        break;
                    case "HTTP":
                        foundObject.ObjectImage.ImageLocation = objstate.Image;
                        break;


                    default:
                        transid = objstate.Image.Substring(0, 64);
                        if (!System.IO.Directory.Exists("root/" + transid))
                        {
                            Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:18332");
                        }
                        foundObject.ObjectImage.ImageLocation = @"root/" + objstate.Image;

                        break;
                }
                foundObject.ObjectName.Text = objstate.Name;
                foundObject.ObjectDescription.Text = objstate.Description;
                foreach (KeyValuePair<string, DateTime> creator in objstate.Creators.Skip(1))
                {
                    if (creator.Value.Year > 1)
                    {
                        PROState profile = PROState.GetProfileByAddress(creator.Key, txtLogin.Text, txtPassword.Text, txtUrl.Text);

                        if (profile.URN != null && foundObject.ObjectCreators.Text == "")
                        {


                            foundObject.ObjectCreators.Text = TruncateAddress(profile.URN);
                            foundObject.ObjectCreators.Links.Add(0, creator.Key.Length, creator.Key);
                        }
                        else
                        {


                            if (profile.URN != null && foundObject.ObjectCreators2.Text == "")
                            {
                                foundObject.ObjectCreators2.Text = TruncateAddress(profile.URN);
                                foundObject.ObjectCreators2.Links.Add(0, creator.Key.Length, creator.Key);
                            }

                        }
                    }
                    else
                    {

                        if (foundObject.ObjectCreators.Text == "")
                        {


                            foundObject.ObjectCreators.Text = TruncateAddress(creator.Key);
                            foundObject.ObjectCreators.Links.Add(0, creator.Key.Length, creator.Key);
                        }
                        else
                        {


                            if (foundObject.ObjectCreators2.Text == "")
                            {
                                foundObject.ObjectCreators2.Text = TruncateAddress(creator.Key);
                                foundObject.ObjectCreators2.Links.Add(0, creator.Key.Length, creator.Key);
                            }

                        }

                    }


                }
                txtLast.Text = objstate.Id.ToString();
                foundObject.ObjectQty.Text = objstate.Owners.Values.Sum().ToString() + "x";
                foundObject.ObjectAddress.Text = objstate.Creators.First().Key;
                OBJState isOfficial = OBJState.GetObjectByURN(objstate.URN, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:18332");
                
                if (isOfficial.URN != null)
                {
                    if (isOfficial.Creators.ContainsKey(foundObject.ObjectAddress.Text))
                    {
                        foundObject.lblOfficial.Visible = true;
                        foundObject.lblOfficial.Text = TruncateAddress(isOfficial.URN);
                    }
                    else
                    {
                        foundObject.txtOfficialURN.Text = isOfficial.Owners.First().Key;
                        foundObject.btnOfficial.Visible = true;
                       
                    }
                }


                flowLayoutPanel1.Controls.Add(foundObject);
            }
        }

        private void GetObjectByFile(string filePath)
        {

            flowLayoutPanel1.Controls.Clear();

            OBJState objstate = OBJState.GetObjectByFile(filePath, txtLogin.Text, txtPassword.Text, txtUrl.Text);

            if (objstate.Owners != null)
            {

                FoundObjectControl foundObject = new FoundObjectControl();

                switch (objstate.Image.ToUpper().Substring(0, 4))
                {
                    case "BTC:":
                        string transid = objstate.Image.Substring(4, 64);
                        if (!System.IO.Directory.Exists("root/" + transid))
                        {
                            Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:8332", "0");
                        }
                        foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("BTC:", @"root/");
                        break;
                    case "MZC:":
                        transid = objstate.Image.Substring(4, 64);
                        if (!System.IO.Directory.Exists("root/" + transid))
                        {
                            Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:12832", "50");
                        }
                        foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("MZC:", @"root/");
                        break;
                    case "IPFS":
                        transid = objstate.Image.Substring(5, 46);

                        if (!System.IO.Directory.Exists("ipfs/" + transid))
                        {
                            Process process2 = new Process();
                            process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                            process2.StartInfo.Arguments = "get " + objstate.Image.Substring(5, 46) + @" -o ipfs\" + transid;
                            process2.StartInfo.UseShellExecute = false;
                            process2.StartInfo.CreateNoWindow = true;
                            process2.Start();
                            process2.WaitForExit();

                            if (System.IO.File.Exists("ipfs/" + transid))
                            {
                                System.IO.File.Move("ipfs/" + transid, "ipfs/" + transid + "_tmp");
                                System.IO.Directory.CreateDirectory("ipfs/" + transid);
                                string fileName = objstate.Image.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                System.IO.File.Move("ipfs/" + transid + "_tmp", @"ipfs/" + transid + @"/" + fileName);
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
                                            Arguments = "pin add " + transid,
                                            UseShellExecute = false,
                                            CreateNoWindow = true
                                        }
                                    };
                                    process3.Start();
                                }
                            }

                        }
                        if (objstate.Image.Length == 51)
                        { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/") + @"/artifact"; }
                        else { foundObject.ObjectImage.ImageLocation = objstate.Image.Replace("IPFS:", @"ipfs/"); }

                        break;
                    case "HTTP":
                        foundObject.ObjectImage.ImageLocation = objstate.Image;
                        break;


                    default:
                        transid = objstate.Image.Substring(0, 64);
                        if (!System.IO.Directory.Exists("root/" + transid))
                        {
                            Root.GetRootByTransactionId(transid, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:18332");
                        }
                        foundObject.ObjectImage.ImageLocation = @"root/" + objstate.Image;

                        break;
                }
                foundObject.ObjectName.Text = objstate.Name;
                foundObject.ObjectDescription.Text = objstate.Description;
                foreach (KeyValuePair<string, DateTime> creator in objstate.Creators.Skip(1))
                {


                    if (creator.Value.Year > 1)
                    {
                        PROState profile = PROState.GetProfileByAddress(creator.Key, txtLogin.Text, txtPassword.Text, txtUrl.Text);

                        if (profile.URN != null && foundObject.ObjectCreators.Text == "")
                        {


                            foundObject.ObjectCreators.Text = TruncateAddress(profile.URN);
                            foundObject.ObjectCreators.Links.Add(0, creator.Key.Length, creator.Key);
                        }
                        else
                        {


                            if (profile.URN != null && foundObject.ObjectCreators2.Text == "")
                            {
                                foundObject.ObjectCreators2.Text = TruncateAddress(profile.URN);
                                foundObject.ObjectCreators2.Links.Add(0, creator.Key.Length, creator.Key);
                            }

                        }
                    }
                    else
                    {

                        if (foundObject.ObjectCreators.Text == "")
                        {


                            foundObject.ObjectCreators.Text = TruncateAddress(creator.Key);
                            foundObject.ObjectCreators.Links.Add(0, creator.Key.Length, creator.Key);
                        }
                        else
                        {


                            if (foundObject.ObjectCreators2.Text == "")
                            {
                                foundObject.ObjectCreators2.Text = TruncateAddress(creator.Key);
                                foundObject.ObjectCreators2.Links.Add(0, creator.Key.Length, creator.Key);
                            }

                        }

                    }


                }
                foundObject.ObjectQty.Text = objstate.Owners.Values.Sum().ToString() + "x";
                foundObject.ObjectAddress.Text = objstate.Creators.First().Key;
                OBJState isOfficial = OBJState.GetObjectByURN(objstate.URN, txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:18332");
                if (isOfficial.URN != null)
                {
                    if (isOfficial.Creators.First().Key == foundObject.ObjectAddress.Text)
                    {
                        foundObject.lblOfficial.Visible = true;

                        foundObject.lblOfficial.Text = TruncateAddress(isOfficial.URN);
                    }
                    else
                    {
                        foundObject.txtOfficialURN.Text = isOfficial.Owners.First().Key;
                        foundObject.btnOfficial.Visible = true;
                    }
                }

                flowLayoutPanel1.Controls.Add(foundObject);
            }
        }

        private void ButtonGetOwnedClick(object sender, EventArgs e)
        {

            btnOwned.BackColor = Color.Yellow;
            btnCreated.BackColor = Color.White;
           
            BuildSearchResults();


        }

        private void ButtonGetCreatedClick(object sender, EventArgs e)
        {

            btnCreated.BackColor = Color.Yellow;
            btnOwned.BackColor = Color.White;
            
            BuildSearchResults();

        }

        private void MainUserNameClick(object sender, LinkLabelLinkClickedEventArgs e)
        {



            PROState searchprofile = PROState.GetProfileByAddress(txtSearchAddress.Text, txtLogin.Text, txtPassword.Text, txtUrl.Text);

            if (searchprofile.URN != null)
            {
                txtSearchAddress.Text = searchprofile.URN;
                linkLabel1.Text = TruncateAddress(searchprofile.Creators.First());

                linkLabel1.LinkColor = System.Drawing.SystemColors.Highlight;
            }
            else
            {


                searchprofile = PROState.GetProfileByURN(txtSearchAddress.Text, txtLogin.Text, txtPassword.Text, txtUrl.Text);

                if (searchprofile.URN != null)
                {
                    txtSearchAddress.Text = searchprofile.Creators.First();
                    linkLabel1.Text = searchprofile.URN;
                    linkLabel1.LinkColor = System.Drawing.SystemColors.Highlight;

                }
                else
                {
                    linkLabel1.Text = "anon";
                    linkLabel1.LinkColor = System.Drawing.SystemColors.GradientActiveCaption;

                }
            }




        }

        private void SearchAddressKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                btnOwned.BackColor = Color.White;
                btnCreated.BackColor = Color.White;
                
                BuildSearchResults();

            }

        }

        private async void BuildSearchResults()
        {

            if (!isBuilding)
            {

                isBuilding = true;
                flowLayoutPanel1.Controls.Clear();

                loadedObjects.Clear();

                txtLast.Text = "0";
                int loadQty = (flowLayoutPanel1.Size.Width / 216) * (flowLayoutPanel1.Size.Height / 333);
                loadQty -= flowLayoutPanel1.Controls.Count - ((flowLayoutPanel1.Size.Width / 216));
                txtQty.Text = loadQty.ToString();



                if (SearchId == SearchHistory.Count)
                {
                    SearchHistory.Add(txtSearchAddress.Text);
                    SearchId++;
                }
                else
                {

                    if (SearchId > SearchHistory.Count - 1) { SearchId = SearchHistory.Count - 1; }
                    SearchHistory[SearchId] = txtSearchAddress.Text;

                }



                if (txtSearchAddress.Text.ToLower().StartsWith("http"))
                {
                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel1.AutoScroll = false;
                    var webBrowser1 = new Microsoft.Web.WebView2.WinForms.WebView2();
                    webBrowser1.Size = flowLayoutPanel1.Size;
                    flowLayoutPanel1.Controls.Add(webBrowser1);

                    await webBrowser1.EnsureCoreWebView2Async();
                    webBrowser1.CoreWebView2.Navigate(txtSearchAddress.Text);
                }
                else
                {

                    flowLayoutPanel1.AutoScroll = true;


                    if (txtSearchAddress.Text.StartsWith("#"))
                    {

                        GetObjectsByKeyword(txtSearchAddress.Text.Replace("#", ""));


                    }
                    else
                    {

                        if (txtSearchAddress.Text.ToLower().StartsWith(@"ipfs:") && txtSearchAddress.Text.Replace(@"//", "").Replace(@"\\", "").Length >= 51)
                        {
                            string ipfsHash = txtSearchAddress.Text.Replace(@"//", "").Replace(@"\\", "").Substring(5, 46);

                            if (!System.IO.Directory.Exists("ipfs/" + ipfsHash))
                            {

                                var SUP = new Options { CreateIfMissing = true };
                                string isLoading;
                                using (var db = new DB(SUP, @"ipfs"))
                                {
                                    isLoading = db.Get(ipfsHash);

                                }

                                if (isLoading != "loading")
                                {
                                    using (var db = new DB(SUP, @"ipfs"))
                                    {

                                        db.Put(ipfsHash, "loading");

                                    }
                                    Task ipfsTask = Task.Run(() =>
                                {
                                    Process process2 = new Process();
                                    process2.StartInfo.FileName = @"ipfs\ipfs.exe";
                                    process2.StartInfo.Arguments = "get " + ipfsHash + @" -o ipfs\" + ipfsHash;
                                    process2.StartInfo.UseShellExecute = false;
                                    process2.StartInfo.CreateNoWindow = true;
                                    process2.Start();
                                    process2.WaitForExit();

                                    if (System.IO.File.Exists("ipfs/" + ipfsHash))
                                    {
                                        System.IO.File.Move("ipfs/" + ipfsHash, "ipfs/" + ipfsHash + "_tmp");
                                        System.IO.Directory.CreateDirectory("ipfs/" + ipfsHash);
                                        string fileName = txtSearchAddress.Text.Replace(@"//", "").Replace(@"\\", "").Substring(51);
                                        if (fileName == "") { fileName = "artifact"; } else { fileName = fileName.Replace(@"/", "").Replace(@"\", ""); }
                                        System.IO.File.Move("ipfs/" + ipfsHash + "_tmp", @"ipfs/" + ipfsHash + @"/" + fileName);

                                    }



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
                                                    Arguments = "pin add " + ipfsHash,
                                                    UseShellExecute = false,
                                                    CreateNoWindow = true
                                                }
                                            };
                                            process3.Start();
                                        }
                                    }



                                    if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + ipfsHash))
                                    {
                                        Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + ipfsHash);
                                    }
                                    else { Label filenotFound = new Label(); filenotFound.AutoSize = true; filenotFound.Text = "IPFS: Search failed! Verify IPFS pinning is enbaled"; flowLayoutPanel1.Controls.Clear(); flowLayoutPanel1.Controls.Add(filenotFound); }

                                    using (var db = new DB(SUP, @"ipfs"))
                                    {
                                        db.Delete(ipfsHash);

                                    }
                                });
                                }
                            }
                            else
                            {

                                Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ipfs\" + ipfsHash);
                            }


                        }
                        else
                        {
                            if (txtSearchAddress.Text.StartsWith(@"sup:"))
                            {
                                GetObjectsByURN(txtSearchAddress.Text.Replace("sup:", "").Replace(@"\\", "").Replace(@"//", ""));
                            }
                            else
                            {

                                Regex regexTransactionId = new Regex(@"\b[0-9a-f]{64}\b");

                                if (txtSearchAddress.Text.Count() > 64 && regexTransactionId.IsMatch(txtSearchAddress.Text) && txtSearchAddress.Text.Contains(".htm"))
                                {
                                    if (txtSearchAddress.Text.StartsWith("MZC:"))
                                    {
                                        Root.GetRootByTransactionId(txtSearchAddress.Text.Substring(4, 64), txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:12832", "50");
                                    }
                                    else
                                    {
                                        if (txtSearchAddress.Text.StartsWith("BTC:"))
                                        {
                                            Root.GetRootByTransactionId(txtSearchAddress.Text.Substring(4, 64), txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:8332", "0");
                                        }
                                        else
                                        {
                                            Root.GetRootByTransactionId(txtSearchAddress.Text.Substring(0, 64), txtLogin.Text, txtPassword.Text, @"http://127.0.0.1:18332");
                                        }

                                    }
                                    Match match = regexTransactionId.Match(txtSearchAddress.Text);
                                    string browserPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\root\" + txtSearchAddress.Text.Replace("MZC:", "").Replace("BTC:", "");
                                    browserPath = @"file:///" + browserPath.Replace(@"\", @"/");
                                    flowLayoutPanel1.Controls.Clear();
                                    flowLayoutPanel1.AutoScroll = false;
                                    var webBrowser1 = new Microsoft.Web.WebView2.WinForms.WebView2();
                                    webBrowser1.Size = flowLayoutPanel1.Size;
                                    flowLayoutPanel1.Controls.Add(webBrowser1);

                                    await webBrowser1.EnsureCoreWebView2Async();
                                    webBrowser1.CoreWebView2.Navigate(browserPath.Replace(@"/", @"\"));
                                }
                                else
                                {

                                    GetObjectsbyAddress(txtSearchAddress.Text.Replace("@", ""));

                                }

                            }
                        }
                    }
                }
                isBuilding = false;
            }

        }


        private void SearchAddressUpdate()
        {

            if (!isBuilding)
            {
                isBuilding = true;


                if (txtSearchAddress.Text.StartsWith("#"))
                {
                    GetObjectsByKeyword(txtSearchAddress.Text.Replace("#", ""));

                }
                else
                {

                    GetObjectsbyAddress(txtSearchAddress.Text.Replace("@", ""));

                }

                isBuilding = false;
            }

        }

        private void ObjectBrowserLoad(object sender, EventArgs e)
        {
            if (_objectaddress.Length > 0)
            {
                txtSearchAddress.Text = _objectaddress;

                if (txtSearchAddress.Text.StartsWith("#"))
                {
                    GetObjectsByKeyword(txtSearchAddress.Text.Replace("#", ""));

                }
                else
                    GetObjectsbyAddress(txtSearchAddress.Text.Replace("@", ""));
            }
            else
            {
                var SUP = new Options { CreateIfMissing = true };

                using (var db = new DB(SUP, @"ipfs"))
                {

                    string ipfsdaemon = db.Get("ipfs-daemon");

                    if (ipfsdaemon == "true")
                    {

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
            }


        }

        private void ButtonLoadWorkBench(object sender, EventArgs e)
        {
            new WorkBench().Show();
        }

        private void ButtonLoadConnections(object sender, EventArgs e)
        {
            new Connections().Show();
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

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count > 0 && flowLayoutPanel1.Controls[0] is Microsoft.Web.WebView2.WinForms.WebView2)
            {
                flowLayoutPanel1.Controls[0].Size = flowLayoutPanel1.Size;
            }

            int loadQty = (flowLayoutPanel1.Size.Width / 100) * (flowLayoutPanel1.Size.Height / 200);

            loadQty -= flowLayoutPanel1.Controls.Count;

            txtQty.Text = loadQty.ToString();
            if (loadQty > 0)
            {

                SearchAddressUpdate();

            }

        }


        private void btnHistoryBack_Click(object sender, EventArgs e)
        {

            if (SearchHistory.Count - 1 > 0)
            {
                SearchId--;
                if (SearchId < 0) { SearchId = 0; }

                txtSearchAddress.Text = SearchHistory[SearchId].ToString();
                txtSearchAddress.Focus();
                SendKeys.SendWait("{Enter}");

            }
        }

        private void btnHistoryForward_Click(object sender, EventArgs e)
        {
            if (SearchHistory.Count - 1 > SearchId)
            {
                SearchId++;
                txtSearchAddress.Text = SearchHistory[SearchId].ToString();
                txtSearchAddress.Focus();
                SendKeys.SendWait("{Enter}");
            }
        }

        private void btnMint_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Check if the data being dragged is a file
            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
            {
                // Allow the drop operation
                e.Effect = System.Windows.Forms.DragDropEffects.Copy;
            }
            else
            {
                // Prevent the drop operation
                e.Effect = System.Windows.Forms.DragDropEffects.None;
            }
        }

        private void flowLayoutPanel1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Get the file paths of the files being dropped
            string[] filePaths = (string[])e.Data.GetData((System.Windows.Forms.DataFormats.FileDrop));
            string filePath = filePaths[0];

            GetObjectByFile(filePath);

        }

        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {

            int loadQty = 1 + (flowLayoutPanel1.Size.Width / 100 * (flowLayoutPanel1.Size.Height / 200) * 2);

            txtQty.Text = loadQty.ToString();
            SearchAddressUpdate();


        }

        private void flowLayoutPanel1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (flowLayoutPanel1.VerticalScroll.Value + flowLayoutPanel1.Height >= flowLayoutPanel1.VerticalScroll.Maximum)
            {
                int loadQty = 1 + (flowLayoutPanel1.Size.Width / 100 * (flowLayoutPanel1.Size.Height / 200) * 2);

                txtQty.Text = loadQty.ToString();

                SearchAddressUpdate();

            }

        }


     

    }
}
