﻿using LevelDB;
using NBitcoin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUP.P2FK
{
    public class PRO
    {
        public string urn { get; set; }
        public string fnm { get; set; }
        public string mnm { get; set; }
        public string lnm { get; set; }
        public string sfx { get; set; }
        public string bio { get; set; }
        public string img { get; set; }
        public Dictionary<string, string> url { get; set; }
        public Dictionary<string, string> loc { get; set; }
        public string pkx { get; set; }
        public string pky { get; set; }
        public List<int> cre { get; set; }


    }
    public class PROState
    {
        public string URN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public Dictionary<string, string> URL { get; set; }
        public Dictionary<string, string> Location { get; set; }
        public string PKX { get; set; }
        public string PKY { get; set; }
        public List<string> Creators { get; set; }
        public int ProcessHeight { get; set; }
        public DateTime ChangeDate { get; set; }
        //ensures levelDB is thread safely
        private readonly static object levelDBLocker = new object();

        public static PROState GetProfileByAddress(string profileaddress, string username, string password, string url, string versionByte = "111", bool verbose = false)
        {

            PROState profileState = new PROState();
            var OBJ = new Options { CreateIfMissing = true };
            string JSONOBJ;
            string logstatus;
            string diskpath = "root\\" + profileaddress + "\\";


            // fetch current JSONOBJ from disk if it exists
            try
            {
                JSONOBJ = System.IO.File.ReadAllText(diskpath + "PRO.json");
                profileState = JsonConvert.DeserializeObject<PROState>(JSONOBJ);
            }
            catch { }

            var intProcessHeight = profileState.ProcessHeight;
            Root[] profileTransactions;

            //return all roots found at address
            profileTransactions = Root.GetRootsByAddress(profileaddress, username, password, url, intProcessHeight, 300, versionByte);

            if (intProcessHeight > 0 && profileTransactions.Count() == 1) { return profileState; }

            foreach (Root transaction in profileTransactions)
            {

                intProcessHeight = transaction.Id;
                string sortableProcessHeight = intProcessHeight.ToString("X").PadLeft(9, '0');
                logstatus = null;


                //ignore any transaction that is not signed
                if (transaction.Signed && (transaction.File.ContainsKey("PRO")))
                {

                    string sigSeen;

                    using (var db = new DB(OBJ, @"root\pro"))
                    {
                        sigSeen = db.Get(transaction.Signature);
                    }

                    if (sigSeen == null || sigSeen == transaction.TransactionId)
                    {


                        using (var db = new DB(OBJ, @"root\pro"))
                        {
                            db.Put(transaction.Signature, transaction.TransactionId);
                        }


                        PRO profileinspector = null;
                        try
                        {
                            profileinspector = JsonConvert.DeserializeObject<PRO>(File.ReadAllText(@"root\" + transaction.TransactionId + @"\PRO"));
                        }
                        catch
                        {

                            logstatus = "txid:" + transaction.TransactionId + ",profile,inspect,\"failed due to invalid transaction format\"";

                        }




                        if (logstatus == null && profileState.Creators == null && transaction.SignedBy == profileaddress)
                        {

                            profileState.Creators = new List<string> { };

                            if (profileinspector.cre != null)
                            {
                                foreach (int keywordId in profileinspector.cre)
                                {

                                    string creator = transaction.Keyword.Reverse().ElementAt(keywordId).Key;

                                    if (!profileState.Creators.Contains(creator))
                                    {
                                        profileState.Creators.Add(creator);
                                    }

                                }

                            }
                            else { profileState.Creators.Add(profileaddress); }

                            profileState.ChangeDate = transaction.BlockDate;
                            profileinspector.cre = null;
                        }




                        //has proper authority to make OBJ changes
                        if (logstatus == null && profileState.Creators != null && profileState.Creators.Contains(transaction.SignedBy))
                        {

                            if (profileinspector.urn != null) { profileState.ChangeDate = transaction.BlockDate; profileState.URN = profileinspector.urn; }
                            if (profileinspector.fnm != null) { profileState.ChangeDate = transaction.BlockDate; profileState.FirstName = profileinspector.fnm; }
                            if (profileinspector.mnm != null) { profileState.ChangeDate = transaction.BlockDate; profileState.MiddleName = profileinspector.mnm; }
                            if (profileinspector.lnm != null) { profileState.ChangeDate = transaction.BlockDate; profileState.LastName = profileinspector.lnm; }
                            if (profileinspector.sfx != null) { profileState.ChangeDate = transaction.BlockDate; profileState.Suffix = profileinspector.sfx; }
                            if (profileinspector.bio != null) { profileState.ChangeDate = transaction.BlockDate; profileState.Bio = profileinspector.bio; }
                            if (profileinspector.img != null) { profileState.ChangeDate = transaction.BlockDate; profileState.Image = profileinspector.img; }
                            if (profileinspector.url != null) { profileState.ChangeDate = transaction.BlockDate; profileState.URL = profileinspector.url; }
                            if (profileinspector.loc != null) { profileState.ChangeDate = transaction.BlockDate; profileState.Location = profileinspector.loc; }
                            if (profileinspector.pkx != null) { profileState.ChangeDate = transaction.BlockDate; profileState.PKX = profileinspector.pkx; }
                            if (profileinspector.pky != null) { profileState.ChangeDate = transaction.BlockDate; profileState.PKY = profileinspector.pky; }
                            if (profileinspector.cre != null) 
                            {
                                profileState.Creators.Clear();
                                foreach (int keywordId in profileinspector.cre)
                                {
                                    string creator = transaction.Keyword.Reverse().ElementAt(keywordId).Key;

                                    if (!profileState.Creators.Contains(creator))
                                    {
                                        profileState.Creators.Add(creator);
                                    }

                                }
                                profileState.ChangeDate = transaction.BlockDate; 
                            
                            }

                            if (profileState.ChangeDate == transaction.BlockDate)
                            {
                                logstatus = "txid:" + transaction.TransactionId + ",profile,update,\"success\"";
                            }
                            else
                            {
                                logstatus = "txid:" + transaction.TransactionId + ",profile,update,\"failed due to nothing to update\"";
                            }

                        }
                        else { logstatus = "txid:" + transaction.TransactionId + " failed due to insufficent privlidges"; }

                    }
                    else { logstatus = "txid:" + transaction.TransactionId + " transaction failed due to duplicate signature"; }

                    if (verbose)
                    {
                        if (logstatus != "")
                        {

                            lock (levelDBLocker)
                            {
                                using (var db = new DB(OBJ, @"root\event"))
                                {
                                    db.Put(profileaddress + "!" + sortableProcessHeight + "!" + "0", logstatus);
                                }
                            }

                        }
                    }
                }else {  }///not sure why their is an else may not be necessary..

            }

            //used to determine where to begin profile State processing when retrieved from cache
            profileState.ProcessHeight = intProcessHeight;
            var profileSerialized = JsonConvert.SerializeObject(profileState);

            try
            {
                System.IO.File.WriteAllText(@"root\" + profileaddress + @"\" + "PRO.json", profileSerialized);
            }
            catch
            {
                if (!Directory.Exists(@"root\" + profileaddress))
                {
                    Directory.CreateDirectory(@"root\" + profileaddress);
                }
                System.IO.File.WriteAllText(@"root\" + profileaddress + @"\" + "PRO.json", profileSerialized);
            }

            return profileState;

        }
        public static PROState GetProfileByURN(string searchstring, string username, string password, string url, string versionByte = "111", int skip = 0)
        {
            PROState profileState = new PROState { };

            Root[] profileTransactions;
            string profileaddress = Root.GetPublicAddressByKeyword(searchstring, versionByte);

            //return all roots found at address
            profileTransactions = Root.GetRootsByAddress(profileaddress, username, password, url, skip, 300, versionByte);
            HashSet<string> addedValues = new HashSet<string>();
            foreach (Root transaction in profileTransactions)
            {


                //ignore any transaction that is not signed
                if (transaction.Signed && transaction.File.ContainsKey("PRO"))
                {
                    string findObject = transaction.Keyword.ElementAt(transaction.Keyword.Count - 1).Key;
                    PROState isObject = GetProfileByAddress(findObject, username, password, url, versionByte);

                    if (isObject.URN != null && isObject.URN == searchstring && isObject.ChangeDate > DateTime.Now.AddYears(-3))
                    {
                        if (isObject.Creators.ElementAt(0) == findObject)
                        {

                            return isObject;

                        }

                    }


                }


            }
            return profileState;

        }

    }
}



