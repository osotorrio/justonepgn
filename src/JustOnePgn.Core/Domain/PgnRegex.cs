namespace JustOnePgn.Core.Domain
{
    internal class PgnRegex
    {
        internal static string Moves = @"((?:[PNBRQK]?[a-h]?[1-8]?x?[a-h][1-8](?:\=[PNBRQK])?|O(-?O){1,2})[\+#]?(\s*[\!\?]+)?)";
        internal static string Metadata = @"\[\w+ " + "\"" + "(.*)" + "\"" + @"\]"; // \[\w+ (\"(.*)\")\]
        internal static string ResultAtTheEnd = @"(1-0)$|(1\/2-1\/2)$|(0-1)$|(\*)$";
    }
}
