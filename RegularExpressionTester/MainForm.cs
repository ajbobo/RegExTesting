using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;

// Some Regular Expressions that I like to play with
// (?<left>(\+|-)?(\d+(\.\d+)?|\.\d+))-(?<right>(\+|-)?(\d+(\.\d+)?|\.\d+))|(?<single>(\+|-)?(\d+(\.\d+)?|\.\d+))
// (?<left>\d+)-(?<right>\d+)|(?<single>\d+)
// (?<First>\S+)\s+(?<Last>\S+)

namespace RegularExpressionTester
{
	public partial class MainForm : Form
	{
		private uint[] palette;

		public MainForm()
		{
			InitializeComponent();
			InitializeColors();
		}

		private void Evaluate_RegEx(object sender, EventArgs e)
		{
			rtbData.TextChanged -= new EventHandler(Evaluate_RegEx); // Disconnect this function to avoid infinite loops

			Random rand = new Random();
			bool liveupdate = btnLiveUpdate.Checked;

			string regexp = rtbExpression.Text;
			string data = rtbData.Text;

			try
			{
				StringBuilder outputbuilder = new StringBuilder();
				StringBuilder databuilder = null;
				Regex regex = new Regex(regexp);
				string[] groups = regex.GetGroupNames();
				MatchCollection matches = regex.Matches(data);
				
				// Create the RTF text for the results (the header and color tables are the same for the data and output boxes)
				// Add the header - this seems to be pretty standard
				outputbuilder.AppendLine(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Courier New;}}");
				// Create the color table - one color for each match
				outputbuilder.Append(@"{\colortbl ");
				for (int x = 0; x < matches.Count; x++)
				{
					Color curcolor = ColorTranslator.FromOle((int)palette[x]);
					outputbuilder.Append(GetColorString(curcolor));
				}
				outputbuilder.AppendLine(@";}");
				// Add the text header - more standard stuff, I think
				outputbuilder.Append(@"\viewkind4\uc1\pard\f0\fs17");

				// The header and color table for the data and the output are the same
				if (liveupdate)
					databuilder = new StringBuilder(outputbuilder.ToString());

				// Update the formatted data (either in the data box or in the output box)
				StringBuilder target = (liveupdate ? databuilder : outputbuilder);
				int lastindex = 0;
				for (int x = 0; x < matches.Count; x++)
				{
					Match match = matches[x];
					// Print non-highlighted characters before the match
					target.Append(@"\highlight0 ");
					for (int chr = lastindex; chr < match.Index; chr++)
					{
						target.Append(data[chr]);
					}
					// Print highlighted characters in the match
					target.AppendFormat(@"\ul\highlight{0} ", x + 1);
					for (int chr = match.Index; chr < match.Index + match.Length; chr++)
					{
						target.AppendFormat(@"{0}",data[chr]);
					}
					target.Append(@"\ulnone ");
					lastindex = match.Index + match.Length;
				}
				// Print non-highlighted characters after the last match
				target.Append(@"\highlight0 ");
				for (int chr = lastindex; chr < data.Length; chr++)
				{
					target.Append(data[chr]);
				}
				target.AppendLine(@"\par");

				// Insert a blank line before results (if not in Live Update mode)
				if (!liveupdate)
					outputbuilder.AppendLine(@"\par");

				// Add each match to the output
				for (int x = 0; x < matches.Count; x++)
				{
					Match match = matches[x];
					outputbuilder.AppendFormat(@"\highlight{0} Whole Match: ",x + 1);
					outputbuilder.AppendFormat(@"\highlight0 _{0}_\par\r\n",match.Value);
					// Add each group that is matched
					for (int group = 0; group <= groups.GetUpperBound(0); group++)
					{
						string groupname = groups[group];
						string groupval = match.Groups[groupname].Value;
						if (groupval.Length > 0)
						{
							outputbuilder.AppendFormat(@"\tab {0}:_{1}_\par\r\n",groupname,groupval);
						}
					}
				}
				// End the output RTF and display it
				outputbuilder.AppendLine(@"}");
				rtbOutput.Rtf = outputbuilder.ToString();
				if (liveupdate)
				{
					databuilder.AppendLine(@"}");
					int curpoint = rtbData.SelectionStart;
					rtbData.Rtf = databuilder.ToString();
					rtbData.Select(curpoint, 0); // Put the caret back where it was when the user last typed something
				}
			}
			catch (Exception ex)
			{
				string dummy = ex.Message;
				StringBuilder target = new StringBuilder();

				// Error message in the output box
				target.AppendLine(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Courier New;}}");
				target.AppendLine(@"\viewkind4\uc1\pard\f0\fs17\highlight0 Unable to parse Expression\par");
				target.AppendLine(@"}");
				rtbOutput.Rtf = target.ToString();

				// Non-formatted text in the data box
				target.Remove(0, target.Length);
				target.AppendLine(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Courier New;}}");
				target.AppendFormat(@"\viewkind4\uc1\pard\f0\fs17\highlight0 {0}\par\r\n", rtbData.Text);
				target.AppendLine(@"}");
				rtbData.Rtf = target.ToString();
			}

			rtbData.TextChanged += new EventHandler(Evaluate_RegEx);
		}

		private void btnLiveUpdate_CheckedChanged(object sender, EventArgs e)
		{
			rtbData.TextChanged -= new EventHandler(Evaluate_RegEx);

			StringBuilder databuilder = new StringBuilder();
			databuilder.AppendLine(@"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Courier New;}}");
			databuilder.AppendFormat(@"\viewkind4\uc1\pard\f0\fs17\highlight0 {0}\par\r\n",rtbData.Text);
			databuilder.AppendLine(@"}");
			rtbData.Rtf = databuilder.ToString();

			Evaluate_RegEx(null, EventArgs.Empty);

			rtbData.TextChanged += new EventHandler(Evaluate_RegEx);
		}

		private string GetColorString(Color color)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat(@";\red{0}\green{1}\blue{2}", color.R, color.G, color.B);
			return builder.ToString();
		}

		private void InitializeColors()
		{
			#region Color palette definition
			// These uint values were derived with a separate program I wrote (ColorList)
			palette = new uint[100] { 3937500,	// Crimson
											16711680,	// Blue
											65407,		// Chartreuse
											2763429,	// Brown
											8894686,	// BurlyWood
											10526303,	// CadetBlue
											1993170,	// Chocolate
											14822282,	// BlueViolet
											5275647,	// Coral
											15570276,	// CornflowerBlue
											14481663,	// Cornsilk
											16776960,	// Cyan
											9109504,	// DarkBlue
											9145088,	// DarkCyan
											755384,	// DarkGoldenrod
											11119017,	// DarkGray
											25600,		// DarkGreen
											7059389,	// DarkKhaki
											9109643,	// DarkMagenta
											3107669,	// DarkOliveGreen
											36095,		// DarkOrange
											13382297,	// DarkOrchid
											139,		// DarkRed
											8034025,	// DarkSalmon
											9157775,	// DarkSeaGreen
											9125192,	// DarkSlateBlue
											5197615,	// DarkSlateGray
											13749760,	// DarkTurquoise
											13828244,	// DarkViolet
											9639167,	// DeepPink
											16760576,	// DeepSkyBlue
											6908265,	// DimGray
											16748574,	// DodgerBlue
											2237106,	// Firebrick
											15792895,	// FloralWhite
											2263842,	// ForestGreen
											16711935,	// Fuchsia
											14474460,	// Gainsboro
											16775416,	// GhostWhite
											55295,		// Gold
											2139610,	// Goldenrod
											8421504,	// Gray
											32768,		// Green
											3145645,	// GreenYellow
											15794160,	// Honeydew
											11823615,	// HotPink
											6053069,	// IndianRed
											8519755,	// Indigo
											15794175,	// Ivory
											9234160,	// Khaki
											16443110,	// Lavender
											16118015,	// LavenderBlush
											64636,		// LawnGreen
											13499135,	// LemonChiffon
											15128749,	// LightBlue
											8421616,	// LightCoral
											16777184,	// LightCyan
											13826810,	// LightGoldenrodYellow
											13882323,	// LightGray
											9498256,	// LightGreen
											12695295,	// LightPink
											8036607,	// LightSalmon
											11186720,	// LightSeaGreen
											16436871,	// LightSkyBlue
											10061943,	// LightSlateGray
											14599344,	// LightSteelBlue
											14745599,	// LightYellow
											65280,		// Lime
											3329330,	// LimeGreen
											15134970,	// Linen
											16711935,	// Magenta
											128,		// Maroon
											11193702,	// MediumAquamarine
											13434880,	// MediumBlue
											13850042,	// MediumOrchid
											14381203,	// MediumPurple
											7451452,	// MediumSeaGreen
											15624315,	// MediumSlateBlue
											10156544,	// MediumSpringGreen
											13422920,	// MediumTurquoise
											8721863,	// MediumVioletRed
											7346457,	// MidnightBlue
											16449525,	// MintCream
											14804223,	// MistyRose
											11920639,	// Moccasin
											11394815,	// NavajoWhite
											8388608,	// Navy
											15136253,	// OldLace
											32896,		// Olive
											2330219,	// OliveDrab
											42495,		// Orange
											17919,		// OrangeRed
											14053594,	// Orchid
											11200750,	// PaleGoldenrod
											10025880,	// PaleGreen
											15658671,	// PaleTurquoise
											9662683,	// PaleVioletRed
											14020607,	// PapayaWhip
											8388736,	// Purple
											12180223	// PeachPuff
											/*4163021,	// Peru
											13353215,	// Pink
											14524637,	// Plum
											15130800,	// PowderBlue
											255,		// Red
											9408444,	// RosyBrown
											14772545,	// RoyalBlue
											1262987,	// SaddleBrown
											7504122,	// Salmon
											6333684,	// SandyBrown
											5737262,	// SeaGreen
											15660543,	// SeaShell
											2970272,	// Sienna
											12632256,	// Silver
											15453831,	// SkyBlue
											13458026,	// SlateBlue
											9470064,	// SlateGray
											16448255,	// Snow
											8388352,	// SpringGreen
											11829830,	// SteelBlue
											9221330,	// Tan
											8421376,	// Teal
											14204888,	// Thistle
											4678655,	// Tomato
											16777215,	// Transparent
											13688896,	// Turquoise
											15631086,	// Violet
											11788021,	// Wheat
											16777215,	// White
											16119285,	// WhiteSmoke
											65535,		// Yellow
											3329434,	// YellowGreen
											16775408,	// AliceBlue
											14150650,	// AntiqueWhite
											16776960,	// Aqua
											13959039,	// Aquamarine
											16777200,	// Azure
											14480885,	// Beige
											12903679,	// Bisque
											13495295	// BlanchedAlmond
											*/
											};
			#endregion
		}
	}
}
