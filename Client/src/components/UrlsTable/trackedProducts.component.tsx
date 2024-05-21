import { useEffect } from "react";
import tokenService from "../../Services/token.service";
import "./trackedProducts.component.style.css";
import productService from "../../Services/product.service";

export default function TrackedProducts() {
  useEffect(() => {
    console.log(productService.GetTableUrlsData());
  }, []);

  return (
    <div>
      <div>
        <h1>Найбільш популярні пошукові запити</h1>
      </div>
    </div>
  );
}
