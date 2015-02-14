using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusyShopCQRS.Contracts.Commands;

namespace BusyShopCQRS.Web.Tests
{
    public class TestBase
    {
        protected readonly string _baseAddress = string.Format("http://{0}:9000/", Environment.MachineName);

        public List<CreateProduct> Products = new List<CreateProduct>
        {
            new CreateProduct(new Guid("9B90124D-E22D-4557-A77F-42C731CCCB0F"), "Little Cuffer", 7),
            new CreateProduct(new Guid("0E0CB75A-015F-497F-B003-253BBC0D30A7"), "Bride’s Nighty", 10),
            new CreateProduct(new Guid("DBD5BC3A-736B-492D-AE7E-9EFE46702EF6"), "4 Square", 10),
            new CreateProduct(new Guid("33A9BDD8-9ABD-4B7C-AE60-E1299D6F3433"), "Calypso", 10),
            new CreateProduct(new Guid("7BB78584-0B88-4AD6-8302-6E48D097E9A7"), "Fist A Cuffs", 11),
            new CreateProduct(new Guid("D0057A79-62E2-4D15-A570-687E87DD5D70"), "The Mediterranean", 13),
            new CreateProduct(new Guid("FFE26BE3-9F45-4561-A818-ED7219D02DB8"), "COWBOY", 13),
            new CreateProduct(new Guid("3FFD47C3-7C25-415F-95AC-A1BE6FCC4CEF"), "Sunday Roast", 15),
            new CreateProduct(new Guid("C6185D17-4CC4-4B54-B9B7-DBA7D6D00AB7"), "Chip Buddy", 3),
            new CreateProduct(new Guid("CB1CD04D-E958-426A-8EC1-F6E0E1FD1933"), "Tropical Delight", 4)
        };

        public List<CreateCustomer> Customers = new List<CreateCustomer>
        {
            new CreateCustomer(new Guid("D83315E0-77A2-4D82-85D7-4A9A29EDE294"), "JOHN SMITH"),
            new CreateCustomer(new Guid("2F1E6ECB-EE8F-4A3A-98CF-EEBE92D5099B"), "JOE SMITH"),
            new CreateCustomer(new Guid("372F4507-6FB3-4718-8F77-9A1A70E19DD7"), "BOB SMITH"),
            new CreateCustomer(new Guid("F956EF5C-C867-44F8-8EB5-A81FD26598C9"), "MIKE SMITH"),
            new CreateCustomer(new Guid("A2CBCB1D-22D7-492C-9512-E114E46510B9"), "JUAN CARLOS"),
            new CreateCustomer(new Guid("3624302F-8FE8-4DD2-A141-2388A1468FB3"), "JANE SMITH"),
            new CreateCustomer(new Guid("1AE94931-4739-4CF2-8CDE-04ACFC29B3A5"), "MIKE JONES"),
            new CreateCustomer(new Guid("0263FBEB-65F9-45FF-8D0E-160A2C127511"), "DAVID SMITH"),
            new CreateCustomer(new Guid("9EA3BF2D-10D2-4B41-A537-9DCD5A88C39D"), "SARAH SMITH"),
            new CreateCustomer(new Guid("DFA32373-4BE7-470E-A4FE-C7815F1140B2"), "JAMES SMITH"),
            new CreateCustomer(new Guid("690D6794-1387-4C65-89BB-E1E0F0519949"), "PAUL SMITH"),
            new CreateCustomer(new Guid("6F8D313F-C867-403D-8997-AF2165405637"), "MARIO ROSSI"),
            new CreateCustomer(new Guid("A4CC18D0-B046-436B-B077-0B68C5E18782"), "STEVE SMITH"),
            new CreateCustomer(new Guid("07154918-2536-4CBC-9F7E-DD7863C29C3C"), "MARK SMITH"),
            new CreateCustomer(new Guid("E7AB8EA5-2908-4AF2-A818-D2402919A3AC"), "CHRIS SMITH"),
            new CreateCustomer(new Guid("AEA54E5A-2F09-4DDF-9583-95ECC8E00D9A"), "JUAN PEREZ"),
            new CreateCustomer(new Guid("5BE12129-4522-4996-8ED7-F6894F0A51FA"), "MICHAEL SMITH"),
            new CreateCustomer(new Guid("267C4EF2-22F8-478F-AEA5-19C2F0745D18"), "JASON SMITH"),
            new CreateCustomer(new Guid("8B94AD9B-BC55-450F-9782-C5910083ED69"), "JOHN JOHNSON"),
            new CreateCustomer(new Guid("1353B4CB-FBA4-4933-BE62-7E4D6AB19035"), "LISA SMITH"),
        };
    }
}