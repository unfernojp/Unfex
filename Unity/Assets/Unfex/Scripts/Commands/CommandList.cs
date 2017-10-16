/**
 * Unfex - unferno extentions
 * 
 * @overview	UX provides a basic set of classes that can be used as a foundation for application development.
 * @author		taka:unferno.jp
 * @version		1.0.0
 * @see			http://unferno.jp/
 * 
 * Licensed under the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */

using System.Collections.Generic;

namespace Unfex.Commands {
	
	public abstract class CommandList:Command {
		
		#region Properties
		public int count {
			get { return _count; }
		}
		protected int _count = 0;
		
		public int total {
			get { return _commandList.Count; }
		}
		#endregion
		
		
		
		#region Fields
		protected List<Command> _commandList = new List<Command>();
		#endregion
		
		
		
		#region Constructors
		public CommandList() {
			
		}
		#endregion
		
		
		
		#region Methods
		public CommandList Add( params Command[] commands ) {
			AddRange( commands );
			
			return this;
		}

		public CommandList AddRange( Command[] commands ) {
			AddRange( new List<Command>( commands ) );

			return this;
		}

		public CommandList AddRange( List<Command> commands ) {
			List<Command> list = new List<Command>();

			for ( int i = 0; i < commands.Count; i++ ) {
				Command command = commands[i];

				if ( command == null ) { continue; }

				list.Add( command );
			}

			_commandList.AddRange( list );
			
			return this;
		}

		public CommandList Insert( int index, params Command[] commands ) {
			InsertRange( index, commands );
			
			return this;
		}

		public CommandList InsertRange( int index, Command[] commands ) {
			InsertRange( index, new List<Command>( commands ) );

			return this;
		}

		public CommandList InsertRange( int index, List<Command> commands ) {
			List<Command> list = new List<Command>();

			for ( int i = 0; i < commands.Count; i++ ) {
				Command command = commands[i];

				if ( command == null ) { continue; }

				list.Add( command );
			}

			_commandList.InsertRange( index, list );

			return this;
		}

		public CommandList Clear() {
			_commandList = new List<Command>();

			return this;
		}
		
		protected override void cancelProcess() {
			for ( int i = 0; i < _commandList.Count; i++ ) {
				Command command = _commandList[i] as Command;
				
				if ( command.state == Command.State.Executing ) {
					command.Cancel();
				}
			}
		}
		#endregion
	}
}
