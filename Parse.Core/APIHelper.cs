using Parse.Core.Domain;
using RestSharp;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Parse.Core
{
	public class APIHelper
	{
		public APIHelper()
		{
		}

		public static string AdjustInv(string xmlData, string fkey, string pattern, string serial, string no)
		{
			string message;
			try
			{
				Company current = Parse.Core.AppContext.Current.company;
				string apiurl = current.Domain;
				string username = current.UserName;
				string password = current.PassWord;
				string data = string.Concat(new string[] { "{'xmlData':'", xmlData, "','fkey':'", fkey, "','pattern':'", pattern, "','serial':'", serial, "', 'invNo':'", no, "'}" });
				message = APIHelper.callApi(apiurl, "api/business/adjustInv", data, username, password, Method.POST);
			}
			catch (Exception exception)
			{
				message = exception.Message;
			}
			return message;
		}

		public static string callApi(string apiUri, string action, string data = null, string username = null, string password = null, Method method = Method.POST)
		{
			RestClient restClient = new RestClient(apiUri);
			restClient.Proxy = WebRequest.DefaultWebProxy;
			RestRequest request = new RestRequest(action);
			request.Method = method;
			request.AddHeader("Content-Type", "application/json");
			if (data != null)
			{
				request.AddParameter("application/json", data, ParameterType.RequestBody);
			}
			string value = APIHelper.makeAuthenticationString(request.Method, username, password);
			request.AddHeader("Authentication", value);
			IRestResponse response = restClient.Execute(request);
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				return "ERR:1";
			}
			if (response.StatusCode != HttpStatusCode.OK)
			{
				return "ERR:2";
			}
			return response.Content;
		}

		public static string ConvertInv(string fkey, string pattern, string serial, string no)
		{
			string message;
			try
			{
				Company current = Parse.Core.AppContext.Current.company;
				string apiurl = current.Domain;
				string username = current.UserName;
				string password = current.PassWord;
				string data = string.Concat(new string[] { "{'fkey':'", fkey, "', 'pattern':'", pattern, "','serial':'", serial, "', 'invNo':'", no, "'}" });
				message = APIHelper.callApi(apiurl, "api/convertinv/convertForStore", data, username, password, Method.POST);
			}
			catch (Exception exception)
			{
				message = exception.Message;
			}
			return message;
		}

		public static string makeAuthenticationString(Method method, string username, string password)
		{
			DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			TimeSpan timeSpan = DateTime.UtcNow - epochStart;
			string Timestamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();
			Guid guid = Guid.NewGuid();
			string nonce = guid.ToString("N").ToLower();
			string signatureRawData = string.Format("{0}{1}{2}", method.ToString().ToUpper(), Timestamp, nonce);
			string signature = Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(signatureRawData)));
			return string.Format("{0}:{1}:{2}:{3}:{4}", new object[] { signature, nonce, Timestamp, username, password });
		}

		public static string PublishInv(string xmlData, string pattern, string serial = null)
		{
			string message;
			try
			{
				Company current = Parse.Core.AppContext.Current.company;
				string apiurl = current.Domain;
				string username = current.UserName;
				string password = current.PassWord;
				string data = string.Concat(new string[] { "{'xmlData':'", xmlData, "','pattern':'", pattern, "','serial':'", serial, "'}" });
				message = APIHelper.callApi(apiurl, "api/publish/importAndPublishInv", data, username, password, Method.POST);
			}
			catch (Exception exception)
			{
				message = exception.Message;
			}
			return message;
		}

		public static string ReplaceInv(string xmlData, string fkey, string pattern, string serial, string no)
		{
			string message;
			try
			{
				Company current = Parse.Core.AppContext.Current.company;
				string apiurl = current.Domain;
				string username = current.UserName;
				string password = current.PassWord;
				string data = string.Concat(new string[] { "{'xmlData':'", xmlData, "','fkey':'", fkey, "','pattern':'", pattern, "','serial':'", serial, "', 'invNo':'", no, "'}" });
				message = APIHelper.callApi(apiurl, "api/business/replaceInv", data, username, password, Method.POST);
			}
			catch (Exception exception)
			{
				message = exception.Message;
			}
			return message;
		}
	}
}