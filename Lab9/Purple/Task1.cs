namespace Lab9.Purple
{
    public class Task1 : Purple
    {
        private string _output;

        public string Output => _output;

        public Task1 (string text) : base(text)
        {
            _output = default(string);
        }

        public override void Review ()
        {
            if (Input == null)
            {
                _output = default(string);
                return;
            }

            char[] marks = new char[] {' ', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/'};

            string result = "", word = "";
            
            for (int i = 0; i < Input.Length; i++)
                if (marks.Contains(Input[i]))
                {
                    if (word.Length != 0)
                    {
                        if (word.Any(char.IsDigit))
                            result += word;
                        else
                            result += new string(word.Reverse().ToArray());

                        word = "";
                    }

                    result += Input[i];
                } else
                    word += Input[i];

            if (word.Length != 0)
                if (word.Any(char.IsDigit))
                    result += word;
                else
                    result += new string(word.Reverse().ToArray());

            _output = result;
        }

        public override string ToString ()
        {
            return _output;
        }
    }
}