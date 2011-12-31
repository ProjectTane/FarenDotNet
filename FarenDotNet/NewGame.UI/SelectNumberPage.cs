using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;

namespace FarenDotNet.NewGame.UI
{
	public partial class SelectNumberPage : FarenDotNet.NewGame.UI.Page
	{
		Root _root;
		RadioButton[] _nPlayer, _level;

		public SelectNumberPage(Root root)
		{
			_root = root;
			InitializeComponent();
			this.PrepareNPlayer();
			this.PrepareLevel();
			this.SetData();
			this.CanNext = true;
		}

		private void SetData()
		{
			_nPlayer[_root.N_Player].Checked = true;
			_level[_root.Level - 1].Checked = true;
		}

		private void PrepareNPlayer()
		{
			_nPlayer = new[]{
				player0,
				player1,
				player2,
				player3,
				player4
			};

			for (int i = 0; i < _nPlayer.Length; i++)
				_nPlayer[i].Tag = i;

			foreach (var radio in _nPlayer)
				radio.CheckedChanged += this.nPlayerChanged;
		}

		private void PrepareLevel()
		{
			_level = new[]{
				level1,
				level2,
				level3,
				level4,
				level5
			};

			for (int i = 0; i < _level.Length; i++)
				_level[i].Tag = (i + 1);

			foreach (var radio in _level)
				radio.CheckedChanged += this.LevelChanged;
		}

		protected internal override void OnGoBack(EventArgs e)
		{
			base.OnGoBack(e);
			_root.N_Player = 1;
		}

		private void nPlayerChanged(object sender, EventArgs e)
		{
			int num = (int)((RadioButton)sender).Tag;
			_root.N_Player = num;
		}

		private void LevelChanged(object sender, EventArgs e)
		{
			int level = (int)((RadioButton)sender).Tag;
			_root.Level = level;
		}
	}
}
