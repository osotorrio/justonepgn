namespace JustOnePgn.Core.Contracts
{
    public interface IHashStrings
    {
        string GenerateSHA256String(string input);

        string GenerateSHA512String(string inputString);
    }
}
