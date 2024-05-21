import { useEffect, useState } from "react";
import "./productInfo.component.style.css";

import urlService from "../../Services/product.service";
import { Link, useNavigate, useParams } from "react-router-dom";
import productService from "../../Services/product.service";

function ProductInfo() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [product, setProduct] = useState();

  useEffect(() => {
    const fetchProducts = async () => {
      if (id) {
        const data = await productService.GetProduct(id);
        console.log(data);
      } else {
        navigate("/");
      }
    };

    fetchProducts();
  }, []);

  return <></>;
}

export default ProductInfo;
