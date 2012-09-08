using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BDT.Domain.Migrations;

namespace BDT.Domain
{
    public class BdtContextInitializer : DropCreateDatabaseIfModelChanges<BdtContext>
    {
        protected override void Seed(BdtContext context)
        {
            Seeder.Seed(context);
        }
    }
}
