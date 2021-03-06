﻿using System;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Crow
{
	public abstract class NumericControl : TemplatedControl
	{
		#region CTOR
		public NumericControl () : base()
		{
		}
		public NumericControl(double minimum, double maximum, double step)
			: base()
		{
		}
		#endregion

		#region private fields
		double _actualValue, minValue, maxValue, smallStep, bigStep;
		#endregion

		#region public properties
		[XmlAttributeAttribute()][DefaultValue(0.0)]
		public virtual double Minimum {
			get { return minValue; }
			set {
				if (minValue == value)
					return;

				minValue = value;
				NotifyValueChanged ("Minimum", minValue);
				RegisterForRedraw ();
			}
		}
		[XmlAttributeAttribute()][DefaultValue(100.0)]
		public virtual double Maximum
		{
			get { return maxValue; }
			set {
				if (maxValue == value)
					return;

				maxValue = value;
				NotifyValueChanged ("Maximum", maxValue);
				RegisterForRedraw ();
			}
		}
		[XmlAttributeAttribute()][DefaultValue(1.0)]
		public virtual double SmallIncrement
		{
			get { return smallStep; }
			set {
				if (smallStep == value)
					return;

				smallStep = value;
				NotifyValueChanged ("SmallIncrement", smallStep);
				RegisterForRedraw ();
			}
		}
		[XmlAttributeAttribute()][DefaultValue(5.0)]
		public virtual double LargeIncrement
		{
			get { return bigStep; }
			set {
				if (bigStep == value)
					return;

				bigStep = value;
				NotifyValueChanged ("LargeIncrement", bigStep);
				RegisterForRedraw ();
			}
		}
		[XmlAttributeAttribute()][DefaultValue(0.0)]
		public double Value
		{
			get { return _actualValue; }
			set
			{
				if (value == _actualValue)
					return;

				if (value < minValue)
					_actualValue = minValue;
				else if (value > maxValue)
					_actualValue = maxValue;
				else                    
					_actualValue = value;

				NotifyValueChanged("Value",  _actualValue);
				RegisterForGraphicUpdate();
			}
		}
		#endregion

	}
}

