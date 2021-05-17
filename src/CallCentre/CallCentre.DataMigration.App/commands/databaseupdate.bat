setlocal 
SET CC_API_SQL_CONNECTION=Data Source=DATABASE_SERVER;Initial Catalog=CallCentreDB;Integrated Security=True;MultipleActiveResultSets=True
endlocal
dotnet ef  database update
