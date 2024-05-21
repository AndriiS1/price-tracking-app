export interface Statistic {
  price: number;
  date: string;
}

export interface SingleStoreStatistics {
  id: string;
  name: string;
  statistic: Statistic[];
}

export interface SingleProduct {
  id: string;
  name: string;
  storeStatistics: SingleStoreStatistics[];
}
