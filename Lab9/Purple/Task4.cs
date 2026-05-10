namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        private string _output;
        private (string, char)[] _codes;
        private string _result;

        public string Output => _output;

        public Task4 (string text, (string, char)[] codes) : base(text)
        {
            _output = null;
            _codes = codes;
            _result = "";
        }

        public override void Review()
        {
            _result = "";

            if (Input == null)
            {
                _output = null;
                return;
            }

            for (int i = 0; i < Input.Length; i++)
            {
                bool added = false;

                for (int j = 0; j < _codes.Length; j++)
                {
                    if (Input[i] == _codes[j].Item2)
                    {
                        _result += _codes[j].Item1;
                        added = true;
                        break;
                    }
                }

                if (!added) _result += Input[i];
            }

            _output = _result;
        }

        public override string ToString()
        {
            return Output;
        }
    }
}