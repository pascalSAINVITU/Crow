﻿//
//  Measure.cs
//
//  Author:
//       Jean-Philippe Bruyère <jp_bruyere@hotmail.com>
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

namespace Crow
{
	public enum Unit { Pixel, Percent }
	public struct Measure
	{
		public int Value;
		public Unit Units;

		public static Measure Fit = new Measure(-1);
		//TODO:implement stretching as 100%, not 0%
		public static Measure Stretched = new Measure(0, Unit.Percent);

		public Measure (int _value, Unit _units = Unit.Pixel)
		{
			Value = _value;
			Units = _units;
		}
		public bool IsFixed { get { return Value > 0 && Units == Unit.Pixel; }}
		#region Operators
		public static implicit operator int(Measure m){
			return m.Value;
		}
		public static implicit operator Measure(int i){
			return new Measure(i);
		}
		public static implicit operator string(Measure m){
			return m.ToString();
		}
		public static implicit operator Measure(string s){
			return Measure.Parse(s);
		}

		public static bool operator ==(Measure m1, Measure m2){
			return m1.Value == m2.Value && m1.Units == m2.Units;
		}
		public static bool operator !=(Measure m1, Measure m2){
			return !(m1.Value == m2.Value && m1.Units == m2.Units);
		}
		#endregion
		public override int GetHashCode ()
		{
			return Value.GetHashCode ();
		}
		public override bool Equals (object obj)
		{
			return (obj == null || obj.GetType() != typeof(Measure)) ?
				false :
				this == (Measure)obj;
		}
		public override string ToString ()
		{
			return Value == -1 ? "Fit" :
				Value == 0 ? "Stretched" :
				Units == Unit.Percent ? Value.ToString () + "%" : Value.ToString ();
		}
		public static Measure Parse(string s){
			if (string.IsNullOrEmpty (s))
				return Measure.Stretched;

			string st = s.Trim ();
			int tmp = 0;

			if (string.Equals ("Fit", st, StringComparison.Ordinal))
				return Measure.Fit;
			else if (string.Equals ("Stretched", s, StringComparison.Ordinal))
				return Measure.Stretched;
			else {
				if (st.EndsWith ("%", StringComparison.Ordinal)) {
					if (int.TryParse (s.Substring(0, st.Length - 1), out tmp))
						return new Measure (tmp, Unit.Percent);
				}else if (int.TryParse (s, out tmp))
					return new Measure (tmp);
			}

			throw new Exception ("Error parsing Measure.");
		}

	}
}