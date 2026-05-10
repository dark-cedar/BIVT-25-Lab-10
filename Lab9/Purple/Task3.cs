using System.Globalization;

namespace Lab9.Purple
{
    public class Task3 : Purple
    {
        private string[] _result;
        // ["по", "ст", "но", "ни", "ле"]
        private (string, char)[] _codes;
        // [("по", ' '), ("ст", '!'), ("но", '"'), ("ни", '#'), ("ле", '$')]
        private string _output;
        // "Пос! много!т"х исс!дова"й ..."

        public (string, char)[] Codes => _codes;
        public string Output => _output;

        public Task3 (string text) : base(text)
        {
            _result = new string[0];
            _codes = new (string, char)[0];
            _output = null;
        }

        public override void Review()
        {
            _result = new string[0];
            _codes = new (string, char)[0];

            if (Input == null)
            {
                _output = null;
                return;
            }

            Dictionary<string, int> table = new Dictionary<string, int>();

            for (int i = 0; i < Input.Length - 1; i++)
            {
                if (!char.IsLetter(Input[i]) || !char.IsLetter(Input[i + 1]))
                    continue;

                string key = Input[i].ToString() + Input[i + 1].ToString();
                
                if (table.TryGetValue(key, out int count))
                    table[key] = count + 1;
                else
                    table[key] = 1;
            }          

            (string Key, int Value)[][] sorted = table
                .GroupBy(pair => pair.Value)
                .OrderByDescending(group => group.Key)
                .Select(group => group
                    .Select(pair => (pair.Key, pair.Value))
                    .ToArray())
                .ToArray();
            // [
            //    [("по", 12), ("ст", 12)],
            //    [("но", 10)],
            //    [("ни", 9), ("ле", 9), ("ра", 9)]
            // ]

            for (int i = 0; i < sorted.Length; i++)
            {
                if (_result.Length == 5) break;

                if (sorted[i].Length == 1)
                {
                    Array.Resize(ref _result, _result.Length + 1);
                    _result[_result.Length - 1] = sorted[i][0].Key;
                    continue;
                }

                int[,] matrix = new int[sorted[i].Length, 2];

                for (int j = 0; j < sorted[i].Length; j++)
                {
                    matrix[j, 0] = j;
                    matrix[j, 1] = Input.IndexOf(sorted[i][j].Key);
                }

                for (int m_i = 0; m_i < matrix.GetLength(0) - 1; m_i++)
                    for (int m_j = 0; m_j < matrix.GetLength(0) - 1 - m_i; m_j++)
                        if (matrix[m_j, 1] > matrix[m_j + 1, 1])
                            for (int m_k = 0; m_k < matrix.GetLength(1); m_k++)
                                (matrix[m_j, m_k], matrix[m_j + 1, m_k]) = (matrix[m_j + 1, m_k], matrix[m_j, m_k]);
                
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (_result.Length == 5) break;

                    Array.Resize(ref _result, _result.Length + 1);
                    _result[_result.Length - 1] = sorted[i][matrix[j, 0]].Key;
                }
            }

            string answer = "";

            for (int i = 32; i <= 126; i++)
                if (!Input.Contains((char)i))
                    answer += (char)i;

            _codes = new (string, char)[_result.Length];
            // [("по", ' '), ("ст", '!'), ("но", '"'), ("ни", '#'), ("ле", '$')]

            for (int i = 0; i < _codes.Length; i++)
                _codes[i] = (_result[i], answer[i]);

            _output = Input;

            for (int i = 0; i < _codes.Length; i++)
                _output = _output.Replace(_codes[i].Item1, _codes[i].Item2.ToString());
        }

        public override string ToString()
        {
            return Output;
        }
    }
}