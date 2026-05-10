namespace Lab9.Purple
{
    public class Task2 : Purple
    {
        private string[] _result;
        private string[] _output;

        public string[] Output => _output;

        public Task2 (string text) : base(text)
        {
            _result = new string[0];
            _output = null;
        }

        public override void Review()
        {
            if (Input == null)
            {
                _output = null;
                return;
            }
            
            string[] words = Input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string current = "";

            for (int i = 0; i < words.Length; i++)
                if (current == "")
                    current = words[i];
                else if (current.Length + " ".Length + words[i].Length <= 50)
                    current += " " + words[i];
                else
                {
                    Array.Resize(ref _result, _result.Length + 1);
                    _result[_result.Length - 1] = current;
                    current = words[i];
                }

            if (current != "")
            {
                Array.Resize(ref _result, _result.Length + 1);
                _result[_result.Length - 1] = current;
                current = "";
            }


            for (int i = 0; i < _result.Length; i++)
            {
                string[] stack = _result[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (stack.Length == 1)
                {
                    _result[i] = stack[0];
                    continue;
                }

                int length = 0;
                for (int k = 0; k < stack.Length; k++)
                    length += stack[k].Length;
                
                int amount = 50 - length;

                int every = amount / (stack.Length - 1);
                int first = amount % (stack.Length - 1);

                for (int j = 0; j < stack.Length; j++)
                {
                    if (j == stack.Length - 1)
                        current += stack[j];
                    else
                    {
                        int spaces = every;

                        if (first > 0)
                        {
                            spaces++;
                            first--;
                        }

                        current += stack[j] + new string(' ', spaces);
                    }
                }

                _result[i] = current;
                current = "";
            }

            _output = _result;
        }

        public override string ToString()
        {
            if (_output == null)
                return "";

            return string.Join(Environment.NewLine, _output);
        }
    }
}