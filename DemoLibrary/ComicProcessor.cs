using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLibrary
{
	public class ComicProcessor
	{
		public int MaxComicNumber { get; set; }

		public static async Task<ComicModel> LoadComic(int comicNumber = 0)
		{
			string url;

			if (comicNumber > 0)
				url = $"http://xkcd.com/{comicNumber}/info.0.json";
			else
				url = "http://xkcd.com/info.0.json";

			// fais une req sur l'url et attend la réponse
			using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
			{
				if (response.IsSuccessStatusCode)
				{
					// map le json lu dans la req http dans le model
					ComicModel comic = await response.Content.ReadAsAsync<ComicModel>();

					return comic;
				}
				else
					throw new Exception(response.ReasonPhrase);
			}
		}
	}
}
