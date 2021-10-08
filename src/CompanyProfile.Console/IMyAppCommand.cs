using ASI.Console;
using ASI.Console.Commands;

namespace CompanyProfile.Console
{
    public interface ICompanyProfileCommand : IAsiConsoleCommand<CompanyProfileEnvironment>
    {
        void IAsiConsoleCommand<CompanyProfileEnvironment>.Execute(CommandContext<CompanyProfileEnvironment> context)
        {
            Execute(new CompanyProfileContext(context.Args, context.Commands, context.Environments));
        }

        void Execute(CompanyProfileContext context);
    }
}
