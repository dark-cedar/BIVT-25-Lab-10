namespace Lab10.Purple
{
    public class DTOCode
    {
        public string Pair { get; set; }
        public char Code { get; set; }

        public DTOCode()
        {
            Pair = string.Empty;
            Code = '\0';
        }

        public DTOCode(string pair, char code)
        {
            Pair = pair;
            Code = code;
        }
    }
}