using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeUI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            //insert().Wait();
            //InsertMultiSamurais().Wait();
            //InsertMultiDiffObj().Wait();
            //SimpleSamuraiQuery();
            //MoreQueries();
            //MoreQueriesById();
            //QueriesFind().Wait();
            //InsertNewPKFKGRAPH();
            //EagerLoadSamuraiWithQuotes();
            //FilterWithRelatedData();
            //ModifyingRelatedDataWithTracked();
            //EnlistSamuraiToBattleUntracked();
            //AddNewSamuraiViaDisconnectedBattkeObheck();
            //ImplicitAndDynamicType();
            //PrePopulateSamuraiAndBattles().Wait();
            //JoinBattleAndSamurai();
            //Console.ReadKey();
            //GetSamuraiWithBattles();
            //AddNewSamuraiWithSecretIdentity();
            //AddSecretIdentityToExistingSamurai();
        }

        private static void AddSecretIdentityToExistingSamurai()
        {
            Samurai samurai;

            samurai = _context.Samurais.Find(2);

            samurai.SecretIdentity = new SecretIdentity { RealName = "Marie" };
            _context.Samurais.Attach(samurai);
            _context.SaveChanges();
        }

        private static void AddNewSamuraiWithSecretIdentity()
        {
            var samurai = new Samurai { Name = "Ninja Master" };
            samurai.SecretIdentity = new SecretIdentity {RealName= "Johan" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void GetSamuraiWithBattles()
        {
            var SamuraiWithBattles = _context.Samurais.Include(s => s.SamuraiBattles).ThenInclude(sb => sb.Battle).FirstOrDefault(s => s.Id == 1);
            var BattlesWithSamurais = _context.Battles.Include(s => s.SamuraiBattles).ThenInclude(sb => sb.Samurai).FirstOrDefault(s => s.Id == 1);

            foreach (var item in SamuraiWithBattles.SamuraiBattles)
            {
                Console.WriteLine(item.Battle.Name);
            }
            foreach (var item1 in BattlesWithSamurais.SamuraiBattles)
            {
                Console.WriteLine(item1.Samurai.Name);
            }
        }

        private static void AddNewSamuraiViaDisconnectedBattkeObheck()
        {
            Battle battle;
            using (var context = new SamuraiContext())
            {
                battle = context.Battles.Find(1);
            }
            var SamuraiNew = new Samurai { Name="Johan" };
            battle.SamuraiBattles.Add(new SamuraiBattle { Samurai = SamuraiNew });
            _context.Battles.Attach(battle);
            _context.SaveChanges();
        }

        private static void JoinBattleAndSamurai()
        {
            var join = new SamuraiBattle { SamuraiId = 1, BattleId = 1 };
            _context.Add(join);
            _context.SaveChanges();
        }

        private static async Task PrePopulateSamuraiAndBattles()
        {
            using (SamuraiContext context = new SamuraiContext())
            {
                 await context.AddRangeAsync(
                    new Samurai { Name = "Kikuchiyo" },
                    new Samurai { Name = "Kambei Shimada" },
                    new Samurai { Name = "Shichiroji" },
                    new Samurai { Name = "Katsushiro okamoto" },
                    new Samurai { Name = "Kyuzo" }
                    );

                await context.Battles.AddRangeAsync(
                   new Battle { Name="Battle of Okehazama", StartDate=new DateTime(1560,05,25), EndDate= new DateTime(1560,06,1)},
                   new Battle { Name = "Battle of Shiroyama", StartDate = new DateTime(1975, 04, 25), EndDate = new DateTime(1975, 05, 1) }
                    );
                await context.SaveChangesAsync();
            }
        }

        private static void ImplicitAndDynamicType()
        {
            var test = (title: "Min overskrift", author: "Hiolly web");
            Console.WriteLine($" test title er : {test.title} {test.author}");

            var testlist = new List<string>();
            testlist.Add(test.author +" "+ test.title);
        }

        private static void EnlistSamuraiToBattleUntracked()
        {
            Battle battle;
            using (var separateOperation = new SamuraiContext())
            {
                battle = separateOperation.Battles.Find(1);
            }
            battle.SamuraiBattles.Add(new SamuraiBattle { SamuraiId = 4 });
            _context.Battles.Attach(battle);
            _context.ChangeTracker.DetectChanges();
            _context.SaveChanges();
        }

        private static void ModifyingRelatedDataWithTracked()
        {
            var samuarai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault();
            _context.Quotes.Remove(samuarai.Quotes[5]);
            _context.SaveChanges();
        }

        private static void FilterWithRelatedData()
        {
            var samurais = _context.Samurais.Where(s => s.Quotes.Any(q => q.Text.Contains("aloha"))).ToList();
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraieithQuSec = _context.Samurais.Include(s => s.Quotes).Include(q => q.SecretIdentity).ToList();
            var samuraiwithQuotes = _context.Samurais.Where(s => s.Name.Contains("Miki")).Include(s => s.Quotes).ToList();
            var someProperties = _context.Samurais.Select(s=> new { s.Id, s.Name, s.Quotes.Count }).ToList();
            var happyQuotes = _context.Quotes.Where(q => q.Text.Contains("aloha")).ToList();
        }

        private static void InsertNewPKFKGRAPH()
        {
            var samurai = new Samurai
            {
                Name = "Miki",
                Quotes = new List<Quote>
                {
                    new Quote{Text="Aloha"},
                    new Quote{Text= "Min ven"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static async Task QueriesFind()
        {
            var samurai = await _context.Samurais.Where(s => EF.Functions.Like(s.Name, "A%")).ToListAsync();
            foreach (var item in samurai)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static void MoreQueriesById()
        {
            var samurai = _context.Samurais.Find(2);
            Console.WriteLine(samurai.Name);
        }

        private static void MoreQueries()
        {
            var samurai = _context.Samurais.Where(s => s.Name == "Johan").FirstOrDefault();
            Console.WriteLine(samurai.Name);
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurai = context.Samurais.ToList();

                foreach (var item in samurai)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

        private static async Task InsertMultiDiffObj()
        {
            var samurai = new Samurai { Name = "Oda Nobunaga" };
            var battle = new Battle
            {
                Name = "Battle of Nagashino",
                StartDate = new DateTime(1575, 06, 15),
                EndDate = new DateTime(1575, 06, 28)
            };
            using (var context = new SamuraiContext())
            {
               await context.AddRangeAsync(samurai, battle);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InsertMultiSamurais()
        {
            var samurai = new Samurai { Name = "Marie" };
            var samuraiSammy = new Samurai { Name = "Amalie" };
            using (var context = new SamuraiContext())
            {
                await context.Samurais.AddRangeAsync(samurai, samuraiSammy);
                await context.SaveChangesAsync();
            }
        }

        static async Task insert()
        {
            var samurai = new Samurai { Name = "Johan" };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                await context.SaveChangesAsync();
            }
        }
    }
}
