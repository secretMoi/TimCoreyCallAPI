using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using DemoLibrary;

namespace ApiClientTimCorey
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private int _maxNumber;
		private int _currentNumber;


		public MainWindow()
		{
			InitializeComponent();

			ApiHelper.InitializeClient(); // initialise la connexion à une api

			nextImageButton.IsEnabled = false;
		}

		private async Task LoadImage(int imageNumber = 0)
		{
			ComicModel comic = await ComicProcessor.LoadComic(imageNumber);

			if (imageNumber == 0)
				_maxNumber = comic.Num;

			_currentNumber = comic.Num;

			var uriSource = new Uri(comic.Img, UriKind.Absolute);
			comicImage.Source = new BitmapImage(uriSource);
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			await LoadImage();
		}

		private async void previousImageButton_Click(object sender, RoutedEventArgs e)
		{
			if (_currentNumber > 1)
			{
				_currentNumber--;
				nextImageButton.IsEnabled = true;
				await LoadImage(_currentNumber);

				if (_currentNumber == 1)
					previousImageButton.IsEnabled = false;
			}
		}

		private async void nextImageButton_Click(object sender, RoutedEventArgs e)
		{
			if (_currentNumber < _maxNumber)
			{
				_currentNumber++;
				previousImageButton.IsEnabled = true;
				await LoadImage(_currentNumber);

				if (_currentNumber == _maxNumber)
					nextImageButton.IsEnabled = false;
			}
		}

		private void sunInformationButton_Click(object sender, RoutedEventArgs e)
		{
			SunInfo sunInfo = new SunInfo();
			sunInfo.Show();
		}
	}
}
