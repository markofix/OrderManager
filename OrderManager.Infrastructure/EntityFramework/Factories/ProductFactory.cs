using OrderManager.Domain.Entites;

namespace OrderManager.Infrastructure.EntityFramework.Factories
{
    internal class ProductFactory : IProductFactory
    {
        public Product BuildProductFromDbModel(Models.Product dbProduct)
        {
            Product? product = null;

            switch (dbProduct.ProductTypeId)
            {
                case (byte)Domain.Enums.ProductType.MainDish:
                {
                    product = new MainDish(dbProduct.Name, dbProduct.Price)
                    {
                        Id = dbProduct.Id,
                    };
                    break;
                }
                case (byte)Domain.Enums.ProductType.SpecialOffer:
                {
                    var specialOffer = dbProduct.SpecialOffers.Single(x => x.ProductId == dbProduct.Id);
                    product = new SpecialOffer(specialOffer.SpecialOfferItems.Select(item => BuildProductFromDbModel(item.Product)), dbProduct.Name, dbProduct.Price)
                    {
                        Id = dbProduct.Id,
                    };
                    break;
                }
                case (byte)Domain.Enums.ProductType.Dessert:
                {
                    product = new Dessert()
                    {
                        Id = dbProduct.Id,
                    };
                    break;
                }
                case (byte)Domain.Enums.ProductType.Drink:
                {
                    product = new Drink()
                    {
                        Id = dbProduct.Id,
                    };
                    break;
                }
            }

            return product!;
        }
    }
}
