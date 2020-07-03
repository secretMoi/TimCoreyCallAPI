using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLibrary
{
	public class SunProcessor
	{

		public static async Task<SunModel> LoadSunInformation()
		{
			string url = "https://api.sunrise-sunset.org/json?lat=50.627600&lng=5.554730&date=today";

			// fais une req sur l'url et attend la réponse
			using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
			{
				if (response.IsSuccessStatusCode)
				{
					// map le json lu dans la req http dans le model
					SunResultModel result = await response.Content.ReadAsAsync<SunResultModel>();

					return result.Results;
				}
				else
					throw new Exception(response.ReasonPhrase);
			}
		}
	}
}
