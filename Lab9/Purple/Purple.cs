namespace Lab9.Purple
{
    public abstract class Purple
    {
        private string _input;

        public string Input => _input;

        protected Purple (string text)
        {
            _input = text;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }
    }
}