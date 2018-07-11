// ////////////////////////////////////////////////////////////////////////////
//
//  $RCSfile: ParseMessageEventArgs.cs,v $
//
//  $Revision: 1.1.1.1 $
//
//  Last change:
//    $Author: Robert $
//    $Date: 2004/06/22 09:56:47 $
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
//
//  Original Code from Christian Tratz (via www.codeproject.com).
//  Changed by R. Lelieveld, SimVA GmbH.
//
// ////////////////////////////////////////////////////////////////////////////
using System;

namespace Sadco.FamilyDoctor.Core.Controls.ResizableListBox
{
	/// <summary>
	/// 
	/// </summary>
	public class ListBoxItemEventArgs : System.EventArgs
	{
		private object m_Source;

		public ListBoxItemEventArgs() : base()
		{		}

		public ListBoxItemEventArgs(object source) : this()
		{
            m_Source = source;
		}

		public object Source
		{
			get { return m_Source; }
			set { m_Source = value; }
		}
	}
}
