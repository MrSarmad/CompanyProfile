using ASI.Console;
using ASI.Console.Commands;
using ASI.Contracts.CompanyProfile;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Core.Models;
using CompanyProfile.Core.MyEntities;

namespace CompanyProfile.Console.Commands
{
    [Command("svc-test")]
    public class SvcTestCommmand : ICompanyProfileCommand
    {
        public void Execute(CompanyProfileContext context)
        {
            //asi/125724 in local is tenantid 4396
            var sp = context.GetServiceProvider(4396);

            var svc = sp.GetService<IMyEntityService>()!;

            var retValue = svc.AddAsync(new MyEntity
            {
                Name = "My Entity Name",
                Description = "My Entity Desc"
            }).GetAwaiter().GetResult();
            Terminal.Green($"Inserted entity {retValue.Id}: {retValue.Name}");

            var id = retValue.Id;
            var myEntity = svc.Get(id);

            if (myEntity != null)
                Terminal.Green($"Found entity {id}: {myEntity.Name}");
            else
                Terminal.Red($"Couldn't find entity {id}");
        }
    }

    [Command("ctrl-test")]
    public class ControllerTestCommmand : ICompanyProfileCommand
    {
        public void Execute(CompanyProfileContext context)
        {
            //asi/125724 in local is tenantid 4396
            var sp = context.GetServiceProvider(4396);

            var svc = sp.GetService<IMyEntityService>()!;
            var mapper = sp.GetService<IMapper>()!;

            var view = new MyEntityView
            {
                Name = "My Entity Name",
                Description = "My Entity Desc"
            };

            //here, replicate the controller mapping
            var model = mapper.Map<MyEntity>(view);
            var retValue = svc.AddAsync(model);
            var res = mapper.Map<MyEntityView>(retValue);
            //return res; //<-- api would normally return here

            Terminal.Green($"Inserted entity {res.Id}: {res.Name}");
        }
    }
}