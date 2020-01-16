using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using FreelancerCorp.DataAccessLayer.Entities;

namespace FreelancerCorp.DataAccessLayer.Initializers {
    public class FreelancerCorpInitializer : DropCreateDatabaseIfModelChanges<FreelancerCorpDbContext> {

        protected override void Seed(FreelancerCorpDbContext context) {

            /**
             * INITIALIZATION OF FREELANCERS
             */

            Freelancer generalFreelancer = new Freelancer(Enums.Sex.TRANSGENDER, new DateTime(1955, 8, 31), "VajLand", "", "Jozko Vajda", "")
            {
                UserName = "Jozko Vajda",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.FEMALE, new DateTime(1978, 4, 12), "Trnava", "feminist@abc.com", "Ivanka Feminist", "women for women")
            {
                UserName = "IvaFem",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.MALE, new DateTime(1954, 12, 24), "Puste Ulany", "jozok@abc.com", "Jozinko K.", "...")
            {
                UserName = "Jozzii",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.MALE, new DateTime(1987, 5, 5), "Puste Ulany", "ferdamravec@abc.com", "Ferdinand II.", "mravenisko je moj domov")
            {
                UserName = "FerdinandII",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.MALE, new DateTime(1994, 1, 1), "Puste Ulany", "justaman@abc.com", "Randy Guy", "puste ulany are full of men")
            {
                UserName = "RG",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);

            generalFreelancer = new Freelancer(Enums.Sex.TRANSGENDER, new DateTime(1987, 6, 5), "Bratislava", "whoamI@abc.com", "Hopeless Soul", "I dont know who I am")
            {
                UserName = "HOPELESS",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);

            //--- OFFERS ---

            Offer newOffer = new Offer(Enums.Category.MANAGEMENT, "CEO", "Are you a great manager? Come to us :3", 7500, 5, "");
            newOffer.Creator = generalFreelancer;
            newOffer.CreatorRole = "Freelancer";
            context.Offers.AddOrUpdate(newOffer);

            //--- OFFERS ---

            generalFreelancer = new Freelancer(Enums.Sex.TRANSGENDER, new DateTime(1966, 4, 15), "Trnava", "ilikeit@abc.com", "Samuel De'l Secksi", "I like to move it move it, I like to move it move it, Ya like to... MOVE IT")
            {
                UserName = "SDSecksi",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);

            generalFreelancer = new Freelancer(Enums.Sex.FEMALE, new DateTime(1979, 2, 14), "Krupina", "valentine@abc.com", "Valentine Kanizslaw", "love for all")
            {
                UserName = "ValentiNE",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);

            //--- OFFERS ---

            newOffer = new Offer(Enums.Category.GRAPHICS, "Butterfly", "3D model of a beautiful butterfly", 100, 7, "Can be freely used by everyone.");
            newOffer.Creator = generalFreelancer;
            newOffer.CreatorRole = "Freelancer";
            context.Offers.AddOrUpdate(newOffer);

            newOffer = new Offer(Enums.Category.GRAPHICS, "BloodyMary", "3D model of a horror character", 250, 7, "Author should be credited after purchase.");
            newOffer.Creator = generalFreelancer;
            newOffer.CreatorRole = "Freelancer";
            context.Offers.AddOrUpdate(newOffer);

            //--- OFFERS ---

            generalFreelancer = new Freelancer(Enums.Sex.FEMALE, new DateTime(1987, 7, 25), "Dolny Lopasov", "suuuchcuteness@aah.com", "Otaku Girl", "ADORE ANIME !!!")
            {
                UserName = "Animebitch",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.MALE, new DateTime(1997, 12, 15), "Bratislava", "casualguy@abd.com", "Filiponacci Slacky", "read the credits at the bottom of this page... I am such a narcis")
            {
                UserName = "MrSlackyFislip",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);

            //--- OFFERS ---

            newOffer = new Offer(Enums.Category.IT, "This Website", "Please take it from me!!!", 0, 9, "I just want to get rid of it!!!");
            newOffer.Creator = generalFreelancer;
            newOffer.CreatorRole = "Freelancer";
            context.Offers.AddOrUpdate(newOffer);

            //--- OFFERS ---

            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1974, 1, 5), "Kosice", "kociskyperson@dfg.com", "Aurel Capricornicus", "no spoilers")
            {
                UserName = "Capricornicus",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1996, 1, 1), "Lorem Ipsum", "dolor@sit.amet", "Consectetur Adipiscing Elit", "Quisque vitae congue sem, mollis egestas purus.")
            {
                UserName = "Elit",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1978, 1, 1), "Orci Varius", "natoque@penatibus.et", "Magnis dis Parturient", "Montes, nascetur ridiculus mus.")
            {
                UserName = "Magnis",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1982, 1, 1), "Maecenas", "sodales@orci.eu", "Quam Bibendum", "Vitae dignissim ligula scelerisque.")
            {
                UserName = "QuaBibe",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1935, 1, 1), "In hac Habitasse", "platea@dictumst", "Vestibulum Vehicula", "Eu erat sit amet mattis.")
            {
                UserName = "VesVehicula",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1975, 1, 1), "Nam Lacinia", "consectetur@efficitur.integer", "Tempus Ante", "In est cursus, nec malesuada eros blandit.")
            {
                UserName = "TempusA.",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1976, 1, 1), "Fusce Gravida", "nisl@non.porta", "Interdum Augue", "Libero imperdiet nisl, quis rhoncus elit orci eu enim.")
            {
                UserName = "Augue Interdum",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1976, 1, 1), "Ut Efficitur", "diam@et.posuere", "Dignissim Nulla", "Scelerisque tellus quam, ut aliquam lacus varius id.")
            {
                UserName = "Nula",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1985, 1, 1), "Suspendisse Sed", "molestie@leo.mauris", "Facilisis Leo", "Quis erat egestas mattis.")
            {
                UserName = "Leo",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1991, 1, 1), "In hac Habitasse", "platea@dictumst.integer", "Risus Odio", "Scelerisque at urna in, scelerisque dictum neque.")
            {
                UserName = "RisusOdio",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1957, 1, 1), "Nullam Cursus", "faucibus@eleifend.aliquam", "Scelerisque Dictum", "Tortor, in mollis nibh commodo vitae.")
            {
                UserName = "Dicto",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1978, 1, 1), "Duis Sed Tincidunt", "erat@a.facilisis", "Nulla Sed", "Consequat sodales quam, eget accumsan lorem rutrum eu.")
            {
                UserName = "NulaSed",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1984, 1, 1), "Curabitur Facilisis", "aliquet@quam.aliquam", "Rhoncus Nunc", "Quis volutpat dapibus, leo lacus blandit tellus, eu viverra neque massa non ante.")
            {
                UserName = "rhoncusnuncus",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1982, 1, 1), "Phasellus Sagittis", "ut@justo.non", "Maximus Donec", "Euismod vehicula enim, sit amet pulvinar augue.")
            {
                UserName = "MaxDon",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1998, 1, 1), "Nam et Ultricies", "orci@aenean.sodales", "Vel Erat", "Eu accumsan.")
            {
                UserName = "Vel Erat",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1999, 1, 1), "Interdum et Malesuada", "fames@ac.ante", "Ipsum Primis", "In faucibus.")
            {
                UserName = "Primo",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);
            generalFreelancer = new Freelancer(Enums.Sex.NONBINARY, new DateTime(1976, 1, 1), "Aenean Mollis", "nulla@tincidunt.lobortis", "Purus Vitae", "Semper ligula.")
            {
                UserName = "aenean",
                UserRole = "Freelancer"
            };
            context.Freelancers.AddOrUpdate(generalFreelancer);

            /**
             * INITIALIZATION OF CORPORATIONS
             */

            Corporation generalCorporation = new Corporation("1 AAA", "firstever@corp.com", "FIRST ON THE LIST", "Always first")
            {
                UserName = "alwaysFIRSTy",
                UserRole = "Corporation",
                Location = "Žilina"
            };
            context.Corporations.AddOrUpdate(generalCorporation);

            //--- OFFERS ---

            newOffer = new Offer(Enums.Category.IT, "Programmer", "You know what to do", 10000, 27, "Advised: be the very best you can be.");
            newOffer.Creator = generalCorporation;
            newOffer.CreatorRole = "Corporation";
            context.Offers.AddOrUpdate(newOffer);

            newOffer = new Offer(Enums.Category.IT, "Tester", "You know what to do", 8000, 27, "Advised: be the very best you can be.");
            newOffer.Creator = generalCorporation;
            newOffer.CreatorRole = "Corporation";
            context.Offers.AddOrUpdate(newOffer);

            newOffer = new Offer(Enums.Category.IT, "Developer", "You know what to do", 12000, 27, "Advised: be the very best you can be.");
            newOffer.Creator = generalCorporation;
            newOffer.CreatorRole = "Corporation";
            context.Offers.AddOrUpdate(newOffer);


            //--- OFFERS ---

            generalCorporation = new Corporation("2 BBB", "second@corp.com", "Second...", "WE WILL GET ON THE FIRST PLACE!")
            {
                UserName = "ach.secondy",
                UserRole = "Corporation",
                Location = "Banska Bystrica"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("A 123", "zliehov@corp.com", "ThirdAndGlad", "Zliechov is third in row, woooow.")
            {
                UserName = "GladZliechovy",
                UserRole = "Corporation",
                Location = "Zliechov"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("B 456", "humny@corp.com", "Heureka", "...")
            {
                UserName = "Heury",
                UserRole = "Corporation",
                Location = "Humenne"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("C 789", "zdiary@corp.com", "Aleluja", "..")
            {
                UserName = "Alely",
                UserRole = "Corporation",
                Location = "Ždiar nad Hronom"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("D 147", "braty@corp.com", "AleaActaEs", ".")
            {
                UserName = "AAEy",
                UserRole = "Corporation",
                Location = "Bratislava"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("E 258", "kremny1@corp.com", "DOOMSDAY", ".. it is on ..")
            {
                UserName = "Doomy",
                UserRole = "Corporation",
                Location = "Kremnica"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("F 369", "kremny2@corp.com", "SAVIOURS", ".. and we will stop it ..")
            {
                UserName = "Saviy",
                UserRole = "Corporation",
                Location = "Kremnica"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("G 987", "willingtobesecond@corp.com", "WillingToBeSecond", "WE WILL GET ON THE SECOND PLACE!")
            {
                UserName = "Wanty",
                UserRole = "Corporation",
                Location = "Banska Bystrica"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("H 654", "namesty@corp.com", "HahSociety", "Laughing for eternity!")
            {
                UserName = "HahhAHHAHHAHAHha",
                UserRole = "Corporation",
                Location = "Namestovo"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("I 321", "popy@corp.com", "BOSSES", "We rule around here.")
            {
                UserName = "B",
                UserRole = "Corporation",
                Location = "Poprad"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("J 741", "popy_slaves@corp.com", "S..slaves", "W...we doon...dont rrru..uu..rule.")
            {
                UserName = "slavy",
                UserRole = "Corporation",
                Location = "Poprad"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("K 852", "leceny@corp.com", "SingySingCorp!", "Managers for big singing stars!")
            {
                UserName = "Singy",
                UserRole = "Corporation",
                Location = "Lučenec"
            };
            context.Corporations.AddOrUpdate(generalCorporation);
            generalCorporation = new Corporation("L 963", "lastforever@corp.com", "LAST ON THE LIST", "Forever last")
            {
                UserName = "Lasty",
                UserRole = "Corporation",
                Location = "Žilina"
            };
            context.Corporations.AddOrUpdate(generalCorporation);

            /**
            * INITIALIZATION OF OFFERS
            * Added when a new freelancer or corporation created
            */

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
