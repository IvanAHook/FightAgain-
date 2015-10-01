using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class FileReader {

	public List<string> ReadWavesFile( string fileName ) {
		TextAsset wavesFile = Resources.Load( fileName ) as TextAsset;
		List<string> fileLines = new List<string>();
		string [] linesFromFile = wavesFile.text.Split( "\n"[0] );

		foreach ( string line in linesFromFile ) {
			if( line != null )
				fileLines.Add( line );
		}
		return fileLines;

		/*try {
			string line;
			StreamReader reader = new StreamReader( fileName, Encoding.Default );
			using( reader ) {
				do {
					line = reader.ReadLine();
					if( line != null )
						fileLines.Add( line );
				} while ( line != null );
				
				reader.Close();
				return fileLines;
			}
		} catch( IOException e ) {
			// print to somewhere
		}
		return null;*/
	}

}
