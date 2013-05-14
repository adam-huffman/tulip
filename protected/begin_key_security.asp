<%
	Dim strKey
	
	'Get the Key from the GET string
	strKey = Request.QueryString("key")

	'If we recieved a key from the Get
	If LEN(strKey) > 0 Then
   		
		'Connect to the database and retrieve the timestamp
		Set Conn = Server.CreateObject("ADODB.Connection")
		Conn.Open "PROVIDER=SQLOLEDB;DATA SOURCE=database_server\database_server_instance;UID=database_user_name;PWD=database_user_password;DATABASE=database_name"
		
		sql = "SELECT [timestamp] FROM [database_table_name] WHERE [uid] = '" + strKey + "'"
		
		Set strTimeStamp = Conn.Execute(sql)
		
		'If a timestamp was returned
		If Not strTimeStamp.EOF Then
			
		'Session Time Stamp from SQL Query
		strSessionTimeStamp = strTimeStamp("timestamp")
			
		'Web Server Time Stamp from SQL Query
		strWebServerTimeStamp = Now()
			
		'Difference between the two times
		strDifference = DateDiff("S", strSessionTimeStamp, strWebServerTimeStamp)
			
		If strDifference < 10 Then
		'Generate WebPage==================================
%>