namespace Advent.Code.Models
{
    public class Monkey
    {
        public int Id { get; set; }
        public List<string> Items { get; set; }

        public string Expr { get; set; }
        public int Divisble { get; set; }
        public int Positive { get; set; }
        public int Negative { get; set; }

        public Monkey(int _id, List<string> _items, string _expr, int _pos, int _neg, int _div)
        {
            Id = _id;
            Items = _items;
            Expr = _expr;
            Positive = _pos;
            Negative = _neg;
            Divisble = _div;
        }
    }
}