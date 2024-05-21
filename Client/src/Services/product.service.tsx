import api from "./api";
import {
  tracked_products_route,
  product_search_route,
} from "../ApiRoutes/apiRoutes";
import { Product, parseProducts } from "./Dtos/trackedProduct.response";

class ProductService {
  async GetTableUrlsData(page: number, size: number) {
    const response = await api.get(
      `${tracked_products_route}?page=${page}&size=${size}`
    );
    return parseProducts(response.data);
  }
}

export default new ProductService();
