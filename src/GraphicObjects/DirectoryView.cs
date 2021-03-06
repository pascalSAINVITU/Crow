﻿//
//  DirectoryView.cs
//
//  Author:
//       Jean-Philippe Bruyère <jp.bruyere@hotmail.com>
//
//  Copyright (c) 2016 jp
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.IO;

namespace Crow
{
	public class DirectoryView : TemplatedControl
	{
		#region CTOR
		public DirectoryView ()
			: base()
		{}
		#endregion

		#region events
		public event EventHandler<SelectionChangeEventArgs> SelectedItemChanged;
		#endregion

		string _root = "/";
		bool _showFiles;

		object _selectedItem;
		[XmlIgnore]public object SelectedItem {
			get {
				return _selectedItem;
			}
			set { 
				if (value == _selectedItem)
					return;
				_selectedItem = value;
				NotifyValueChanged ("SelectedItem", _selectedItem);
			}
		}
		[XmlAttributeAttribute()][DefaultValue(true)]
		public virtual bool ShowFiles {
			get { return _showFiles; }
			set {
				if (_showFiles == value)
					return;
				_showFiles = value;
				NotifyValueChanged ("ShowFiles", _showFiles);
			}
		}
		[XmlAttributeAttribute][DefaultValue("/")]
		public virtual string Root {
			get { return _root; }
			set {
				if (_root == value)
					return;
				_root = value;
				NotifyValueChanged ("Root", _root);
				NotifyValueChanged ("CurrentDirectory", CurrentDirectory);
			}
		}
		[XmlIgnore]public FileSystemInfo[] CurrentDirectory {
			get { return new DirectoryInfo (Root).GetFileSystemInfos (); }
		}
		public void onSelectedItemChanged (object sender, SelectionChangeEventArgs e){
			SelectedItem = e.NewValue;
			SelectedItemChanged.Raise (this, e);
		}
	}
}

