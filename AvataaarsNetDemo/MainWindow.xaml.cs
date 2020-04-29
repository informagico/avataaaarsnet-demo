using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using AvataaarsNet.Models;
using Newtonsoft.Json;

namespace AvataaarsNetDemo
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		private Bitmap _Avatar;
		public Bitmap Avatar
		{
			get
			{
				return _Avatar;
			}
			set
			{
				if (_Avatar == value)
					return;

				_Avatar = value;

				OnPropertyChanged();
			}
		}

		private AvataaarsConfiguration _Configuration;
		public AvataaarsConfiguration Configuration
		{
			get
			{
				return _Configuration;
			}
			set
			{
				if (_Configuration == value)
					return;

				_Configuration = value;

				OnPropertyChanged();
			}
		}

		private bool _EnableWidth = true;
		public bool EnableWidth
		{
			get
			{
				return _EnableWidth;
			}
			set
			{
				if (_EnableWidth == value)
					return;

				_EnableWidth = value;

				OnPropertyChanged();
			}
		}

		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
			{
				var e = new PropertyChangedEventArgs(propertyName);
				handler(this, e);
			}
		}

		private void SaveFile_Click(object sender, RoutedEventArgs e)
		{
			Avatar.Save("avataaars.png", ImageFormat.Png);
		}

		private void SaveConfig_Click(object sender, RoutedEventArgs e)
		{
			string json = JsonConvert.SerializeObject(Configuration);
			File.WriteAllText("configuration.json", json);
		}

		private void LoadConfig_Click(object sender, RoutedEventArgs e)
		{
			string json = File.ReadAllText("configuration.json");
			Configuration = JsonConvert.DeserializeObject<AvataaarsConfiguration>(json);
		}

		private void RandomizeConfig_Click(object sender, RoutedEventArgs e)
		{
			Configuration = AvataaarsSettings.Randomize(Configuration.AvatarWidth);
		}
	}
}
