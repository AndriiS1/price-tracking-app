// PriceStatistic.ts
export interface PriceStatistic {
  price: number;
  date: string;
}

// StoreStatistic.ts
export interface StoreStatistic {
  storeId: string;
  storeName: string;
  storeLastStatistic: PriceStatistic; // Update to storeLastStatistic
}

// Product.ts
export interface Product {
  productId: string;
  name: string;
  storeStatistics: StoreStatistic[];
}

// PriceStatisticClass.ts
export class PriceStatisticClass {
  constructor(public price: number, public date: string) {}
}

// StoreStatisticClass.ts
export class StoreStatisticClass {
  constructor(
    public storeId: string,
    public storeName: string,
    public storeLastStatistic: PriceStatisticClass // Update to storeLastStatistic
  ) {}
}

// ProductClass.ts
export class ProductClass {
  constructor(
    public productId: string,
    public name: string,
    public storeStatistics: StoreStatisticClass[]
  ) {}
}

export function parseProducts(data: any[]): ProductClass[] {
  return data.map((productData) => {
    const storeStatistics = productData.storeStatistics.map(
      (storeStat: any) => {
        const priceStatistic = new PriceStatisticClass(
          storeStat.storeLastStatistic.price,
          storeStat.storeLastStatistic.date
        );
        return new StoreStatisticClass(
          storeStat.storeId,
          storeStat.storeName,
          priceStatistic
        );
      }
    );
    return new ProductClass(
      productData.productId,
      productData.name,
      storeStatistics
    );
  });
}
