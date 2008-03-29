//XMPP .NET Library Copyright (C) 2006, 2007 Dieter Lunn
//
//This library is free software; you can redistribute it and/or modify it under
//the terms of the GNU Lesser General Public License as published by the Free
//Software Foundation; either version 3 of the License, or (at your option)
//any later version.
//
//This library is distributed in the hope that it will be useful, but WITHOUT
//ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
//FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
//
//You should have received a copy of the GNU Lesser General Public License along
//with this library; if not, write to the Free Software Foundation, Inc., 59
//Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System;
using xmpp;
using xmpp.common;
#if DEBUG
using xmpp.logging;
#endif

namespace xmpp.states
{
	/// <summary>
	/// 
	/// </summary>
	public class SASLState : State
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="state">
		/// A <see cref="ProtocolState"/>
		/// </param>
		public SASLState(ProtocolState state)
		{
			current = state;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data">
		/// A <see cref="System.Object"/>
		/// </param>
		public override void Execute(xmpp.common.Tag data)
		{
#if DEBUG
			Logger.Debug(this, "Processing next SASL step");
#endif
			xmpp.common.Tag res = current.Processor.Step(data);
			if (res != null)
			{
				current.Socket.Write(res);
			}
			else
			{
#if DEBUG
				Logger.Debug(this, "Sending start stream again");
#endif
				current.Authenticated = true;
				current.State = new ConnectedState(current);
				current.Execute(null);
			}
			
			//Logger.Debug(this, "Outside in the cold where I am not supposed to be.");
		}
	}
}