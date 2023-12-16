namespace Core.Entities
{
    public class GroupBy : BaseKeys
    {
        public string SqlGroupBy { get; set; }
        public GroupBy()
        {

        }
        public override string ToString()
        {
            return string.Join(" ", KeyBefore, SqlGroupBy, KeyAfter);
        }
    }
}