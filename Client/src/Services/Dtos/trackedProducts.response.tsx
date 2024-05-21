export interface PriceStatistic {
  price: number;
  date: string;
}

export interface StoreStatistic {
  id: string;
  name: string;
  lastStatistic: PriceStatistic;
}

export interface Product {
  id: string;
  name: string;
  storeStatistics: StoreStatistic[];
}

export class PriceStatisticClass {
  constructor(public price: number, public date: string) {}
}

export class StoreStatisticClass {
  constructor(
    public id: string,
    public name: string,
    public lastStatistic: PriceStatisticClass
  ) {}
}

export class ProductClass {
  constructor(
    public id: string,
    public name: string,
    public storeStatistics: StoreStatisticClass[]
  ) {}
}

export function parseProducts(data: any[]): ProductClass[] {
  return data.map((productData) => {
    const storeStatistics = productData.storeStatistics.map(
      (storeStat: any) => {
        const priceStatistic = new PriceStatisticClass(
          storeStat.lastStatistic.price,
          storeStat.lastStatistic.date
        );
        return new StoreStatisticClass(
          storeStat.id,
          storeStat.name,
          priceStatistic
        );
      }
    );
    return new ProductClass(productData.id, productData.name, storeStatistics);
  });
}
