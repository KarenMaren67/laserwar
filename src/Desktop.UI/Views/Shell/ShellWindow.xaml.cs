using MahApps.Metro.Controls;

namespace Departments.Views.Shell
{
	/// <summary>
	/// Interaction logic for ShellWindow.xaml
	/// </summary>
	public partial class ShellWindow : MetroWindow
	{
		public ShellWindow()
		{
			InitializeComponent();
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			TemplateApplied = true;
		}

		/// <summary>
		/// Только после того как шаблон применен мы можем использовать Mahapps диалоги.
		/// </summary>
		public bool TemplateApplied { get; private set; }
	}
}
