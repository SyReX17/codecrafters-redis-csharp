namespace codecrafters_redis;

public class StringReader
{
    private readonly string _str;

    private int _position;

    public StringReader(string str)
    {
        _str = str;
        _position = 0;
    }
    
    public int Read()
    {
        if (_position >= _str.Length)
        {
            return -1;
        }
        return _str[_position++];
    }

    public int Read(char[] buffer, int index, int count)
    {
        if (_position >= _str.Length)
        {
            return -1;
        }
        count = Math.Min(count, _str.Length - _position);
        _str.CopyTo(_position, buffer, index, count);
        _position += count;
        return count;
    }
}