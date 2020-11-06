using System.Linq;
using Abp.Application.Editions;
using BS_Zoom_Demo.Editions;
using BS_Zoom_Demo.EntityFramework;

namespace BS_Zoom_Demo.Migrations.SeedData
{
    public class DefaultEditionsCreator
    {
        private readonly BS_Zoom_DemoDbContext _context;

        public DefaultEditionsCreator(BS_Zoom_DemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var defaultEdition = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition == null)
            {
                defaultEdition = new Edition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
                _context.Editions.Add(defaultEdition);
                _context.SaveChanges();

                //TODO: Add desired features to the standard edition, if wanted!
            }   
        }
    }
}