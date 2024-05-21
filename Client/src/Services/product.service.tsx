import api from "./api";
import {
  tracked_products_route,
  product_search_route,
} from "../ApiRoutes/apiRoutes";
import { Product } from "./Dtos/trackedProducts.response";
import { SingleProduct } from "./Dtos/trackedProduct.response";

class ProductService {
  async GetTableUrlsData(page: number, size: number): Promise<Product[]> {
    const response = await api.get(
      `${tracked_products_route}?page=${page}&size=${size}`
    );
    return response.data;
  }

  async GetProduct(id: string): Promise<SingleProduct> {
    const response = await api.get(`${tracked_products_route}/${id}`);
    return response.data;
  }
}

export default new ProductService();
