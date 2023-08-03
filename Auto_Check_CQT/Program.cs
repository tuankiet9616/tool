using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Net;
using System.IO;
using ViettelAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.RegularExpressions;

namespace Auto_Check_CQT
{
    public class Program
    {
        public static string pathFileLog;
        static void Main(string[] args)
        {
            try
            {
                string pathPrj = args[0];
                string username = args[1];
                string password = args[2];
                string domain = args[3];
                string folderLog = args[4];

                Console.WriteLine("Path Project : " + pathPrj);
                Console.WriteLine("Username : " + username);
                Console.WriteLine("Password : " + password);
                Console.WriteLine("Domain : " + domain);

                DateTime now = DateTime.Now;
                string curDt = now.ToString("ddMMyyyy");

                if (!Directory.Exists(folderLog))
                {
                    Directory.CreateDirectory(folderLog);
                }

                pathFileLog = folderLog + "\\log_" + curDt + ".log";

                /* Open Connection */
                string fileDB = pathPrj + "\\dbSqlLite.db";

                SQLiteConnection sqlite = new SQLiteConnection("Data Source=" + fileDB + ";New=True;");
                try
                {
                    /* Pause Console */
                    //Console.ReadLine();

                    SQLiteCommand cmd;
                    sqlite.Open();

                    cmd = sqlite.CreateCommand();
                    /* Open Connection */

                    List<string> fkeys = Program.GetInvoices(cmd);
                    if (fkeys.Count() > 0)
                    {
                        string token = Program.GetToken(username, password);
                        Program.GetCQT(fkeys, username, domain, token, cmd);
                    }

                    /* Close Connection */
                    sqlite.Close();
                    /* Close Connection */
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi : " + ex.Message);
                    File.AppendAllText(pathFileLog, now + " | Error : " + ex.Message + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;
                File.AppendAllText(pathFileLog, now + " | Error : " + ex.Message + Environment.NewLine);
            }
        }

        public static void UpdateCQT(string fkey, string invoiceNo, string serial, string codeOfTax, string exchangestatus, string exchangedes, SQLiteCommand cmd)
        {
            try
            {
                cmd.CommandText = "update InvoiceVAT set No = :no, Serial = :serial, CodeOfTax = :codeoftax, ExchangeStatus = :exchangestatus, ExchangeDes = :exchangedes where Fkey=:fkey";
                cmd.Parameters.Add("no", DbType.String).Value = invoiceNo;
                cmd.Parameters.Add("serial", DbType.String).Value = serial;
                cmd.Parameters.Add("codeoftax", DbType.String).Value = codeOfTax;
                cmd.Parameters.Add("exchangestatus", DbType.String).Value = exchangestatus;
                cmd.Parameters.Add("exchangedes", DbType.String).Value = exchangedes;
                cmd.Parameters.Add("fkey", DbType.String).Value = fkey;
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                if (cmd == null)
                {
                    Console.WriteLine("true");
                }
                Console.WriteLine("Main Function-6");
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Lỗi : " + ex.Message);
                DateTime now = DateTime.Now;
                File.AppendAllText(pathFileLog, now + " | Error : " + ex.Message + Environment.NewLine);
            }
        }

        public static List<string> GetInvoices(SQLiteCommand cmd)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine("Current Time 2: " + now);
            File.AppendAllText(pathFileLog, now + " | Get Invoice" + Environment.NewLine);

            DataTable dt = new DataTable();
            cmd.CommandText = "select * from InvoiceVAT where (Publish = 1 and CodeOfTax is null and ExchangeStatus = 'INVOICE_HAS_CODE_SENT') " +
                                                            "or (Publish = 1 and CodeOfTax is null and ExchangeStatus = 'INVOICE_HAS_CODE_NOT_SENT') " +
                                                            "or (Publish = 1 and CodeOfTax is null and ExchangeStatus is null) " +
                                                            "or (Publish = 1 and No is null)";
            SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
            ad.Fill(dt);
            //cmd.Parameters.Add("exchangestatus", DbType.String).Value = "INVOICE_HAS_CODE_SENT";
            cmd.ExecuteNonQuery();

            List<string> fkeys = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                string fkey = row["Fkey"].ToString();
                string invoiceNoSAP = row["InvoiceNoSAP"].ToString();
                fkeys.Add(fkey);
                Console.WriteLine("Fkey: " + fkey);
                Console.WriteLine("InvoiceNoSAP: " + invoiceNoSAP);
            }
            File.AppendAllText(pathFileLog, now + " | Count Invoice : " + fkeys.Count() + Environment.NewLine);
            return fkeys;
        }

        public static void GetCQT(List<string> fkeys, string username, string domain, string token, SQLiteCommand cmd)
        {
            foreach (string fkey in fkeys)
            {
                string supplierTaxCode = GetCodeTax(username);
                string apiLink = string.Concat(domain, "/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/searchInvoiceByTransactionUuid/");
                string contentType = "application/x-www-form-urlencoded";
                string data = "supplierTaxCode=" + supplierTaxCode + "&transactionUuid=" + fkey;

                Console.WriteLine("ApiLink : " + apiLink);
                Console.WriteLine("Data : " + data);

                DateTime now = DateTime.Now;
                File.AppendAllText(pathFileLog, now + " | " + fkey + " " + "Requesting CQT... : " + Environment.NewLine);

                try
                {
                    APIResultCodeOfTax resultTemp = Program.ParseResult<APIResultCodeOfTax>(Program.webRequestgetCodeOfTax(apiLink, data, token, "POST", contentType, null, 0, 1));
                    if (resultTemp != null)
                    {
                        if (resultTemp.result != null && resultTemp.result.Count > 0)
                        {
                            Console.WriteLine("invoiceNo : " + resultTemp.result[0].invoiceNo);
                            Console.WriteLine("Code Of Tax : " + resultTemp.result[0].codeOfTax);
                            Console.WriteLine("Exchange Status : " + resultTemp.result[0].exchangeStatus);
                            Console.WriteLine("Exchange Des : " + resultTemp.result[0].exchangeDes);

                            string invoiceNo = resultTemp.result[0].invoiceNo;
                            string serial = invoiceNo.Substring(0, 6);
                            string codeOfTax = resultTemp.result[0].codeOfTax;
                            string exchangeStatus = resultTemp.result[0].exchangeStatus;
                            string exchangeDes = resultTemp.result[0].exchangeDes;

                            now = DateTime.Now;
                            File.AppendAllText(pathFileLog, now + " | FKey : " + fkey + Environment.NewLine);
                            File.AppendAllText(pathFileLog, now + " | Invoice No : " + invoiceNo + Environment.NewLine);
                            File.AppendAllText(pathFileLog, now + " | Serial : " + serial + Environment.NewLine);
                            File.AppendAllText(pathFileLog, now + " | Code Of Tax : " + codeOfTax + Environment.NewLine);
                            File.AppendAllText(pathFileLog, now + " | Exchange Status : " + exchangeStatus + Environment.NewLine);
                            File.AppendAllText(pathFileLog, now + " | Exchange Des : " + exchangeDes + Environment.NewLine);

                            if (!string.IsNullOrEmpty(codeOfTax))
                            {
                                UpdateCQT(fkey, invoiceNo, serial, codeOfTax, exchangeStatus, exchangeDes, cmd);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(pathFileLog, now + " | Error : " + ex.Message + Environment.NewLine);
                }
            }
        }

        public static T ParseResult<T>(string result)
        {
            return JsonConvert.DeserializeObject<T>(result);
        }

        public static string webRequestgetCodeOfTax(string pzUrl, string pzData, string accessToken, string pzMethod, string pzContentType, string proxyIP, int port, int ssl)
        {
            try
            {
                if (ssl == 1)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00 | 0x30);
                }

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(pzUrl);
                httpWebRequest.ContentType = pzContentType;
                httpWebRequest.Method = pzMethod;
                httpWebRequest.Timeout = 20 * 1000;
                httpWebRequest.Headers.Add("Cookie", "access_token=" + accessToken);
                httpWebRequest.Accept = "application/json";

                if (!string.IsNullOrEmpty(pzData))
                {
                    byte[] buffer = Encoding.Default.GetBytes(pzData);
                    if (buffer != null)
                    {
                        httpWebRequest.ContentLength = buffer.Length;
                        httpWebRequest.GetRequestStream().Write(buffer, 0, buffer.Length);
                    }
                }

                var httpResponse = httpWebRequest.GetResponse();

                var result = string.Empty;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                Console.WriteLine("Http Response : " + result);
                return result;
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;
                File.AppendAllText(pathFileLog, now + " | Error : " + ex.Message + Environment.NewLine);
                return "NOK" + ex.Message;
            }
        }

        public static string GetToken(string username, string password)
        {
            Console.WriteLine("-------- Start Get Token -----------");
            try
            {
                APIResults resultConverted = new APIResults();

                var body = new
                {
                    username = username,
                    password = password
                };


                string jsonData = JsonConvert.SerializeObject(body);
                string contentType = "application/json";
                string apiLink = "https://api-vinvoice.viettel.vn/auth/login";

                Console.WriteLine("-------- Before Request -----------");
                string result = ViettelAPI.APIHelper.webRequestgetToken(apiLink, jsonData, "POST", contentType, null, 0, 1);
                Console.WriteLine("-------- After Request -----------");
                string access_token = JObject.Parse(result)["access_token"].ToString();

                Console.WriteLine("Token : " + access_token);
                return access_token;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi : " + ex.Message);
                DateTime now = DateTime.Now;
                File.AppendAllText(pathFileLog, now + " | Error : " + ex.Message + Environment.NewLine);

                return string.Empty;
            }
        }

        public static string GetCodeTax(string username)
        {
            /* username: 0300813616 
               regex: ^[0 - 9]{ 10}$ */
            string regex_01 = "^[0-9]{10}$";

            /* username: 0300813616_J0428873 
               regex: ^[0-9]{10}[_][0-9a-zA-Z]{8}$ */
            string regex_02 = "^[0-9]{10}[_][0-9a-zA-Z]{8}$";

            /* username: 0300813616-004_J0326208 
               regex: ^[0-9]{10}[-][0-9]{3}[_][0-9a-zA-Z]{8}$ */
            string regex_03 = "^[0-9]{10}[-][0-9]{3}[_][0-9a-zA-Z]{8}$";

            Regex re01 = new Regex(regex_01, RegexOptions.IgnoreCase);
            Regex re02 = new Regex(regex_02, RegexOptions.IgnoreCase);
            Regex re03 = new Regex(regex_03, RegexOptions.IgnoreCase);

            if (re01.IsMatch(username))
            {
                return username;
            }

            if (re02.IsMatch(username))
            {
                string[] us = username.Split('_');
                return us[0];
            }

            if (re03.IsMatch(username))
            {
                string[] us = username.Split('_');
                return us[0];
            }

            return username;
        }
    }
}
