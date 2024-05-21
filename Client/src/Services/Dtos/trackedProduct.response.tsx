export interface Statistic {
  price: number;
  date: string;
}

export interface StoreStatistics {
  id: string;
  name: string;
  statistic: Statistic[];
}

export interface SingleProduct {
  id: string;
  name: string;
  storeStatistics: StoreStatistics[];
}
