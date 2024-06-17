namespace wsmcbl.src.exception;

public class DbException : Exception
{
    public DbException(string element) : base($"Failed to perform transaction on table {element}.")
    {
    }
}