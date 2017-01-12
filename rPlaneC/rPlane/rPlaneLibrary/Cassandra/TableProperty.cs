namespace rPlaneLibrary.Cassandra
{
    public abstract class TableProperty
    {
        public string KeySpace { get; set; }
        public string TableName { get; set; }
    }
}