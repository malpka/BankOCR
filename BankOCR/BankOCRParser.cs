namespace BankOCR
{
    public class BankOCRParser
    {
        private const int MAX_INPUT_LINE_LENGTH = 27;
        private const int VALID_INPUT_LINE_COUNT = 4;
        private const int ACCOUNT_DIGIT_COUNT = 9;

        private ParserOptions options;
        private LineParser lineParser;

        public BankOCRParser()
        {
            
            options = new ParserOptions()
            {
                ReportErrAccount = false,
                ReportIllAccount = false,
                TryToFixErrOrIll = true
            };
            lineParser = new LineParser(options);
        }

        public BankOCRParser(ParserOptions options)
        {
            this.options = options;
            lineParser = new LineParser(options);
        }

        public void Parse(string filename, StreamWriter writer)
        {
            var lines = File.ReadAllLines(filename);
            var parser = new LineParser();
            var groups = lines
                .Select((v, i) => new { v, i })
                .GroupBy(x => (x.i / 4))
                .Select(grp => grp.Select(x => x.v).ToArray());
            foreach (var group in groups)
            {
                string s = string.Join(Environment.NewLine, group);
                writer.WriteLine(parser.Parse(s));
            }
        }
    }
}
