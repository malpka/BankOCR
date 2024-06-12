namespace BankOCR
{
    public class ParserOptions
    {
        public bool TryToFixErrOrIll { get; set; } = true;
        public bool ReportErrAccount { get; set; } = false;
        public bool ReportIllAccount { get; set; } = false;
    }
}
