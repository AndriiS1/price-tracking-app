import api from "./api";
import {
  tracked_products_route,
  product_search_route,
} from "../ApiRoutes/apiRoutes";

class ProductService {
  GetTableUrlsData() {
    return api.get(tracked_products_route).then((response) => response.data);
  }
}

export default new ProductService();
