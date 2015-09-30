using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class FileReader {

	public List<string> ReadWavesFile( string fileName ) {
		List<string> fileLines = new List<string>();
		try {
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
		return null;
	}

}
