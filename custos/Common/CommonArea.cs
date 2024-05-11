using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace custos.Common;

public class Response
{
	public string Status { get; set; }
	public string Message { get; set; }
	public object Data { get; set; }
}
public class Events
{
	public int Id { get; set; }
	public string Event { get; set; }
	public DateTime EventDate { get; set; }
	public string SystemId { get; set; }
}
public class CommonMethod
{
	public static string webGetMethod(string URL)
	{
		string jsonString = "";
		try
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
			request.Method = "GET";
			request.Credentials = CredentialCache.DefaultCredentials;
			((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
			request.Accept = "/";
			request.UseDefaultCredentials = true;
			request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
			request.ContentType = "application/x-www-form-urlencoded";

			WebResponse response = request.GetResponse();
			StreamReader sr = new StreamReader(response.GetResponseStream());
			jsonString = sr.ReadToEnd();
			sr.Close();
		}
		catch (Exception ex)
		{

		}
		return jsonString;
	}
	public static string webPostMethod(string postData, string URL)
	{
		string responseFromServer = "";
		try
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
			request.Method = "POST";
			request.Credentials = CredentialCache.DefaultCredentials;
			((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
			request.Accept = "/";
			request.UseDefaultCredentials = true;
			request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
			byte[] byteArray = Encoding.UTF8.GetBytes(postData);
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = byteArray.Length;
			Stream dataStream = request.GetRequestStream();
			dataStream.Write(byteArray, 0, byteArray.Length);
			dataStream.Close();

			WebResponse response = request.GetResponse();
			dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);
			responseFromServer = reader.ReadToEnd();
			reader.Close();
			dataStream.Close();
			response.Close();
		}
		catch (Exception Ex)
		{
			//responseFromServer = Ex.Message;
		}
		return responseFromServer;
	}
	public  async Task EventLog(Events data)
	{
		var jsonData = JsonConvert.SerializeObject(data);
		using (HttpClient httpClient = new HttpClient())
		{
			var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
			HttpResponseMessage response = await httpClient.PostAsync(CommonAPI.Event_url, content);
			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				Response responseModel = JsonConvert.DeserializeObject<Response>(responseBody);
				if (responseModel != null)
				{

				}
			}
		}
	}

}
public class CommonAPI
{
	public static string Event_url = "http://65.2.100.52:1050/api/eventHistory";
}


public class QuestionData
{
	
    public int answerId { get; set; }
    public string answer { get; set; }
}
public class AnswerData
{
	public int Id { get; set; }
	public string Answer { get; set; }
	public int QuestionId { get; set; }
}