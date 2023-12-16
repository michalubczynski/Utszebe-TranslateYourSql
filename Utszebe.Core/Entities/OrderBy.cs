namespace Core.Entities
{
    public class OrderBy : BaseKeys
    {
        public string SqlOrderBy { get; set; }
        public OrderBy()
        {

        }
        public override string ToString()
        {
            return string.Join(" ", KeyBefore, SqlOrderBy, KeyAfter);
        }

    }
}